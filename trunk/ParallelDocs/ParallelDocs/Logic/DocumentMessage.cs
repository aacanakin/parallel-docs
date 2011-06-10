using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParallelDocs.Logic
{
    class DocumentMessage // atomic document message that will be used in wireless send & receive
    {
        private string documentMessage;
        private int start;
        private int end;
        private int lineNumber;

        public DocumentMessage()
        {
            documentMessage = "";
        } 

        public DocumentMessage(string documentMessage, int start, int end, int lineNumber)
        {
            this.documentMessage = documentMessage;
            this.start = start;
            this.end = end;
            this.lineNumber = lineNumber;
        }

    }
}
