using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mailLibrary
{
    public class MailTo
    {

        private List<string> mailToList = new List<string>();
        private List<string> mailToListName = new List<string>();
        //private string toMail = "";
        //public string TOMAIL { get { return toMail; } private set { toMail = value; } }
        //private string toName = "";
        //public string TONAME { get { return toName; } private set { toName = value; } }

        public string ADDMAILTOLIST { set { mailToList.Add(value); } }
        public string ADDMAILTOLISTNAME { set { mailToListName.Add(value); } }

        public MailTo()
        {
            mailToList.Clear();
        }

        public MailTo(string toMail)
        {
            ADDMAILTOLIST = toMail;
        }

        public MailTo(string toMail, string toName)
        {
            ADDMAILTOLIST = toMail;
            ADDMAILTOLISTNAME = toName;
        }

        public int GetMailToListCount()
        {
            return mailToList.Count;
        }

        public string GetMailToListItem(int index)
        {
            return mailToList[index].ToString();
        }

    }
}
