using System;
using System.Collections.Generic;
using System.Text;

namespace BMtoKOPS
{
    class ScoringCSR : IScoring
    {
        private int max;
        private int[] imps;

        public ScoringCSR()
        {
            max = 100;
            imps = new int[24] {20, 50, 90, 130, 170, 220, 270, 320, 370, 430,
                                500, 600, 750, 900, 1100, 1300, 1500, 1750, 2000, 
                                2250, 2500, 3000, 3500, 4000};

        }

        public String PrintResult(double res)
        {
            return String.Format("{0:0.00}&nbsp;&nbsp;", res).Replace("-", "&minus;");
        }

        public String PrintResult(double res, double sessionMax)
        {
            return String.Format("{0:0.00}&nbsp;&nbsp;", res).Replace("-", "&minus;");
        }

        public double GetDiff(short res1, short res2)
        {
            int result = 0;

            while (result < 23 && Math.Abs(res1 - res2) >= imps[result])
            {
                result++;
            }

            return result * Math.Sign(res1 - res2);
        }

        public double ResultReduction(double res, double numberOfResults, double maxNumberOfResults)
        {
            if (numberOfResults < 2)
                return res;

            return res / (numberOfResults - 1);
        }

        public void SetMax(int sessionMax)
        {
            max = sessionMax;
        }
    }
}
