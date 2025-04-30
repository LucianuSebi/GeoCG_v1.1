using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigLoader;

namespace CGEntity
{
    public class Address
    {

        public int AddressId = 0;
        public string Sirsup = "";  
        public string Siruta = "";
        public bool Intravilan;
        public string DistrictType = "";
        public string DistrictName = "";
        public string StreetType = "";
        public string StreetName = "";
        public string PostalNumber = "";
        public string Block = "";
        public string Entry = "";
        public string Floor = "";
        public string ApNo = "";
        public string Zipcode = "";
        public string Description = "";
        private static int counter = 0;

        public Address()
        {
            this.Sirsup = Default.values["Sirsup"];
            this.Siruta = Default.values["Siruta"];
            this.Intravilan = bool.Parse(Default.values["Intravilan"]);
            this.Zipcode = Default.values["Zipcode"];
            AddressId = ++counter;
        }
        
    }
}
