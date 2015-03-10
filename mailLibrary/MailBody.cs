using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.IO;

namespace mailLibrary
{
    enum mode
    {
        outerlink = 1,
        innerlink = 2
    };

    public class MailBody
    {
        private string bodyHtmlName = "";
        public string BODYHTMLNAME { get { return bodyHtmlName; } private set { bodyHtmlName = value; } }

        private string bodyFolderName = "";
        public string BODYFOLDERNAME { get { return bodyFolderName; } private set { bodyFolderName = value; } }

        private string bodyFolderPath = "";
        public string BODYFOLDERPATH { get { return bodyFolderPath; } private set { bodyFolderPath = value; } }

        private Dictionary<string, string> paramDic;

        private int bodySource = 0;

        private string bodyContent = "";
        
        private object PARAM
        {
            set
            {
                if (value is ICollection)
                {
                    if (value is Dictionary<string, string>)
                        paramDic = value as Dictionary<string, string>;

                    if (value is Hashtable)
                    {
                        Hashtable ht = value as Hashtable;
                        paramDic = new Dictionary<string, string>();

                        foreach (DictionaryEntry hash in ht)
                            paramDic.Add(hash.Key.ToString(), hash.Value.ToString());
                    }
                }
            }
        }

        public MailBody(string bodyFolderPath,string bodyFolderName, string bodyHtmlName, object param = null)
        {
            BODYFOLDERPATH = bodyFolderPath;
            BODYFOLDERNAME = bodyFolderName;
            BODYHTMLNAME = bodyHtmlName;
            PARAM = param;
            bodySource = (int)mode.outerlink;
        }

        public string GetBodyContent()
        {
            if ((mode)bodySource == mode.outerlink)
            {
                loadOuterHtmlBody();
                replaceParams();
            }

            if ((mode)bodySource == mode.innerlink)
            {

            }
            return bodyContent;
        }

        private void loadOuterHtmlBody()
        {
            string path = BODYFOLDERPATH  +  BODYFOLDERNAME;
            string contentPath = BODYFOLDERPATH +  BODYFOLDERNAME + "\\" + BODYHTMLNAME;

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("找不到郵件範本目錄：" + path);

            if (!File.Exists(contentPath))
                throw new FileNotFoundException("找不到郵件檔案：" + contentPath);

            using(StreamReader reader = new StreamReader(contentPath))
            {
                bodyContent = reader.ReadToEnd();
            }
        }

        private void replaceParams()
        {
            if (paramDic != null && paramDic.Keys.Count > 0)
            {
                foreach (KeyValuePair<string, string> de in paramDic)
                {
                    bodyContent = bodyContent.Replace(de.Key.ToString(), de.Value.ToString());
                }
            }
        }

    }
}
