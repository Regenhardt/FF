using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Mars.Components.Starter;
using Mars.Interfaces.Model;
using Fahrerflucht.Simulation.Model;
using Fahrerflucht.Simulation.Model.Agent;
using Fahrerflucht.Simulation.Scheduler;
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
                ModelDescription description = new ModelDescription();
                description.AddLayer<GameLayer>();
                description.AddAgent<Player, GameLayer>();
                SimulationConfig config = SimulationConfig.Deserialize("{\"globals\": {\"steps\": 1000000,\"console\": false,\"options\": {\"delimiter\": \";\",\"format\": \"en-EN\"}},\"layers\": [{\"name\": \"GameLayer\"}],\"agents\": [{\"name\":\"Player\",\"count\": " + _gangScheduler.GetAgentCount() + "}]}");
                SimulationStarter starter = SimulationStarter.Start(description, config);
                starter.Run();
                starter.Dispose();
                Console.WriteLine("Simulation ended");
                // Free Singleton
                SetCurrentSimulation(null);
            });
        }

        public List<List<int>> GetCheckPoints()
        {
            return _runInstance.CheckPoints;
        }

        public GeoJsonObject GetTrack()
        {
            return _runInstance.Track;
        }

        private RunInstance _runInstance;
        private GangScheduler _gangScheduler;
        private static Simulation Singleton = null;
    }
}
