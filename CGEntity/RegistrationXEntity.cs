using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEntity
{
    public class RegistrationXEntity
    {
        public int RegistrationXEntityId;
        public string type = null;
        public int reffrId;
        private static int counter = 0;

        public RegistrationXEntity(string type,int reffrId) {
            RegistrationXEntityId = ++counter;
            this.type = type;
            this.reffrId= reffrId;
        }
        public RegistrationXEntity() { }
    }
}
