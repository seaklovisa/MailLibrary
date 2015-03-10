using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mailLibrary
{
    public class MailFrom
    {
        private string fromMail = "";
        public string FROMMAIL {  get { return fromMail; } private set { fromMail = value; } }
        private string fromName = "";
        public string FROMNAME {  get { return fromName; } private set { fromName = value; } }
        public MailFrom(string fromMail, string fromName)
        {
            FROMMAIL = fromMail;
            FROMNAME = fromName;
        }

        

    }
}
