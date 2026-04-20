namespace GameCatalogue
{
    partial class frmNewProfile
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
            txtNewName = new TextBox();
            lblProfileName = new Label();
            btnAccept = new Button();
            btnCancel = new Button();
            // 
            // txtNewName
            // 
            txtNewName.Location = new Point(117, 18);
            txtNewName.Name = "txtNewName";
            txtNewName.PlaceholderText = "Enter Name...";
            txtNewName.Size = new Size(125, 27);
            txtNewName.TabIndex = 0;
            // 
            // lblProfileName
            // 
            lblProfileName.AutoSize = true;
            lblProfileName.Location = new Point(12, 21);
            lblProfileName.Name = "lblProfileName";
            lblProfileName.Size = new Size(99, 20);
            lblProfileName.TabIndex = 1;
            lblProfileName.Text = "Profile Name:";
            // 
            // btnAccept
            // 
            btnAccept.Location = new Point(17, 62);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(94, 29);
            btnAccept.TabIndex = 2;
            btnAccept.Text = "Accept";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += btnAccept_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(148, 62);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += this.btnCancel_Click;
            // 
            // frmNewProfile
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(266, 105);
            Controls.Add(btnCancel);
            Controls.Add(btnAccept);
            Controls.Add(lblProfileName);
            Controls.Add(txtNewName);
            Name = "frmNewProfile";
            Text = "New Profile";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNewName;
        private Label lblProfileName;
        private Button btnAccept;
        private Button btnCancel;
    }
}