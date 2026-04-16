namespace GameCatalogue
{
    partial class CatalogueWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            catalogueTable = new DataGridView();
            hdrBoxart = new DataGridViewImageColumn();
            hdrTitle = new DataGridViewTextBoxColumn();
            hdrPlatform = new DataGridViewTextBoxColumn();
            hdrRating = new DataGridViewTextBoxColumn();
            hdrReleaseDate = new DataGridViewTextBoxColumn();
            cboPlatform = new ComboBox();
            btnSearch = new Button();
            txtSearch = new TextBox();
            btnClose = new Button();
            cboResults = new ComboBox();
            btnPrev = new Button();
            btnNext = new Button();
            lblPage = new Label();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)catalogueTable).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // catalogueTable
            // 
            catalogueTable.AllowUserToAddRows = false;
            catalogueTable.AllowUserToDeleteRows = false;
            catalogueTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            catalogueTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            catalogueTable.Columns.AddRange(new DataGridViewColumn[] { hdrBoxart, hdrTitle, hdrPlatform, hdrRating, hdrReleaseDate });
            catalogueTable.Location = new Point(0, 44);
            catalogueTable.Name = "catalogueTable";
            catalogueTable.RowHeadersWidth = 51;
            catalogueTable.Size = new Size(927, 403);
            catalogueTable.TabIndex = 0;
            // 
            // hdrBoxart
            // 
            hdrBoxart.DataPropertyName = "ArtImage";
            hdrBoxart.HeaderText = "Box Art";
            hdrBoxart.MinimumWidth = 6;
            hdrBoxart.Name = "hdrBoxart";
            hdrBoxart.ReadOnly = true;
            hdrBoxart.Resizable = DataGridViewTriState.True;
            hdrBoxart.SortMode = DataGridViewColumnSortMode.Automatic;
            hdrBoxart.Width = 125;
            // 
            // hdrTitle
            // 
            hdrTitle.DataPropertyName = "Title";
            hdrTitle.HeaderText = "Title";
            hdrTitle.MinimumWidth = 6;
            hdrTitle.Name = "hdrTitle";
            hdrTitle.ReadOnly = true;
            hdrTitle.Width = 125;
            // 
            // hdrPlatform
            // 
            hdrPlatform.DataPropertyName = "Platform";
            hdrPlatform.HeaderText = "Platform";
            hdrPlatform.MinimumWidth = 6;
            hdrPlatform.Name = "hdrPlatform";
            hdrPlatform.Width = 125;
            // 
            // hdrRating
            // 
            hdrRating.DataPropertyName = "Rating";
            hdrRating.HeaderText = "Rating";
            hdrRating.MinimumWidth = 6;
            hdrRating.Name = "hdrRating";
            hdrRating.ReadOnly = true;
            hdrRating.Width = 125;
            // 
            // hdrReleaseDate
            // 
            hdrReleaseDate.DataPropertyName = "ReleaseDate";
            hdrReleaseDate.HeaderText = "Release Date";
            hdrReleaseDate.MinimumWidth = 6;
            hdrReleaseDate.Name = "hdrReleaseDate";
            hdrReleaseDate.ReadOnly = true;
            hdrReleaseDate.Width = 125;
            // 
            // cboPlatform
            // 
            cboPlatform.FormattingEnabled = true;
            cboPlatform.Location = new Point(169, 9);
            cboPlatform.Name = "cboPlatform";
            cboPlatform.Size = new Size(151, 28);
            cboPlatform.TabIndex = 1;
            cboPlatform.Text = "Platform";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(326, 8);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(10, 9);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by title";
            txtSearch.Size = new Size(153, 27);
            txtSearch.TabIndex = 3;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(828, 6);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // cboResults
            // 
            cboResults.FormattingEnabled = true;
            cboResults.Location = new Point(426, 8);
            cboResults.Name = "cboResults";
            cboResults.Size = new Size(60, 28);
            cboResults.TabIndex = 5;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(532, 7);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(94, 29);
            btnPrev.TabIndex = 6;
            btnPrev.Text = "<<< Prev";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(688, 6);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(94, 29);
            btnNext.TabIndex = 7;
            btnNext.Text = "Next>>>";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // lblPage
            // 
            lblPage.AutoSize = true;
            lblPage.Location = new Point(632, 10);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(44, 20);
            lblPage.TabIndex = 8;
            lblPage.Text = "Page:";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(lblPage);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnNext);
            panel1.Controls.Add(cboPlatform);
            panel1.Controls.Add(btnPrev);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(cboResults);
            panel1.Controls.Add(btnClose);
            panel1.Location = new Point(2, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(925, 44);
            panel1.TabIndex = 9;
            // 
            // CatalogueWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(927, 447);
            Controls.Add(catalogueTable);
            Controls.Add(panel1);
            Name = "CatalogueWindow";
            Text = "Game Catalogue";
            ((System.ComponentModel.ISupportInitialize)catalogueTable).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView catalogueTable;
        private ComboBox cboPlatform;
        private Button btnSearch;
        private TextBox txtSearch;
        private Button btnClose;
        private DataGridViewImageColumn hdrBoxart;
        private DataGridViewTextBoxColumn hdrTitle;
        private DataGridViewTextBoxColumn hdrPlatform;
        private DataGridViewTextBoxColumn hdrRating;
        private DataGridViewTextBoxColumn hdrReleaseDate;
        private ComboBox cboResults;
        private Button btnPrev;
        private Button btnNext;
        private Label lblPage;
        private Panel panel1;
    }
}
