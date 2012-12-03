using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public Tournament() {
        }

        private String PrintTitle(String type) {
            return String.Format(Resource1.ProtocolsHTMLHeader,
                Resource1.logo,
                GetTitle(),
                type);
        }

        public virtual void ReadResults() {
            throw new NotImplementedException("ReadResults not implemented for Tournament class.");
        }

        public String PrintResults() {
            StringBuilder res = new StringBuilder(Resource1.ProtocolsHTMLBegin);
            res.Append(PrintTitle("Result"));
            res.Append(Resource1.ProtocolsHTMLTableResultHeader);

            IEnumerable<KeyValuePair<int, KeyValuePair<double, double>>> places =
                sessionResults.OrderByDescending(result => result.Value.Key);

            int place = 1;

            foreach (KeyValuePair<int, KeyValuePair<double, double>> pair in places) {
                int n = pair.Key;
                int pairNumber = Pairs.GetInternalPairNumber(n);

                if (Pairs.GetPairNames(pairNumber).Length > 0) {
                    res.AppendFormat(KopsHelper.GetLocalInfo(), Resource1.ProtocolsHTMLTableResultBody,
                        place,
                        n.ToString(),
                        Pairs.GetPairNames(pairNumber),
                        Pairs.GetPairRank(pairNumber),
                        Pairs.GetPairRegion(pairNumber),
                        sessionMax > 0 &&
                            pair.Value.Value != 0 ?
                                scoringMethod.PrintResult(pair.Value.Value) : "",
                        sessionMax > 0 ? scoringMethod.PrintResult(pair.Value.Key) : ""
                        );
                }
                if (place > 1 && place % 36 == 0) {
                    res.Append(Resource1.ProtocolsHTMLTableResultFooter);
                    res.Append(PrintTitle("Result"));
                    res.Append(Resource1.ProtocolsHTMLTableResultHeader);
                }

                place++;
            }

            return res.ToString();
        }

        public String PrintPlayerHistory(int n) {
            StringBuilder res = new StringBuilder();
            res.Append(
                PrintTitle(
                    String.Format(KopsHelper.GetLocalInfo(), Resource1.ProtocolsHTMLTableHistoryHeader,
                        n,
                        Pairs.GetPairNames(Pairs.GetInternalPairNumber(n)),
                        Pairs.GetPairRank(Pairs.GetInternalPairNumber(n)),
                        Pairs.GetPairRegion(Pairs.GetInternalPairNumber(n))
                    )
                )
            );

            res.Append(Resource1.histHead);

            res.Append(PrintPlayerHistoryRows(n));

            if (sessionMax > 0) {
                res.AppendFormat(KopsHelper.GetLocalInfo(), Resource1.ProtocolsHTMLTableHistoryFooter,
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
                        line, deal.GetHtml(line.Equals("NS") ? 1 : -1),
                        result.HasValue ? scoringMethod.PrintResult(result.Value, 2.0 * maxNumberOfRecords - 2.0) :
                        "");
                }
            }

            return res.ToString();
        }

        public String GetBoardResults(int n) {
            int round = n / dealsPerRound;

            StringBuilder res = new StringBuilder();
            res.AppendFormat(@"<h1>Board {0}</h1><div style=""text-align: center"">{1}</div>",
                boards[n].GetBoardNumber(), boards[n].PrintHeader());
            res.Append(Resource1.ProtocolsHTMLTableProtocolHeader);

            for (int i = 0; i < movement.Deals(round); i++) {
                String ns = "";
                String ew = "";
                if (boards[n].GetDeal(i).GetNSResult().HasValue) {
                    ns = scoringMethod.PrintResult(boards[n].GetDeal(i).GetNSResult().Value, 2.0 * maxNumberOfRecords - 2.0);
                } else {
                    if (boards[n].GetDeal(i).tdResult.IndexOf('/') > -1) {
                        ns = boards[n].GetDeal(i).tdResult.Split('/')[0] + ".0%";
                    }
                }

                if (boards[n].GetDeal(i).GetEWResult().HasValue) {
                    ew = scoringMethod.PrintResult(boards[n].GetDeal(i).GetEWResult().Value, 2.0 * maxNumberOfRecords - 2.0);
                } else {
                    if (boards[n].GetDeal(i).tdResult.IndexOf('/') > -1) {
                        ew = boards[n].GetDeal(i).tdResult.Split('/')[1] + ".0%";
                    }
                }
                res.AppendFormat(Resource1.ProtocolsHTMLTableProtocolRow,
                    Pairs.GetPairNumber(boards[n].GetDeal(i).line ? movement.GetNS(round, i) : movement.GetEW(round, i)).ToString(),
                    Pairs.GetPairNumber(boards[n].GetDeal(i).line ? movement.GetEW(round, i) : movement.GetNS(round, i)).ToString(),
                    boards[n].GetDeal(i).GetHtml(1),
                    ns,
                    ew);
            }

            res.Append("</table>");

            return res.ToString();
        }

        public String PrintAllHistories() {
            StringBuilder res = new StringBuilder(Resource1.ProtocolsHTMLBegin);

            for (int i = 0; i < Pairs.GetMaxNumber(); i++) {
                if (Pairs.GetNumber(i) > 0) {
                    res.Append(PrintPlayerHistory(i + 1));
                }
            }

            return res.ToString();
        }

        public String PrintListHistories(List<int> nums) {
            StringBuilder res = new StringBuilder(Resource1.ProtocolsHTMLBegin);

            for (int i = 0; i < nums.Count; i++) {
                if (Pairs.GetNumber(nums[i] - 1) > 0) {
                    res.Append(PrintPlayerHistory(nums[i]));
                }
            }

            return res.ToString();
        }

        public String PrintProtocols() {
            StringBuilder res = new StringBuilder(Resource1.ProtocolsHTMLBegin);

            for (int i = 0; i < boards.Count; i++) {
                if (i % dealsPerRound == 0) {
                    if (i != 0) {
                        res.Append("</tr></table>");
                    }
                    res.Append(@"<table class=""main"" style=""page-break-after: auto""><tr>");
                }

                res.AppendFormat("<td>{0}</td>", GetBoardResults(i));
            }


            res.Append(@"</tr></table>").Append(Resource1.ProtocolsHTMLEnd);
            return res.ToString();
        }

        protected String GetTitle() {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < title.Count; i++) {
                if (!title[i].Equals(String.Empty)) {
                    res.AppendFormat(Resource1.ProtocolsHTMLTitle,
                        title[i]);
                }
            }
            return res.ToString();
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
