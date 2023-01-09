using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Utils
{
    public class MathUtils
    {
        /// <summary>
        /// Calculates the value of f(value) on the logistic function
        /// </summary>
        /// <param name="value">A double</param>
        /// <returns>The value of f(value) on the logistic function</returns>
        public static double Sigmoid(double value)
        {
            return 1.0 / (1.0 + Math.Exp(-value));
        }
        
        /// <summary>
        /// Rotates a vector by an angle
        /// </summary>
        /// <param name="inVec">Vector to be rotated</param>
        /// <param name="angle">Angle by which the Vector will be rotated</param>
        /// <returns>Rotated Vector</returns>
        public static Vector2 RotateVec(Vector2 inVec, double angle)
        {
            return new Vector2((float)(inVec.X * Math.Cos(angle) - inVec.Y * Math.Sin(angle)), 
                (float)(inVec.X * Math.Sin(angle) + inVec.Y * Math.Cos(angle)));
        }

        /// <summary>
        /// Calculates angle between two vectors
        /// </summary>
        /// <param name="vec">First vector</param>
        /// <param name="originX">X coordinate of second vector</param>
        /// <param name="originY">Y coordinate of second vector</param>
        /// <returns>Angle between the two vectors</returns>
        public static float GetAngle(Vector2 vec, double originX, double originY)
        {
            return (float)((Math.Atan2(vec.Y, vec.X) - Math.Atan2(originY, originX)) * (180.0 / Math.PI));
        }

        /// <summary>
        /// Calculates the euclidean distance between two vectors
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns>Euclidean distance</returns>
        public static double Dist(Vector2 a, Vector2 b)
        {
            return Math.Sqrt((Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2)));
        }

        /// <summary>
        /// Determines the closest point to a position out of a list of given points
        /// </summary>
        /// <param name="position">The position</param>
        /// <param name="points">The list of points</param>
        /// <returns>The closest point</returns>
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

        /// <summary>
        /// Calculates the driven distance of an agent
        /// </summary>
        /// <param name="cps">List of passed checkpoints</param>
        /// <param name="points">Checkpoint list</param>
        /// <returns>Driven Distance</returns>
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
    }
}
