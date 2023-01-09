using System.Collections.Generic;
using System;

namespace Utils
{
    public class RandomUtils
    {
        /// <summary>
        /// Rolls a random double within the bounds
        /// </summary>
        /// <param name="minimum">The lower bound</param>
        /// <param name="maximum">The upper bound</param>
        /// <param name="random">The Random object which rolls the number</param>
        /// <returns>The rolled number</returns>
        public static double GetRandomNumber(double minimum, double maximum, Random random)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        /// <summary>
        /// Returns a 0 or 1 at random
        /// </summary>
        /// <param name="random">The Random object which rolls the number</param>
        /// <returns>The flipped number</returns>
        public static bool RandomCoinFlip(Random random)
        {
            return random.Next() % 2 == 0;
        }

        /// <summary>
        /// Rolls a pair of two integers
        /// </summary>
        /// <param name="max">The exclusive upper bound</param>
        /// <param name="random">The Random object which rolls the number</param>
        /// <returns>A 2 large integer Array with two random numbers</returns>
        public static int[] GetRandomUniquePair(int max, Random random)
        {
            int[] pair = new int[2];
            pair[0] = random.Next() % max;
            while ((pair[1] = random.Next() % max) == pair[0]) { }
            return pair;
        }

        /// <summary>
        /// Rolls multiple pairs of two integers
        /// </summary>
        /// <param name="count">The amount of rolled pairs</param>
        /// <param name="max">The exclusive upper bound</param>
        /// <param name="random">The Random object which rolls the number</param>
        /// <returns>A list of 2 large integer Arrays with two random numbers</returns>
        public static List<int[]> GetRandomUniquePairs(int count, int max, Random random)
        {
            List<int[]> pairList = new List<int[]>();
            while (pairList.Count != count)
            {
                int[] newPair = GetRandomUniquePair(max, random);
                pairList.Add(newPair);
                for (int i = 0; i < pairList.Count - 1; i++)
                {
                    if (pairList[i][0] == newPair[0] && pairList[i][1] == newPair[1])
                    {
                        pairList.RemoveAt(pairList.Count - 1);
                        break;
                    }
                }
            }
            return pairList;
        }
    }
}