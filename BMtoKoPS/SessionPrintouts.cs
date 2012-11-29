using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace BMtoKOPS
{
    public partial class SessionPrintouts : UserControl
    {
        private ITournament t;

        public SessionPrintouts(ITournament tournament)
        {
            this.t = tournament;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t.ReadResults();

            DisplayHtml(t.PrintResults());
        }


        private void button3_Click(object sender, EventArgs e)
        {
            t.ReadResults();

            DisplayHtml(t.PrintProtocols());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            t.ReadResults();


            DisplayHtml(t.PrintAllHistories());
        }


        private void DisplayHtml(string html)
        {

            webBrowser1.Navigate("about:blank");

            if (webBrowser1.Document != null)
            {

                webBrowser1.Document.Write(string.Empty);

            }

            webBrowser1.DocumentText = html;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t.ReadResults();

            List<int> nums = new List<int>();

            for (int i = 0; i < textBox1.Lines.Length; i++)
            {
                int res = 0;
                if (int.TryParse(textBox1.Lines[i], out res))
                {
                    nums.Add(res);
                }
            }

            DisplayHtml(t.PrintListHistories(nums));

            textBox1.Clear();
        }


    }
}
