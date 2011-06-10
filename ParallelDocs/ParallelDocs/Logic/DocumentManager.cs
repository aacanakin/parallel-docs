using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ParallelDocs.Logic
{
    static class DocumentManager
    {
        private static Document currentDocument = null;

        public static void init()
        {
            Document d = new Document();
			d.setOwner(ProfileManager.getCurrentProfile().getFullName());		
            DocumentManager.setCurrentDocument(d);                   
        }

        public static Document openDocument(string path) {

            return null;
        }

        public static void saveDocument(string content, string path_name)
        {
            
        }

        public static void saveAsDocument(Document d)
        {

        }

        public static Document getCurrentDocument() { return currentDocument; }

        public static void setCurrentDocument(Document d) { currentDocument = d; }
    }
}
