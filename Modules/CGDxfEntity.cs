using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGEntity;
using ConfigLoader;
using netDxf;

namespace Modules
{
    internal class CGDxfEntity
    {
        string inputDxfPath = Config.values["CacheDxfDirectory"];
        string idTextLayerName = Config.values["IDTextLayerName"];
        string mainParcelaPolylineLayerName = Config.values["MainParcelPolylineLayerName"];
        string parcelaPolylineLayerName = Config.values["ParcelPolylineLayerName"];
        string nrParcelaLayerName = Config.values["NrParcelaLayerName"];
        string categorieParcelaLayerName = Config.values["CategorieParcelaLayerName"];
        string imprejmuitLayerName = Config.values["ImprejmuitLayerName"];
        string cladirePolylineLayerName = Config.values["CladirePolylineLayerName"];
        string cladireDestinatieLayerName = Config.values["CladireDestinatieLayerName"];
        string cladireInaltimeLayerName = Config.values["CladireInaltimeLayerName"];
        string cladireIDLayerName = Config.values["CladireIDLayerName"];

        public Land land = new Land();
        public CGDxfEntity(int landId) {

            DxfDocument dxf = DxfDocument.Load(inputDxfPath + "/" + landId + ".dxf");

        }
    }
}
