using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Mars.Components.Environments.Cartesian;
using Mars.Interfaces.Agents;
using NeuralNetwork.EvolutionaryAlgorithm;
using Utils;
using static Fahrerflucht.Simulation.Scheduler.GangScheduler;
using Position = Mars.Interfaces.Environments.Position;

namespace Fahrerflucht.Simulation.Model.Agent
{
    public class Player : ICharacter, IAgent<GameLayer>
    {
        public void Init(GameLayer layer)
        {
            // Setup layer & agent
            _layer = layer;
            _member = layer.GetSimuliation().AddAgent(this);
            _passedCPs = new HashSet<int>();
            _lastTickSinceCP = 0;
            _lastCPSize = 0;

            // Setup sensors
            _sensors = new List<Sensor>();
            foreach (SensorConfig config in _member.BoundGang.GetSensorConfig())
            {
                _sensors.Add(new Sensor(config.Angle, config.Range));
            }

            // "Respawn" Agent / Set back to start
            Respawn();
        }

        public void Respawn()
        {
            Position = _member.BoundGang.GetAgentSpawn();
            _movingVec = new Vector2(1, 0);
            _alive = true;
            _passedCPs.Clear();
            _lastTickSinceCP = 0;
            _lastCPSize = 0;
        }
        
        public AgentSnapshotDTO GetAgentSnapshot()
        {
            AgentSnapshotDTO snapshot = new AgentSnapshotDTO();
            snapshot.Position = new Vector2((float)Position.X, (float)Position.Y);
            snapshot.Color = _member.Color;
            snapshot.MovingVec = _movingVec;
            snapshot.IsAlive = _alive;
            snapshot.SensorData = new List<SensorDataDTO>();
            foreach (Sensor sensor in _sensors)
            {
                snapshot.SensorData.Add(sensor.GetData());
            }
            return snapshot;
        }
 
        public bool isAlive()
        {
            return _alive;
        }

        private double[] GetSensorData()
        {
            double[] sensorData = new double[_sensors.Count];

            for (int i = 0; i < _sensors.Count; i++)
            {
                sensorData[i] = _sensors[i].ReadSensor(Position, _movingVec, _layer);
            }

            return sensorData;
        }

        private Vector2 GetMovingDir(double[] weights)
        {
            // Weights can be in the range -1.0 -> 1.0 (adjust that to make it 0.0 -> 1.0)
            weights[0] = weights[0] / 2.0 + 0.5;
            weights[1] = weights[1] / 2.0 + 0.5;

            double x = _movingVec.X, y = _movingVec.Y;

            Vector2 rightVec = new Vector2(_movingVec.Y, -1.0f * _movingVec.X);
            Vector2 leftVec = new Vector2(-1.0f * _movingVec.Y, _movingVec.X);

            x += weights[0] * leftVec.X + weights[1] * rightVec.X;
            y += weights[0] * leftVec.Y + weights[1] * rightVec.Y;

            double distance = Math.Sqrt(x * x + y * y);
            return new Vector2((float)(x / distance), (float)(y / distance));
        }

        public double CalculateCPsDistance()
        {
            return MathUtils.CalculateRouteDistance(_passedCPs, _layer.GetSimuliation().GetCheckPoints());
        }

        public void Tick()
        {
            _member.BoundGang.agentCheckForEvent(_member.ID);
            if (!_alive)
            {
                //System.Threading.Thread.Sleep(1);
                return;
            }
            else
            {
                if (_lastCPSize != _passedCPs.Count)
                {
                    _lastCPSize = _passedCPs.Count;
                    _lastTickSinceCP = 0;
                } else
                {
                    if (_lastTickSinceCP++ > 100)
                    {
                        _alive = false;
                        return;
                    }
                }
            }

            Genome myGenome = GetGenome();
             _movingVec = GetMovingDir(myGenome.Process(GetSensorData()));
            
            Vector2 newPos = new Vector2((float)Position.X + _movingVec.X, (float)Position.Y + _movingVec.Y);
            _passedCPs.Add(MathUtils.GetClosestPoint(newPos, _layer.GetSimuliation().GetCheckPoints()));

            if (_layer.IsOOB(newPos))
            {
                // Agent died / pause agent for evolution         
                _alive = false;
            }
            else
            {
                Position.X = newPos.X;
                Position.Y = newPos.Y;
            }
        }
        public Genome GetGenome()
        {
            return _member.BoundGang.GetMemberGenome(_member.ID);
        }

        public CollisionKind? HandleCollision(ICharacter other)
        {
            return CollisionKind.Pass;
        }

        public Position Position { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();
        public double Extent { get; set; } = 1;

        private Vector2 _movingVec;
        private GameLayer _layer;
        private List<Sensor> _sensors;
        private bool _alive;
        private HashSet<int> _passedCPs;
        private GangMember _member;
        private int _lastTickSinceCP;
        private int _lastCPSize;
    }
}