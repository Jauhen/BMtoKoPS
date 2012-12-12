using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BMtoKOPS.Output;
using BMtoKOPS.Scoring;
using BMtoKOPS.KOPS;

namespace BMtoKOPS {
  /// <summary>
  /// Basic class to run tournament
  /// </summary>
  public class Tournament : ITournament {
    protected List<String> title;
    public Pairs Pairs;
    public List<Player> PlayersNames;
    protected Dictionary<int, KeyValuePair<double, double>> sessionResults;
    protected int sessionMax;
    protected int dealsPerRound;
    protected Movement movement;
    protected int records;
    protected int maxNumberOfRecords;
    protected string path;
    public IScoring scoringMethod;
    protected List<Board> boards;

    public Tournament() {}

    public virtual void ReadResults() {
      throw new NotImplementedException("ReadResults not implemented for Tournament class.");
    }

    /// <summary>
    /// Print tournament results.
    /// </summary>
    /// <returns>Htmls of result.</returns>
    public String PrintResults() {
      HtmlResults htmlResult = new HtmlResults();

      htmlResult.Title = title;

      IEnumerable<KeyValuePair<int, KeyValuePair<double, double>>> places =
          sessionResults.OrderByDescending(result => result.Value.Key);

      int place = 1;

      foreach (KeyValuePair<int, KeyValuePair<double, double>> pair in places) {
        int n = pair.Key;
        int pairNumber = Pairs.GetInternalPairNumber(n);

        if (Pairs.GetPairNames(pairNumber).Length > 0) {
          HtmlResults.Record record = new HtmlResults.Record();
          
          record.Place = place;
          record.Number = n;
          record.Names = Pairs.GetPairNames(pairNumber);
          record.Rank = Pairs.GetPairRank(pairNumber);
          record.Region = Pairs.GetPairRegion(pairNumber);
          record.Correction = sessionMax > 0 && pair.Value.Value != 0 ?
              scoringMethod.PrintResult(pair.Value.Value) : "";
          record.Result = sessionMax > 0 ? scoringMethod.PrintResult(pair.Value.Key) : "";

          htmlResult.Records.Add(record);
        }

        place++;
      }

      return htmlResult.print();
    }

    public String PrintPlayerHistory(int n) {
      StringBuilder res = new StringBuilder();
      res.Append(
          PrintTitle(
              String.Format(KopsHelper.GetLocalInfo(), HtmlResources.ProtocolsHTMLTableHistoryHeader,
                  n,
                  Pairs.GetPairNames(Pairs.GetInternalPairNumber(n)),
                  Pairs.GetPairRank(Pairs.GetInternalPairNumber(n)),
                  Pairs.GetPairRegion(Pairs.GetInternalPairNumber(n))
              )
          )
      );

      res.Append(HtmlResources.histHead);

      res.Append(PrintPlayerHistoryRows(n));

      if (sessionMax > 0) {
        res.AppendFormat(KopsHelper.GetLocalInfo(), HtmlResources.ProtocolsHTMLTableHistoryFooter,
                scoringMethod.PrintResult(sessionResults[n].Value),
                scoringMethod.PrintResult(sessionResults[n].Key));
      }

      res.Append("</table>");

      return res.ToString();
    }

    public String PrintPlayerHistoryRows(int n) {
      StringBuilder res = new StringBuilder();

      for (int i = 0; i < boards.Count; i++) {
        int round = i / dealsPerRound;
        int op = 0;
        String line = "";

        KopsDeal deal = null;
        Nullable<double> result = null;

        for (int j = 0; j < movement.Deals(round); j++) {
          bool wrongLine = boards[i].GetDeal(j).line;

          if ((wrongLine && Pairs.GetPairNumber(movement.GetNS(round, j)) == n) ||
              (!wrongLine && Pairs.GetPairNumber(movement.GetEW(round, j)) == n)) {
            line = "NS";
            op = wrongLine ? movement.GetEW(round, j) : movement.GetNS(round, j);
            deal = boards[i].GetDeal(j);
            result = boards[i].GetDeal(j).GetNSResult();
          } else if ((wrongLine && Pairs.GetPairNumber(movement.GetEW(round, j)) == n) ||
                (!wrongLine && Pairs.GetPairNumber(movement.GetNS(round, j)) == n)) {
            line = "EW";
            op = wrongLine ? movement.GetNS(round, j) : movement.GetEW(round, j);
            deal = boards[i].GetDeal(j);
            result = boards[i].GetDeal(j).GetEWResult();
          }
        }
        if (op != 0) {
          if (i % dealsPerRound == 0) {
            int pairInternalNumber = Pairs.GetInternalPairNumber(Pairs.GetPairNumber(op));
            res.AppendFormat(KopsHelper.GetLocalInfo(), @"<tr><td class=""right nbr1"" rowspan=""{0}"">{1}</td>
                         <td class=""nbr"" rowspan=""{0}"">{2}</td>
                            <td class=""right nbr"" rowspan=""{0}"">{3}</td>
                             <td class=""nbr"" rowspan=""{0}"">{4}</td>",
                   dealsPerRound,
                   Pairs.GetPairNumber(op),
                   Pairs.GetPairNames(pairInternalNumber),
                   Pairs.GetPairRank(pairInternalNumber) > 0 ?
                    String.Format(KopsHelper.GetLocalInfo(), "{0:0.0}",
                     Pairs.GetPairRank(pairInternalNumber)) : "",
                   Pairs.GetPairRegion(pairInternalNumber));
          } else {
            res.Append("<tr>");
          }

          res.AppendFormat(KopsHelper.GetLocalInfo(), @"<td class=""right"">{0}</td><td>{1}</td>{2}<td class=""right"">{3}</td></tr>",
              boards[i].GetBoardNumber(),
              line, deal.GetHtml(line.Equals("NS") ? 1 : -1).print(),
              result.HasValue ? scoringMethod.PrintResult(result.Value, 2.0 * maxNumberOfRecords - 2.0) :
              "");
        }
      }

      return res.ToString();
    }

    public HtmlProtocols.Board GetBoardResults(int n) {
      HtmlProtocols.Board board = new HtmlProtocols.Board();

      int round = n / dealsPerRound;

      board.Number = boards[n].GetBoardNumber();
      board.setHeader(boards[n]);

      StringBuilder res = new StringBuilder();

      for (int i = 0; i < movement.Deals(round); i++) {
        String ns = "";
        String ew = "";
        KopsDeal deal = boards[n].GetDeal(i);

        if (deal.GetNSResult().HasValue) {
          ns = scoringMethod.PrintResult(deal.GetNSResult().Value, 2.0 * maxNumberOfRecords - 2.0);
        } else {
          if (deal.tdResult.IndexOf('/') > -1) {
            ns = deal.tdResult.Split('/')[0] + ".0%";
          }
        }

        if (deal.GetEWResult().HasValue) {
          ew = scoringMethod.PrintResult(deal.GetEWResult().Value, 2.0 * maxNumberOfRecords - 2.0);
        } else {
          if (deal.tdResult.IndexOf('/') > -1) {
            ew = deal.tdResult.Split('/')[1] + ".0%";
          }
        }

        HtmlProtocols.Deal printedDeal = new HtmlProtocols.Deal();

        printedDeal.NsNumber = Pairs.GetPairNumber(deal.line ? movement.GetNS(round, i) : movement.GetEW(round, i)).ToString();
        printedDeal.EwNumber = Pairs.GetPairNumber(deal.line ? movement.GetEW(round, i) : movement.GetNS(round, i)).ToString();
        printedDeal.Play = deal.GetHtml(1);
        printedDeal.NsResult = ns;
        printedDeal.EwResult = ew;

        board.Deals.Add(printedDeal);
      }

      return board;
    }

    public String PrintAllHistories() {
      StringBuilder res = new StringBuilder(HtmlResources.ProtocolsHTMLBegin);

      for (int i = 0; i < Pairs.GetMaxNumber(); i++) {
        if (Pairs.GetNumber(i) > 0) {
          res.Append(PrintPlayerHistory(i + 1));
        }
      }

      return res.ToString();
    }

    public String PrintListHistories(List<int> nums) {
      StringBuilder res = new StringBuilder(HtmlResources.ProtocolsHTMLBegin);

      for (int i = 0; i < nums.Count; i++) {
        if (Pairs.GetNumber(nums[i] - 1) > 0) {
          res.Append(PrintPlayerHistory(nums[i]));
        }
      }

      return res.ToString();
    }

    public String PrintProtocols() {
      HtmlProtocols htmlProtocol = new HtmlProtocols();
      htmlProtocol.Title = title;

      for (int i = 0; i < boards.Count; i++) {
        htmlProtocol.Boards.Add(GetBoardResults(i));
      }

      htmlProtocol.dealsPerRound = dealsPerRound;

      return htmlProtocol.print();
    }

    protected String GetTitle() {
      StringBuilder res = new StringBuilder();
      for (int i = 0; i < title.Count; i++) {
        if (!title[i].Equals(String.Empty)) {
          res.AppendFormat(HtmlResources.ProtocolsHTMLTitle,
              title[i]);
        }
      }
      return res.ToString();
    }


    private String PrintTitle(String type) {
      return String.Format(HtmlResources.ProtocolsHTMLHeader,
          HtmlResources.logo,
          GetTitle(),
          type);
    }

    protected void CalculateResults() {
      List<int> dealPerRound = new List<int>();

      for (int i = 0; i < boards.Count; i++) {
        dealPerRound.Add(boards[i].GetPlayedDeals());
      }

      maxNumberOfRecords = dealPerRound.Max();

      for (int i = 0; i < boards.Count; i++) {
        boards[i].CalculateResuilts(maxNumberOfRecords, scoringMethod);
      }
    }

    public double GetMaximalResult() {
      return sessionMax;
    }

    public double GetResultOfPair(int number) {
      return sessionResults[number].Key;
    }
  }
}
