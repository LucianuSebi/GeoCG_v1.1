using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEntity
{
    internal class Registration
    {

        public int RegistrationId = 0;
        public string RegistrationType = "";
        public string RightType = "";
        public string RightComment = "";
        public string Title = "";
        public string QuotaType = "";
        public string InitialQuota = "";
        public string ActualQuota = "";
        public string ValueCurrency = "";
        public string ValueAmount = "";
        public string Comments = "";
        public string LBPartNo = "";
        public string Position = "";
        public string AppNo = "";
        public string AppDate = "";
        public string Notes = "";
        public Deed deed = new Deed();
        public List<Person> Persons = new List<Person>();
        private static int counter = 0;


        public Registration()
        {
            RegistrationId = ++counter;
        }

    }
}
