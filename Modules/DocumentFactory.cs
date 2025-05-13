using CGEntity;
using FunctionLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public static class DocumentFactory
    {
        public static Document CreateDocument(Land land, string preset =null)
        {
            Document document = new Document();
            document.land = land;
            switch (preset)
            {
                case "comuna":
                    document.registrations=PresetLib.PresetComunaReg(document);
                    break;
            }
            return document;

        }

        
    }
}
