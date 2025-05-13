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
        public string FirstName = null;
        public string LastName = null;
        public string PreviousLastName = null;
        public string FatherInitial = null;
        public string CitizenshipCountry = "RO";
        public string IdCode = null;
        public string IdCardType = null;
        public string IdCardSerialNo = null;
        public string IdCardNumber = null;
        public string Phone = null;
        public string Email = null;
        public string Notes = null;
        public Address Address = new Address();
        private static int counter = 0;


        public Person()
        {
            PersonId = ++counter;
        }
        public Person(Person person)
        {
            PersonId = ++counter;
            this.IsPhysical = person.IsPhysical;
            this.Defunct = person.Defunct;
            this.Identified = person.Identified;
            this.FirstName = person.FirstName;
            this.LastName = person.LastName;
            this.PreviousLastName = person.PreviousLastName;
            this.FatherInitial = person.FatherInitial;
            this.CitizenshipCountry=person.CitizenshipCountry;
            this.IdCode = person.IdCode;
            this.IdCardType = person.IdCardType;
            this.IdCardSerialNo = person.IdCardSerialNo;
            this.IdCardNumber = person.IdCardNumber;
            this.Phone = person.Phone;
            this.Email = person.Email;
            this.Notes = person.Notes;
            this.Address = person.Address;

        }

    }
}
