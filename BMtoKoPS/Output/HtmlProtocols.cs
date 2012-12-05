using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        return "<td colspan=\"4\">PASS</td><td align=\"center\">0</td>";
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
        return String.Format("<td colspan=\"4\"></td><td {1}>{0}</td>",
              Result, Style);
      }
    }

    public class PlayedDeal : IHtml {
      public string Contract { get; set; }
      public string Dealer { get; set; }
      public string Lead { get; set; }
      public string Tricks { get; set; }
      public int Score { get; set; }
      public bool IsPositive { get; set; }

      public String print() {
        return String.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td class=\"right\">{3}</td><td {5}>{4}</td>",
              Contract, Dealer, Lead,
              Tricks,
              Score,
              IsPositive ? "" : "class=\"right\"");
      }
    }
  }
}
