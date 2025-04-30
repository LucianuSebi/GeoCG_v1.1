using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEntity
{
    internal class Deed
    {

        public int DeedId = 0;
        public string DeedNumber = "";
        public string DeedDate = "";
        public string DeedType = "";
        public string Authority = "";
        public string ValueAmount = "";
        public string ValueCurrency = "";
        public string Notes = "";
        private static int counter = 0;


        public Deed()
        {
            DeedId = ++counter;
        }

    }
}
