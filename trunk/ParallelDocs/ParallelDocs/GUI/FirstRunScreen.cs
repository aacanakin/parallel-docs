using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ParallelDocs.Logic;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace ParallelDocs.GUI
{
    public partial class FirstRunScreen : Form
    {
        public FirstRunScreen()
        {
            InitializeComponent();
        }

        private void firstRunScreen_Load(object sender, EventArgs e)
        {

        }

        private void firstRunScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult result;
            //result = MessageBox.Show(this, "Are You Sure", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    //this.Close();
            //    Process.GetCurrentProcess().Kill();
            //}
            //else
            //{
            //    e.Cancel = true;
            //    // Do Nothing
            //}
        }

        private void createProfileButton_Click(object sender, EventArgs e)
        {
            string fullname = profileNameTextBox.Text;
            string email = profileEmailTextBox.Text;
            bool isCreated = ProfileManager.createProfile(fullname, email);

            if (!isCreated)
            {
                MessageBox.Show("Enter valid e-mail or profile name. Profile name must not include white spaces", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else {
                
                this.Hide();
            }
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        private void FirstRunScreen_FormClosed(object sender, FormClosedEventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show(this, "Are You Sure", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //this.Close();
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                //e.Cancel = true;
                // Do Nothing
            }

        }
    }
}
