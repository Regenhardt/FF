using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Mars.Components.Starter;
using Mars.Interfaces.Model;
using Fahrerflucht.Simulation.Model;
using Fahrerflucht.Simulation.Model.Agent;
using Fahrerflucht.Simulation.Scheduler;
using NeuralNetwork.EvolutionaryAlgorithm;
using Utils;
using static Utils.GeoJson;
using static Fahrerflucht.Simulation.Scheduler.GangScheduler;

namespace Fahrerflucht.Simulation
{
    public class Simulation
    {
        internal Simulation(RunInstance runInstance)
        {
            // Set run config / instance and assign gang scheduler
            _runInstance = runInstance;
            _gangScheduler = new GangScheduler(_runInstance.Config.Groups);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool SetCurrentSimulation(Simulation instance)
        {
            if (Singleton == null || instance == null)
            {
                Singleton = instance;
                return true;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Simulation GetCurrentSimulation()
        {
            return Singleton;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public GangMember AddAgent(Player agent)
        {
            return _gangScheduler.AddMember(agent);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool AgentsReady()
        {
            return _gangScheduler.AreAllMembersRegistered();
        }

        public List<AgentSnapshotDTO> GetAgentSnapshots()
        {
            return _gangScheduler.GetAgentSnapshots();
        }

        public void Start()
        {
            Task.Run(() =>
            {
                // Set current simulation as the one being globally executed (Singleton)
                SetCurrentSimulation(this);
                // Start Mars Simulation
                Console.WriteLine("Started simulation");
                var description = new ModelDescription();
                description.AddLayer<GameLayer>();
                description.AddAgent<Player, GameLayer>();
                var config = SimulationConfig.Deserialize("{\"globals\": {\"steps\": 1000,\"console\": false,\"options\": {\"delimiter\": \";\",\"format\": \"en-EN\"}},\"layers\": [{\"name\": \"GameLayer\"}],\"agents\": [{\"name\":\"Player\",\"count\": " + _gangScheduler.GetAgentCount() + "}]}");
                var starter = SimulationStarter.Start(description, config);
                starter.Run();
                starter.Dispose();
                exportData();
                Console.WriteLine("Simulation ended");
                // Free Singleton
                SetCurrentSimulation(null);
            });
        }

        private void exportData()
        {
            var outputPath = "output/" + Path.GetFileNameWithoutExtension(_runInstance.Config.MapFile) +  "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + "/";
            Directory.CreateDirectory(outputPath);
            foreach (var gang in _gangScheduler._gangs)
            {
                exportTeam(outputPath, gang);
            }
        }

        private void exportTeam(string basePath, Gang gang)
        {
            var teamPath = basePath + gang.Name + "/";
            Directory.CreateDirectory(teamPath);
            {
                using StreamWriter writer = new StreamWriter(teamPath + "/fitnessdata.csv");
                // Fitness Data
                foreach (var fitness in gang.FitnessData)
                {
                    writer.WriteLine(fitness[0]);
                }
            }
            {
                // Genome Data
                int i = 0;
                foreach (var genome in gang.BestGenomes)
                {
                    using StreamWriter genomeWriter = new StreamWriter(teamPath + "/genome"+ (i++) + ".json");
                    genomeWriter.Write(genome.ExportNeuralNetwork());
                }
            }
            // gang.FitnessData;
            // gang.BestGenomes;

        }

        public List<List<int>> GetCheckPoints()
        {
            return _runInstance.CheckPoints;
        }

        public GeoJsonObject GetTrack()
        {
            return _runInstance.Track;
        }

        private readonly RunInstance _runInstance;
        private readonly GangScheduler _gangScheduler;
        private static Simulation Singleton = null;
    }
}
