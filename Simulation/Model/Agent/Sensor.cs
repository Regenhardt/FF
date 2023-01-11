using System.Numerics;
using Position = Mars.Interfaces.Environments.Position;

namespace Simulation.Model.Agent
{
    internal class Sensor
    {
        public Sensor(double angle, double radius)
        {
            _angle = angle;
            _radius = radius;
            _lastRead = 0.0;
        }

        public SensorDataDTO GetData()
        {
            var data = new SensorDataDTO
            {
                LastSensorRead = _lastRead,
                SensorVec = _lastVec
            };
            return data;
        }

        public double ReadSensor(Position Position, Vector2 dir, GameLayer layer)
        {
            Vector2 vec = new Vector2((float)Position.X, (float)Position.Y);
            _lastVec = -1 * Utils.MathUtils.RotateVec(dir, _angle) * (float)_radius;
            _lastRead = 0.0;

            for (double i = 0.1; i <= 1.0; i += 0.1)
            {
                if (layer.IsOOB(vec + -1 * Utils.MathUtils.RotateVec(dir, _angle) * (float)(i * _radius)))
                    return _lastRead = 1.0 - i;
            }

            return _lastRead = 0.0;
        }

        private double   _angle;
        private double   _radius;
        private Vector2  _lastVec;
        private double   _lastRead;
    }
}
