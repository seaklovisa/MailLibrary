using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace mailLibrary
{
    public class MailAttachment
    {
        List<string> mailAttsFullPathName = new List<string>();
        List<string> mailAttsFilePath = new List<string>();
        List<string> mailAttsFileName = new List<string>();
        private string filePath = "";
        private string fileName = "";

        public List<string> GetMailAttachmentListFullPathName{
            get { return mailAttsFullPathName; }
        }

        public List<string> GetMailAttsFilePath
        {
            get { return mailAttsFilePath; }
        }

        public List<string> GetMailAttsFileName
        {
            get { return mailAttsFileName; }
        }

        public MailAttachment(string filePathName)
        {
            mailAttsFullPathName.Add(filePathName);
        }

        public MailAttachment(string filePath, string fileName)
        {
            mailAttsFilePath.Add(filePath);
            mailAttsFileName.Add(fileName);
        }

        public void AddAttachment(string filePath,string fileName)
        {
            mailAttsFilePath.Add(filePath);
            mailAttsFileName.Add(fileName);
        }

        public void AddAttachment(string fullFileName)
        {
            mailAttsFullPathName.Add(fullFileName);
        }



    }
}
