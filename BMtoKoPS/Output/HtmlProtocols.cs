using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMtoKOPS.Output {
  public class HtmlProtocols {
    public List<String> Title { get; private set; }
    public List<String> Boards { get; private set; }

    private HtmlProtocols() {}

    public static Builder newBuilder() {
      return new Builder();
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

    public String print(int dealsPerRound) {
      StringBuilder res = new StringBuilder(HtmlResources.ProtocolsHTMLBegin);

      for (int i = 0; i < Boards.Count; i++) {
        if (i % dealsPerRound == 0) {
          if (i != 0) {
            res.Append("</tr></table>");
          }
          res.Append(@"<table class=""main"" style=""page-break-after: auto""><tr>");
        }

        res.AppendFormat("<td>{0}</td>", Boards[i]);
      }

      res.Append(@"</tr></table>").Append(HtmlResources.ProtocolsHTMLEnd);

      return res.ToString();
    }

    public class Builder {

      private HtmlProtocols result = new HtmlProtocols();

      public Builder() {
        result.Boards = new List<string>();
      }

      public HtmlProtocols build() {
        var res = result;
        result = null;

        return res;
      }

      public Builder Title(List<string> title) {
        result.Title = title;
        return this;
      }

      public Builder AddBoard(string board) {
        result.Boards.Add(board);
        return this;
      }
    }
  }
}
