using System;
using System.Collections.Generic;

namespace CircuitLib
{
    public class CircuitSolution
    {
        public string Raw { get; }

        public Dictionary<int, char> Solution { get; }

        internal CircuitSolution(string raw)
        {
            Raw = raw;
            Solution = FormatRaw(raw);
        }

        private Dictionary<int, char> FormatRaw(string raw)
        {
            Dictionary<int, char> solution = new Dictionary<int, char>();
            int lastBitPlusOne = 0;
            for (int i = 0; i < raw.Length; i++)
            {
                char currentRaw = raw[i];
                if (!(currentRaw <= '9' && currentRaw >= '0'))
                {
                    char bit = currentRaw;
                    solution.Add(Convert.ToInt32(raw[lastBitPlusOne..i]), bit);
                    lastBitPlusOne = ++i;
                }
            }

            return solution;
        }
    }
}
