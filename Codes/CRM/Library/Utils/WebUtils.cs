using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;

namespace CRM.Library.Utils
{
    public class WebUtils 
    {
        public static bool SendMail(string Host, String Port, string FromEmail, string FromName, string ToEmail, string ccEmail, string Subject, string Body)
        {
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(FromEmail, FromName);
            if (ToEmail.Contains(";"))
            {
                string[] toEmailAddress = ToEmail.Split(';');
                foreach (string address in toEmailAddress)
                {
                    if (!string.IsNullOrEmpty(address.Trim()))
                        Msg.To.Add(new MailAddress(address.Trim()));
                }
            }
            else
            {
                Msg.To.Add(new MailAddress(ToEmail.Trim()));
            }

            if (ccEmail.Contains(";"))
            {
                string[] ccEmailAddress = ccEmail.Split(';');
                foreach (string address in ccEmailAddress)
                {
                    if (!string.IsNullOrEmpty(address.Trim()))
                        Msg.CC.Add(new MailAddress(address.Trim()));
                }
            }           

            Msg.IsBodyHtml = true;
            Msg.Subject = Subject;
            Msg.Body = Body;
            SmtpClient objMail = new SmtpClient(Host, Convert.ToInt32(Port));
            try
            {
                objMail.Send(Msg);
                return true;
            }
            catch (SmtpException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool SendMail(string Host, String Port, string FromEmail, string FromName, string ToEmail, string ccEmail, string bccEmail, string Subject, string Body)
        {
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(FromEmail, FromName);
            if (ToEmail.Contains(";"))
            {
                string[] toEmailAddress = ToEmail.Split(';');
                foreach (string address in toEmailAddress)
                {
                    if (!string.IsNullOrEmpty(address.Trim()))
                        Msg.To.Add(new MailAddress(address.Trim()));
                }
            }
            else
            {
                Msg.To.Add(new MailAddress(ToEmail.Trim()));
            }

            if(!String.IsNullOrEmpty(bccEmail))
            {
                if (bccEmail.Contains(";"))
                {
                    string[] bccEmailAddress = bccEmail.Split(';');
                    foreach (string address in bccEmailAddress)
                    {
                        if (!string.IsNullOrEmpty(address.Trim()))
                            Msg.Bcc.Add(new MailAddress(address.Trim()));
                    }
                }
                else
                    Msg.Bcc.Add(new MailAddress(bccEmail.Trim()));
            }

            if (!String.IsNullOrEmpty(ccEmail))
            {
                if (ccEmail.Contains(";"))
                {
                    string[] ccEmailAddress = ccEmail.Split(';');
                    foreach (string address in ccEmailAddress)
                    {
                        if (!string.IsNullOrEmpty(address.Trim()))
                            Msg.CC.Add(new MailAddress(address.Trim()));
                    }
                }
                else
                    Msg.CC.Add(new MailAddress(bccEmail.Trim()));
            }

            Msg.IsBodyHtml = true;
            Msg.Subject = Subject;
            Msg.Body = Body;
            SmtpClient objMail = new SmtpClient(Host, Convert.ToInt32(Port));
            try
            {
                objMail.Send(Msg);
                return true;
            }
            catch (SmtpException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ReadFile(string strPath)
        {
            string result = "";

            if (!string.IsNullOrEmpty(strPath))
            {
                if (File.Exists(strPath))
                {
                    TextReader tr = new StreamReader(strPath, System.Text.Encoding.UTF8);
                    result = tr.ReadToEnd();
                    tr.Close();
                }
                else
                {
                    return "Error Message: Can not found file " + strPath;
                }
            }

            return result;
        }
    }

}