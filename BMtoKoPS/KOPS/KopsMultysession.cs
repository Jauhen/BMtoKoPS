using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BMtoKOPS.KOPS;

namespace BMtoKOPS.KOPS {
    public class KopsMultysession : ITournament {
        private string path;
        private List<KopsTournament> tournaments;
        private KopsTournament baseTournament;
        private int counting;
        private List<double> weights;
        private List<MultysessionResult> results;

        public KopsMultysession(string path) {
            this.path = path;

            StreamReader f = new StreamReader(path);

            List<string> file = new List<string>();

            while (!f.EndOfStream) {
                file.Add(f.ReadLine().Split('*')[0]);
            }

            f.Close();

            int sessions = int.Parse(file[0]);

            tournaments = new List<KopsTournament>();

            for (int i = 0; i < sessions; i++) {
                tournaments.Add(new KopsTournament(String.Format(@"{0}\{1}.INF",
                    Path.GetDirectoryName(path),
                    file[i + 1]), new KopsReader()));
            }

            for (int i = 1; i < sessions + 1; i++) {
                if (file[sessions + 1].Trim().Equals(file[i])) {
                    baseTournament = tournaments[i - 1];
                }
            }

            counting = int.Parse(file[sessions + 2]);

            weights = new List<double>();

            for (int i = sessions + 3; i < sessions * 2 + 3; i++) {
                weights.Add(KopsHelper.GetDoubleFromString(file[i]));
            }

        }

        public void ReadResults() {
            foreach (Tournament t in tournaments) {
                t.ReadResults();
            }

            //number, players, rank, region, list of tournaments
            results = new List<MultysessionResult>();

            for (int i = 1; i < baseTournament.Pairs.GetMaxNumber() + 1; i++) {
                if (i > 1 && baseTournament.Pairs.GetInternalPairNumber(i) ==
                    baseTournament.Pairs.GetInternalPairNumber(i - 1)) {
                    continue;
                }
                //i - internal number started with 0
                int n = baseTournament.Pairs.GetInternalPairNumber(i);

                results.Add(new MultysessionResult(i,
                    baseTournament.Pairs.GetPairNames(n),
                    baseTournament.Pairs.GetPairRank(n),
                    baseTournament.Pairs.GetPairRegion(n),
                    tournaments.Count));
            }

            double totalWeight = 0;
            for (int i = 0; i < weights.Count; i++) {
                totalWeight += weights[i];
            }

            foreach (MultysessionResult r in results) {
                double total = 0;
                double maxs = 0;

                for (int i = 0; i < tournaments.Count; i++) {
                    double points = tournaments[i].GetResultOfPair(r.GetNumber());
                    double max = tournaments[i].GetMaximalResult();

                    if (counting == 0) {
                        total += points * weights[i];
                        maxs += max * weights[i];
                    } else {
                        total += points / max * weights[i] / totalWeight;
                    }

                    r.SetResult(i, points / max * 100);
                }

                if (tournaments[0].scoring == 0) {
                    r.SetTotal(counting == 0 ? total / maxs * 100 : total * 100);
                } else {
                    r.SetTotal(total);
                }
            }

        }

        public String PrintResults() {
            StringBuilder res = new StringBuilder(Resource1.ProtocolsHTMLBegin);
            res.Append(PrintTitle("Result"));
            res.Append(ProtocolsHTMLTableResultHeader());

            IEnumerable<MultysessionResult> places =
                results.OrderByDescending(result => result.GetTotal());

            int place = 1;

            foreach (MultysessionResult r in places) {

                res.Append(r.GetHTML(place, tournaments[0].scoring == 0));

                if (place > 1 && place % (tournaments.Count < 3 ? 37 : 60) == 0) {
                    res.Append(Resource1.ProtocolsHTMLTableResultFooter);
                    res.Append(PrintTitle("Result"));
                    res.Append(ProtocolsHTMLTableResultHeader());
                }

                place++;
            }

            return res.ToString();
        }

        public String PrintProtocols() {
            return "<h1>PrintProtocols</h1>";
        }

        public String PrintAllHistories() {
            StringBuilder res = new StringBuilder(Resource1.ProtocolsHTMLBegin);

            IEnumerable<MultysessionResult> places =
                results.OrderByDescending(result => result.GetNumber());


            foreach (MultysessionResult r in places) {
                res.Append(PrintPlayerHistory(r.GetNumber()));
            }

            return res.ToString();
        }

        public String PrintListHistories(List<int> nums) {
            StringBuilder res = new StringBuilder(Resource1.ProtocolsHTMLBegin);

            for (int i = 0; i < nums.Count; i++) {
                if (baseTournament.Pairs.GetNumber(nums[i] - 1) > 0) {
                    res.Append(PrintPlayerHistory(nums[i]));
                }
            }

            return res.ToString();
        }

        private String PrintTitle(String type) {
            return String.Format(Resource1.ProtocolsHTMLHeader,
                Resource1.logo,
                "Total",
                type);
        }

        private String ProtocolsHTMLTableResultHeader() {
            String res = Resource1.ProtocolsHTMLTableResultHeader;

            StringBuilder res2 = new StringBuilder();

            for (int i = 0; i < weights.Count; i++) {
                res2.AppendFormat("<th>{0}: &nbsp;{1:00}%</th>",
                    i + 1, weights[i] * 100);
            }

            return res.Replace("<th> +/&minus;</th>", res2.ToString());

        }

        public String PrintPlayerHistory(int n) {
            StringBuilder res = new StringBuilder();
            res.Append(
                PrintTitle(
                    String.Format(Resource1.ProtocolsHTMLTableHistoryHeader,
                        n,
                        baseTournament.Pairs.GetPairNames(baseTournament.Pairs.GetInternalPairNumber(n)),
                        baseTournament.Pairs.GetPairRank(baseTournament.Pairs.GetInternalPairNumber(n)),
                        baseTournament.Pairs.GetPairRegion(baseTournament.Pairs.GetInternalPairNumber(n))
                    )

                )
            );

            res.Append(Resource1.histHead);

            for (int i = 0; i < tournaments.Count; i++) {
                res.Append(tournaments[i].PrintPlayerHistoryRows(n));
            }

            res.AppendFormat(Resource1.ProtocolsHTMLTableHistoryFooter,
                    "",
                    String.Format(tournaments[0].scoring == 0 ? "{0:0.00}%" : "{0:0.00}",
                        results[baseTournament.Pairs.GetInternalPairNumber(n)].GetTotal()));
            //TODO Calculate total

            res.Append("</table>");

            return res.ToString();
        }
    }
}
