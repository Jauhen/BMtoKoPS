using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMtoKOPS.KOPS;

namespace BMtoKOPS.Output {
  /// <summary>
  /// Print tournament results. 
  /// </summary>
  public class HtmlResults {
    public List<String> Title { get; private set; }
    public List<Dictionary<string, object>> Records { get; private set; } 

    private HtmlResults() {}

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

    public String print() {

      String header = GetHeader("Result");

      StringBuilder res = new StringBuilder(HtmlResources.ProtocolsHTMLBegin);
      res.Append(header);
      res.Append(HtmlResources.ProtocolsHTMLTableResultHeader);

      foreach (Dictionary<string, object> record in Records) {
        res.AppendFormat(KopsHelper.GetLocalInfo(), HtmlResources.ProtocolsHTMLTableResultBody,
          record["place"],
          record["number"],
          record["names"],
          record["rank"],
          record["region"],
          record["correction"],
          record["result"]);

        // Start new page after 35 result.
        // TODO: Calculate number of result per page (from 25 to 35)
        int place = (int) record["place"];
        if (place % 36 == 0) {
          res.Append(HtmlResources.ProtocolsHTMLTableResultFooter);
          res.Append(header);
          res.Append(HtmlResources.ProtocolsHTMLTableResultHeader);
        }
      }

      return res.ToString();
    }

    public class Builder {
      private HtmlResults result = new HtmlResults();

      public Builder() {
        result.Records = new List<Dictionary<string, object>>();
      }

      public HtmlResults build() {
        var res = result;
        result = null;

        return res;
      }

      public Builder Title(List<string> title) {
        result.Title = title;
        return this;
      }

      public Builder AddRecord(Dictionary<string, object> record) {
        result.Records.Add(record);
        return this;
      }
    }
  }
}
