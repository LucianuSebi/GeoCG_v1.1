using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CGEntity;
using ConfigLoader;
using netDxf;
using netDxf.Entities;
using netDxf.Tables;
using static netDxf.Entities.HatchBoundaryPath;
using FunctionLibrary;

namespace Modules
{
    public class DxfProcessor
    {
        public static void ProcessDxf()
        {
            try
            {
                string inputDxfPath = Config.path+ Config.values["InputDxfPath"];
                string outputDirectory = Config.values["CacheDxfDirectory"];
                int startID = int.Parse(Config.values["StartID"]);
                int endID = int.Parse(Config.values["EndID"]);
                string idTextLayerName = Config.values["IDTextLayerName"];
                string mainParcelaPolylineLayerName = Config.values["MainParcelPolylineLayerName"];
                string parcelaPolylineLayerName = Config.values["ParcelPolylineLayerName"];
                string nrParcelaLayerName = Config.values["NrParcelaLayerName"];
                string categorieParcelaLayerName = Config.values["CategorieParcelaLayerName"];
                string imprejmuitLayerName = Config.values["ImprejmuitLayerName"];
                string cladirePolylineLayerName = Config.values["CladirePolylineLayerName"];
                string cladireDestinatieLayerName = Config.values["CladireDestinatieLayerName"];
                string cladireInaltimeLayerName = Config.values["CladireInaltimeLayerName"];
                string cladireIDLayerName = Config.values["CladireIDLayerName"];

                if (!File.Exists(inputDxfPath))
                    throw new FileNotFoundException("Fisierul DXF nu a fost gasit.", inputDxfPath);

                DxfDocument dxf = DxfDocument.Load(inputDxfPath);
                if (dxf == null)
                    throw new Exception("Fisierul DXF nu a putut fi deschis.");

                Console.WriteLine("Procesarea fisierului a inceput.");


                //initializam listele de obiecte
                List<Text> idTexts = DxfLib.TextRangeSort(DxfLib.TextFromDxf(idTextLayerName, dxf), startID, endID);
                List<Text> parcelaNr = DxfLib.TextFromDxf(nrParcelaLayerName, dxf);
                List<Text> parcelaUseCat = DxfLib.TextFromDxf(categorieParcelaLayerName, dxf);
                List<Text> parcelaImprejmuit = DxfLib.TextFromDxf(imprejmuitLayerName, dxf);
                List<Text> cladireDestinatie = DxfLib.TextFromDxf(cladireDestinatieLayerName, dxf);
                List<Text> cladireInaltime = DxfLib.TextFromDxf(cladireInaltimeLayerName, dxf);
                List<Text> cladireID = DxfLib.TextFromDxf(cladireIDLayerName, dxf);

                //initializam listele de polilinii
                List<Polyline2D> mainParcele = DxfLib.PolylineFromDxf(mainParcelaPolylineLayerName, dxf);
                List<Polyline2D> parcele = DxfLib.PolylineFromDxf(parcelaPolylineLayerName, dxf);
                List<Polyline2D> cladiri = DxfLib.PolylineFromDxf(cladirePolylineLayerName, dxf);

                int counterTerenuri = 0;
                foreach (Text text in idTexts)
                {
                    //cautam terenul caruia ii este atribuit id-ul curent
                    List<Polyline2D> mainParcelaAux = DxfLib.FindParent(new Vertex(text.Position.X, text.Position.Y), mainParcele);

                    if (mainParcelaAux.Count()!=0)
                    {
                        Polyline2D mainParcela = mainParcelaAux.First();

                        counterTerenuri++;
                        DxfDocument newDxf = new DxfDocument();

                        //salvam elementele principale
                        newDxf.Entities.Add((EntityObject)mainParcela.Clone());
                        newDxf.Entities.Add((EntityObject)text.Clone());

                        //salvam textele
                        DxfLib.SaveChildren(parcelaNr, mainParcela, newDxf);
                        DxfLib.SaveChildren(parcelaUseCat, mainParcela, newDxf);
                        DxfLib.SaveChildren(parcelaImprejmuit, mainParcela, newDxf);
                        DxfLib.SaveChildren(cladireDestinatie, mainParcela, newDxf);
                        DxfLib.SaveChildren(cladireInaltime, mainParcela, newDxf);
                        DxfLib.SaveChildren(cladireID, mainParcela, newDxf);

                        //salvam poliliniile incluse
                        
                        DxfLib.SaveChildren(cladiri, mainParcela, newDxf);

                        string outputPath = Path.Combine(Config.path+outputDirectory, $"{text.Value}.dxf");
                        newDxf.Save(outputPath);
                        DxfLib.SetViewportCenter(outputPath, mainParcela);
                    }
                    else
                    {
                        Console.WriteLine("EROARE in DxfProcessor: ID-ul " + text.Value + " nu are un teren atribuit.");
                    }
                }
                Console.WriteLine("DxfProcessor: Procesarea fisierului s-a incheiat cu "+ counterTerenuri+" terenuri gasite.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("EROARE in DxfProcessor: " + ex.Message);
            }
        }
    }
}
