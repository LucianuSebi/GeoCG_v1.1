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
        public string Sirsup = null;  
        public string Siruta = null;
        public bool Intravilan;
        public string DistrictType = null;
        public string DistrictName = null;
        public string StreetType = null;
        public string StreetName = null;
        public string PostalNumber = null;
        public string Block = null;
        public string Entry = null;
        public string Floor = null;
        public string ApNo = null;
        public string Zipcode = null;
        public string Description = null;
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
