using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.IO;
using System.Collections;

namespace mailLibrary
{
    public class Mail
    {
        public void sendMail(MailFrom from ,MailTo to,MailSubject subject,MailServer mailServer,MailBody bodyContent
            ,MailAttachment atch = null, string logLocation = "MailLog")
        {
            string logPath = "";
            string fileName = "";
            logPath = System.AppDomain.CurrentDomain.BaseDirectory + logLocation + "\\";
            fileName = System.DateTime.Now.ToString("yyyyMMdd") + "_mail.txt";
            string path = logPath + fileName;
            //檢查有沒有發信log資料夾
            if ( !Directory.Exists(logPath)){
                throw new DirectoryNotFoundException("找不到" + logLocation + " Log目錄:" + logPath);
            }
            //throw new DirectoryNotFoundException(System.AppDomain.CurrentDomain.BaseDirectory);
            // writeLog("=======寄信開始", path);

            int seq = 0;
            string mailBody = null;
            //'set mail images
            //string imgPath = System.IO.Directory.GetCurrentDirectory().ToString();
            string imgPath = System.AppDomain.CurrentDomain.BaseDirectory;
            /*
            StreamReader reader = new StreamReader(imgPath + "/images/MailTemplate.html");
            string htmlStr = reader.ReadToEnd();
            */
            string body = bodyContent.GetBodyContent();
            /*
            string body = "";
            body += "<table><tr><td>親愛的先生/小姐您好! </td></tr>";
            body += "<tr><td>關於您所問的問題回復如下：</td></tr>";
            body += "<tr><td>案件編號： </td></tr>";
            body += "<tr><td>案件主旨：</td></tr>";
            body += "<tr><td>案件內容：</td></tr>";
            body += "<tr><td>主辦單位：</td></tr>";
            body += "<tr><td>回復日期：</td></tr>";
            body += "<tr><td>處理情形：<br /></td></tr>";
            */
            try
            {
                seq += 1;
                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                MailAddress mailFrom = new MailAddress(from.FROMMAIL, from.FROMNAME);
                //MailAddress mailTo = new MailAddress(to.TOMAIL,to.TONAME);          

                //MailAddress mailFrom = new MailAddress(from.FROMNAME + "<" + from.FROMMAIL + ">");
                //MailAddress mailTo = new MailAddress("" + to.TONAME + "<" + to.TOMAIL + ">");
                //msg.BodyEncoding = Encoding.UTF8;
                //msg.SubjectEncoding = Encoding.UTF8;
                
                msg.From = mailFrom;

                for (int i = 0; i < to.GetMailToListCount(); i++)
                {
                    msg.To.Add(to.GetMailToListItem(i));
                }

                msg.Subject = subject.SETSUBJECT;
                msg.IsBodyHtml = true;

                if (atch != null)
                {
                    for (int i = 0; i < atch.GetMailAttsFilePath.Count; i++)
                    {
                        //System.Net.Mail.Attachment att = new Attachment(atch.GetMailAttachmentList[i]);
                        //Console.WriteLine(atch.GetMailAttachmentList[i]);
                        //att.NameEncoding = Encoding.UTF8;
                        //msg.Attachments.Add(att);
                       // msg.Attachments.Add(new Attachment(atch.GetMailAttsFileName[i], MediaTypeNames.Application.Octet));

                        msg.Attachments.Add(new Attachment(new FileStream(atch.GetMailAttsFilePath[i] + atch.GetMailAttsFileName[i], FileMode.Open, FileAccess.Read), atch.GetMailAttsFileName[i], MediaTypeNames.Application.Octet));
                        //msg.Attachments.Add(new Attachment(new FileStream(atch.GetMailAttsFilePath[i] + atch.GetMailAttsFileName[i], FileMode.Open, FileAccess.Read),"行政院環境保護署未來兩週會議清單(總表).pdf", MediaTypeNames.Application.Pdf));
                       
                    }

                    foreach (var aitt in msg.Attachments)
                        aitt.NameEncoding = Encoding.UTF8;
                }
                
                mailBody = body;
                /*
                System.Net.Mail.LinkedResource imgUp = new System.Net.Mail.LinkedResource(imgPath + "/images/up.gif", "images/gif");
                imgUp.ContentId = "up";
                System.Net.Mail.LinkedResource imgHead = new System.Net.Mail.LinkedResource(imgPath + "//images/head.jpg", "images/jpg");
                imgHead.ContentId = "head";
                System.Net.Mail.LinkedResource imgIntra = new System.Net.Mail.LinkedResource(imgPath + "/images/intra.jpg", "images/jpg");
                imgIntra.ContentId = "intra";
                System.Net.Mail.LinkedResource imgDown = new System.Net.Mail.LinkedResource(imgPath + "/images/down.gif", "images/gif");
                imgDown.ContentId = "down";
                */
                //Dim av As AlternateView = AlternateView.CreateAlternateViewFromString(mailBody.Replace("{USER}", "小蘋果"), Encoding.UTF8, Mime.MediaTypeNames.Text.Html)
                AlternateView av = AlternateView.CreateAlternateViewFromString(mailBody.Replace("{USER}", from.FROMNAME), Encoding.UTF8, MediaTypeNames.Text.Html);
                /*
                av.LinkedResources.Add(imgUp);
                av.LinkedResources.Add(imgHead);
                av.LinkedResources.Add(imgIntra);
                av.LinkedResources.Add(imgDown);
                */
                msg.AlternateViews.Add(av);
                // ''msg.Subject = row.Item("PwdLastSet") & "--" & row.Item("Name") & " 您的intra密碼已超過3個月未修改"
                // ''msg.Body = row.Item("Mail").ToString & " 您好~您的AD帳號已三個月未修改,請儘速修改,謝謝"
                smtp.Host = mailServer.SERVER;
                smtp.Port = mailServer.PORT;
                //smtp.Port = 25;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(mailServer.ACCOUNT, mailServer.PWD);
                /*
                if (seq < 10)
                {
                    smtp.Send(msg);
                }
                 */
                smtp.Send(msg);
                
                
                msg.Dispose();
                //writeLog(" " + sequence + " - 案號:" + seril + " 主旨:" + subject + "  時間:" + System.DateTime.Now + "    ", path);
                //writeLog(" " + sequence + " - 案號:" + seril + " 主旨:" + subject + "  時間:" + System.DateTime.Now + "    ", path);
            }
            catch (Exception ex)
            {
                throw ex;
                //txtCont = "ErrorMail  Name：" + name + "   錯誤訊息：" + ex.Message.ToString();
                //writeLog(txtCont, path);

                //writeLog(" " + sequence + " - 案號:" + seril + " 主旨:" + subject + "  時間:" + System.DateTime.Now + " ErrorMail  Name： " + ex.Message.ToString() + "    ", path);
            }

            //writeLog("=======寄信結束", path);
        }
    }
}
