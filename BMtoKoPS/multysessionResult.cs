using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMtoKOPS
{
    public class MultysessionResult
    {
        private int number;
        private string names;
        private double rank;
        private string region;
        private List<double> results;
        private double total;

        public MultysessionResult(int number, string names, double rank, string region, int sessions)
        {
            this.number = number;
            this.names = names;
            this.rank = rank;
            this.region = region;
            results = new List<double>(sessions);
            for (int i = 0; i < sessions; i++)
            {
                results.Add(0);
            }
        }

        public void SetResult(int session, double result)
        {
            results[session] = result;
        }

        public void SetTotal(double total)
        {
            this.total = total;
        }

        public String GetHTML(int place, bool isMax)
        {
            if (names.Equals("<nobr> &mdash; </nobr>"))
            {
                return "";
            }

            StringBuilder res = new StringBuilder();
            res.AppendFormat(@"<tr><td style=""text-align: right""><b>{0}</b></td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>",
                place,
                number,
                names,
                rank,
                region);

            for (int i = 0; i < results.Count; i++)
            {
                res.AppendFormat(isMax ? @"<td style=""text-align: right"">{0:0.00}%</td>" : @"<td  style=""text-align: right"">{0:0.00}</td>",
                    results[i]);
            }

            res.AppendFormat(isMax ? @"<td style=""text-align: right""><b>{0:0.00}%</b></td>" : @"<td style=""text-align: right""><b>{0:0.00}</b></td>",
                total);

            return res.ToString();
        }

        public int GetNumber()
        {
            return number;
        }

        public double GetTotal()
        {
            return total;
        }
    }
}
