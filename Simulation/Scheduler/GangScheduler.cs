using Simulation.Model.Agent;
using NeuralNetwork.EvolutionaryAlgorithm;
using Raylib_cs;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utils;

namespace Simulation.Scheduler
{
    public class GangScheduler
    {
        public class GangMember
        {
            public int ID;
            public Color Color;
            public Gang BoundGang;
        }

        public GangScheduler(List<SimulationGroup> groups)
        {
            _curFilledGang = 0;
            _gangs = new List<Gang>();

            foreach (SimulationGroup group in groups)
            {
                _gangs.Add(new Gang(group));
            }
        }

        public int GetAgentCount()
        {
            int agentCount = 0;
            foreach (Gang gang in _gangs)
            {
                agentCount += gang.GetAgentCount();
            }
            return agentCount;
        }

        public GangMember AddMember(Player agent)
        {
            if (_gangs[_curFilledGang].IsFull())
            {
                ++_curFilledGang;
            }

            return new GangMember { ID = _gangs[_curFilledGang].AddPlayer(agent), Color = _gangs[_curFilledGang].GetColor(), BoundGang = _gangs[_curFilledGang] };  
        }

        public bool AreAllMembersRegistered()
        {
            return _gangs[_gangs.Count - 1].IsFull();
        }

        public List<AgentSnapshotDTO> GetAgentSnapshots()
        {
            List<AgentSnapshotDTO> snapshots = new List<AgentSnapshotDTO>();

            foreach (var gang in _gangs)
            {
                gang.GetAgentSnapshots().ForEach(item => snapshots.Add(item));
            }

            return snapshots;
        }

        private int _curFilledGang;
        public List<Gang> _gangs;
    }
}
