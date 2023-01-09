using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using static Utils.GeoJson;

namespace Utils
{
    public class SensorConfig
    {
        public int Angle;

        public int Range;
    }

    public class SimulationGroup
    {
        public string Name;

        public List<int> Color;

        public List<int> StartPosition;

        public int AgentCount;

        public int MinKeepThreshold;

        public int ThresholdFitness;

        public double BestPerformerKeepRate;

        public int RandomSeed;

        public int HiddenLayerCount;

        public int HiddenNeuronCount;

        public List<SensorConfig> Sensors;
    }

    public class RunConfig
    {
        public string MapFile;

        public string MapBG;

        public string CheckPointFile;

        public List<SimulationGroup> Groups;

        public static RunConfig FromFile(string file)
        {
            return JsonConvert.DeserializeObject<RunConfig>(File.ReadAllText(file));
        }
    }

    public class RunInstance
    {
        public RunConfig Config;

        public GeoJsonObject Track;

        public List<List<int>> CheckPoints;

        public static RunInstance FromFile(string file)
        {
            RunInstance runInstance = new RunInstance();
            runInstance.Config = RunConfig.FromFile(file);
            runInstance.Track = GeoJson.FromFile(runInstance.Config.MapFile);
            runInstance.CheckPoints = FromFileCP(runInstance.Config.CheckPointFile).CheckPoints;
            return runInstance;
        }
    }
}