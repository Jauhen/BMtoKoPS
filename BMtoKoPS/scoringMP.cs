using System;
using System.Collections.Generic;
using System.Text;

namespace BMtoKOPS
{
    public class ScoringMP : IScoring
    {
        private int max;

        public ScoringMP()
        {
            max = 100;
        }

        public String PrintResult(double res)
        {
            return String.Format("{0:0.00}%", 100.0 * res / max);
        }

        public String PrintResult(double res, double sessionMax)
        {
            return String.Format("{0:0.00}%", 100.0 * res / sessionMax);
        }

        public double GetDiff(short res1, short res2)
        {
            if (res1 > res2)
                return 2;

            if (res1 == res2)
                return 1;

            return 0;
        }

        public double ResultReduction(double res, double numberOfResults, double maxNumberOfResults)
        {
            if (numberOfResults < 2)
                return 0;

            if (numberOfResults == maxNumberOfResults)
                return res;

            return (1.0 + res) * ((maxNumberOfResults) / numberOfResults) - 1;
        }

        public void SetMax(int sessionMax)
        {
            max = sessionMax;
        }
    }
}
