using CGEntity;
using ConfigLoader;
using FunctionLibrary;
using netDxf;
using netDxf.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class CGDxfEntity
    {
        private string inputDxfPath = Config.values["CacheDxfDirectory"];
        private string idTextLayerName = Config.values["IDTextLayerName"];
        private string mainParcelaPolylineLayerName = Config.values["MainParcelPolylineLayerName"];
        private string parcelaPolylineLayerName = Config.values["ParcelPolylineLayerName"];
        private string nrParcelaLayerName = Config.values["NrParcelaLayerName"];
        private string categorieParcelaLayerName = Config.values["CategorieParcelaLayerName"];
        private string imprejmuitLayerName = Config.values["ImprejmuitLayerName"];
        private string cladirePolylineLayerName = Config.values["CladirePolylineLayerName"];
        private string cladireDestinatieLayerName = Config.values["CladireDestinatieLayerName"];
        private string cladireInaltimeLayerName = Config.values["CladireInaltimeLayerName"];
        private string cladireIDLayerName = Config.values["CladireIDLayerName"];
        private int startID = int.Parse(Config.values["StartID"]);
        private int endID = int.Parse(Config.values["EndID"]);

        private static CGDxfEntity? _instance;
        private List<Document> _documents = new List<Document>();
        public static IReadOnlyList<Document> documents => Instance._documents;

        public static void Initialize()
        {
            _instance = new CGDxfEntity();
        }
        private static CGDxfEntity Instance
        {
            get
            {
                return _instance;
            }
        }
        private CGDxfEntity() {
                for (int landId = startID; landId <= endID; landId++)
                {
                    try
                    {
                        inputDxfPath = Path.Combine(Config.districtPath + Config.values["CacheDxfDirectory"], landId + ".dxf");

                        if (!File.Exists(inputDxfPath))
                            throw new FileNotFoundException("Fisierul nu a putut fi gasit: " + inputDxfPath);

                        DxfDocument dxf = DxfDocument.Load(inputDxfPath);
                        if (dxf == null)
                            throw new Exception("Fisierul DXF nu a putut fi deschis.");



                        //initializam listele de obiecte
                        Text idTexts = DxfLib.TextFromDxf(idTextLayerName, dxf).First();
                        List<Text> parcelaNr = DxfLib.TextFromDxf(nrParcelaLayerName, dxf);
                        List<Text> parcelaUseCat = DxfLib.TextFromDxf(categorieParcelaLayerName, dxf);
                        List<Text> parcelaImprejmuit = DxfLib.TextFromDxf(imprejmuitLayerName, dxf);
                        List<Text> cladireDestinatie = DxfLib.TextFromDxf(cladireDestinatieLayerName, dxf);
                        List<Text> cladireInaltime = DxfLib.TextFromDxf(cladireInaltimeLayerName, dxf);
                        List<Text> cladireID = DxfLib.TextFromDxf(cladireIDLayerName, dxf);

                        //initializam listele de polilinii
                        Polyline mainParcela = DxfToCGLib.ToCGPolyline(DxfLib.PolylineFromDxf(mainParcelaPolylineLayerName, dxf).First());
                        List<Polyline2D> parcele = DxfLib.PolylineFromDxf(parcelaPolylineLayerName, dxf);
                        List<Polyline2D> cladiri = DxfLib.PolylineFromDxf(cladirePolylineLayerName, dxf);

                        Land land = new Land(landId, mainParcela);
                        land.address.Siruta = null;

                        switch (parcelaNr.Count())
                        {
                            case 0:
                                land.AddParcel(new Parcel(mainParcela.Vertices, "1"));
                                break;

                            case 1:
                                bool imprejmuit = false;
                                if (parcelaImprejmuit.First().Value != "N") imprejmuit = true;
                                land.AddParcel(new Parcel(mainParcela.Vertices, parcelaUseCat.First().Value, imprejmuit, "1"));
                                break;

                            default:
                                break;
                        }

                        _documents.Add(DocumentFactory.CreateDocument(land,"comuna"));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("EROARE in CGDxfEntity: " + ex.Message);
                    }
            }
        }
    }
}
