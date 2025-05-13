using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigLoader;

namespace CGEntity
{
    public partial class Document
    {
        public List<Registration> registrations= new List<Registration>();
        public Land land= new Land();
        public Document(){
        }
    }
}