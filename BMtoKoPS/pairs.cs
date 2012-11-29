using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMtoKOPS
{
    public class Pairs
    {
        private List<List<Player>> pairs;
        private List<int> numbers;

        public Pairs()
        {
            pairs = new List<List<Player>>();
        }

        public void AddPair(List<Player> players) 
        {
            pairs.Add(players);
        }

        public String GetPairNames(int n)
        {
            if (pairs[n][0].n.Length == 0 && pairs[n][1].n.Length == 0)
            {
                return "";
            }
            else
            {
                return String.Format("<nobr>{0} &mdash; {1}</nobr>",
                    pairs[n][0].n,
                    pairs[n][1].n);
            }
        }

        public double GetPairRank(int n)
        {
            return pairs[n][0].w + pairs[n][1].w;
        }

        public String GetPairRegion(int n)
        {
            return pairs[n][0].l.Equals(pairs[n][1].l) ?
                    pairs[n][0].l :
                    String.Format("{0}/{1}", pairs[n][0].l, pairs[n][1].l);
        }

        public void SetNumbers(List<int> numbers)
        {
            this.numbers = numbers;
        }

        public int GetMaxNumber()
        {
            return numbers.Count;
        }

        public int GetNumber(int i)
        {
            return numbers[i];
        }

        public int GetInternalPairNumber(int n)
        {
            int counter = -1;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] != 0 && i < n)
                {
                    counter++;
                }
            }
            return counter;
        }

        public int GetPairNumber(int n)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == n)
                {
                    return i + 1;
                }
            }

            return 0;
        }

        public int GetNumberOfPairs()
        {
            return pairs.Count();
        }

    }
}
