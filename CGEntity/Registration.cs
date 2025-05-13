using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEntity
{
    public class Registration
    {

        public int RegistrationId = 0;
        public string RegistrationType = null;
        public string RightType = "PROPRIETATE";
        public string RightComment = null;
        public string Title = null;
        public string QuotaType = "FRACTION_QUOTA";
        public string InitialQuota = null;
        public string ActualQuota = null;
        public string ValueCurrency = null;
        public string ValueAmount = null;
        public string Comments = null;
        public string LBPartNo = null;
        public string Position = null;
        public string AppDate = null;
        public string Notes = null;
        public Deed deed = new Deed();
        public List<Person> Persons = new List<Person>();
        public List<RegistrationXEntity> registrationXEntities = new List<RegistrationXEntity>();
        private static int counter = 0;


        public Registration()
        {
            RegistrationId = ++counter;
        }

    }
}
