using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAO;
using System.Data.OleDb;
using System.IO;

namespace BMtoKOPS
{
    public partial class fMergePairTourn : Form
    {
        private object oMissing;

        public fMergePairTourn()
        {
            InitializeComponent();
            this.oMissing = System.Reflection.Missing.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                label2.Text = openFileDialog2.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label3.Text = saveFileDialog1.FileName;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            BwsHelper file = new BwsHelper(label3.Text);

            List<OleDbConnection> dbs = new List<OleDbConnection>(2);

            dbs.Add(new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0; User Id=; Password=; Data Source='{0}'", label1.Text)));
            dbs[0].Open();

            dbs.Add(new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0; User Id=; Password=; Data Source='{0}'", label2.Text)));
            dbs[1].Open();

            OleDbConnection Myconnection3 = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0; User Id=; Password=; Data Source='{0}'", label3.Text));
            Myconnection3.Open();

            OleDbCommand ins = Myconnection3.CreateCommand();

         
            for (int i = 0; i < 2; i++)
            {

                OleDbCommand read = dbs[i].CreateCommand();
                read.CommandText = "SELECT * FROM PlayerNumbers";

                OleDbDataReader dr = read.ExecuteReader();

                while (dr.Read())
                {
                    ins.CommandText = String.Format("INSERT INTO PlayerNumbers ([Section], [Table], Direction, [Number]) VALUES ({0}, {1}, '{2}', {3})",
                        i + 1, dr[1], dr[2], "NULL");
                    ins.ExecuteNonQuery();
                }

                dr.Close();

                read.CommandText = "SELECT * FROM [Section]";

                dr = read.ExecuteReader();
                while (dr.Read())
                {
                    ins.CommandText = String.Format("INSERT INTO [Section] ([ID], [Letter], [Tables], [MissingPair]) VALUES ({0}, '{1}', {2}, {3})",
                        i + 1, i == 0 ? "A" : "B", dr[2], dr[4]);
                    ins.ExecuteNonQuery();
                }
                dr.Close();


                read.CommandText = "SELECT * FROM RoundData";

                dr = read.ExecuteReader();
                while (dr.Read())
                {
                    ins.CommandText = String.Format("INSERT INTO RoundData ([Section], [Table], [Round], [NSPair], [EWPair], [LowBoard], [HighBoard]) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                        i + 1, dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);
                    ins.ExecuteNonQuery();
                }
                dr.Close();


                read.CommandText = "SELECT * FROM [Tables]";

                dr = read.ExecuteReader();
                while (dr.Read())
                {
                    ins.CommandText = String.Format("INSERT INTO [Tables] ([Section], [Table], [ComputerID], [Status], [LogOnOff], [CurrentRound], [CurrentBoard], [UpdateFromRound]) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                        i + 1, dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);
                    ins.ExecuteNonQuery();
                }
                dr.Close();
            }

            dbs[0].Close();
            dbs[1].Close();

            Myconnection3.Close();
            label4.Text = "Done";

            fBMSettings form = new fBMSettings(false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                for (int i = 1; i < 3; i++)
                {
                    file.SetSettings(form.options);
                    Myconnection3.Open();
                    ins.CommandText = String.Format("UPDATE [Settings] SET [Section]={0} WHERE [Section]=0", i);
                    ins.ExecuteNonQuery();
                    Myconnection3.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OleDbConnection Myconnection3 = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0; User Id=; Password=; Data Source='{0}'", label3.Text));
            Myconnection3.Open();

            List<OleDbConnection> dbs = new List<OleDbConnection>(2);

            dbs.Add(new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0; User Id=; Password=; Data Source='{0}'", label1.Text)));
            dbs[0].Open();

            dbs.Add(new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0; User Id=; Password=; Data Source='{0}'", label2.Text)));
            dbs[1].Open();

            OleDbCommand read = Myconnection3.CreateCommand();
            read.CommandText = "SELECT * FROM ReceivedData WHERE Processed = 0";

            OleDbDataReader dr = read.ExecuteReader();

            while (dr.Read())
            {
                OleDbCommand ins = dbs[(short)dr[1] - 1].CreateCommand();

                ins.CommandText = String.Format("INSERT INTO ReceivedData ([Section], [Table], [Round], [Board], [PairNS], [PairEW], [Declarer], [NS/EW], [Contract], [Result], [LeadCard], [Remarks], [DateLog], [TimeLog]) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')",
                    1, dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14]);
                ins.ExecuteNonQuery();

                if ((short)dr[1] == 1)
                {
                    label5.Text = (int.Parse(label5.Text) + 1).ToString();
                }
                else
                {
                    label6.Text = (int.Parse(label6.Text) + 1).ToString();
                }
            }

            dr.Close();

            read.CommandText = "UPDATE ReceivedData SET Processed = -1";
            read.ExecuteNonQuery();

            Myconnection3.Close();
            dbs[0].Close();
            dbs[1].Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

    }
}
