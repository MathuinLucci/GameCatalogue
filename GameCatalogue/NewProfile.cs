using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameCatalogue
{
    public partial class frmNewProfile : Form
    {
        public string ProfileName { get; private set; } = "";

        public frmNewProfile()
        {
            InitializeComponent();
            this.AcceptButton = btnAccept;
            this.CancelButton = btnCancel;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            ProfileName = txtNewName.Text.Trim();
            if (ProfileName.Length == 0)
            {
                MessageBox.Show("Please enter a profile name.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

}