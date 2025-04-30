using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace ConfigLoader
{
    public static class Config
    {
        static XElement configXml = XElement.Load("config.xml");
        static public Dictionary<string, string> values = new Dictionary<string, string>
        {
            ["InputDxfPath"] = configXml.Element("InputDxfPath")?.Value,
            ["CacheDxfDirectory"] = configXml.Element("CacheDxfDirectory")?.Value,
            ["StartID"] = configXml.Element("StartID")?.Value,
            ["EndID"] = configXml.Element("EndID")?.Value,
            ["IDTextLayerName"] = configXml.Element("IDTextLayerName")?.Value,
            ["MainParcelPolylineLayerName"] = configXml.Element("MainParcelPolylineLayerName")?.Value,
            ["ParcelPolylineLayerName"] = configXml.Element("ParcelPolylineLayerName")?.Value,
            ["NrParcelaLayerName"] = configXml.Element("NrParcelaLayerName")?.Value,
            ["CategorieParcelaLayerName"] = configXml.Element("CategorieParcelaLayerName")?.Value,
            ["ImprejmuitLayerName"] = configXml.Element("ImprejmuitLayerName")?.Value,
            ["CladirePolylineLayerName"] = configXml.Element("CladirePolylineLayerName")?.Value,
            ["CladireDestinatieLayerName"] = configXml.Element("CladireDestinatieLayerName")?.Value,
            ["CladireInaltimeLayerName"] = configXml.Element("CladireInaltimeLayerName")?.Value,
            ["CladireIDLayerName"] = configXml.Element("CladireIDLayerName")?.Value
        };
    }
}
