using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMtoKOPS.KOPS;

namespace BMtoKOPS.Output {
  /// <summary>
  /// Print tournament results. 
  /// </summary>
  public class HtmlResults : IHtml {
    public List<String> Title { get; set; }
    public List<Record> Records { get; set; } 

    public HtmlResults() {
      Records = new List<Record>();
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

      String header = GetHeader("Result");

      StringBuilder res = new StringBuilder(HtmlResources.ProtocolsHTMLBegin);
      res.Append(header);
      res.Append(HtmlResources.ProtocolsHTMLTableResultHeader);

      foreach (Record record in Records) {
        res.Append(record.print());

        // Start new page after 35 result.
        // TODO: Calculate number of result per page (from 25 to 35)
        if (record.Place % 36 == 0) {
          res.Append(HtmlResources.ProtocolsHTMLTableResultFooter);
          res.Append(header);
          res.Append(HtmlResources.ProtocolsHTMLTableResultHeader);
        }
      }

      return res.ToString();
    }

    public class Record : IHtml {
      public int Place { get; set; }
      public int Number { get; set; }
      public string Names { get; set; }
      public double Rank { get; set; }
      public string Region { get; set; }
      public string Correction { get; set; }
      public string Result { get; set; }

      public Record() {}

      public String print() {
        return String.Format(KopsHelper.GetLocalInfo(), HtmlResources.ProtocolsHTMLTableResultBody,
            Place, Number, Names, Rank, Region, Correction, Result);
      }
    }
  }
}
