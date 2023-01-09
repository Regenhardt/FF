using Fahrerflucht.Simulation.Model.Agent;
using Mars.Interfaces.Environments;
using NeuralNetwork.EvolutionaryAlgorithm;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Utils;

namespace Fahrerflucht.Simulation.Scheduler
{
    public class Gang
    {
        public Gang(SimulationGroup group)
        {
            _players = new List<Player>();
            _algorithm = new EvoAlgorithm(group.AgentCount, group.Sensors.Count, 2, group.HiddenLayerCount, group.HiddenNeuronCount, group.RandomSeed);
            _group = group;
            _iterationEnd = false;
        }

        public int GetAgentCount()
        {
            return _group.AgentCount;
        }

        public Color GetColor()
        {
            return new Color(_group.Color[0], _group.Color[1], _group.Color[2], 255);
        }

        public Genome GetMemberGenome(int id)
        {
            return _algorithm.GetMyGenome(id);
        }

        public List<SensorConfig> GetSensorConfig()
        {
            return _group.Sensors;
        }

        public Position GetAgentSpawn()
        {
            return Position.CreatePosition(_group.StartPosition[0], _group.StartPosition[1]);
        }

        public bool IsFull()
        {
            return _players.Count == _group.AgentCount;
        }

        public int AddPlayer(Player agent)
        {
            _players.Add(agent);
            return _players.Count - 1;
        }

        public List<AgentSnapshotDTO> GetAgentSnapshots()
        {
            List<AgentSnapshotDTO> snapshots = new List<AgentSnapshotDTO>();

            foreach (var agent in _players)
            {
                snapshots.Add(agent.GetAgentSnapshot());
            }

            return snapshots;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void agentCheckForEvent(int callerID)
        {
            if (areAllAgentsDead())
            {
                _iterationEnd = true;
              
            }

            if (_iterationEnd && callerID == _players.Count - 1)
            {
                // Sync up agents for equal tick times?
                agentSyncEvent();
                foreach (var agent in _players)
                {
                    agent.Respawn();
                }
                Console.WriteLine("Iteration!");
                _iterationEnd = false;
            }
        }

        private bool areAllAgentsDead()
        {
            foreach (var agent in _players)
            {
                if (agent.isAlive())
                    return false;
            }
            return true;
        }

        private void agentSyncEvent()
        {
            foreach (var agent in _players)
            {
                double distance = agent.CalculateCPsDistance();
                agent.getGenome().Fitness = distance;
                // var snap = agent.GetAgentSnapshot();
                //Console.WriteLine("Agent: " + distance);//string.Join("", agent._passedCPs));

            }
            _algorithm.Evolution(_group.BestPerformerKeepRate, _group.ThresholdFitness, _group.MinKeepThreshold);
        }

        private bool _iterationEnd;
        private List<Player> _players;
        private EvoAlgorithm _algorithm;
        private SimulationGroup _group;
    }
}
