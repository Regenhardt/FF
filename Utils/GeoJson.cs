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

        /// <summary>
        /// Creates a GeoJsonObject from a GeoJson file
        /// </summary>
        /// <param name="file">Path to the GeoJson file</param>
        /// <returns>Created GeoJsonObject</returns>
        public static GeoJsonObject FromFile(string file)
        {
            return JsonConvert.DeserializeObject<GeoJsonObject>(File.ReadAllText(file));
        }

        /// <summary>
        /// Creates a GeoJsonCheckPoints from a Json file
        /// </summary>
        /// <param name="file">List to the Json file</param>
        /// <returns>Created GeoJsonCheckPoints object</returns>
        public static GeoJsonCheckPoints FromFileCP(string file)
        {
            return JsonConvert.DeserializeObject<GeoJsonCheckPoints>(File.ReadAllText(file));
        }
    }
}
