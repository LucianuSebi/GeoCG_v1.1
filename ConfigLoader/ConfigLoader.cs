﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ConfigLoader
{
    public class Config
    {

        private static Config? _instance;
        public static string districtPath = null;

        public static void Initialize(string xmlPath)
        {
            if (_instance is not null)
                throw new InvalidOperationException("Config.Initialize may only be called once.");

            _instance = new Config(xmlPath);
        }
        public static IReadOnlyDictionary<string, string> values
        {
            get
            {
                if (_instance is null)
                    throw new InvalidOperationException(
                        "Config not initialized. Call Config.Initialize(path) first.");

                return _instance._values;
            }
        }

        private readonly Dictionary<string, string> _values;

        private Config(string path)
        {
            string xmlPath = path + "/config.xml";
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new ArgumentException("Config path must be a non-empty string.", nameof(xmlPath));
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException("Config file not found.", xmlPath);

            var doc = XElement.Load(xmlPath);
            _values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            Config.districtPath = path +"/";

            foreach (var elem in doc.Elements())
            {
                _values[elem.Name.LocalName] = elem.Value ?? null;
            }
        }
    }
}
