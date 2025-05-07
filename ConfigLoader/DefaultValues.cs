using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace ConfigLoader
{
    public class Default
    {
        private static Default? _instance;
        public static void Initialize(string xmlPath)
        {
            if (_instance is not null)
                throw new InvalidOperationException("Default.Initialize may only be called once.");

            _instance = new Default(xmlPath);
        }
        public static IReadOnlyDictionary<string, string> values
        {
            get
            {
                if (_instance is null)
                    throw new InvalidOperationException(
                        "Default not initialized. Call Default.Initialize(path) first.");

                return _instance._values;
            }
        }
        private readonly Dictionary<string, string> _values;

        private Default(string xmlPath)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new ArgumentException("Path must be non-empty", nameof(xmlPath));
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException("defaultValue.xml not found.", xmlPath);

            var doc = XElement.Load(xmlPath);
            _values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var elem in doc.Elements())
            {
                _values[elem.Name.LocalName] = elem.Value ?? string.Empty;
            }
        }
    }
}
