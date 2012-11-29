using System;
using System.Collections.Generic;
using DAO;
using System.Data.OleDb;
using System.IO;

namespace BMtoKOPS {
    class BwsHelper {
        public static string name;
        private object oMissing;

        public BwsHelper(string dbname) {
            this.oMissing = System.Reflection.Missing.Value;
            BwsHelper.name = dbname;
            if (!File.Exists(name)) {
                this.CreateDatabase();
            }

            this.AddMachineName();
        }

        private void CreateField(TableDef tblName, String strFieldName, Boolean booAllowZeroLength, DataTypeEnum fieldType,
                                int lngAttributes, int intMaxLength, Object defaultValue) {
            Field tmpNewField = tblName.CreateField(strFieldName, fieldType, intMaxLength);

            if (fieldType == DataTypeEnum.dbText || fieldType == DataTypeEnum.dbMemo)
                tmpNewField.AllowZeroLength = booAllowZeroLength;

            tmpNewField.Attributes = lngAttributes;

            if (defaultValue != null)
                tmpNewField.DefaultValue = defaultValue;

            tblName.Fields.Append(tmpNewField);
        }

        private void CreateDatabase() {


            DBEngineClass dbe = new DBEngineClass();
            Database db = dbe.CreateDatabase(name, LanguageConstants.dbLangGeneral, DatabaseTypeEnum.dbVersion40);

            //table Clients. Used to store the name of the clients computers.
            TableDef tblNew = db.CreateTableDef("Clients", this.oMissing, this.oMissing, this.oMissing);
            this.CreateField(tblNew, "ID", false, DataTypeEnum.dbLong, (int) FieldAttributeEnum.dbAutoIncrField, 0, null);
            this.CreateField(tblNew, "Computer", false, DataTypeEnum.dbText, 0, 255, null);
            db.TableDefs.Append(tblNew);

            //table Sections. Contains the sections within your session.
            tblNew = db.CreateTableDef("Section", this.oMissing, this.oMissing, this.oMissing);
            this.CreateField(tblNew, "ID", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "Letter", false, DataTypeEnum.dbText, 0, 2, null);
            this.CreateField(tblNew, "Tables", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "MissingPair", false, DataTypeEnum.dbInteger, 0, 0, 0);
            db.TableDefs.Append(tblNew);

            //table Tables. Contains all the tables for all sections.
            tblNew = db.CreateTableDef("Tables", this.oMissing, this.oMissing, this.oMissing);
            this.CreateField(tblNew, "Section", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "Table", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "ComputerID", false, DataTypeEnum.dbInteger, 0, 0, 0);
            this.CreateField(tblNew, "Status", false, DataTypeEnum.dbInteger, 0, 0, 0);
            this.CreateField(tblNew, "LogOnOff", false, DataTypeEnum.dbInteger, 0, 0, 2);
            this.CreateField(tblNew, "CurrentRound", false, DataTypeEnum.dbInteger, 0, 0, 0);
            this.CreateField(tblNew, "CurrentBoard", false, DataTypeEnum.dbInteger, 0, 0, 0);
            this.CreateField(tblNew, "UpdateFromRound", false, DataTypeEnum.dbInteger, 0, 0, 0);
            db.TableDefs.Append(tblNew);

            //table RoundData. Contains the movement data for all tables.
            tblNew = db.CreateTableDef("RoundData", this.oMissing, this.oMissing, this.oMissing);
            this.CreateField(tblNew, "Section", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "Table", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "Round", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "NSPair", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "EWPair", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "LowBoard", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "HighBoard", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "KoPSTour", false, DataTypeEnum.dbInteger, 0, 0, null);
            db.TableDefs.Append(tblNew);

            //table ReceivedData. You need to create it here, but it will be filled by
            //BPC when results are coming in.
            for (int i = 0; i < 2; i++) {
                tblNew = db.CreateTableDef(i == 0 ? "ReceivedData" : "IntermediateData", this.oMissing, this.oMissing, this.oMissing);
                this.CreateField(tblNew, "ID", false, DataTypeEnum.dbLong, (int) FieldAttributeEnum.dbAutoIncrField, 0, 1);
                this.CreateField(tblNew, "Section", false, DataTypeEnum.dbInteger, 0, 0, null);
                this.CreateField(tblNew, "Table", false, DataTypeEnum.dbInteger, 0, 0, null);
                this.CreateField(tblNew, "Round", false, DataTypeEnum.dbInteger, 0, 0, null);
                this.CreateField(tblNew, "Board", false, DataTypeEnum.dbInteger, 0, 0, null);
                this.CreateField(tblNew, "PairNS", false, DataTypeEnum.dbInteger, 0, 0, null);
                this.CreateField(tblNew, "PairEW", false, DataTypeEnum.dbInteger, 0, 0, null);
                this.CreateField(tblNew, "Declarer", false, DataTypeEnum.dbInteger, 0, 0, null);
                this.CreateField(tblNew, "NS/EW", true, DataTypeEnum.dbText, 0, 2, null);
                this.CreateField(tblNew, "Contract", true, DataTypeEnum.dbText, 0, 10, null);
                this.CreateField(tblNew, "Result", true, DataTypeEnum.dbText, 0, 10, null);
                this.CreateField(tblNew, "LeadCard", true, DataTypeEnum.dbText, 0, 10, null);
                this.CreateField(tblNew, "Remarks", true, DataTypeEnum.dbText, 0, 255, null);
                this.CreateField(tblNew, "DateLog", false, DataTypeEnum.dbDate, 0, 0, null);
                this.CreateField(tblNew, "TimeLog", false, DataTypeEnum.dbDate, 0, 0, null);
                this.CreateField(tblNew, "Processed", false, DataTypeEnum.dbBoolean, 0, 0, false);
                for (int j = 1; j < 5; j++) {
                    this.CreateField(tblNew, "Processed" + j.ToString(), false, DataTypeEnum.dbBoolean, 0, 0, false);
                }
                this.CreateField(tblNew, "Erased", false, DataTypeEnum.dbBoolean, 0, 0, false);

                db.TableDefs.Append(tblNew);
            }

            //table PlayerNumbers. Only required if you want your scoring program to have
            //the possibility of registering the player numbers from Bridgemate input.
            tblNew = db.CreateTableDef("PlayerNumbers", this.oMissing, this.oMissing, this.oMissing);
            this.CreateField(tblNew, "Section", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "Table", false, DataTypeEnum.dbInteger, 0, 0, null);
            this.CreateField(tblNew, "Direction", false, DataTypeEnum.dbText, 0, 2, "");
            this.CreateField(tblNew, "Number", true, DataTypeEnum.dbText, 0, 16, "");
            db.TableDefs.Append(tblNew);

            //table Settings. Only required when you want to control Bridgemate settings
            //from the scoring program. Include only those fields you want to control the values.
            tblNew = db.CreateTableDef("Settings", this.oMissing, this.oMissing, this.oMissing);
            this.CreateField(tblNew, "Section", false, DataTypeEnum.dbInteger, 0, 0, 0);
            this.CreateField(tblNew, "ShowResults", false, DataTypeEnum.dbBoolean, 0, 0, true);
            this.CreateField(tblNew, "ShowOwnResult", false, DataTypeEnum.dbBoolean, 0, 0, true);
            this.CreateField(tblNew, "RepeatResults", false, DataTypeEnum.dbBoolean, 0, 0, false);
            this.CreateField(tblNew, "MaximumResults", false, DataTypeEnum.dbInteger, 0, 0, 0);
            this.CreateField(tblNew, "ShowPercentage", false, DataTypeEnum.dbBoolean, 0, 0, false);
            this.CreateField(tblNew, "GroupSections", false, DataTypeEnum.dbBoolean, 0, 0, false);
            this.CreateField(tblNew, "ScorePoints", false, DataTypeEnum.dbInteger, 0, 0, 0);
            this.CreateField(tblNew, "EnterResultsMethod", false, DataTypeEnum.dbInteger, 0, 0, 0);
            this.CreateField(tblNew, "ShowPairNumbers", false, DataTypeEnum.dbBoolean, 0, 0, true);
            this.CreateField(tblNew, "IntermediateResults", false, DataTypeEnum.dbBoolean, 0, 0, false);
            this.CreateField(tblNew, "AutopoweroffTime", false, DataTypeEnum.dbInteger, 0, 0, 10);
            this.CreateField(tblNew, "VerificationTime", false, DataTypeEnum.dbInteger, 0, 0, 2);
            this.CreateField(tblNew, "ShowContract", false, DataTypeEnum.dbInteger, 0, 0, 0);
            this.CreateField(tblNew, "LeadCard", false, DataTypeEnum.dbBoolean, 0, 0, true);
            this.CreateField(tblNew, "MemberNumbers", false, DataTypeEnum.dbBoolean, 0, 0, false);
            this.CreateField(tblNew, "MemberNumbersNoBlankEntry", false, DataTypeEnum.dbBoolean, 0, 0, true);
            this.CreateField(tblNew, "BoardOrderVerification", false, DataTypeEnum.dbBoolean, 0, 0, true);
            this.CreateField(tblNew, "HandRecordValidation", false, DataTypeEnum.dbBoolean, 0, 0, false);
            this.CreateField(tblNew, "AutoShutDownBPC", false, DataTypeEnum.dbBoolean, 0, 0, false);
            db.TableDefs.Append(tblNew);

            db.Close();
            dbe.FreeLocks();
        }

        private void AddMachineName() {
            OleDbConnection Myconnection = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0; User Id=; Password=; Data Source='{0}'", name));
            Myconnection.Open();

            OleDbCommand com = Myconnection.CreateCommand();

            com.CommandText = "DELETE FROM Clients";
            com.ExecuteNonQuery();

            com.CommandText = String.Format("INSERT INTO Clients (Computer) VALUES ('{0}')", Environment.MachineName);
            com.ExecuteNonQuery();

            Myconnection.Close();
        }

        public void SetSettings(Dictionary<string, object> settings) {
            OleDbConnection Myconnection = new OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0; User Id=; Password=; Data Source='{0}'", name));
            Myconnection.Open();

            OleDbCommand com = Myconnection.CreateCommand();

            com.CommandText = "SELECT COUNT(*) FROM Settings";
            //if (int.Parse(com.ExecuteScalar().ToString()) == 0)
            //{
            com.CommandText = "INSERT INTO Settings (ShowResults) VALUES(1)";
            com.ExecuteNonQuery();
            //}

            foreach (KeyValuePair<string, object> kvp in settings) {
                string val = kvp.Value.ToString();
                if (val == "False") {
                    val = "0";
                } else if (val == "True") {
                    val = "1";
                }

                if (kvp.Key == "EnterResultsMethod") {
                    if (val == "1")
                        val = "0";
                    else
                        val = "1";
                }
                com.CommandText = String.Format("UPDATE Settings SET {0} = {1}", kvp.Key, val);
                com.ExecuteNonQuery();
            }

            com.CommandText = "UPDATE Settings SET GroupSections = 0";
            com.ExecuteNonQuery();
            com.CommandText = "UPDATE Settings SET ScorePoints = 0";
            com.ExecuteNonQuery();


            Myconnection.Close();
        }
    }
}
