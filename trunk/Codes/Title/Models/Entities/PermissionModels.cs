
namespace CRM.Models.Entities
{
    public class ModuleModel
    {
        private int moduleId;

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }


        private string moduleName;

        public string ModuleName
        {
            get { return moduleName; }
            set { moduleName = value; }
        }

        private string permissionIds;

        public string PermissionIds
        {
            get { return permissionIds; }
            set { permissionIds = value; }
        }

        private string permissionNames;

        public string PermissionNames
        {
            get { return permissionNames; }
            set { permissionNames = value; }
        }
    }
}