
using ConfigLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEntity
{
    public class Building : Polyline
    {

        public int buildingId;
        public int legalArea;
        public string buildingDestination;
        public int levelsNo;
        public int iuno;
        public string notes = "";
        public bool isLegal;
        public string cadGenNo = "";
        public Address address = new Address();
        private static int counter = 0;

        public Building(Polyline polyline): base(polyline) 
        {
            this.buildingId = ++counter;
            this.legalArea = this.Area;
            this.buildingDestination = Default.values["BuildingDestination"];
            this.levelsNo = int.Parse(Default.values["LevelsNo"]);
            this.isLegal = bool.Parse(Default.values["IsLegal"]);
            this.iuno = int.Parse(Default.values["Iuno"]);
        }
        public void SetAddress(Address address)
        {
            this.address = address;
        }

    }
}
