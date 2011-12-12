using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class ModuleData
    {
        public string Name;
        public int ID;

        public ModuleData() { }

        public ModuleData(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    public class PermissonData
    {
        public string Name;
        public int ID;

        public PermissonData() { }

        public PermissonData(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

}