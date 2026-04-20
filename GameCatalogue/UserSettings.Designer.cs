namespace GameCatalogue
{
    partial class frmUserSettings
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
            label1 = new Label();
            cbxProfiles = new ComboBox();
            btnSelect = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 15);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 1;
            label1.Text = "Profile:";
            // 
            // cbxProfiles
            // 
            cbxProfiles.FormattingEnabled = true;
            cbxProfiles.Location = new Point(84, 12);
            cbxProfiles.Name = "cbxProfiles";
            cbxProfiles.Size = new Size(151, 28);
            cbxProfiles.TabIndex = 3;
            cbxProfiles.Text = "Select a profile...";
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(251, 11);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(94, 29);
            btnSelect.TabIndex = 4;
            btnSelect.Text = "Select";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(351, 11);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // frmUserSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 58);
            Controls.Add(btnCancel);
            Controls.Add(btnSelect);
            Controls.Add(cbxProfiles);
            Controls.Add(label1);
            Name = "frmUserSettings";
            Text = "User Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUserName;
        private Label label1;
        private ComboBox cbxProfiles;
        private Button btnSelect;
        private Button btnCancel;
    }
}