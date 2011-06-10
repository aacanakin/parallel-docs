using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ParallelDocs.Logic;
using System.Net;

namespace ParallelDocs.GUI
{
    public partial class ShareScreen : Form
    {
        public ShareScreen()
        {
            InitializeComponent();
        }

       

        private void shareRefreshButton_Click(object sender, EventArgs e)
        {
            ShareScreen_Load(sender,e);
        }

        private void ShareScreen_Load(object sender, EventArgs e)
        {

            try
            {
                IPAddress[] ips = ConnectionManager.getAllUnicastAddresses();
                ipDataGridView.Rows.Clear();

                foreach (IPAddress ip in ips)
                {
                    ipDataGridView.Rows.Add(null, "", "", ip.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error );          
               
            }
            
        }

        private void ipDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


               
    }
}
