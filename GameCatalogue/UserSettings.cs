using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameCatalogue
{
    public partial class frmUserSettings : Form
    {
        private readonly DatabaseManager db;

        private void CreateNewProfile()
        {
            // Open the popup
            var popup = new frmNewProfile();
            if (popup.ShowDialog() != DialogResult.OK)
                return;

            string newName = popup.ProfileName;

            // Create profile in DB
            int newProfileId = db.CreateProfile(newName);

            // Save default settings for this profile
            db.SaveUserSettings(newProfileId, newName, "Title", true);

            // Return this profile to the main window
            SelectedProfileId = newProfileId;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadExistingProfile()
        {
            var profiles = db.LoadProfiles();
            var selected = profiles[cbxProfiles.SelectedIndex - 1];

            SelectedProfileId = selected.id;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadProfiles()
        {
            cbxProfiles.Items.Clear();
            cbxProfiles.Items.Add("New Profile...");

            var profiles = db.LoadProfiles();

            foreach (var p in profiles)
                cbxProfiles.Items.Add(p.name);

            // If a profile is already active, select it
            if (currentProfileId != -1)
            {
                var profilesList = db.LoadProfiles();
                int index = profilesList.FindIndex(p => p.id == currentProfileId);
                if (index >= 0)
                    cbxProfiles.SelectedIndex = index + 1;
            }
            else
            {
                cbxProfiles.SelectedIndex = 0;
            }
        }

        public int SelectedProfileId { get; private set; }
        private int currentProfileId;

        public frmUserSettings(DatabaseManager database, int profileId)
        {
            InitializeComponent();
            this.AcceptButton = btnSelect;
            this.CancelButton = btnCancel;
            db = database;
            currentProfileId = profileId;

            LoadProfiles();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            // If "New Profile..." is selected
            if (cbxProfiles.SelectedIndex == 0)
            {
                CreateNewProfile();
                return;
            }

            // Otherwise load the selected profile
            LoadExistingProfile();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
