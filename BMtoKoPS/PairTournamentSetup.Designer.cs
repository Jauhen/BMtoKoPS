namespace BMtoKOPS
{
    partial class PairTournamentSetup
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabData = new System.Windows.Forms.TabPage();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPlayers = new System.Windows.Forms.TabPage();
            this.dataGridPlayers = new System.Windows.Forms.DataGridView();
            this.tabBM = new System.Windows.Forms.TabPage();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.player1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.region1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.rank1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.player2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.region2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.rank2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPlayers)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabData);
            this.tabControl1.Controls.Add(this.tabPlayers);
            this.tabControl1.Controls.Add(this.tabBM);
            this.tabControl1.Controls.Add(this.tabResults);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(662, 375);
            this.tabControl1.TabIndex = 0;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.comboBox2);
            this.tabData.Controls.Add(this.label4);
            this.tabData.Controls.Add(this.numericUpDown1);
            this.tabData.Controls.Add(this.label3);
            this.tabData.Controls.Add(this.comboBox1);
            this.tabData.Controls.Add(this.label2);
            this.tabData.Controls.Add(this.label1);
            this.tabData.Controls.Add(this.textBox1);
            this.tabData.Location = new System.Drawing.Point(4, 25);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(654, 346);
            this.tabData.TabIndex = 0;
            this.tabData.Text = "Data";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Mitchell",
            "Howell",
            "5-board rounds",
            "3-board rounds"});
            this.comboBox2.Location = new System.Drawing.Point(13, 127);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(162, 21);
            this.comboBox2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Movement";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(190, 127);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(59, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Deals";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Match Points",
            "Cavendish",
            "Butler",
            "Sundy Times"});
            this.comboBox1.Location = new System.Drawing.Point(13, 78);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(236, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scoring";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 23);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(239, 32);
            this.textBox1.TabIndex = 0;
            // 
            // tabPlayers
            // 
            this.tabPlayers.Controls.Add(this.dataGridPlayers);
            this.tabPlayers.Location = new System.Drawing.Point(4, 25);
            this.tabPlayers.Name = "tabPlayers";
            this.tabPlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlayers.Size = new System.Drawing.Size(654, 346);
            this.tabPlayers.TabIndex = 1;
            this.tabPlayers.Text = "Players";
            this.tabPlayers.UseVisualStyleBackColor = true;
            // 
            // dataGridPlayers
            // 
            this.dataGridPlayers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPlayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.player1,
            this.region1,
            this.rank1,
            this.player2,
            this.region2,
            this.rank2});
            this.dataGridPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridPlayers.Location = new System.Drawing.Point(3, 3);
            this.dataGridPlayers.Name = "dataGridPlayers";
            this.dataGridPlayers.RowHeadersVisible = false;
            this.dataGridPlayers.Size = new System.Drawing.Size(648, 340);
            this.dataGridPlayers.TabIndex = 0;
            this.dataGridPlayers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridPlayers_CellContentClick);
            // 
            // tabBM
            // 
            this.tabBM.Location = new System.Drawing.Point(4, 25);
            this.tabBM.Name = "tabBM";
            this.tabBM.Size = new System.Drawing.Size(654, 346);
            this.tabBM.TabIndex = 2;
            this.tabBM.Text = "Bridgemates";
            this.tabBM.UseVisualStyleBackColor = true;
            // 
            // tabResults
            // 
            this.tabResults.Location = new System.Drawing.Point(4, 25);
            this.tabResults.Name = "tabResults";
            this.tabResults.Size = new System.Drawing.Size(654, 346);
            this.tabResults.TabIndex = 3;
            this.tabResults.Text = "Results";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // id
            // 
            this.id.FillWeight = 28.28948F;
            this.id.HeaderText = "Id";
            this.id.MaxInputLength = 3;
            this.id.MinimumWidth = 25;
            this.id.Name = "id";
            this.id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // player1
            // 
            this.player1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.player1.FillWeight = 41.72699F;
            this.player1.HeaderText = "Player 1";
            this.player1.MinimumWidth = 250;
            this.player1.Name = "player1";
            this.player1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // region1
            // 
            this.region1.FillWeight = 355.33F;
            this.region1.HeaderText = "Region";
            this.region1.Items.AddRange(new object[] {
            "Minsk",
            "Molodechno"});
            this.region1.MinimumWidth = 100;
            this.region1.Name = "region1";
            this.region1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.region1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // rank1
            // 
            this.rank1.FillWeight = 149.4726F;
            this.rank1.HeaderText = "Rank";
            this.rank1.Items.AddRange(new object[] {
            "0",
            "0.5",
            "1.0",
            "1.5",
            "2.5",
            "4.0",
            "5.0",
            "7.0",
            "12.0",
            "18.0"});
            this.rank1.MinimumWidth = 50;
            this.rank1.Name = "rank1";
            this.rank1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.rank1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // player2
            // 
            this.player2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.player2.FillWeight = 41.72699F;
            this.player2.HeaderText = "Player 2";
            this.player2.MinimumWidth = 250;
            this.player2.Name = "player2";
            this.player2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // region2
            // 
            this.region2.FillWeight = 41.72699F;
            this.region2.HeaderText = "Region";
            this.region2.Items.AddRange(new object[] {
            "Minsk",
            "Molodechno"});
            this.region2.MinimumWidth = 100;
            this.region2.Name = "region2";
            this.region2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.region2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // rank2
            // 
            this.rank2.FillWeight = 41.72699F;
            this.rank2.HeaderText = "Rank";
            this.rank2.Items.AddRange(new object[] {
            "0",
            "0.5",
            "1.0",
            "1.5",
            "2.5",
            "4.0",
            "5.0",
            "7.0",
            "12.0",
            "18.0"});
            this.rank2.MinimumWidth = 50;
            this.rank2.Name = "rank2";
            this.rank2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // PairTournamentSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "PairTournamentSetup";
            this.Size = new System.Drawing.Size(662, 375);
            this.tabControl1.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.tabData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPlayers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPlayers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.TabPage tabPlayers;
        private System.Windows.Forms.TabPage tabBM;
        private System.Windows.Forms.TabPage tabResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridPlayers;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn player1;
        private System.Windows.Forms.DataGridViewComboBoxColumn region1;
        private System.Windows.Forms.DataGridViewComboBoxColumn rank1;
        private System.Windows.Forms.DataGridViewTextBoxColumn player2;
        private System.Windows.Forms.DataGridViewComboBoxColumn region2;
        private System.Windows.Forms.DataGridViewComboBoxColumn rank2;
    }
}
