using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace ConfigLoader
{
    public static class Default
    {
        static XElement defaultValueXml = XElement.Load("defaultValue.xml");
        static public Dictionary<string, string> values = new Dictionary<string, string>
        {
            ["SectorId"] = defaultValueXml.Element("SectorId")?.Value,
            ["Siruta"] = defaultValueXml.Element("Siruta")?.Value,
            ["Sirsup"] = defaultValueXml.Element("Sirsup")?.Value,
            ["Zipcode"] = defaultValueXml.Element("Zipcode")?.Value,
            ["Intravilan"] = defaultValueXml.Element("Intravilan")?.Value,
            ["Enclosed"] = defaultValueXml.Element("Enclosed")?.Value,
            ["Coarea"] = defaultValueXml.Element("Coarea")?.Value,
            ["UseCat"] = defaultValueXml.Element("UseCat")?.Value,
            ["BuildingDestination"] = defaultValueXml.Element("BuildingDestination")?.Value,
            ["LevelsNo"] = defaultValueXml.Element("LevelsNo")?.Value,
            ["IsLegal"] = defaultValueXml.Element("IsLegal")?.Value,
            ["Iuno"] = defaultValueXml.Element("Iuno")?.Value
        };
    }
}
