using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mailLibrary
{
    public class MailServer
    {
        private string server = "";
        private string account = "";
        private string pwd = "";
        private int port;

        public string SERVER { get {return server; }  private set{server = value;} }
        public string ACCOUNT { get { return account; } private set { account = value; } }
        public string PWD { get { return pwd; } private set { pwd = value; } }
        public int PORT { get { return port; } private set { port = value; } }
        public MailServer(string server,string account,string pwd,int port = 25)
        {
            SERVER = server;
            ACCOUNT = account;
            PWD = pwd;
            PORT = port;
        }
    }
}
