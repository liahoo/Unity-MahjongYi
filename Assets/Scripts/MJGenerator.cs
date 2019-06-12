using System;
using System.Collections.Generic;

namespace MahjongGame
{
    public static class MJGenerator
    {
        public static List<int> GenerateForFirstChoose(int count)
        {
            List<int> indexList = new List<int>();
            List<int> fullIndex = new List<int>();

            int next = count;
            while (next > 0)
            {
                int nextValue = UnityEngine.Random.Range(11, 39);
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
