using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMtoKOPS.KOPS;

namespace BMtoKOPS.Output {
  public class HtmlProtocols : IHtml {
    public List<String> Title { get; set; }
    public List<Board> Boards { get; set; }
    public int dealsPerRound { get; set; }

    public HtmlProtocols() {
      Boards = new List<Board>();
    }

    private String GetHeader(String type) {
      StringBuilder title = new StringBuilder();
      for (int i = 0; i < Title.Count; i++) {
        if (!Title[i].Equals(String.Empty)) {
          title.AppendFormat(HtmlResources.ProtocolsHTMLTitle, Title[i]);
        }
      }

      return String.Format(HtmlResources.ProtocolsHTMLHeader,
          HtmlResources.logo,
          title,
          type);
    }

    public String print() {
      StringBuilder res = new StringBuilder(HtmlResources.ProtocolsHTMLBegin);

      for (int i = 0; i < Boards.Count; i++) {
        if (i % dealsPerRound == 0) {
          if (i != 0) {
            res.Append("</tr></table>");
          }
          res.Append(@"<table class=""main"" style=""page-break-after: auto""><tr>");
        }

        res.AppendFormat("<td>{0}</td>", Boards[i].print());
      }

      res.Append(@"</tr></table>").Append(HtmlResources.ProtocolsHTMLEnd);

      return res.ToString();
    }

    public class Board : IHtml {
      public int Number { get; set; }
      public string Header { get; set; }
      public List<Deal> Deals { get; set; }

      public Board() {
        Deals = new List<Deal>();
      }

      public String print() {
        StringBuilder res = new StringBuilder();
        res.AppendFormat(@"<h1>Board {0}</h1><div style=""text-align: center"">{1}</div>",
            Number, Header);

        res.Append(HtmlResources.ProtocolsHTMLTableProtocolHeader);

        foreach (Deal deal in Deals) {
          res.Append(deal.print());
        }

        res.Append("</table>");

        return res.ToString();
      }

      public void setHeader(BMtoKOPS.Board board) {
        if (board.hasPBN) {
          //TODO: PBN without maxs
          Header = String.Format(HtmlResources.ProtocolsHTMLTablePBN,
              board.Dealer(), //0
              board.Vulnerable(), //1
              GetCards("N", board), //2
              GetCards("W", board), //3
              ReplaceSuit("G" + (board.number % 16 + 1).ToString()), //4
              GetCards("E", board), //5
              GetCards("S", board), //6
              ReplaceSuit("N"), //7
              ReplaceSuit("S"), //8
              ReplaceSuit("H"), //9
              ReplaceSuit("D"), //10
              ReplaceSuit("C"), //11
              GetAbility("N", board), //12
              GetAbility("E", board), //13
              GetAbility("S", board), //14
              GetAbility("W", board), //15
              board.minimax != "" ? board.minimax[0] + ReplaceSuit(board.minimax[1].ToString()) : "", //16
              board.minimax != "" ?
                  (board.minimax[2] == 'D' ?
                      ("x by " + board.minimax[3] + "; " + board.minimax.Substring(4).Replace("-", "&minus;")) :
                      " by " + board.minimax[2] + "; " + board.minimax.Substring(3).Replace("-", "&minus;"))
                  : ""// 17
              );
        } else {
          Header = String.Format(@"Dealer: {0}, Vulnerable: {1}",
              board.Dealer(),
              board.Vulnerable());
        }
      }

      public String GetCards(String seat, BMtoKOPS.Board board) {
        string[] h = board.deal.Split(' ');
        switch (seat) {
          case "N":
            return GetPrintedHand(h[0]);
          case "E":
            return GetPrintedHand(h[1]);
          case "S":
            return GetPrintedHand(h[2]);
          case "W":
            return GetPrintedHand(h[3]);
        }
        return String.Empty;
      }

      /// <summary>
      /// Convert PBN string like AT7.KQJ64.6432.T to HTML
      /// </summary>
      /// <param name="hand">String like AT7.KQJ64.6432.T</param>
      /// <returns>HTML table cell</returns>
      public string GetPrintedHand(string hand) {
        //add space between chars then split by full stop, also replace T by 10
        String[] s =
            String
                .Join(" ", hand.ToCharArray().Select(x => x.ToString()).ToArray())
                .Replace("T", "10")
                .Split('.');

        String[] suits = new String[] { "S", "H", "D", "C" };

        for (int i = 0; i < 4; i++) {
          s[i] = String.Format("{0}&nbsp; {1}",
              ReplaceSuit(suits[i]),
              s[i].Trim());
        }

        return String.Format(@"<td class=""w"">{0}</td>",
            String.Join("<br />", s));
      }

      public String GetAbility(String seat, BMtoKOPS.Board board) {
        if (board.ability.Equals(String.Empty)) {
          return String.Empty;
        }

        string[] mms = board.ability.Split(' ');
        switch (seat) {
          case "N":
            return GetPrintedMaxPerHand(mms[0]);
          case "E":
            return GetPrintedMaxPerHand(mms[1]);
          case "S":
            return GetPrintedMaxPerHand(mms[2]);
          case "W":
            return GetPrintedMaxPerHand(mms[3]);
        }
        return String.Empty;
      }

      /// <summary>
      /// Convert PBN string like N:5AC72 to HTML
      /// </summary>
      /// <param name="maxes">String like N:5AC72</param>
      /// <returns>Set of HTML table cells</returns>
      public String GetPrintedMaxPerHand(String maxes) {
        StringBuilder res = new StringBuilder();

        if (maxes.Equals(String.Empty)) {
          return String.Empty;
        }

        for (int i = 2; i < 7; i++) {
          res.AppendFormat(@"<td class=""an1"">{0}</td>",
              Convert.ToInt32(maxes.Substring(i, 1), 16));
        }

        return res.ToString();
      }
    }

    public class Deal : IHtml {
      public string NsNumber { private get; set; }
      public string EwNumber { private get; set; }
      public IHtml Play { private get; set; }
      public string NsResult { private get; set; }
      public string EwResult { private get; set; }

      public Deal() {}

      public String print() {
        return String.Format(HtmlResources.ProtocolsHTMLTableProtocolRow,
              NsNumber, EwNumber, Play.print(), NsResult, EwResult);
      }
    }

    public class PassedDeal : IHtml {
      public String print() {
        return @"<td colspan=""4"">PASS</td><td align=""center"">0</td>";
      }
    }

    public class NotPlayedDeal : IHtml {
      public String print() {
        return @"<td colspan=""5"">Not Played</td>";
      }
    }

    public class TDDeal : IHtml {
      public string Result { get; set; }
      public string Style { get; set; } 

      public String print() {
        return String.Format(@"<td colspan=""4""></td><td {1}>{0}</td>",
              Result, Style);
      }
    }

    public class PlayedDeal : IHtml {
      public string Contract { get; set; }
      public string Dealer { get; set; }
      public string Lead { get; set; }
      public int Tricks { get; set; }
      public int Score { get; set; }
      public bool IsPositive { get; set; }

      public String print() {
        return String.Format(@"<td>{0}</td><td>{1}</td><td>{2}</td><td class=""right"">{3}</td><td {5}>{4}</td>",
              HtmlProtocols.ReplaceSuit(Contract), 
              Dealer, 
              HtmlProtocols.ReplaceSuit(Lead),
              Tricks == 0 ? "=" : (Tricks > 0 ? String.Format("+{0}", Tricks) : Tricks.ToString()),
              Score,
              IsPositive ? "" : @"class=""right""");
      }
    }

    public static String ReplaceSuit(String s) {
      if (s == null)
        return "";

      if (s.IndexOf('C') > -1) {
        return s.Replace("C", String.Format(@"<img src=""{0}"" width=""13"" height=""11"">", HtmlResources.c));
      }
      if (s.IndexOf('D') > -1) {
        return s.Replace("D", String.Format(@"<img src=""{0}"" width=""13"" height=""11"">", HtmlResources.d));
      }
      if (s.IndexOf('H') > -1) {
        return s.Replace("H", String.Format(@"<img src=""{0}"" width=""13"" height=""11"">", HtmlResources.h));
      }
      if (s.IndexOf('S') > -1) {
        return s.Replace("S", String.Format(@"<img src=""{0}"" width=""13"" height=""11"">", HtmlResources.s));
      }
      if (s.IndexOf('N') > -1) {
        return s.Replace("N", String.Format(@"<img src=""{0}"" width=""13"" height=""11"">", HtmlResources.n));
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
  }
}
