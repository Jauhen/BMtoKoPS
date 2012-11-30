using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using BMtoKOPS.KOPS;

namespace BMtoKOPS
{
    public partial class MainForm : Form
    {
        public string bwsfile;
        private List<KopsTournament> tournaments;

        public MainForm()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openPairTournamentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openPairTournament.ShowDialog() == DialogResult.OK)
            {
                string[] str = openPairTournament.FileNames;

                if (str.Length > 0)
                {
             /*       bwsfile = String.Format(@"{0}\{1}.bws", Path.GetDirectoryName(str[0]), Path.GetFileNameWithoutExtension(str[0]));
                    BwsHelper bws = new BwsHelper(bwsfile);

                    fBMSettings form = new fBMSettings(false);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        bws.SetSettings(form.options);
                    }

                    if (str.Length > 1)
                    {
                        Dictionary<string,object> split = new Dictionary<string,object>();
                        split.Add("GroupSections", false);
                        bws.SetSettings(split);
                    }
*/
                    tournaments = new List<KopsTournament>();

                    foreach (string s in str)
                    {
                        KopsTournament t = new KopsTournament(s, new KopsReader());
                        tournaments.Add(t);
                        /* add new tab for tournament */
                        TabPage tp = new TabPage(Path.GetFileNameWithoutExtension(s));
                        tabControl1.TabPages.Add(tp);
                        SessionPrintouts control = new SessionPrintouts(t);
                        control.Parent = tp;
                        control.Dock = DockStyle.Fill;
                    }
                }
                if (tournaments.Count > 0)
                {
                    //filling grid width tournament data
         /*           BindingSource bSource = new BindingSource();
                    bSource.DataSource = tournaments[0].results.Tables["Fill"];
                    dataGridTourn1.DataSource = bSource;
                    dataGridTourn1.Width = (dataGridTourn1.ColumnCount) * 23 + 1;
                    dataGridTourn1.Height = (dataGridTourn1.RowCount) * 20 + 2;

                    for(int i = 0 ; i < dataGridTourn1.RowCount; i++) {
                        dataGridTourn1.Rows[i].Cells[0].Style.BackColor = Color.Blue;
                        dataGridTourn1.Rows[i].Cells[0].Style.ForeColor = Color.White;
                    }
                    for (int i = 0; i < dataGridTourn1.ColumnCount; i++)
                    {
                        dataGridTourn1.Rows[0].Cells[i].Style.BackColor = Color.Blue;
                        dataGridTourn1.Rows[0].Cells[i].Style.ForeColor = Color.White;
                    }
*/
                    
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            fMergePairTourn f = new fMergePairTourn();
            f.Show();
            
        }

        private void PrintPageEvent(object sender, PrintPageEventArgs ev)
        {
            string strHello = "Hello Printer!";
            Font oFont = new Font("Arial", 10);
            Rectangle marginRect = ev.MarginBounds;

            ev.Graphics.DrawRectangle(new Pen(System.Drawing.Color.Black), marginRect);
            ev.Graphics.DrawString(strHello, oFont, new SolidBrush(System.Drawing.Color.Blue),
              (ev.PageBounds.Right / 2), ev.PageBounds.Bottom / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fMergePairTourn f = new fMergePairTourn();
            f.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openPairTournamentToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            TabPage tp = new TabPage("test");
            tabControl1.TabPages.Add(tp);
            PairTournamentSetup control = new PairTournamentSetup();
            control.Parent = tp;
            control.Dock = DockStyle.Fill;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (openPairMultySession.ShowDialog() == DialogResult.OK)
            {
                string[] str = openPairMultySession.FileNames;

                if (str.Length > 0)
                {
                    KopsMultysession t = new KopsMultysession(str[0]);

                    /* add new tab for tournament */
                    TabPage tp = new TabPage(Path.GetFileNameWithoutExtension(str[0]));
                    tabControl1.TabPages.Add(tp);
                    SessionPrintouts control = new SessionPrintouts(t);
                    control.Parent = tp;
                    control.Dock = DockStyle.Fill;
                }
            }
        }

    }
}
