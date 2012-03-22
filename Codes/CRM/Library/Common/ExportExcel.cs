using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;


namespace CRM.Library.Common
{
    public class ExportExcel
    {
        private string _fileName;
        private bool _is_render_no=true;

        private IEnumerable _list;
        private string[] _column_list= null;
        private string[] _header_excel = null;
        private string[] _colunm_text_type = null;
        private string[] _footer_excel = null;
        private TableStyle _tableStyle;
        private TableItemStyle _headerStyle;
        private TableItemStyle _itemStyle;
        private TableItemStyle _footerStyle;
        TableItemStyle _titleStyle ;
        private string _title = "";
        private List<CoupleString> _exportBy = null;
        private TableItemStyle _exportByStyle;
        
        public bool IsRenderNo
        {
             set { _is_render_no = value; }
        }
        public string Title
        {
            set { _title = value; }
        }
        public List<CoupleString> ExportBy
        {
            set { _exportBy = value; }
        }
        public string[] ColumnTextType
        {
            set { _colunm_text_type = value; }
        }
        public string[] ColumnList
        {
            set { _column_list = value; }
        }
        public string[] HeaderExcel
        {
            set { _header_excel = value; }
        }
        public string[] FooterExcel
        {
            set { _footer_excel = value; }
        }
        public string FileName
        {
            set { _fileName=value; }
        }

        public IEnumerable List
        {
            set { _list=value; }
        }

        public ExportExcel(TableItemStyle titleStyle, TableStyle tableStyle, TableItemStyle headerStyle, TableItemStyle itemStyle, TableItemStyle footerStyle)
        {
           
            _tableStyle = tableStyle;
            _headerStyle = headerStyle;
            _itemStyle = itemStyle;
            _titleStyle = titleStyle;
            _footerStyle = footerStyle;
            if (_titleStyle == null)
            {
                _titleStyle = new TableItemStyle();
                _titleStyle.ForeColor = Color.DarkBlue;
                _titleStyle.Font.Bold = true;
                _titleStyle.Font.Name = "Arial";
                _titleStyle.Font.Size = 18;


            }
            //Write export by
            //@author : tai.pham
            //@date : 2011-01-16
            if (_exportByStyle == null)
            {
                _exportByStyle = new TableItemStyle();
                _exportByStyle.ForeColor = Color.Black;
                _titleStyle.Font.Bold = false;
                _exportByStyle.CssClass = "text";
                _exportByStyle.HorizontalAlign = HorizontalAlign.Left;
                _exportByStyle.Font.Name = "Arial";
                _exportByStyle.Font.Size = 12;


            }
            // provide defaults
            if (_tableStyle == null)
            {
                _tableStyle = new TableStyle();
                _tableStyle.Font.Name = "Arial";
                _tableStyle.BorderWidth = 0;
                _tableStyle.CellPadding = 0;
                _tableStyle.CellSpacing = 0;
            }
            if (_headerStyle == null)
            {
                
                _headerStyle = new TableItemStyle();
                _headerStyle.BorderStyle = BorderStyle.Groove;
                _headerStyle.BorderColor = Color.Black;
                _headerStyle.BorderWidth = Unit.Parse(".5pt");
                _headerStyle.BackColor = Color.DarkGray;
                _headerStyle.Font.Bold = true;
                _headerStyle.Font.Size = 10;
            }
            if (_itemStyle == null)
            {
                _itemStyle = new TableItemStyle();
                _itemStyle.BorderColor = Color.Black;
                _itemStyle.BorderStyle = BorderStyle.Groove;
                _itemStyle.BorderWidth = Unit.Parse(".5pt");                
                _itemStyle.Font.Size = 9;
                             
            }
            if (_footerStyle == null)
            {

                _footerStyle = new TableItemStyle();
                _footerStyle.BorderColor = Color.Black;
                _footerStyle.BorderStyle = BorderStyle.Groove;
                _footerStyle.BorderWidth = Unit.Parse(".5pt");                               
                _footerStyle.Font.Bold = true;
                _footerStyle.Font.Size = 10;
            }
        }
        public ExportExcel():this (null, null, null, null, null)
        {
            
        }

        /// <summary>
        /// Get property value
        /// </summary>
        /// <param name="name">Property name. Ex: Candidate.FirstName</param>
        /// <param name="obj">The object content property: Ex: Interview (Intervie.Candidate.FirstName)</param>
        /// <returns></returns>
        public object GetPropValue(string name, object obj)
        {
            foreach (string part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        public void Execute()
        {
            // Create HtmlTextWriter
           
            StringWriter sw = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(sw);
           
            //Write title
            if(_title.Trim()!="")
            {
                _titleStyle.AddAttributesToRender(tw);
                tw.RenderBeginTag(HtmlTextWriterTag.Table);
                tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                tw.AddAttribute(HtmlTextWriterAttribute.Colspan, "5", false);
                tw.RenderBeginTag(HtmlTextWriterTag.Td);                                      
                tw.Write(_title);                                                                
                tw.RenderEndTag();
                tw.RenderEndTag();
                tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                tw.AddAttribute(HtmlTextWriterAttribute.Height, "24", false);
                tw.RenderBeginTag(HtmlTextWriterTag.Td);
                tw.RenderEndTag();
                tw.RenderEndTag();
                //tw.RenderEndTag();
            }

            //Write export by
            //@author : tai.pham
            //@date : 2011-01-16
            if(_exportBy != null)
            {
                foreach (var item in _exportBy)
                {
                    _exportByStyle.AddAttributesToRender(tw);
                    //tw.RenderBeginTag(HtmlTextWriterTag.Table);
                        tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                            tw.AddAttribute(HtmlTextWriterAttribute.Colspan, "3", false);
                            tw.RenderBeginTag(HtmlTextWriterTag.Td);
                            tw.Write(item.key);
                            tw.RenderEndTag();

                            tw.AddAttribute(HtmlTextWriterAttribute.Colspan, "5", false);
                            tw.RenderBeginTag(HtmlTextWriterTag.Td);
                            tw.Write(item.value);
                        tw.RenderEndTag();
                }
                // add a empty row
                _exportByStyle.AddAttributesToRender(tw);
                    tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                        tw.RenderBeginTag(HtmlTextWriterTag.Td);
                        tw.RenderEndTag();
                    tw.RenderEndTag();
                tw.RenderEndTag();
            }
            
            // Build HTML Table from Items
           
            _tableStyle.AddAttributesToRender(tw);
            tw.RenderBeginTag(HtmlTextWriterTag.Table);
      
            // Create Header Row
            tw.RenderBeginTag(HtmlTextWriterTag.Tr);
            if (_is_render_no == true)
            {
                _headerStyle.AddAttributesToRender(tw);
                tw.RenderBeginTag(HtmlTextWriterTag.Th);
                tw.Write("No");
                tw.RenderEndTag();
            }
            foreach (String header in _header_excel )
            {
              
                _headerStyle.AddAttributesToRender(tw);
                tw.RenderBeginTag(HtmlTextWriterTag.Th);
                tw.Write(header);
                tw.RenderEndTag();
            }
            tw.RenderEndTag();
            // Create Data Rows
            int i = 1;
            tw.RenderBeginTag(HtmlTextWriterTag.Tbody);
            foreach (Object row in _list)
            {
                TableItemStyle itemStyle = _itemStyle;
                tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                if (_is_render_no == true)
                {
                    itemStyle.AddAttributesToRender(tw);
                    tw.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
                    tw.RenderBeginTag(HtmlTextWriterTag.Td);

                     tw.Write(i++);
                    tw.RenderEndTag();
                }
                foreach (string header in _column_list)
                {
                    itemStyle = _itemStyle;
                    string[] headerArr = header.Split(Convert.ToChar(":")); //Split property and style

                    object obj = new object();
                    string strValue = string.Empty; //Final value export to excel

                    //Join First, Middle, Last name of properties
                    if (headerArr[0].StartsWith("GetFullName~") && headerArr[0].Split(Convert.ToChar("~")).Length > 0)
                    {
                        string properties = headerArr[0].Split('~')[1];
                        string[] listProperties = properties.Split('_'); //Split First_Middle_Last name

                        //Join full name
                        for (int index = 0; index < listProperties.Length; index++)
                        {
                            obj = GetPropValue(listProperties[index], row);
                            strValue += obj == null ? string.Empty : obj.ToString() + " ";
                        }

                        strValue = strValue.TrimEnd();
                    }
                    else
                    {
                        obj = GetPropValue(headerArr[0], row);
                        strValue = obj == null ? "" : obj.ToString();

                        if (headerArr.Count() == 2)
                        {
                            switch (headerArr[1].ToString().ToLower())
                            {
                                case "text":
                                    itemStyle.CssClass = "text";
                                    itemStyle.HorizontalAlign = HorizontalAlign.Center;
                                    break;
                                case "text-left":
                                    itemStyle.CssClass = "text";
                                    itemStyle.HorizontalAlign = HorizontalAlign.Left;
                                    break;
                                case "scientific":
                                    itemStyle.CssClass = "scientific";
                                    itemStyle.HorizontalAlign = HorizontalAlign.Left;
                                    break;
                                case "date":
                                    itemStyle.CssClass = "text";
                                    itemStyle.HorizontalAlign = HorizontalAlign.Center;
                                    strValue = obj == null ? "" : ((DateTime)obj).ToString(Constants.DATETIME_FORMAT_VIEW);
                                    break;
                                case "datetime":
                                    itemStyle.CssClass = "text";
                                    itemStyle.HorizontalAlign = HorizontalAlign.Center;
                                    strValue = obj == null ? "" : ((DateTime)obj).ToString(Constants.DATETIME_FORMAT_TIME);
                                    break;
                                case "gender":
                                    strValue = obj == null ? "" : (bool)obj == Constants.MALE ? "Male" : "Female";
                                    break;
                                case "married":
                                    strValue = obj == null ? "" : (bool)obj == Constants.MARRIED ? "Married" : "Single";
                                    break;
                                case "labor":
                                    strValue = obj == null ? "" : (bool)obj == Constants.LABOR_UNION_FALSE ? "No" : "Yes";
                                    break;
                                case "hhmm":
                                    itemStyle.CssClass = "text";
                                    strValue = obj == null ? "" : CommonFunc.FormatTime((double)obj);
                                    break;
                                case "jr":
                                    strValue = obj == null ? "" : Constants.JOB_REQUEST_PREFIX + obj;
                                    break;
                                case "pr":
                                    strValue = obj == null ? "" : Constants.PR_REQUEST_PREFIX + obj;
                                    break;
                                case "candidate":
                                    strValue = obj == null ? "" : CommonFunc.GetCandidateStatus((int)obj);
                                    break;
                                case "actionsendmail":
                                    strValue = obj == null ? "" : (bool)obj != true ? "No" : "Yes";
                                    break;
                                case "jr_request":
                                    strValue = obj == null ? "" : (int)obj == Constants.JR_REQUEST_TYPE_NEW ? "New" : "Replace";
                                    break;
                                case "jobrequestprefix":
                                    strValue = obj == null ? "" : Constants.JOB_REQUEST_ITEM_PREFIX + obj.ToString();
                                    break;
                                case "dayofweek":
                                    itemStyle.HorizontalAlign = HorizontalAlign.Center;
                                    strValue = obj == null ? "" : ((DateTime)obj).DayOfWeek.ToString();
                                    break;
                                case "number":
                                    itemStyle.HorizontalAlign = HorizontalAlign.Right;
                                    break;
                                case "currency":
                                    if (obj != null)
                                    {
                                        var arr = strValue.Split(' ');
                                        strValue = CommonFunc.FormatCurrency(double.Parse(arr[0])) + " " + arr[1];
                                    }
                                    else
                                        strValue = "";
                                    break;
                                default:
                                    itemStyle.CssClass = "";
                                    itemStyle.HorizontalAlign = HorizontalAlign.Left;
                                    break;
                            }
                        }
                        else
                        {
                            itemStyle.CssClass = "";
                            itemStyle.HorizontalAlign = HorizontalAlign.Left;
                        }
                    }

                    itemStyle.AddAttributesToRender(tw);
                    
                    tw.RenderBeginTag(HtmlTextWriterTag.Td);
                    tw.Write( HttpUtility.HtmlEncode(strValue));
                    tw.RenderEndTag();
                }
                tw.RenderEndTag();
            }
            tw.RenderEndTag(); // tbody

            if (_footer_excel != null)
            {
                TableItemStyle itemStyle = _footerStyle;
                // Create footer Row
                tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                string text = "";
                foreach (String footer in _footer_excel)
                {
                    text = footer;
                    string[] headerArr=footer.Split(Convert.ToChar(":"));

                    if (headerArr.Count() == 2)
                    {
                        text = headerArr[0];
                        switch (headerArr[1].ToString().ToLower())
                        {
                            case "left":
                                itemStyle.HorizontalAlign = HorizontalAlign.Left;
                                break;
                            case "right":
                                itemStyle.HorizontalAlign = HorizontalAlign.Right;
                                break;
                            case "center":
                                itemStyle.HorizontalAlign = HorizontalAlign.Center;
                                break;
                            case "hhmm":
                                itemStyle.HorizontalAlign = HorizontalAlign.Right;
                                itemStyle.CssClass = "text";
                                text = (string.IsNullOrEmpty(text) ?"": CommonFunc.FormatTime(double.Parse(text)));
                                break;
                            default:
                                itemStyle.HorizontalAlign = HorizontalAlign.Center;
                                break;
                        }
                    }
                    itemStyle.AddAttributesToRender(tw);
                    tw.RenderBeginTag(HtmlTextWriterTag.Th);
                    tw.Write(text);
                    tw.RenderEndTag();
                }
                tw.RenderEndTag();
            }
            //End footer
            tw.RenderEndTag(); // table
            WriteFile(_fileName, "application/vnd.ms-excel", sw.ToString());
            
        }

        private static void WriteFile(string fileName, string contentType, string content)
        {
            try
            {
                HttpContext context = HttpContext.Current;
                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                string style = @"<style> .text { mso-number-format:\@;} </style> ";
                context.Response.Write(style);
                context.Response.Write("<meta http-equiv=Content-Type content='text/html; charset=utf-8' />");
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = contentType;
                context.Response.Write(content);
                context.Response.End();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
    
}