using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEntity
{
    public class Deed
    {

        public int DeedId = 0;
        public string DeedNumber = null;
        public string DeedDate = null;
        public string DeedType = null;
        public string Authority = null;
        public string ValueAmount = null;
        public string ValueCurrency = null;
        public string Notes = null;
        private static int counter = 0;


        public Deed()
        {
            DeedId = ++counter;
        }
        public Deed(Deed deed)
        {
            DeedId = ++counter;
            this.DeedNumber= deed.DeedNumber;
            this.DeedDate = deed.DeedDate;
            this.DeedType = deed.DeedType;
            this.Authority = deed.Authority;
            this.ValueCurrency = deed.ValueCurrency;
            this.ValueAmount = deed.ValueAmount;
            this.Notes = deed.Notes;
        }
    }
}
