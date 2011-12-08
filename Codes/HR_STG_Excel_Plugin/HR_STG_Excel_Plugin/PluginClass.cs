using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAO;
using DTO;

namespace HR_STG_Excel_Plugin
{
    public class PluginClass : PluginInterface.IPlugin
    {
        public string GetPluginName()
        {
            return "Excel 2003";
        }

        public string GetPluginFullName()
        {
            return "HR_STG_Excel_Plugin";
        }

        public string GetPluginType()
        {
            return "Export";
        }

        public string GetPluginID()
        {
            return "01";
        }

        public string GetPluginDescription()
        {
            return "This plugin allow admin to export current files from HR-STG data to Excel 2003 file";
        }

        public bool GetPluginStatus()
        {
            return UC_ExportExcel.pluginStatus;
        }

        public void SetPluginStatus(bool bStatus)
        {
            UC_ExportExcel.pluginStatus = bStatus;
        }

        public UserControl GetPluginUC()
        {
            return new UC_ExportExcel();
        }

        //private List<HoSo_DTO> SetPluginObject2DTO(string sContent)
        //{
        //    List<HoSo_DTO> list_Temp = new List<HoSo_DTO>();

        //    foreach (object dto in list_dto)
        //    {
        //        HoSo_DTO dto_HoSo = (HoSo_DTO)dto;
        //        list_Temp.Add(dto_HoSo);
        //    }

        //    return list_Temp;
        //}

        public UserControl GetPluginUC(string sContent)
        {
            return new UC_ExportExcel(sContent);
        }
    }
}
