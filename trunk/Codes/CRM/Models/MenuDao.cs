using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Common;
using System.Text;

namespace CRM.Models
{
    public class MenuDao : BaseDao
    {
        //public List<Menu> GetList()
        //{
        //    return dbContext.Menus.Where(p => p.IsActive && !p.DeleteFlag).
        //        OrderBy(p => p.DisplayOrder).ToList();
        //}
        public List<Menu> GetList(bool? isActive)
        {
            return dbContext.Menus.Where(p => (!isActive.HasValue || p.IsActive == isActive) && !p.DeleteFlag).
                OrderBy(p => p.DisplayOrder).ToList();
        }
        //public List<Menu> GetListHasChild(bool hasChild)
        //{
        //    if (hasChild)
        //        return dbContext.Menus.Where(p => dbContext.Menus.Select(q => q.ParentId).
        //            Contains(p.ID) && p.IsActive && !p.DeleteFlag).OrderBy(p => p.DisplayOrder).ToList();
        //    else
        //        return dbContext.Menus.Where(p => !dbContext.Menus.Select(q => q.ParentId).
        //            Contains(p.ID) && p.IsActive && !p.DeleteFlag).OrderBy(p => p.DisplayOrder).ToList();
        //}
        public List<Menu> GetNotChildList(bool? isActive, bool sortAsc)
        {
            var tmpData = dbContext.Menus.Where(p => (!isActive.HasValue || p.IsActive == isActive)
                && !p.DeleteFlag && !p.ParentId.HasValue);
            return sortAsc ? tmpData.OrderBy(p => p.DisplayOrder).ToList() :
                tmpData.OrderByDescending(p => p.DisplayOrder).ToList();

        }
        public List<Menu> GetChild(int menuId)
        {
            return dbContext.Menus.Where(p => p.IsActive && !p.DeleteFlag && p.ParentId == menuId).
                OrderBy(p => p.DisplayOrder).ToList();
        }
        /// <summary>
        /// if isActive does not have value -> don't mind this field
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public Menu GetByID(int id, bool? isActive)
        {
            return dbContext.Menus.Where(p => !p.DeleteFlag && p.ID == id
                && (!isActive.HasValue || p.IsActive == isActive)).FirstOrDefault();
        }

        public Menu GetByLink(string link)
        {
            return dbContext.Menus.Where(q => q.Link == link && !q.DeleteFlag && q.IsActive).FirstOrDefault();
        }

        //public String GetCurrentMenu(string link)
        //{
        //    string result = string.Empty;
        //    Menu objMenu = GetByLink(link);
        //    string link = string.Empty;
        //    string test = RecusionLinkMenu(objMenu, thu);
        //    return result;
        //}

        public Menu GetByParent(int parentID)
        {
            return dbContext.Menus.Where(q => q.ParentId == parentID && !q.DeleteFlag && q.IsActive).FirstOrDefault();
        }



        //public string RecusionLinkMenu(Menu obj, string thu)
        //{
        //    string result = string.Empty;
        //    if (obj.ParentId.HasValue)
        //    {
        //        Menu objParent = GetByID(obj.ParentId.Value, true);
        //        thu = RecusionLinkMenu(objParent, thu);
        //        thu += obj.Name + " » ";
        //    }
        //    else
        //    {
        //        thu = thu.Insert(0, obj.Name + " » ");
        //    }
        //    return thu;
        //}



        public Message Insert(Menu menuObj, List<MenuPermission> menuPermissionList)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                menuObj.CreateDate = menuObj.UpdateDate = DateTime.Now;
                dbContext.Menus.InsertOnSubmit(menuObj);
                dbContext.SubmitChanges();
                foreach (MenuPermission mp in menuPermissionList)
                    mp.MenuId = menuObj.ID;
                dbContext.MenuPermissions.InsertAllOnSubmit(menuPermissionList);
                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Menu \"" + menuObj.Name + "\"", "inserted");
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        public List<Menu> GetNotChildList(List<Menu> menu, bool? isActive)
        {
            return menu.Where(p => (!isActive.HasValue || p.IsActive == isActive) && !p.DeleteFlag && !p.ParentId.HasValue).
                OrderBy(p => p.DisplayOrder).ToList();
        }
        public List<Menu> GetChild(List<Menu> menu, int menuId, bool? isActive)
        {
            return menu.Where(p => (!isActive.HasValue || p.IsActive == isActive) && !p.DeleteFlag && p.ParentId == menuId).
                OrderBy(p => p.DisplayOrder).ToList();
        }
        //Added by Huy Ly 6-1-2011
        public List<Menu> GetNotChildList(List<Menu> menu)
        {
            return menu.Where(p => p.IsActive && !p.DeleteFlag && !p.ParentId.HasValue).
                OrderBy(p => p.DisplayOrder).ToList();
        }
        public List<Menu> GetChild(List<Menu> menu, int menuId)
        {
            return menu.Where(p => p.IsActive && !p.DeleteFlag && p.ParentId == menuId).
                OrderBy(p => p.DisplayOrder).ToList();
        }

        public Message Delete(int menuID)
        {
            Message msg = null;
            //DbTransaction trans = null;
            try
            {
                //dbContext.Connection.Open();
                //trans = dbContext.Connection.BeginTransaction();
                //dbContext.Transaction = trans;
                Menu menu = GetByID(menuID, null);
                if (menu == null)
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "Selected menu", "database");
                else
                {
                    List<Menu> childList = GetChild(menuID);
                    foreach (var child in childList)
                        child.ParentId = null;
                    menu.DeleteFlag = true;
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Menu \"" + menu.Name + "\" ", "deleted");
                    dbContext.SubmitChanges();
                }
            }
            catch
            {
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
        public List<MenuPermission> GetMenuPermissionList(int menuID)
        {
            return dbContext.MenuPermissions.Where(p => p.MenuId == menuID).ToList();
        }

        public Message Update(Menu menuObj, List<MenuPermission> menuPermissionList)
        {
            Message msg = null;
            try
            {
                Menu menuDB = GetByID(menuObj.ID, null);
                if (menuDB == null)
                    msg = new Message(MessageConstants.E0005, MessageType.Error, "Menu \"" + menuObj.Name + "\" ", "database");
                menuDB.Name = menuObj.Name;
                menuDB.Link = menuObj.Link;
                menuDB.DisplayOrder = menuObj.DisplayOrder;

                menuDB.IsActive = menuObj.IsActive;
                menuDB.UpdateDate = DateTime.Now;
                if (menuDB.ImageUrl != menuObj.ImageUrl)
                {
                    menuDB.ImageUrl = menuObj.ImageUrl;
                    string oldImage = HttpContext.Current.Server.MapPath(Constants.MENU_PAGE_ICON_FOLDER + menuObj.ImageUrl);
                    if (System.IO.File.Exists(oldImage))
                        System.IO.File.Delete(oldImage);
                }
                var oldList = GetMenuPermissionList(menuObj.ID);
                dbContext.MenuPermissions.DeleteAllOnSubmit(oldList);
                foreach (MenuPermission mp in menuPermissionList)
                    mp.MenuId = menuObj.ID;
                dbContext.MenuPermissions.InsertAllOnSubmit(menuPermissionList);

                dbContext.SubmitChanges();
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Menu \"" + menuObj.Name + "\"", "updated");

            }
            catch
            {

                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }
}