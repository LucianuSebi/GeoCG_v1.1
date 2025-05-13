using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigLoader;

namespace CGEntity
{
    public class Land : Polyline
    {
        public int landId;
        public string sectorId;
        public bool enclosed;
        public bool coarea;
        public string legalArea = null;
        public List<Parcel> parcels = new List<Parcel>();

        public Address address = new Address();

        public Land(int landId, Polyline mainPolyline): base(mainPolyline)
        {
            this.landId = landId;
            this.sectorId = Default.values["SectorId"];
            this.enclosed = bool.Parse(Default.values["Enclosed"]);
            this.coarea = bool.Parse(Default.values["Coarea"]);
        }

        public Land() {
            this.landId = -1;
            this.sectorId = Default.values["SectorId"];
            this.enclosed = bool.Parse(Default.values["Enclosed"]);
            this.coarea = bool.Parse(Default.values["Coarea"]);
        }

        public void AddParcel(Parcel parcela)
        {
            this.parcels.Add(parcela);
        }
        public void AddParcel(List<Vertex> vertices, string useCat,bool intravilan, string parcelNo)
        {
            this.parcels.Add(new Parcel(vertices,useCat,intravilan, parcelNo));
        }

        public void SetPolyline(Polyline polyline)
        {
            this.Vertices=polyline.Vertices;
            this.CalculateArea();
        }

        public void SetAddress(Address address)
        {
            this.address = address;
        }
        

    }
}
