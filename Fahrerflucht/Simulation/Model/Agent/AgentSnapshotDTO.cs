using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;

namespace Fahrerflucht.Simulation.Model.Agent
{
    public class SensorDataDTO
    {
        public Vector2 SensorVec;
        public double LastSensorRead;
    }

    public class AgentSnapshotDTO
    {
        public Vector2 Position;
        public Vector2 MovingVec;
        public Color Color;
        public bool IsAlive;
        public List<SensorDataDTO> SensorData;
    }
}
