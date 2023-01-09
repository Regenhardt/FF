using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Utils
{
    public class GeoJson
    {
        public class Geometry
        {
            public string Type { get; set; }
            public List<List<List<float>>> Coordinates { get; set; }
        }

        public class Feature
        {
            public string Type { get; set; }
            public Geometry Geometry { get; set; }
        }

        public class GeoJsonObject
        {
            public string Type { get; set; }
            public List<Feature> Features { get; set; }
        }

        public class GeoJsonCheckPoints
        {
            public List<List<int>> CheckPoints { get; set; }
        }

        public static GeoJsonObject FromFile(string file)
        {
            return JsonConvert.DeserializeObject<GeoJsonObject>(File.ReadAllText(file));
        }

        public static GeoJsonCheckPoints FromFileCP(string file)
        {
            return JsonConvert.DeserializeObject<GeoJsonCheckPoints>(File.ReadAllText(file));
        }
    }
}
