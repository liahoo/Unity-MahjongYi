using System;
using System.Collections.Generic;

namespace MahjongGame
{
    public static class MJHelper
    {
        public static int ConvertValueToHoldingImageIndex(int value) {
            return Array.IndexOf(SpritesValues, value);
        }

        public static int[] SpritesValues = {
            11,12,13,14,15,16,17,18,19,
            21,22,23,24,25,26,27,28,29,
            31,32,33,34,35,36,37,38,39,
            41,42,43,44,45,46,47
        };
        public static List<int> GenerateForFirstChoose(int count)
        {
            List<int> indexList = new List<int>();
            List<int> fullIndex = new List<int>();

            int next = count;
            while (next > 0)
            {
                int nextValue = UnityEngine.Random.Range(11, 48); // 9 + 9 + 9 +
                if (nextValue % 10 == 0 || fullIndex.Contains(nextValue))
                {
                    continue;
                }
                int existCount = indexList.FindAll(n => n == nextValue).Count;
                if (existCount < 4)
                {
                    indexList.Add(nextValue);
                    existCount++;
                }
                else
                {
                    fullIndex.Add(nextValue);
                    continue;
                }

                if (existCount == 4)
                {
                    fullIndex.Add(nextValue);
                }
                next--;
            }
            return indexList;
        }
    }
}
