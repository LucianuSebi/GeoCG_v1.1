using CGEntity;
using ConfigLoader;
using netDxf.Entities;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Modules
{
    public class CgXmlEntity
    {
        public string filePath;
        private readonly XNamespace _xsi = "http://www.w3.org/2001/XMLSchema-instance";
        private readonly XDocument _document;
        private XElement Root => _document.Root!;
        Document CGDocument = new Document();

        public CgXmlEntity(Document CGDocument)
        {
            filePath = Path.Combine(Config.districtPath+Config.values["OutputCGXmlDirectory"], CGDocument.land.landId + ".cgxml");

            if (File.Exists(filePath))
            {
                _document = XDocument.Load(filePath);
            }
            else
            {
                _document = CreateNewDocument();
                AddFileDescription(CGDocument.land.landId);
                this.CGDocument = CGDocument;
                AddLand();
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
                new XElement("LICENSEDNAME", null),
                new XElement("LICENSENUMBER", null),
                new XElement("FILEID", 1)
            );

            AddElement(elt);
        }

        public void AddAddress(Address address)
        {
            var elt = new XElement("Address",
                new XElement("ADDRESSID", address.AddressId),
                new XElement("SIRSUP", address.Sirsup),
                new XElement("SIRUTA", address.Siruta),
                new XElement("INTRAVILAN", address.Intravilan),
                new XElement("DISTRICTTYPE", null),
                new XElement("DISTRICTNAME", null),
                new XElement("STREETTYPE", null),
                new XElement("STREETNAME", null),
                new XElement("POSTALNUMBER", null),
                new XElement("BLOCK", null),
                new XElement("FLOOR", null),
                new XElement("APNO", null),
                new XElement("ZIPCODE", address.Zipcode),
                new XElement("DESCRIPTION", null),
                new XElement("SECTION", null)
            );

            AddElement(elt);
        }

        
        public void AddPoints(Land land)
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
        public void AddPoints(Building building)
        {
            foreach (Vertex vertex in building.Vertices)
            {
                var elt = new XElement("Points",
                new XElement("POINTID", vertex.pointId),
                new XElement("BUILDINGID", building.buildingId),
                new XElement("NO", vertex.pointNo),
                new XElement("X", vertex.X),
                new XElement("Y", vertex.Y)
                );

                AddElement(elt);
            }

        }

        public void AddLand()
        {
            var elt = new XElement("Land",
                new XElement("LANDID", CGDocument.land.landId),
                new XElement("CADSECTOR", CGDocument.land.sectorId),
                new XElement("ADDRESSID", CGDocument.land.address.AddressId),
                new XElement("MEASUREDAREA", CGDocument.land.Area),
                new XElement("ISNEW", true),
                new XElement("NOTES", null),
                new XElement("ENCLOSED", CGDocument.land.enclosed),
                new XElement("COAREA", CGDocument.land.coarea),
                new XElement("E2IDENTIFIER", null),
                new XElement("PAPERCADNO", null),
                new XElement("PAPERLBNO", null),
                new XElement("TOPONO", null),
                new XElement("CADGENNO", CGDocument.land.landId)
            );

            AddElement(elt);
            AddPoints(CGDocument.land);
            AddParcels();
            AddAddress(CGDocument.land.address);
            AddRegistrations();
        }
        public void AddRegistrations()
        {
            foreach (Registration reg in CGDocument.registrations)
            {
                var elt = new XElement("Registration",
                new XElement("REGISTRATIONID", reg.RegistrationId),
                new XElement("REGISTRATIONTYPE", reg.RegistrationType),
                new XElement("RIGHTTYPE", reg.RightType),
                new XElement("NOTES",reg.Notes),
                new XElement("RIGHTCOMMENT", reg.RightComment),
                new XElement("DEEDID", reg.deed.DeedId),
                new XElement("TITLE", reg.Title),
                new XElement("QUOTATYPE", reg.QuotaType),
                new XElement("ACTUALQUOTA", reg.ActualQuota),
                new XElement("COMMENTS", null),
                new XElement("LBPARTNO", reg.LBPartNo),
                new XElement("POSITION", reg.Position),
                new XElement("APPNO", CGDocument.land.landId),
                new XElement("APPDATE", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffK"))
                );
                AddElement(elt);
                AddPersons(reg);
                AddDeed(reg.deed);
                AddRegistrationXEntitys(reg);

            }
        }
        public void AddRegistrationXEntitys(Registration reg)
        {
            foreach(RegistrationXEntity registrationXEntity in reg.registrationXEntities)
            {
                var elt = new XElement("RegistrationXEntity",
                new XElement("REGISTRATIONXENTITYID", registrationXEntity.RegistrationXEntityId),
                new XElement("REGISTRATIONID", reg.RegistrationId)
                );
                switch (registrationXEntity.type)
                {
                    case "land":
                        elt.Add(new XElement("LANDID", registrationXEntity.reffrId));
                        break;
                    case "building":
                        elt.Add(new XElement("BUILDINGID", registrationXEntity.reffrId));
                        break;

                }
                AddElement(elt);
            }
        }

        public void AddPersons (Registration reg)
        {
            foreach(Person person in reg.Persons)
            {
                var elt = new XElement("Person",
                new XElement("PERSONID", person.PersonId),
                new XElement("ADDRESSID", person.Address.AddressId),
                new XElement("FIRSTNAME", person.FirstName),
                new XElement("ISPHYSICAL", person.IsPhysical),
                new XElement("LASTNAME", person.LastName),
                new XElement("DEFUNCT", person.Defunct),
                new XElement("IDENTIFIED", person.Identified),
                new XElement("IDCODE", person.IdCode),
                new XElement("PREVIOUSLASTNAME", person.PreviousLastName),
                new XElement("FATHERINITIAL", person.FatherInitial),
                new XElement("CITIZENSHIPCOUNTRY", person.CitizenshipCountry),
                new XElement("IDCARDTYPE", person.IdCardType),
                new XElement("IDCARDSERIALNO", person.IdCardSerialNo),
                new XElement("IDCARDNUMBER", person.IdCardNumber),
                new XElement("NOTES", person.Notes),
                new XElement("PHONE", person.Notes),
                new XElement("EMAIL", person.Email),
                new XElement("FILEID", CGDocument.land.landId),
                new XElement("REGISTRATIONID", reg.RegistrationId)
                );
                AddElement(elt);
                AddAddress(person.Address);
            }
        }

        public void AddDeed(Deed deed)
        {
            var elt = new XElement("Deed",
                new XElement("DEEDID", deed.DeedId),
                new XElement("DEEDNUMBER", deed.DeedNumber),
                new XElement("DEEDDATE", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffK")),
                new XElement("DEEDTYPE", deed.DeedType),
                new XElement("AUTHORITY", deed.Authority),
                new XElement("NOTES", deed.Notes),
                new XElement("FILEID", CGDocument.land.landId)
            );
            AddElement(elt);
        }

        public void AddParcels()
        {
            foreach(Parcel parcel in CGDocument.land.parcels)
            {
                var elt = new XElement("Parcel",
                new XElement("PARCELID", parcel.parcelId),
                new XElement("LANDID", CGDocument.land.landId),
                new XElement("NUMBER", parcel.parcelNo),
                new XElement("MEASUREDAREA", parcel.Area),
                new XElement("USECATEGORY", parcel.useCat),
                new XElement("INTRAVILAN", parcel.intravilan),
                new XElement("TITLENO", null),
                new XElement("LANDPLOTNO", null),
                new XElement("PARCELNO", null),
                new XElement("NOTES", null),
                new XElement("TOPONO", null),
                new XElement("CADGENNO", parcel.parcelNo)
                );

                AddElement(elt);
            }
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
            _document.Save(filePath);
        }
    }
}
