using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Mvc.Html;
using CRM.Library.Common;
namespace CRM.Library.Controls
{
    public class GridViewControls
    {
        
        #region Field
        private  string _id = "list";
        private  string _url = "";
        private  string _datatype = "JSON";
        private  string _mtype = "GET";
        private string[] _colNames;
        private string[] _colModel;
        private  string _pager = "jQuery('#pager')";        
        
        private string _rowList = "[20, 30, 50, 100, 200]";
        public string RowList
        {
            get { return _rowList; }
            set { _rowList = value; }
        }

        private SortOrder _sortorder = SortOrder.desc;
        public SortOrder SortOrder
        {
            get { return _sortorder; }
            set { _sortorder = value; }
        }

        private string _sortname = "";
        public string SortName
        {
            get { return _sortname; }
            set { _sortname = value; }
        }

        private int _rowNum = 20;
        public int RowNum
        {
            get { return _rowNum; }
            set { _rowNum = value; }
        }

        private int _page = 1;
        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }
        private bool _multiselect = true;
        public bool MultiSelect
        {
            get { return _multiselect; }
            set { _multiselect = value; }
        }
        private  bool _viewrecords = true;
        
        private int _width = 1024;
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        private  string _height = "auto";
        public string Height        
        {
            get { return _height; }
            set { _height = value; }
        }

        private  string _imgpath = "/scripts/grid/themes/basic/images";
        private  string _loadui = "block";
        
        private string _loadComplete = "";
        public string LoadComplete
        {
            get { return _loadComplete; }
            set { _loadComplete = value; }
        }

        private bool _hoverrows = true;
        public bool HoverRows
        {
            get { return _hoverrows; }
            set { _hoverrows = value; }
        }

        private string _onSelectRow = "";
        public string OnSelectRow
        {
            get { return _onSelectRow; }
            set { _onSelectRow = value; }
        }
        
        #endregion
        #region Function
        public MvcHtmlString ShowGrid(string id, string controllerUrl, string[] colNames, string[] colModel)
        {

            _id = id;
            _url = controllerUrl;
            _colModel = colModel;
            _colNames = colNames;

            StringBuilder innerScr = new StringBuilder();
            innerScr.AppendLine("jQuery('#" + id + "').jqGrid({");
            innerScr.AppendLine("url:'" + _url + "',");
            innerScr.AppendLine("datatype:'" + _datatype + "',");
            innerScr.AppendLine("mtype:'" + _mtype + "',");
            string strHeader = "";
            foreach (string str in _colNames)
            {
                strHeader += "'" + str + "',";
            }
            innerScr.AppendLine("colNames:[" + strHeader.TrimEnd(',') + "],");
            string strCols = "";
            foreach (string str in _colModel)
            {
                strCols += str + ",";
            }
            innerScr.AppendLine("colModel:[" + strCols.TrimEnd(',') + "],");
            innerScr.AppendLine("pager:" + _pager + ",");
            innerScr.AppendLine("rowList:" + _rowList + ",");
            innerScr.AppendLine("sortname:'" + _sortname + "',");
            innerScr.AppendLine("sortorder:'" + _sortorder + "',");
            innerScr.AppendLine("rowNum:" + _rowNum + ",");
            innerScr.AppendLine("autowidth:false" + ",");
            innerScr.AppendLine("shrinkToFit:'false',");
            //innerScr.AppendLine("scrollOffset: 0,");
            //innerScr.AppendLine("scroll:'true" + "',");            
            innerScr.AppendLine("page:" + _page + ",");
            innerScr.AppendLine("multiselect:" + _multiselect.ToString().ToLower() + ",");
            innerScr.AppendLine("viewrecords:" + _viewrecords.ToString().ToLower() + ",");
            innerScr.AppendLine("width:" + _width + ", height:'" + _height + "',");            
            innerScr.AppendLine("imgpath:'" + _imgpath + "',");            
            innerScr.AppendLine("loadui:'" + _loadui + "',");
            innerScr.AppendLine("hoverrows:'" + _hoverrows + "',");
            innerScr.AppendLine("onSelectRow:function (id) {" + _onSelectRow + "},");
            innerScr.AppendLine("loadComplete:function () {" + _loadComplete + "}");
            innerScr.AppendLine("});");
            //innerScr.AppendLine("});");
            return BaseHelper.ScriptHelper(innerScr.ToString());
        }
        public MvcHtmlString ShowGrid(string controllerUrl, string[] colNames, string[] colModel)
        {
            return ShowGrid(_id, controllerUrl, colNames, colModel);
        }
        #endregion
    }
}