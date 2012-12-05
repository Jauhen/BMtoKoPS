﻿using System;
using System.IO;
using BMtoKOPS.Output;

namespace BMtoKOPS.KOPS {
  public class KopsDeal : Deal {
    private String k;
    private String d;
    private String l;
    private short t;
    public short result;
    public String tdResult;
    public bool line;

    public KopsDeal(BinaryReader reader) {
      line = true;
      if (reader == null) {
        k = "9";
        d = "";
        l = "";
        t = 0;
        result = 10000;
        return;
      }

      byte kontr = reader.ReadByte();
      k = kontr.ToString();

      switch (reader.ReadByte()) {
        case 0:
          k += "C";
          break;
        case 1:
          k += "D";
          break;
        case 2:
          k += "H";
          break;
        case 3:
          k += "S";
          break;
        case 4:
          k += "N";
          break;
      }

      switch (reader.ReadByte()) {
        case 1:
          k += "x";
          break;
        case 2:
          k += "xx";
          break;
      }

      switch (reader.ReadByte()) {
        case 0:
          d = "N";
          break;
        case 1:
          d = "E";
          break;
        case 2:
          d = "S";
          break;
        case 3:
          d = "W";
          break;
      }

      switch (reader.ReadByte()) {
        case 0:
          l = "C";
          break;
        case 1:
          l = "D";
          break;
        case 2:
          l = "H";
          break;
        case 3:
          l = "S";
          break;
      }

      switch (reader.ReadByte()) {
        case 1:
          l += "2";
          break;
        case 2:
          l += "3";
          break;
        case 3:
          l += "4";
          break;
        case 4:
          l += "5";
          break;
        case 5:
          l += "6";
          break;
        case 6:
          l += "7";
          break;
        case 7:
          l += "8";
          break;
        case 8:
          l += "9";
          break;
        case 9:
          l += "T";
          break;
        case 10:
          l += "J";
          break;
        case 11:
          l += "Q";
          break;
        case 12:
          l += "K";
          break;
        case 13:
          l += "A";
          break;
      }

      t = reader.ReadInt16();
    }

    public void SetResult(short res) {
      result = res;
      tdResult = String.Empty;

      switch (result - 10000) {
        case 0:
          tdResult = "50/50";
          break;
        case 44:
          tdResult = "60/60";
          break;
        case 42:
          tdResult = "60/50";
          break;
        case 40:
          tdResult = "60/40";
          break;
        case 2:
          tdResult = "50/60";
          break;
        case -2:
          tdResult = "50/40";
          break;
        case -44:
          tdResult = "40/40";
          break;
        case -42:
          tdResult = "40/50";
          break;
        case -40:
          tdResult = "40/60";
          break;
      }

      if (result < 9000) {
        if (result % 10 == 1 || result % 10 == -9) {
          line = false;
          result--;
        }
      }


    }

    public IHtml GetHtml(int isNS) {
      if (result != 30000) {
        if (!CorrectContract(k)) {
          HtmlProtocols.TDDeal deal = new HtmlProtocols.TDDeal();
          deal.Result = tdResult.Equals(String.Empty) ? result.ToString() : tdResult;
          deal.Style = result < 0 ? "class=\"right\"" : (!tdResult.Equals(String.Empty) ? "align=\"center\"" : "");
          return deal;
        } else if (k.Equals("0C")) {
          return new HtmlProtocols.PassedDeal();
        } else {
          HtmlProtocols.PlayedDeal deal = new HtmlProtocols.PlayedDeal();
          deal.Contract = ReplaceSuit(k);
          deal.Dealer = d;
          deal.Lead = ReplaceSuit(l);
          deal.Tricks = t == 0 ? "=" : (t > 0 ? String.Format("+{0}", t) : t.ToString());
          deal.Score = result * isNS;
          deal.IsPositive = result >= 0;
          return deal;
        }
      } else {
        return new HtmlProtocols.NotPlayedDeal();
      }
    }

    private bool CorrectContract(String k) {
      String r = k;
      r = r.Replace('C', ' ').Replace('D', ' ').Replace('H', ' ').Replace('S', ' ').Replace('N', ' ').Replace('x', ' ');
      return int.Parse(r.Trim()) < 8;
    }

    public static String ReplaceSuit(String s) {
      if (s == null)
        return "";

      if (s.IndexOf('C') > -1) {
        return s.Replace("C", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", HtmlResources.c));
      }
      if (s.IndexOf('D') > -1) {
        return s.Replace("D", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", HtmlResources.d));
      }
      if (s.IndexOf('H') > -1) {
        return s.Replace("H", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", HtmlResources.h));
      }
      if (s.IndexOf('S') > -1) {
        return s.Replace("S", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", HtmlResources.s));
      }
      if (s.IndexOf('N') > -1) {
        return s.Replace("N", String.Format("<img src=\"{0}\" width=\"13\" height=\"11\">", HtmlResources.n));
      }
      switch (s) {
        case "G1":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d1);
        case "G2":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d2);
        case "G3":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d3);
        case "G4":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d4);
        case "G5":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d5);
        case "G6":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d6);
        case "G7":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d7);
        case "G8":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d8);
        case "G9":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d9);
        case "G10":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d10);
        case "G11":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d11);
        case "G12":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d12);
        case "G13":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d13);
        case "G14":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d14);
        case "G15":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d15);
        case "G16":
          return String.Format("<img src=\"{0}\" width=\"63\" height=\"63\">", HtmlResources.d16);
      }
      return s;
    }

    public String GetText {
      get {
        String res;

        if (result != 30000) {
          if (k.Equals("9C")) {
            res = String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
            "", "", "", "", result);
          } else if (k.Equals("10Cx")) {
            res = String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                "ARB", "", "", "", tdResult);
          } else {
            res = String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                k, d, l, t, result);
          }
        } else {
          res = "Not Played";
        }

        return res;
      }
    }

    public bool HasResult() {
      if (result > 9000)
        return false;

      if (!CorrectContract(k) && !k.Equals("9C"))
        return false;

      return true;
    }
  }
}
