using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Library.Common
{
    public class Eform
    {

        private string[] control = { "TextBox", "RadioButton", "CheckBox", "TextArea" }; 
        #region CommonFunction
        /// <summary>
        /// Insert colection control id and value on form into database,if not exits then addnew else then update
        /// Rule to define ControlID is:
        /// 1.If control is Checkbox then start with "CheckBox" letter and after is number(ex:CheckBox1).
        /// 2.If control is Textbox then start with "TextBox" letter and after is number(ex:TextBox1).
        /// ....
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public Message SaveControlToDB(FormCollection form, bool isInsert)
        {

            Message msg = null;
            List<EForm_Detail> detailList = new List<EForm_Detail>();
            string EformID = form.Get("Hidden1");
            
            for (int i = 0; i < form.Count; i++)
            {                
                string controlID = form.GetKey(i);
                bool flag = false;
                foreach (String str in control)
                {
                    if (controlID.StartsWith(str))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    CRM.Models.EForm_Detail detailItem = new Models.EForm_Detail();
                    string value = form.Get(controlID);
                    if (controlID.StartsWith("CheckBox"))
                    {
                        value = value.Split(Convert.ToChar(","))[0];
                    }
                    detailItem.ControlID = controlID;
                    detailItem.Value = value;
                    detailItem.EFormID = Int32.Parse(EformID);
                    detailList.Add(detailItem);

                }
            }
            CRM.Models.EformDao efDao = new EformDao();
            msg = isInsert == true ? efDao.Inser(detailList) : efDao.Update(detailList);

            return msg;
        }


        public Message SaveControlToDB(FormCollection form, PerformanceReview pr, bool isInsert)
        {

            Message msg = null;
            List<EForm_Detail> detailList = new List<EForm_Detail>();
            string EformID = form.Get("Hidden1");

            for (int i = 0; i < form.Count; i++)
            {
                string controlID = form.GetKey(i);
                bool flag = false;
                foreach (String str in control)
                {
                    if (controlID.StartsWith(str))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    CRM.Models.EForm_Detail detailItem = new Models.EForm_Detail();
                    string value = form.Get(controlID);
                    if (controlID.StartsWith("CheckBox"))
                    {
                        value = value.Split(Convert.ToChar(","))[0];
                    }
                    detailItem.ControlID = controlID;
                    detailItem.Value = value;
                    detailItem.EFormID = Int32.Parse(EformID);
                    detailList.Add(detailItem);

                }
            }
            CRM.Models.EformDao efDao = new EformDao();
            msg = isInsert == true ? efDao.Inser(detailList, pr) : efDao.Update(detailList, pr);

            return msg;
        }

        public Message SaveControlToDB(FormCollection form, PerformanceReview pr, PRComment comment, bool isInsert)
        {

            Message msg = null;
            List<EForm_Detail> detailList = new List<EForm_Detail>();
            string EformID = form.Get("Hidden1");

            for (int i = 0; i < form.Count; i++)
            {
                string controlID = form.GetKey(i);
                bool flag = false;
                foreach (String str in control)
                {
                    if (controlID.StartsWith(str))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    CRM.Models.EForm_Detail detailItem = new Models.EForm_Detail();
                    string value = form.Get(controlID);
                    if (controlID.StartsWith("CheckBox"))
                    {
                        value = value.Split(Convert.ToChar(","))[0];
                    }
                    detailItem.ControlID = controlID;
                    detailItem.Value = value;
                    detailItem.EFormID = Int32.Parse(EformID);
                    detailList.Add(detailItem);

                }
            }
            CRM.Models.EformDao efDao = new EformDao();
            msg = isInsert == true ? efDao.Inser(detailList, pr, comment) : efDao.Update(detailList, pr, comment);

            return msg;
        }

        #endregion
    }
}