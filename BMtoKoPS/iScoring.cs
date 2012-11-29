using System;

namespace BMtoKOPS {
    public interface IScoring {
        void SetMax(int max);
        String PrintResult(double res);
        String PrintResult(double res, double max);
        double GetDiff(short res1, short res2);
        double ResultReduction(double res, double numberOfResults, double maxNumberOfResults);
    }
}
