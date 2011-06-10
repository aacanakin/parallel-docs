namespace ParallelDocs.GUI
{
    partial class ShareScreen
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
            this.shareButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ipDataGridView = new System.Windows.Forms.DataGridView();
            this.SelectColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.profileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ipColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shareRefreshButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ipDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // shareButton
            // 
            this.shareButton.Location = new System.Drawing.Point(114, 222);
            this.shareButton.Name = "shareButton";
            this.shareButton.Size = new System.Drawing.Size(75, 23);
            this.shareButton.TabIndex = 0;
            this.shareButton.Text = "Share";
            this.shareButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ipDataGridView);
            this.groupBox1.Controls.Add(this.shareRefreshButton);
            this.groupBox1.Controls.Add(this.shareButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 281);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peers";
            // 
            // ipDataGridView
            // 
            this.ipDataGridView.AllowUserToAddRows = false;
            this.ipDataGridView.AllowUserToDeleteRows = false;
            this.ipDataGridView.AllowUserToResizeRows = false;
            this.ipDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.ipDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ipDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectColumn,
            this.profileColumn,
            this.pcColumn,
            this.ipColumn});
            this.ipDataGridView.Location = new System.Drawing.Point(6, 19);
            this.ipDataGridView.MultiSelect = false;
            this.ipDataGridView.Name = "ipDataGridView";
            this.ipDataGridView.RowHeadersVisible = false;
            this.ipDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ipDataGridView.Size = new System.Drawing.Size(367, 197);
            this.ipDataGridView.TabIndex = 2;
            this.ipDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ipDataGridView_CellContentClick);
            // 
            // SelectColumn
            // 
            this.SelectColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SelectColumn.HeaderText = "Select";
            this.SelectColumn.Name = "SelectColumn";
            this.SelectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SelectColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SelectColumn.Width = 62;
            // 
            // profileColumn
            // 
            this.profileColumn.HeaderText = "Profile";
            this.profileColumn.Name = "profileColumn";
            // 
            // pcColumn
            // 
            this.pcColumn.HeaderText = "PC NAME";
            this.pcColumn.Name = "pcColumn";
            // 
            // ipColumn
            // 
            this.ipColumn.HeaderText = "IP";
            this.ipColumn.Name = "ipColumn";
            // 
            // shareRefreshButton
            // 
            this.shareRefreshButton.Location = new System.Drawing.Point(195, 222);
            this.shareRefreshButton.Name = "shareRefreshButton";
            this.shareRefreshButton.Size = new System.Drawing.Size(75, 23);
            this.shareRefreshButton.TabIndex = 1;
            this.shareRefreshButton.Text = "Refresh";
            this.shareRefreshButton.UseVisualStyleBackColor = true;
            this.shareRefreshButton.Click += new System.EventHandler(this.shareRefreshButton_Click);
            // 
            // ShareScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 299);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ShareScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Share Document";
            this.Load += new System.EventHandler(this.ShareScreen_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ipDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button shareButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button shareRefreshButton;
        private System.Windows.Forms.DataGridView ipDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn profileColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ipColumn;

        public System.EventHandler button1_Click { get; set; }
    }
}