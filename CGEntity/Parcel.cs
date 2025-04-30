
using ConfigLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEntity
{

    public class Parcel: Polyline
    {

        public int parcelId;
        private static int counter = 0;
        public string parcelNo = "";
        public string useCat;
        public bool intravilan;
        public List<Building> buildings= new List<Building>();
        public Parcel(List<Vertex> vertices) : base(vertices)
        {
            this.parcelId = ++counter;
            this.useCat = Default.values["UseCat"];
            this.intravilan = bool.Parse(Default.values["Intravilan"]);
        }
        public void AddBuilding(Building building)
        {
            this.buildings.Add( building );
        }
        public void AddBuilding(Polyline polyline)
        {
            this.buildings.Add(new Building(polyline));
        }
    }
}
