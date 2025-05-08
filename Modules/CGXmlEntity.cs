using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CGEntity;
using netDxf.Entities;

namespace Modules
{
    public class CgXmlEntity
    {
        private readonly string _filePath;
        private readonly XNamespace _xsi = "http://www.w3.org/2001/XMLSchema-instance";
        private readonly XDocument _document;
        private XElement Root => _document.Root!;

        public CgXmlEntity(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path must be specified.", nameof(filePath));

            _filePath = filePath;

            if (File.Exists(_filePath))
            {
                _document = XDocument.Load(_filePath);
            }
            else
            {
                _document = CreateNewDocument();
                Save();
            }
        }

        public void AddFileDescription(int cgId)
        {
            var elt = new XElement("FileDescription",
                new XElement("FILENAME", $"{cgId}.cgxml"),
                new XElement("EXPORTDATE", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffK")),
                new XElement("FILEVERSION", "3.0"),
                new XElement("OPERATIONTYPE", "GENERAL_CADASTRE"),
                new XElement("LICENSEDNAME", string.Empty),
                new XElement("LICENSENUMBER", string.Empty),
                new XElement("FILEID", 1)
            );

            AddElement(elt);
        }

        public void AddAddress(Address address)
        {
            var elt = new XElement("Address",
                new XElement("ADDRESSID", address.AddressId),
                new XElement("SIRSUP", string.Empty),
                new XElement("SIRUTA", string.Empty),
                new XElement("INTRAVILAN", address.Intravilan),
                new XElement("DISTRICTTYPE", string.Empty),
                new XElement("DISTRICTNAME", string.Empty),
                new XElement("STREETTYPE", string.Empty),
                new XElement("STREETNAME", string.Empty),
                new XElement("POSTALNUMBER", string.Empty),
                new XElement("BLOCK", string.Empty),
                new XElement("FLOOR", string.Empty),
                new XElement("APNO", string.Empty),
                new XElement("ZIPCODE", address.Zipcode),
                new XElement("DESCRIPTION", string.Empty),
                new XElement("SECTION", string.Empty)
            );

            AddElement(elt);
        }

        
        public void AddPoint(Land land)
        {
            foreach (Vertex vertex in land.Vertices)
            {
                var elt = new XElement("Points",
                new XElement("POINTID", vertex.pointId),
                new XElement("IMMOVABLEID", land.landId),
                new XElement("NO", vertex.pointNo),
                new XElement("X", vertex.X),
                new XElement("Y", vertex.Y)
                );

                AddElement(elt);
            }

        }
        public void AddPoint(Building building)
        {
            foreach (Vertex vertex in building.Vertices)
            {
                var elt = new XElement("Points",
                new XElement("POINTID", vertex.pointId),
                new XElement("IMMOVABLEID", building.buildingId),
                new XElement("NO", vertex.pointNo),
                new XElement("X", vertex.X),
                new XElement("Y", vertex.Y)
                );

                AddElement(elt);
            }

        }

        public void AddLand(int landId, int sectorId, int addressId, int area, bool enclosed, bool coarea, int cgId)
        {
            var elt = new XElement("Land",
                new XElement("LANDID", landId),
                new XElement("CADSECTOR", sectorId),
                new XElement("ADDRESSID", addressId),
                new XElement("MEASUREDAREA", area),
                new XElement("ISNEW", true),
                new XElement("NOTES", string.Empty),
                new XElement("ENCLOSED", enclosed),
                new XElement("COAREA", coarea),
                new XElement("E2IDENTIFIER", string.Empty),
                new XElement("PAPERCADNO", string.Empty),
                new XElement("PAPERLBNO", string.Empty),
                new XElement("TOPONO", string.Empty),
                new XElement("CADGENNO", cgId)
            );

            AddElement(elt);
        }

        public void AddParcel(int parcelId, int landId, int area, string useCategory, bool intravilan)
        {
            var elt = new XElement("Parcel",
                new XElement("PARCELID", parcelId),
                new XElement("LANDID", landId),
                new XElement("NUMBER", parcelId),
                new XElement("MEASUREDAREA", area),
                new XElement("USECATEGORY", useCategory),
                new XElement("INTRAVILAN", intravilan),
                new XElement("TITLENO", string.Empty),
                new XElement("LANDPLOTNO", string.Empty),
                new XElement("PARCELNO", string.Empty),
                new XElement("NOTES", string.Empty),
                new XElement("TOPONO", string.Empty),
                new XElement("CADGENNO", parcelId)
            );

            AddElement(elt);
        }

        private XDocument CreateNewDocument()
        {
            var declaration = new XDeclaration("1.0", "utf-8", "yes");
            var root = new XElement("CGXML",
                new XAttribute(XNamespace.Xmlns + "xsi", _xsi),
                new XAttribute(_xsi + "noNamespaceSchemaLocation", string.Empty)
            );

            return new XDocument(declaration, root);
        }

        private void AddElement(XElement element)
        {
            Root.Add(element);
        }

        public void Save()
        {
            _document.Save(_filePath);
        }

        public void Save(string filepath)
        {
            _document.Save(filepath);
        }
    }
}
