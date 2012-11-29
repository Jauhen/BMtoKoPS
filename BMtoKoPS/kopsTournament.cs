using System.Collections.Generic;
using System.IO;
using System.Data;
using System;

namespace BMtoKOPS {
    public class KopsTournament : Tournament {
        private int pairsNumber;
        public int scoring;

        public KopsTournament(string path) {
            this.path = path;

            StreamReader f = new StreamReader(path);

            List<string> file = new List<string>();

            while (!f.EndOfStream) {
                file.Add(f.ReadLine().Split('*')[0]);
            }

            f.Close();

            Pairs = new Pairs();

            SetTournamentData(file);

            //   InitDataSet();

            ReadPlayersNames();
        }

        /// <summary>
        /// Setup tournament
        /// </summary>
        /// <param name="data">KOPS inf file</param>
        private void SetTournamentData(List<string> data) {
            int tournamentBase = int.Parse(data[0]);

            if (tournamentBase > 10000) {
                tournamentBase -= 10000;
                scoringMethod = new ScoringCSR();
                scoring = 1;
            } else {
                scoringMethod = new ScoringMP();
                scoring = 0;
            }

            int rounds = int.Parse(data[1]);
            dealsPerRound = int.Parse(data[2]);
            int mitchellSections = int.Parse(data[3]);
            int numerationJump = int.Parse(data[4]);
            int roundUntilLineChanged = int.Parse(data[5]);
            int dealingRound = int.Parse(data[6]);
            pairsNumber = int.Parse(data[7]);
            records = int.Parse(data[8]);
            //int.Parse(data[9]); //read AverageRecord
            //int.Parse(data[10]); //read MaxRecord
            //int.Parse(data[13]); // read Deals Number
            int appendix = int.Parse(data[14]);

            List<int> pairNumbers = new List<int>(pairsNumber);

            if (numerationJump == 0)
                numerationJump = tournamentBase;

            for (int i = 0; i < numerationJump * 2 * (mitchellSections + 2); i++)
            //for (int i = 0; i < 16; i++)
                {
                pairNumbers.Add(0);
            }

            for (int i = 0; i < pairsNumber; i++) {
                pairNumbers[int.Parse(data[15 + i]) - 1] = i + 1;
            }

            Pairs.SetNumbers(pairNumbers);

            movement = new Movement(tournamentBase, mitchellSections);

            movement.Generate(rounds, dealingRound, roundUntilLineChanged,
                appendix > 0 ? data.GetRange(15 + pairsNumber, (appendix + 1) * 2 * tournamentBase) : null,
                pairsNumber, appendix);

            boards = new List<Board>();

            Nullable<int> index = ReadINR();

            for (int i = 0; i < tournamentBase * dealsPerRound; i++) {
                //TODO: add processing of INR files
                boards.Add(new Board(i + (index.HasValue ? index.Value : 0)));
            }
        }

        /*
        private void InitDataSet()
        {
            DataTable table = new DataTable("Fill");
            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;
            DataRow row;

            for (var i = 0; i < tournamentBase * dealsPerRound + 1; i++)
            {
                // Create new DataColumn, set DataType, 
                // ColumnName and add to DataTable.    
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = i.ToString();
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);
            }

            // Instantiate the DataSet variable.
            //      results = new DataSet();
            // Add the new DataTable to the DataSet.
            //   results.Tables.Add(table);


            // Create three new DataRow objects and add 
            // them to the DataTable
            row = table.NewRow();
            for (var j = 1; j < tournamentBase * dealsPerRound + 1; j++)
            {
                row[j.ToString()] = j;
            }
            table.Rows.Add(row);

            for (int i = 1; i < records + 1; i++)
            {
                row = table.NewRow();
                row[0] = i;
                for (var j = 1; j < tournamentBase * dealsPerRound + 1; j++)
                {
                    row[(i + 1).ToString()] = System.DBNull.Value;
                }
                table.Rows.Add(row);
            }

        }
        */

        /// <summary>
        /// 
        /// </summary>
        public override void ReadResults() {
            String pathRes = String.Format(@"{0}\{1}.ZAP",
                Path.GetDirectoryName(path),
                Path.GetFileNameWithoutExtension(path));

            BinaryReader fRes = new BinaryReader(File.OpenRead(pathRes));

            List<short> res = new List<short>();
            List<KopsDeal> readDeals = new List<KopsDeal>();

            while (fRes.BaseStream.Position < fRes.BaseStream.Length) {
                res.Add(fRes.ReadInt16());
            }

            fRes.Close();

            try {
                String pathDeal = String.Format(@"{0}\{1}.KNT",
                    Path.GetDirectoryName(path),
                    Path.GetFileNameWithoutExtension(path));

                BinaryReader fDeal = new BinaryReader(File.OpenRead(pathDeal));

                while (fDeal.BaseStream.Position < fDeal.BaseStream.Length) {
                    readDeals.Add(new KopsDeal(fDeal));
                }

                fDeal.Close();

                if (res.Count != readDeals.Count) {
                    System.Windows.Forms.MessageBox.Show("Incorrect numbers of results");
                    return;
                }
            } catch (Exception) {
                System.Windows.Forms.MessageBox.Show("No file with contracts");
                for (int i = 0; i < res.Count; i++) {
                    readDeals.Add(new KopsDeal(null));
                }
            }

            for (int i = 0; i < readDeals.Count; i++) {
                readDeals[i].SetResult(res[i]);
            }

            for (int i = 0; i < readDeals.Count / records; i++) {
                List<KopsDeal> d = new List<KopsDeal>();
                for (int j = 0; j < records; j++) {
                    d.Add(readDeals[i * records + j]);
                }
                boards[i].SetDeals(d);
            }

            CalculateResults();

            sessionMax = 0;
            ReadSessionResults();
            ReadSessionName();
            ReadPBN();
        }

        private void ReadPlayersNames() {
            String path = String.Format(@"{0}\{1}.LUC",
                Path.GetDirectoryName(this.path),
                Path.GetFileNameWithoutExtension(this.path));

            StreamReader f = new StreamReader(path);

            String s = f.ReadLine();

            f.Close();


            for (int i = 0; i < pairsNumber; i++) {
                List<Player> playersNames = new List<Player>();

                Player pl = new Player(s.Substring(i * 26 * 2, 26));
                playersNames.Add(pl);
                pl = new Player(s.Substring(i * 26 * 2 + 26, 26));
                playersNames.Add(pl);

                Pairs.AddPair(playersNames);
            }
        }

        private void ReadSessionResults() {
            String path = String.Format(@"{0}\{1}.SES",
                Path.GetDirectoryName(this.path),
                Path.GetFileNameWithoutExtension(this.path));

            try {
                StreamReader f = new StreamReader(path);

                String type = f.ReadLine();
                if (type.Equals("MAXY")) {
                    sessionMax = int.Parse(f.ReadLine().Split('\t')[0]);
                    scoringMethod.SetMax(sessionMax);
                } else {
                    f.ReadLine();
                    sessionMax = 100;
                }
                sessionResults = new Dictionary<int, KeyValuePair<double, double>>();

                f.ReadLine();

                while (!f.EndOfStream) {
                    String s = f.ReadLine();

                    if (s.Split('\t').Length > 2) {
                        sessionResults.Add(int.Parse(s.Split('\t')[0]),
                            new KeyValuePair<double, double>(
                                KopsHelper.GetDoubleFromString(s.Split('\t')[1]),
                                KopsHelper.GetDoubleFromString(s.Split('\t')[2])));
                    }
                }

                f.Close();
            } catch (Exception) {
                System.Windows.Forms.MessageBox.Show("No Session Results.");
            }
        }

        private void ReadSessionName() {
            String path = String.Format(@"{0}\{1}.NAG",
                Path.GetDirectoryName(this.path),
                Path.GetFileNameWithoutExtension(this.path));

            title = new List<string>();

            try {
                StreamReader f = new StreamReader(path);
                title.AddRange(f.ReadLine().Split('\\'));
                f.Close();
            } catch (Exception) {
                System.Windows.Forms.MessageBox.Show("No Session Name.");
            }
        }

        private Nullable<int> ReadINR() {
            String pathRes = String.Format(@"{0}\{1}.INR",
                Path.GetDirectoryName(path),
                Path.GetFileNameWithoutExtension(path));

            try {
                StreamReader reader = new StreamReader(pathRes);
                String line = reader.ReadLine();
                reader.Close();
                return int.Parse(line);
            } catch (FileNotFoundException) {
                return null;
            }
        }

        private void ReadPBN() {
            String pathRes = String.Format(@"{0}\{1}.PBN",
                Path.GetDirectoryName(path),
                Path.GetFileNameWithoutExtension(path));

            try {
                StreamReader fRes = new StreamReader(pathRes);
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.

                List<string> hand = new List<string>();
                int board = 0;
                String deal = String.Empty;
                String ability = String.Empty;
                String minimax = String.Empty;

                while ((line = fRes.ReadLine()) != null && board < boards.Count) {
                    if (line.StartsWith("[Deal ")) {
                        if (!deal.Equals(String.Empty)) {
                            boards[board++].SetPBN(deal, ability, minimax);
                            deal = String.Empty;
                            ability = String.Empty;
                            minimax = String.Empty;
                        }
                        deal = line.Substring(9, 67);
                    } else if (line.StartsWith("[Ability ")) {
                        ability = line.Substring(10, 31);
                    } else if (line.StartsWith("[Minimax")) {
                        char[] end = { '"', ']' };
                        minimax = line.Substring(10).TrimEnd(end);
                    }
                }
            } catch (FileNotFoundException) {
            }
        }
    }
}
