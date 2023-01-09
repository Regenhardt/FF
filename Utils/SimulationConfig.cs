using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using static Utils.GeoJson;

namespace Utils
{
    public class SensorConfig
    {
        /// <summary>
        /// Angle of the Sensor
        /// </summary>
        public int Angle;

        /// <summary>
        /// Range in which the Sensor detects
        /// </summary>
        public int Range;
    }

    public class SimulationGroup
    {
        /// <summary>
        /// Name of the Group
        /// </summary>
        public string Name;

        /// <summary>
        /// Color in which the agents of the Group are shown
        /// </summary>
        public List<int> Color;

        /// <summary>
        /// Start position of the group
        /// </summary>
        public List<int> StartPosition;

        /// <summary>
        /// Amount of agents in the group
        /// </summary>
        public int AgentCount;

        /// <summary>
        /// The minimal amount of genomes that must be used in the breeding process to prevent thresholdFitness from filtering out too many parents
        /// </summary>
        public int MinKeepThreshold;

        /// <summary>
        /// The minimal amount of fitness the genomes must have to be used for breeding / gene mixing and mutation
        /// </summary>
        public int ThresholdFitness;

        /// <summary>
        /// The percentage of the top performers that will remain untouched (0.0 - 1.0)
        /// </summary>
        public double BestPerformerKeepRate;

        /// <summary>
        /// Seed for random events of the Group
        /// </summary>
        public int RandomSeed;

        /// <summary>
        /// Describes the amount of hidden layers for the genomes
        /// </summary>
        public int HiddenLayerCount;

        /// <summary>
        /// Describes the amount of hidden neurons for the genomes
        /// </summary>
        public int HiddenNeuronCount;

        /// <summary>
        /// The List of the sensors each agent in the group uses
        /// </summary>
        public List<SensorConfig> Sensors;
    }

    public class RunConfig
    {
        /// <summary>
        /// Path to File which stores the map of the track
        /// </summary>
        public string MapFile;

        /// <summary>
        /// Path to file which stores the background image of the track
        /// </summary>
        public string MapBG;

        /// <summary>
        /// Path to file which stores the checkpoints for the track
        /// </summary>
        public string CheckPointFile;

        /// <summary>
        /// List of the SimulationGroups in this Run
        /// </summary>
        public List<SimulationGroup> Groups;

        /// <summary>
        /// Reads a RunConfig from a Json file and creates an object from it
        /// </summary>
        /// <param name="file">Path to the RunConfig Json file</param>
        /// <returns>RunConfig object</returns>
        public static RunConfig FromFile(string file)
        {
            return JsonConvert.DeserializeObject<RunConfig>(File.ReadAllText(file));
        }
    }

    public class RunInstance
    {
        /// <summary>
        /// The RunConfig for this Instance
        /// </summary>
        public RunConfig Config;

        /// <summary>
        /// The GeoJsonObject of the used track
        /// </summary>
        public GeoJsonObject Track;

        /// <summary>
        /// The List of Checkpoints on the track
        /// </summary>
        public List<List<int>> CheckPoints;

        /// <summary>
        /// Reads a RunInstance from a Json file and creates an object from it
        /// </summary>
        /// <param name="file">Path to the RunConfig Json file</param>
        /// <returns>RunInstance Object</returns>
        public static RunInstance FromFile(string file)
        {
            var runInstance = new RunInstance();
            runInstance.Config = RunConfig.FromFile(file);
            runInstance.Track = GeoJson.FromFile(runInstance.Config.MapFile);
            runInstance.CheckPoints = FromFileCP(runInstance.Config.CheckPointFile).CheckPoints;
            return runInstance;
        }
    }
}