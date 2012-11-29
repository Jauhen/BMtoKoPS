namespace BMtoKOPS
{
    partial class fBMSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbShowResults = new System.Windows.Forms.CheckBox();
            this.cbShowPercentage = new System.Windows.Forms.CheckBox();
            this.cbRepeatResults = new System.Windows.Forms.CheckBox();
            this.cbScorePoints = new System.Windows.Forms.CheckBox();
            this.cbEnterResultsMethod = new System.Windows.Forms.CheckBox();
            this.cbShowPairNumbers = new System.Windows.Forms.CheckBox();
            this.udAutopoweroffTime = new System.Windows.Forms.NumericUpDown();
            this.udVerificationTime = new System.Windows.Forms.NumericUpDown();
            this.cbLeadCard = new System.Windows.Forms.CheckBox();
            this.cbMemberNumbers = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.udAutopoweroffTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udVerificationTime)).BeginInit();
            this.SuspendLayout();
            // 
            // cbShowResults
            // 
            this.cbShowResults.AutoSize = true;
            this.cbShowResults.Checked = true;
            this.cbShowResults.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowResults.Location = new System.Drawing.Point(12, 12);
            this.cbShowResults.Name = "cbShowResults";
            this.cbShowResults.Size = new System.Drawing.Size(242, 17);
            this.cbShowResults.TabIndex = 0;
            this.cbShowResults.Text = "Show previous results of the board just played";
            this.cbShowResults.UseVisualStyleBackColor = true;
            // 
            // cbShowPercentage
            // 
            this.cbShowPercentage.AutoSize = true;
            this.cbShowPercentage.Location = new System.Drawing.Point(36, 35);
            this.cbShowPercentage.Name = "cbShowPercentage";
            this.cbShowPercentage.Size = new System.Drawing.Size(131, 17);
            this.cbShowPercentage.TabIndex = 1;
            this.cbShowPercentage.Text = "Show the percentage ";
            this.cbShowPercentage.UseVisualStyleBackColor = true;
            // 
            // cbRepeatResults
            // 
            this.cbRepeatResults.AutoSize = true;
            this.cbRepeatResults.Location = new System.Drawing.Point(36, 58);
            this.cbRepeatResults.Name = "cbRepeatResults";
            this.cbRepeatResults.Size = new System.Drawing.Size(135, 17);
            this.cbRepeatResults.TabIndex = 2;
            this.cbRepeatResults.Text = "Allow repeat the results";
            this.cbRepeatResults.UseVisualStyleBackColor = true;
            // 
            // cbScorePoints
            // 
            this.cbScorePoints.AutoSize = true;
            this.cbScorePoints.Checked = true;
            this.cbScorePoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbScorePoints.Location = new System.Drawing.Point(12, 81);
            this.cbScorePoints.Name = "cbScorePoints";
            this.cbScorePoints.Size = new System.Drawing.Size(224, 17);
            this.cbScorePoints.TabIndex = 3;
            this.cbScorePoints.Text = "Show score points from perspective of NS";
            this.cbScorePoints.UseVisualStyleBackColor = true;
            // 
            // cbEnterResultsMethod
            // 
            this.cbEnterResultsMethod.AutoSize = true;
            this.cbEnterResultsMethod.Checked = true;
            this.cbEnterResultsMethod.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnterResultsMethod.Location = new System.Drawing.Point(12, 105);
            this.cbEnterResultsMethod.Name = "cbEnterResultsMethod";
            this.cbEnterResultsMethod.Size = new System.Drawing.Size(297, 17);
            this.cbEnterResultsMethod.TabIndex = 4;
            this.cbEnterResultsMethod.Text = "Number of tricks won/lost compared to the contract +/-/=";
            this.cbEnterResultsMethod.UseVisualStyleBackColor = true;
            // 
            // cbShowPairNumbers
            // 
            this.cbShowPairNumbers.AutoSize = true;
            this.cbShowPairNumbers.Checked = true;
            this.cbShowPairNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowPairNumbers.Location = new System.Drawing.Point(13, 129);
            this.cbShowPairNumbers.Name = "cbShowPairNumbers";
            this.cbShowPairNumbers.Size = new System.Drawing.Size(264, 17);
            this.cbShowPairNumbers.TabIndex = 5;
            this.cbShowPairNumbers.Text = "Show pair numbers in the round information screen";
            this.cbShowPairNumbers.UseVisualStyleBackColor = true;
            // 
            // udAutopoweroffTime
            // 
            this.udAutopoweroffTime.Location = new System.Drawing.Point(157, 152);
            this.udAutopoweroffTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udAutopoweroffTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.udAutopoweroffTime.Name = "udAutopoweroffTime";
            this.udAutopoweroffTime.Size = new System.Drawing.Size(38, 20);
            this.udAutopoweroffTime.TabIndex = 6;
            this.udAutopoweroffTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // udVerificationTime
            // 
            this.udVerificationTime.Location = new System.Drawing.Point(226, 178);
            this.udVerificationTime.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.udVerificationTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udVerificationTime.Name = "udVerificationTime";
            this.udVerificationTime.Size = new System.Drawing.Size(37, 20);
            this.udVerificationTime.TabIndex = 7;
            this.udVerificationTime.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // cbLeadCard
            // 
            this.cbLeadCard.AutoSize = true;
            this.cbLeadCard.Checked = true;
            this.cbLeadCard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLeadCard.Location = new System.Drawing.Point(12, 204);
            this.cbLeadCard.Name = "cbLeadCard";
            this.cbLeadCard.Size = new System.Drawing.Size(95, 17);
            this.cbLeadCard.TabIndex = 8;
            this.cbLeadCard.Text = "Enter leadcard";
            this.cbLeadCard.UseVisualStyleBackColor = true;
            // 
            // cbMemberNumbers
            // 
            this.cbMemberNumbers.AutoSize = true;
            this.cbMemberNumbers.Location = new System.Drawing.Point(12, 227);
            this.cbMemberNumbers.Name = "cbMemberNumbers";
            this.cbMemberNumbers.Size = new System.Drawing.Size(134, 17);
            this.cbMemberNumbers.TabIndex = 9;
            this.cbMemberNumbers.Text = "Enter member numbers";
            this.cbMemberNumbers.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Set the autopower-off time to                seconds";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Time that the verification message is shown                seconds";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(272, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 12;
            this.btnRun.Text = "Run BMPro";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(272, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(272, 70);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 14;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // fBMSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 254);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.udAutopoweroffTime);
            this.Controls.Add(this.udVerificationTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbMemberNumbers);
            this.Controls.Add(this.cbLeadCard);
            this.Controls.Add(this.cbShowPairNumbers);
            this.Controls.Add(this.cbEnterResultsMethod);
            this.Controls.Add(this.cbScorePoints);
            this.Controls.Add(this.cbRepeatResults);
            this.Controls.Add(this.cbShowPercentage);
            this.Controls.Add(this.cbShowResults);
            this.Name = "fBMSettings";
            this.Text = "Bridgemate settings";
            ((System.ComponentModel.ISupportInitialize)(this.udAutopoweroffTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udVerificationTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbShowResults;
        private System.Windows.Forms.CheckBox cbShowPercentage;
        private System.Windows.Forms.CheckBox cbRepeatResults;
        private System.Windows.Forms.CheckBox cbScorePoints;
        private System.Windows.Forms.CheckBox cbEnterResultsMethod;
        private System.Windows.Forms.CheckBox cbShowPairNumbers;
        private System.Windows.Forms.NumericUpDown udAutopoweroffTime;
        private System.Windows.Forms.NumericUpDown udVerificationTime;
        private System.Windows.Forms.CheckBox cbLeadCard;
        private System.Windows.Forms.CheckBox cbMemberNumbers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;

    }
}