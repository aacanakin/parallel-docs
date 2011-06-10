using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;

namespace ParallelDocs.Logic
{
	[Serializable()]

    class Document : ISerializable
    {
        private string path_name;
        private string doc_content; // make it arraylist
        private string owner;
        private List<Profile> editors;
		private bool shared;
        private string name;
        private bool isSaved;
		private Font docFont;

        public Document() {

            doc_content = "";          
            editors = new List<Profile>();
            owner = null;
            name = "untitled.txt";
            isSaved = false;
			shared = false;
			docFont = null;
        }

        public void changeDocContent(int start, int end, string content) {

            // It is very useful in wireless. Optimize this.
            //string contentToBeChanged = doc_content.Substring(start, end).ToCharArray;
        }

        // Get & Set Methods
		public void setFont(Font docFont) { this.docFont = docFont; }
		public Font getFont() { return docFont; }
		public void setShared(bool set) { shared = set; }
		public bool getShared() { return shared; }
        public bool getIsSaved() { return isSaved; }
        public string getDocContent() { return doc_content; }
        public string getOwner() { return owner; }
        public List<Profile> getEditors() { return editors; }
        public string getDocName() { return name; }
        public string getPathName() { return path_name; }

        public void setIsSaved(bool isSaved) { this.isSaved = isSaved; }
        public void setDocContent(string doc_content) { this.doc_content = doc_content; }
        public void setOwner(string owner) { this.owner = owner; }
        public void addEditor(Profile editor) 
        {
			bool exists = false;

			foreach(Profile edit in editors)
			{
				if(edit.getIp() == editor.getIp())
				{
					exists = true;
				}				
			}
			if (!exists)
			{
				editors.Add(editor);
			}
			
        }

		public void setDocName(string name) { this.name = name; }
        public void setPathName(string path_name) { this.path_name = path_name; }

		public Document(SerializationInfo info, StreamingContext ctxt)
        {
            this.path_name = (string)info.GetValue("PathName", typeof(string));
            this.doc_content = (string)info.GetValue("DocContent", typeof(string));
            this.owner = (string)info.GetValue("Owner", typeof(string));
			this.editors = (List<Profile>)info.GetValue("Editors", typeof(List<Profile>));
			this.name = (string)info.GetValue("Name", typeof(string));
			this.isSaved = (bool)info.GetValue("IsSaved", typeof(bool));
			this.shared = (bool)info.GetValue("Shared", typeof(bool));
			this.docFont = (Font)info.GetValue("Font", typeof(Font));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {			
            info.AddValue("PathName", this.path_name);
            info.AddValue("DocContent", this.doc_content);
            info.AddValue("Owner", this.owner);
			info.AddValue("Editors", this.editors);
			info.AddValue("Name", this.name);
			info.AddValue("IsSaved", this.isSaved);
			info.AddValue("Shared", this.shared);
			info.AddValue("Font", this.docFont);
        }


    }
}
