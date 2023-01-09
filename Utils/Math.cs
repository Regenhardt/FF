using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Utils
{
    public class MathUtils
    {
        public static double Sigmoid(double value)
        {
            return 1.0 / (1.0 + Math.Exp(-value));
        }
        
        public static Vector2 RotateVec(Vector2 inVec, double angle)
        {
            return new Vector2((float)(inVec.X * Math.Cos(angle) - inVec.Y * Math.Sin(angle)), 
                (float)(inVec.X * Math.Sin(angle) + inVec.Y * Math.Cos(angle)));
        }

        public static float GetAngle(Vector2 vec, double originX, double originY)
        {
            return (float)((Math.Atan2(vec.Y, vec.X) - Math.Atan2(originY, originX)) * (180.0 / Math.PI));
        }

        public static double Dist(Vector2 a, Vector2 b)
        {
            return Math.Sqrt((Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2)));
        }

        public static int GetClosestPoint(Vector2 position, List<List<int>> points)
        {
            double bestDistance = double.MaxValue;
            int bestCP = 0;
            for (int i = 0; i < points.Count; i++)
            {
                double distance = Dist(new Vector2(points[i][0], points[i][1]), position);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestCP = i;
                }
            }
            return bestCP;
        }

        public static double CalculateRouteDistance(HashSet<int> cps, List<List<int>> points)
        {
            double totalDistance = 0;
            int lastCP = cps.Count != 0 ? cps.First() : 0;

            foreach (var cp in cps)
            {
                // TODO: check for driving backwards after 0
                double distance = Dist(new Vector2(points[lastCP][0], points[lastCP][1]), new Vector2(points[cp][0], points[cp][1]));
                totalDistance += distance;
                lastCP = cp;
            }

            return totalDistance;
        }

        public static double DistancePointAndSegment(Vector2 p, Vector2 startp, Vector2 endp)
        {
            // https://stackoverflow.com/questions/53173712/calculating-distance-of-point-to-linear-line
            double a = Dist(startp, endp);
            double b = Dist(startp, p);
            double c = Dist(endp, p);
            double s = (a + b + c) / 2;
            return 2 * Math.Sqrt(s * (s - a) * (s - b) * (s - c)) / a;
        }
    }
}
