using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ParallelDocs.GUI;
using System.IO;

namespace ParallelDocs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
                      
            GUIManager.startUI();
        }

        public static bool first_run()
        {
            if( File.Exists("profile.dat") )
            {
                return false;
            }

            else
            {
                return true;            
            }

            
        }
    }
}
