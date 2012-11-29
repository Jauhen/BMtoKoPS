using System;
using System.IO;

namespace BMtoKOPS
{
    public class KopsDeal : Deal
    {
        private String k;
        private String d;
        private String l;
        private short t;
        public short result;
        public String tdResult;
        public bool line;

        public KopsDeal(BinaryReader reader)
        {
            line = true;
            if (reader == null)
            {
                k = "9";
                d = "";
                l = "";
                t = 0;
                result = 10000;
                return;
            }

            byte kontr = reader.ReadByte();
            k = kontr.ToString();

            switch (reader.ReadByte())
            {
                case 0: k += "C"; break;
                case 1: k += "D"; break;
                case 2: k += "H"; break;
                case 3: k += "S"; break;
                case 4: k += "N"; break;
            }

            switch (reader.ReadByte())
            {
                case 1: k += "x"; break;
                case 2: k += "xx"; break;
            }

            switch (reader.ReadByte())
            {
                case 0: d = "N"; break;
                case 1: d = "E"; break;
                case 2: d = "S"; break;
                case 3: d = "W"; break;
            }

            switch (reader.ReadByte())
            {
                case 0: l = "C"; break;
                case 1: l = "D"; break;
                case 2: l = "H"; break;
                case 3: l = "S"; break;
            }

            switch (reader.ReadByte())
            {
                case 1: l += "2"; break;
                case 2: l += "3"; break;
                case 3: l += "4"; break;
                case 4: l += "5"; break;
                case 5: l += "6"; break;
                case 6: l += "7"; break;
                case 7: l += "8"; break;
                case 8: l += "9"; break;
                case 9: l += "T"; break;
                case 10: l += "J"; break;
                case 11: l += "Q"; break;
                case 12: l += "K"; break;
                case 13: l += "A"; break;
            }

            t = reader.ReadInt16();
        }

        public void SetResult(short res)
        {
            result = res;
            tdResult = String.Empty;

            switch (result - 10000) {
                case 0: tdResult = "50/50"; break;
                case 44: tdResult = "60/60"; break;
                case 42: tdResult = "60/50"; break;
                case 40: tdResult = "60/40"; break;
                case 2: tdResult = "50/60"; break;
                case -2: tdResult = "50/40"; break;
                case -44: tdResult = "40/40"; break;
                case -42: tdResult = "40/50"; break;
                case -40: tdResult = "40/60"; break;
            }

            if (result < 9000)
            {
                if (result % 10 == 1 || result % 10 == -9)
                {
                    line = false;
                    result--;
                }
            }


        }

        public String GetHtml(int isNS)
        {
            String res;

            if (result != 30000)
            {
                if (!CorrectContract(k))
                {
                    res = String.Format("<td colspan=\"4\"></td><td {1}>{0}</td>",
                        tdResult.Equals(String.Empty) ? result.ToString() : tdResult,
                        result < 0 ? "class=\"right\"" : (!tdResult.Equals(String.Empty) ? "align=\"center\"" :""));
                }
                else if (k.Equals("0C"))
                {
                    res = String.Format("<td colspan=\"4\">{0}</td><td {5}>{4}</td>",
                        "PASS", "", "", "", "0", "align=\"center\"");
                }
                else 
                {
                    res = String.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td class=\"right\">{3}</td><td {5}>{4}</td>",
                        ReplaceSuit(k), d, ReplaceSuit(l), 
                        t == 0 ? "=" : (t > 0 ? String.Format("+{0}", t) : t.ToString()), 
                        result * isNS, result < 0 ? "class=\"right\"" : "");
                }
            }
            else
            {
                res = "<td colspan=\"5\">Not Played</td>";
            }


            return res;
        }

        private bool CorrectContract(String k)
        {
            String r = k;
            r = r.Replace('C', ' ').Replace('D', ' ').Replace('H', ' ').Replace('S', ' ').Replace('N', ' ').Replace('x', ' ');
            return int.Parse(r.Trim()) < 8;
        }

        public static String ReplaceSuit(String s)
        {
            if (s == null)
                return "";

            if (s.IndexOf('C') > -1)
            {
                return s.Replace("C", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", Resource1.c));
            }
            if (s.IndexOf('D') > -1)
            {
                return s.Replace("D", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", Resource1.d));
            }
            if (s.IndexOf('H') > -1)
            {
                return s.Replace("H", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", Resource1.h));
            }
            if (s.IndexOf('S') > -1)
            {
                return s.Replace("S", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", Resource1.s));
            }
            if (s.IndexOf('N') > -1)
            {
                return s.Replace("N", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", Resource1.n));
            }
            switch (s)
            {
                case "G1": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d1);
                case "G2": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d2);
                case "G3": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d3);
                case "G4": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d4);
                case "G5": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d5);
                case "G6": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d6);
                case "G7": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d7);
                case "G8": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d8);
                case "G9": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d9);
                case "G10": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d10);
                case "G11": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d11);
                case "G12": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d12);
                case "G13": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d13);
                case "G14": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d14);
                case "G15": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d15);
                case "G16": return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", Resource1.d16);
            }
            return s;
        }

        public String GetText
        {
            get
            {
                String res;

                if (result != 30000)
                {
                    if (k.Equals("9C"))
                    {
                        res = String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                        "", "", "", "", result);
                    }
                    else if (k.Equals("10Cx"))
                    {
                        res = String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                            "ARB", "", "", "", tdResult);
                    }
                    else
                    {
                        res = String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                            k, d, l, t, result);
                    }
                }
                else
                {
                    res = "Not Played";
                }

                return res;
            }
        }

        public bool HasResult()
        {
            if (result > 9000)
                return false;

            if (!CorrectContract(k) && !k.Equals("9C"))
                return false;

            return true;
        }
    }
}
