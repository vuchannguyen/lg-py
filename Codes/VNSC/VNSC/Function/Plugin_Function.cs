using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginInterface;
using System.Reflection;
using System.Windows.Forms;
using VNSC;
using DTO;
using System.IO;

namespace Function
{
    public class Plugin_Function
    {
        public static void CreatePluginFromAssembly(List<IPlugin> list_Plugin, Assembly asm, string sClassname)
        {
            IPlugin temp;
            Type[] types = asm.GetTypes();

            foreach (Type t in types)
            {
                if (t.FullName == sClassname)
                {
                    temp = (IPlugin)Activator.CreateInstance(t);
                    list_Plugin.Add(temp);
                }
            }
        }

        public static void LoadPlugin(List<IPlugin> list_Plugin)
        {
            if (File.Exists(Application.StartupPath + @"\Addin\PluginInterface.dll"))
            {
                // Load cac plugin (*.addin) tu thu muc Addin\
                string[] dsFile = System.IO.Directory.GetFiles(Application.StartupPath + @"\Addin\", "*.addin");
                list_Plugin.Clear();

                foreach (string sFilename in dsFile)
                {
                    byte[] bytesDLL = File.ReadAllBytes(sFilename);
                    Assembly asm = AppDomain.CurrentDomain.Load(bytesDLL);
                    // Don't even have to try to unload the domain                    
                    //Assembly asm = Assembly.LoadFile(sFilename);

                    string sClassname = sFilename.Replace(Application.StartupPath + @"\Addin\", "").Replace(".addin", "") + ".PluginClass";

                    CreatePluginFromAssembly(list_Plugin, asm, sClassname);
                }
            }
            else
            {
                Form_Notice frm = new Form_Notice("Kiểm tra tập tin PluginInterface.dll!", false);
            }
        }

        public static void LoadPlugin2ListView(List<IPlugin> list_Plugin, ListView lv)
        {
            SubFunction.ClearlvItem(lv);

            for (int i = 0; i < list_Plugin.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = SubFunction.setSTT(i + 1);
                lvi.SubItems.Add(list_Plugin[i].GetPluginName());
                if (list_Plugin[i].GetPluginStatus())
                {
                    lvi.SubItems.Add("Enable");
                }
                else
                {
                    lvi.SubItems.Add("Disable");
                }

                lvi.SubItems.Add(list_Plugin[i].GetPluginType());
                lvi.SubItems.Add("");
                lvi.SubItems.Add(list_Plugin[i].GetPluginID());

                lv.Items.Add(lvi);
            }
        }

        //private static List<object> SetPluginDTO2Object(List<HoSo_DTO> list_dto)
        //{
        //    List<object> list_Temp = new List<object>();

        //    foreach (HoSo_DTO dto in list_dto)
        //    {
        //        object dto_HoSo = (object)dto;
        //        list_Temp.Add(dto_HoSo);
        //    }

        //    return list_Temp;
        //}

        public static UserControl GetPluginUC(IPlugin plugin)
        {
            return plugin.GetPluginUC();
        }

        public static UserControl GetPluginUC(IPlugin plugin, string sContent)
        {
            return plugin.GetPluginUC(sContent);
        }
    }
}
