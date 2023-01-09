using System.Collections.Generic;
using System;

namespace Utils
{
    public class RandomUtils
    {
        public static double GetRandomNumber(double minimum, double maximum, Random random)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static bool RandomCoinFlip(Random random)
        {
            return random.Next() % 2 == 0;
        }

        public static int[] GetRandomUniquePair(int max, Random random)
        {
            int[] pair = new int[2];
            pair[0] = random.Next() % max;
            while ((pair[1] = random.Next() % max) == pair[0]) { }
            return pair;
        }

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