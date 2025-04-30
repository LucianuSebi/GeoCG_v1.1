using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Modules
{
    
    internal class CGXmlLibrary
    {

       public static void createXml(string filename)
        {
            
            var declaration = new XDeclaration("1.0", "utf-8", "yes");
            var root = new XElement("CGXML",new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),new XAttribute("xsi:noNamespaceSchemaLocation", ""));
            XDocument xmlDocument = new XDocument(declaration,root);
            xmlDocument.Save(filename);
        }

        static void AddFileDescription(string filename, int cgId)
        {
            XDocument xmlDocument = XDocument.Load(filename);

            if (xmlDocument != null)
            {
                XElement fileDescriptionElement = new XElement(
                "FileDescription",
                    new XElement("FILENAME", cgId + ".cgxml"),
                    new XElement("EXPORTDATE", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff+02:00")),
                    new XElement("FILEVERSION", "3.0"),
                    new XElement("OPERATIONTYPE", "GENERAL_CADASTRE"),
                    new XElement("LICENSEDNAME"),
                    new XElement("LICENSENUMBER"),
                    new XElement("FILEID", 1)
                );


                XElement? root = xmlDocument.Descendants("CGXML").FirstOrDefault();
                root?.Add(fileDescriptionElement);


                xmlDocument.Save(filename);
            }
        }
        static void AddAddress(string filename, int addressId,string intravilan, string postalCode)
        {
            XDocument xmlDocument = XDocument.Load(filename);

            if (xmlDocument != null)
            {
                XElement addressElement = new XElement(
                "Address",
                    new XElement("ADDRESSID", addressId),
                    new XElement("SIRSUP"),
                    new XElement("SIRUTA"),
                    new XElement("INTRAVILAN", intravilan),
                    new XElement("DISTRICTTYPE"),
                    new XElement("DISTRICTNAME"),
                    new XElement("STREETTYPE"),
                    new XElement("STREETNAME"),
                    new XElement("POSTALNUMBER"),
                    new XElement("BLOCK"),
                    new XElement("FLOOR"),
                    new XElement("APNO"),
                    new XElement("ZIPCODE", postalCode),
                    new XElement("DESCRIPTION"),
                    new XElement("SECTION")
                );


                XElement? root = xmlDocument.Descendants("CGXML").FirstOrDefault();
                root?.Add(addressElement);


                xmlDocument.Save(filename);
            }
        }
        static void AddPoint(string filename, PointF pt, int pointId, int immovableId)
        {
            XDocument xmlDocument = XDocument.Load(filename);

            if (xmlDocument != null)
            {
                XElement pointElement = new XElement(
                "Points",
                    new XElement("POINTID", pointId),
                    new XElement("IMMOVABLEID", immovableId),
                    new XElement("NO", pointId),
                    new XElement("X", pt.X),
                    new XElement("Y", pt.Y)
                );

                XElement? root = xmlDocument.Descendants("CGXML").FirstOrDefault();
                root?.Add(pointElement);


                xmlDocument.Save(filename);
            }
        }
        static void AddLand(string filename, int landId, int sectorId, int addressId,int area, bool enclosed, bool coarea,int cgId)
        {
            XDocument xmlDocument = XDocument.Load(filename);

            if (xmlDocument != null)
            {
                XElement landElement = new XElement(
                "Land",
                    new XElement("LANDID", landId),
                    new XElement("CADSECTOR", sectorId),
                    new XElement("ADDRESSID", addressId),
                    new XElement("MEASUREDAREA", area),
                    new XElement("ISNEW", true),
                    new XElement("NOTES"),
                    new XElement("ENCLOSED", enclosed),
                    new XElement("COAREA", coarea),
                    new XElement("E2IDENTIFIER"),
                    new XElement("PAPERCADNO"),
                    new XElement("PAPERLBNO"),
                    new XElement("TOPONO"),
                    new XElement("CADGENNO", cgId)
                );

                XElement? root = xmlDocument.Descendants("CGXML").FirstOrDefault();
                root?.Add(landElement);


                xmlDocument.Save(filename);
            }
        }
        static void AddParcel(string filename, int parcelId, int landId, int area, string useCat, bool intravilan)
        {
            XDocument xmlDocument = XDocument.Load(filename);

            if (xmlDocument != null)
            {
                XElement parcelElement = new XElement(
                "Parcel",
                    new XElement("PARCELID", parcelId),
                    new XElement("LANDID", landId),
                    new XElement("NUMBER", parcelId),
                    new XElement("MEASUREDAREA", area),
                    new XElement("USECATEGORY", useCat),
                    new XElement("INTRAVILAN", intravilan),
                    new XElement("TITLENO"),
                    new XElement("LANDPLOTNO"),
                    new XElement("PARCELNO"),
                    new XElement("NOTES"),
                    new XElement("TOPONO"),
                    new XElement("CADGENNO", parcelId)
                );

                XElement? root = xmlDocument.Descendants("CGXML").FirstOrDefault();
                root?.Add(parcelElement);


                xmlDocument.Save(filename);
            }
        }
    }
}
