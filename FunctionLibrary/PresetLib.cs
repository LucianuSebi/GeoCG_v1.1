using CGEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionLibrary
{
    public static class PresetLib
    {
        public static List<Registration> PresetComunaReg(Document document)
        {
            List<Registration> registrations = new List<Registration>();

            Registration registration1 = new Registration();
            Registration registration2 = new Registration();
            Deed deed = new Deed();
            Person person = new Person();
            Address address = new Address();

            address.Intravilan = true;

            person.FirstName = "NEIDENTIFICAT";
            person.LastName = "NEIDENTIFICAT";
            person.Identified = false;
            person.IdCode = "0000000000000";
            person.Address = address;

            deed.DeedNumber = "L 7";
            deed.DeedDate = "1996-03-13T00:00:00+02:00";
            deed.DeedType = "ACT_NORMATIV";
            deed.Authority = "Parlamentul Romaniei";

            registration1.deed = deed;
            registration1.Persons.Add(person);

            registration1.RegistrationType = "PROVISIONALENTRY";
            registration1.Title = "LEGE";
            registration1.ActualQuota = "1/1";
            registration1.LBPartNo = "2";
            registration1.Position = "1";
            registration1.registrationXEntities.Add(new RegistrationXEntity("land", document.land.landId));

            registration2.deed = new Deed(deed);
            registration2.Persons.Add(new Person(person));

            registration2.RegistrationType = "NOTATION";
            registration2.Notes = "Proprietar neindentificat, rezerva comisiei locale de fond funciar";
            registration2.RightType = null;
            registration2.LBPartNo = "2";
            registration2.Position = "2";
            registration2.registrationXEntities.Add(new RegistrationXEntity("land", document.land.landId));

            registrations.Add(registration1);
            registrations.Add(registration2);

            return registrations;
        }
    }
}
