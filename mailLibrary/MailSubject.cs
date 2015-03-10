using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mailLibrary
{
    public class MailSubject
    {
        private string subject = "無標題信件";
        public string SETSUBJECT { get { return subject; }  set { subject = value; } }
        public MailSubject(string subject)
        {
            SETSUBJECT = subject;
        }

        
    }
}
