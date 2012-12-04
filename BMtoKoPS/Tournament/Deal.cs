using System;
using System.Collections.Generic;
using System.Text;

namespace BMtoKOPS {
  public class Deal {
    private Nullable<double> nsResult;
    private Nullable<double> ewResult;

    public void SetResults(Nullable<double> ns, Nullable<double> ew) {
      nsResult = ns;
      ewResult = ew;
    }

    public Nullable<double> GetNSResult() {
      return nsResult;
    }

    public Nullable<double> GetEWResult() {
      return ewResult;
    }
  }
}
