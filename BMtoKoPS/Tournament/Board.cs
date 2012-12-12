using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMtoKOPS.Scoring;
using BMtoKOPS.KOPS;
using BMtoKOPS.Output;

namespace BMtoKOPS {
  public class Board {
    public int number {get; private set;}
    public bool hasPBN { get; private set; }
    public String deal { get; private set; }
    public String ability { get; private set; }
    public String minimax { get; private set; }
    public List<KopsDeal> deals { get; private set; }

    public Board(int n) {
      number = n;
      hasPBN = false;
    }

    public void SetDeals(List<KopsDeal> deals) {
      this.deals = deals;
    }

    public KopsDeal GetDeal(int index) {
      return deals[index];
    }

    public int GetBoardNumber() {
      return number + 1;
    }

    public void SetPBN(String deal, String ability, String minimax) {
      this.deal = deal;
      this.ability = ability;
      this.minimax = minimax;
      hasPBN = true;
    }

    public String Dealer() {
      switch (number % 4) {
        case 0:
          return "N";
        case 1:
          return "E";
        case 2:
          return "W";
        case 3:
          return "S";
      }

      return "";
    }

    public String Vulnerable() {
      switch (number % 16) {
        case 0:
        case 7:
        case 10:
        case 13:
          return "None";
        case 1:
        case 4:
        case 11:
        case 14:
          return "NS";
        case 2:
        case 5:
        case 8:
        case 15:
          return "EW";
        case 3:
        case 6:
        case 9:
        case 12:
          return "All";
      }
      return "";
    }

    public int GetPlayedDeals() {
      int result = 0;

      for (int i = 0; i < deals.Count; i++) {
        if (deals[i].HasResult()) {
          result++;
        }

        deals[i].SetResults(null, null);
      }

      return result;
    }

    public void CalculateResuilts(int maxNumberOfRecords, IScoring scoringMethod) {
      int playedDeals = GetPlayedDeals();

      for (int i = 0; i < deals.Count; i++) {
        if (deals[i].HasResult()) {
          short res = deals[i].result;
          double ns = 0;
          double ew = 0;
          for (int k = 0; k < deals.Count; k++) {
            if (i != k && deals[k].HasResult()) {
              short res2 = deals[k].result;
              ns += scoringMethod.GetDiff(res, res2);
              ew += scoringMethod.GetDiff(res2, res);
            }
          }
          ns = scoringMethod.ResultReduction(ns, playedDeals, maxNumberOfRecords);
          ew = scoringMethod.ResultReduction(ew, playedDeals, maxNumberOfRecords);

          deals[i].SetResults(ns, ew);
        }
      }
    }
  }
}
