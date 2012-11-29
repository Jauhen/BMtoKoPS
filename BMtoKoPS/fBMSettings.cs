using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BMtoKOPS
{
    public partial class fBMSettings : Form
    {
        private bool isMax;
        public Dictionary<string, object> options;

        public fBMSettings(bool isMax)
        {
            this.isMax = isMax;
            InitializeComponent();
            cbShowPercentage.Checked = this.isMax;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("EnterResultsMethod", cbEnterResultsMethod.Checked);
            dic.Add("LeadCard", cbLeadCard.Checked);
            dic.Add("MemberNumbers", cbMemberNumbers.Checked);
            dic.Add("RepeatResults", cbRepeatResults.Checked);
            dic.Add("ScorePoints", cbScorePoints.Checked);
            dic.Add("ShowPairNumbers", cbShowPairNumbers.Checked);
            dic.Add("ShowPercentage", cbShowPercentage.Checked);
            dic.Add("ShowResults", cbShowResults.Checked);
            dic.Add("AutopoweroffTime", udAutopoweroffTime.Value);
            dic.Add("VerificationTime", udVerificationTime.Value);

            options = dic;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cbEnterResultsMethod.Checked = true;
            cbLeadCard.Checked = true;
            cbMemberNumbers.Checked = false;
            cbRepeatResults.Checked = false;
            cbScorePoints.Checked = true;
            cbShowPairNumbers.Checked = true;
            cbShowPercentage.Checked = this.isMax;
            cbShowResults.Checked = true;
            udAutopoweroffTime.Value = 10;
            udVerificationTime.Value = 2;

            cbShowResults.Focus();
        }
    }
}
