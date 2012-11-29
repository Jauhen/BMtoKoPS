using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMtoKOPS
{
    public class Board
    {
        private int number;
        private bool hasPBN;
        private String deal;
        private String ability;
        private String minimax;
        private List<KopsDeal> deals;

        public Board(int n)
        {
            number = n;
            hasPBN = false;
        }

        public void SetDeals(List<KopsDeal> deals)
        {
            this.deals = deals;
        }

        public KopsDeal GetDeal(int index)
        {
            return deals[index];
        }

        public int GetBoardNumber()
        {
            return number + 1;
        }

        public void SetPBN(String deal, String ability, String minimax)
        {
            this.deal = deal;
            this.ability = ability;
            this.minimax = minimax;
            hasPBN = true;
        }

        private String GetCards(String seat)
        {
            string[] h = deal.Split(' ');
            switch (seat)
            {
                case "N": return GetPrintedHand(h[0]);
                case "E": return GetPrintedHand(h[1]);
                case "S": return GetPrintedHand(h[2]);
                case "W": return GetPrintedHand(h[3]);
            }
            return String.Empty;
        }

        /// <summary>
        /// Convert PBN string like AT7.KQJ64.6432.T to HTML
        /// </summary>
        /// <param name="hand">String like AT7.KQJ64.6432.T</param>
        /// <returns>HTML table cell</returns>
        private string GetPrintedHand(string hand)
        {
            //add space between chars then split by full stop, also replace T by 10
            String[] s =
                String
                    .Join(" ", hand.ToCharArray().Select(x => x.ToString()).ToArray())
                    .Replace("T", "10")
                    .Split('.');

            String[] suits = new String[] { "S", "H", "D", "C" };

            for (int i = 0; i < 4; i++)
            {
                s[i] = String.Format("{0}&nbsp; {1}",
                    KopsDeal.ReplaceSuit(suits[i]),
                    s[i].Trim());
            }

            return String.Format(@"<td class=""w"">{0}</td>",
                String.Join("<br />", s));
        }

        private String GetAbility(String seat)
        {
            if (ability.Equals(String.Empty))
            {
                return String.Empty;
            }

            string[] mms = ability.Split(' ');
            switch (seat)
            {
                case "N": return GetPrintedMaxPerHand(mms[0]);
                case "E": return GetPrintedMaxPerHand(mms[1]);
                case "S": return GetPrintedMaxPerHand(mms[2]);
                case "W": return GetPrintedMaxPerHand(mms[3]);
            }
            return String.Empty;
        }

        /// <summary>
        /// Convert PBN string like N:5AC72 to HTML
        /// </summary>
        /// <param name="maxes">String like N:5AC72</param>
        /// <returns>Set of HTML table cells</returns>
        private String GetPrintedMaxPerHand(String maxes)
        {
            StringBuilder res = new StringBuilder();

            if (maxes.Equals(String.Empty))
            {
                return String.Empty;
            }

            for (int i = 2; i < 7; i++)
            {
                res.AppendFormat(@"<td class=""an1"">{0}</td>",
                    Convert.ToInt32(maxes.Substring(i, 1), 16));
            }

            return res.ToString();
        }

        private String Dealer()
        {
            switch (number % 4)
            {
                case 0: return "N";
                case 1: return "E";
                case 2: return "W"; 
                case 3: return "S";
            }

            return "";
        }

        private String Vulnerable()
        {
            switch (number % 16)
            {
                case 0: case 7: case 10: case 13: return "None";
                case 1: case 4: case 11: case 14: return "NS";
                case 2: case 5: case 8:  case 15: return "EW";
                case 3: case 6: case 9:  case 12: return "All";
            }
            return "";
        }

        public string PrintHeader()
        {
            if (hasPBN)
            {
                //TODO: PBN without maxs
                return String.Format(Resource1.ProtocolsHTMLTablePBN,
                    Dealer(), //0
                    Vulnerable(), //1
                    GetCards("N"), //2
                    GetCards("W"), //3
                    KopsDeal.ReplaceSuit("G" + (number % 16 + 1).ToString()), //4
                    GetCards("E"), //5
                    GetCards("S"), //6
                    KopsDeal.ReplaceSuit("N"), //7
                    KopsDeal.ReplaceSuit("S"), //8
                    KopsDeal.ReplaceSuit("H"), //9
                    KopsDeal.ReplaceSuit("D"), //10
                    KopsDeal.ReplaceSuit("C"), //11
                    GetAbility("N"), //12
                    GetAbility("E"), //13
                    GetAbility("S"), //14
                    GetAbility("W"), //15
                    minimax != "" ? minimax[0] + KopsDeal.ReplaceSuit(minimax[1].ToString()) : "", //16
                    minimax != "" ? 
                        (minimax[2] == 'D' ?
                            ("x by " + minimax[3] + "; " + minimax.Substring(4).Replace("-", "&minus;")) :
                            " by " + minimax[2] + "; " + minimax.Substring(3).Replace("-", "&minus;"))
                        : ""// 17
                    );
            }
            else
            {
                return String.Format(@"Dealer: {0}, Vulnerable: {1}",
                    Dealer(),
                    Vulnerable());
            }
        }

        public int GetPlayedDeals()
        {
            int result = 0;

            for (int i = 0; i < deals.Count; i++)
            {
                if (deals[i].HasResult())
                {
                    result++;
                }

                deals[i].SetResults(null, null);
            }

            return result;
        }

        public void CalculateResuilts(int maxNumberOfRecords, IScoring scoringMethod)
        {
            int playedDeals = GetPlayedDeals();

            for (int i = 0; i < deals.Count; i++)
            {
                if (deals[i].HasResult())
                {
                    short res = deals[i].result;
                    double ns = 0;
                    double ew = 0;
                    for (int k = 0; k < deals.Count; k++)
                    {
                        if (i != k && deals[k].HasResult())
                        {
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
