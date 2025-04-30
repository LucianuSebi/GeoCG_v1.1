using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigLoader;

namespace CGEntity
{
    public class Person
    {

        public int PersonId = 0;
        public bool IsPhysical = true;
        public bool Defunct = false;
        public bool Identified = true;
        public string FirstName = "";
        public string LastName = "";
        public string PreviousLastName = "";
        public string FatherInitial = "";
        public string CitizenshipCountry = "";
        public string IdCode = "";
        public string IdCardType = "";
        public string IdCardSerialNo = "";
        public string IdCardNumber = "";
        public string Phone = "";
        public string Email = "";
        public string Notes = "";
        public Address Address = new Address();
        private static int counter = 0;


        public Person()
        {
            PersonId = ++counter;
        }

    }
}
