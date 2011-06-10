namespace ParallelDocs.GUI
{
    partial class FirstRunScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstRunScreen));
            this.label1 = new System.Windows.Forms.Label();
            this.firstScreenInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.profileEmailTextBox = new System.Windows.Forms.TextBox();
            this.profileNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.createProfileButton = new System.Windows.Forms.Button();
            this.firstScreenInfoGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // firstScreenInfoGroupBox
            // 
            this.firstScreenInfoGroupBox.Controls.Add(this.label1);
            this.firstScreenInfoGroupBox.Location = new System.Drawing.Point(11, 13);
            this.firstScreenInfoGroupBox.Name = "firstScreenInfoGroupBox";
            this.firstScreenInfoGroupBox.Size = new System.Drawing.Size(274, 99);
            this.firstScreenInfoGroupBox.TabIndex = 1;
            this.firstScreenInfoGroupBox.TabStop = false;
            this.firstScreenInfoGroupBox.Text = "Info";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.profileEmailTextBox);
            this.groupBox1.Controls.Add(this.profileNameTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 132);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profile";
            // 
            // profileEmailTextBox
            // 
            this.profileEmailTextBox.Location = new System.Drawing.Point(87, 46);
            this.profileEmailTextBox.Name = "profileEmailTextBox";
            this.profileEmailTextBox.Size = new System.Drawing.Size(170, 20);
            this.profileEmailTextBox.TabIndex = 3;
            // 
            // profileNameTextBox
            // 
            this.profileNameTextBox.Location = new System.Drawing.Point(87, 20);
            this.profileNameTextBox.Name = "profileNameTextBox";
            this.profileNameTextBox.Size = new System.Drawing.Size(170, 20);
            this.profileNameTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "E - mail :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Profile Name :";
            // 
            // createProfileButton
            // 
            this.createProfileButton.Location = new System.Drawing.Point(94, 211);
            this.createProfileButton.Name = "createProfileButton";
            this.createProfileButton.Size = new System.Drawing.Size(108, 23);
            this.createProfileButton.TabIndex = 3;
            this.createProfileButton.Text = "Create Profile";
            this.createProfileButton.UseVisualStyleBackColor = true;
            this.createProfileButton.Click += new System.EventHandler(this.createProfileButton_Click);
            // 
            // FirstRunScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 262);
            this.Controls.Add(this.createProfileButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.firstScreenInfoGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FirstRunScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parallel Docs - First Run";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.firstRunScreen_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FirstRunScreen_FormClosed);
            this.Load += new System.EventHandler(this.firstRunScreen_Load);
            this.firstScreenInfoGroupBox.ResumeLayout(false);
            this.firstScreenInfoGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox firstScreenInfoGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox profileEmailTextBox;
        private System.Windows.Forms.TextBox profileNameTextBox;
        private System.Windows.Forms.Button createProfileButton;



    }
}