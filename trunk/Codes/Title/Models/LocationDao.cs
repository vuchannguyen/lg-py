using System.Collections.Generic;
using System.Linq;
using CRM.Library.Common;

namespace CRM.Models
{
    public class LocationDao:BaseDao
    {
        public List<Office> GetListOfficeAll(bool isActive, bool deleteFlag)
        {
            return dbContext.Offices.Where(p => p.IsActive == isActive && p.DeleteFlag == deleteFlag).ToList();
        }
        /// <summary>
        /// Get office list by branch ID
        /// </summary>
        /// <param name="branchID">the Branch ID, if this param is zero (0) means any branch</param>
        /// <param name="isActive"></param>
        /// <param name="deleteFlag"></param>
        /// <returns></returns>
        public List<Office> GetListOffice(int branchID, bool isActive, bool deleteFlag)
        {
            return dbContext.Offices.Where(p => (p.BranchID == branchID || branchID == 0) && 
                p.IsActive == isActive && p.DeleteFlag == deleteFlag).ToList();
        }
        public List<Floor> GetListFloorAll(bool isActive, bool deleteFlag)
        {
            return dbContext.Floors.Where(p => p.IsActive == isActive && p.DeleteFlag == deleteFlag).ToList();
        }
        /// <summary>
        /// Get floor list by branchID and office ID
        /// </summary>
        /// <param name="branchID">the Branch ID, if this param is zero (0) means any branch</param>
        /// <param name="officeID">the Office ID, if this param is zero (0) means any office</param>
        /// <param name="isActive"></param>
        /// <param name="deleteFlag"></param>
        /// <returns></returns>
        public List<Floor> GetListFloor(int branchID, int officeID, bool isActive, bool deleteFlag)
        {
            return dbContext.Floors.Where(p => (p.OfficeID == officeID || officeID == 0) && 
                (p.Office.BranchID == branchID || branchID == 0) &&
                p.IsActive == isActive && p.DeleteFlag == deleteFlag).ToList();
        }
        public List<Office> GetListOfficeByBranchId(int branchId, bool isActive, bool deleteFlag)
        {
            return dbContext.Offices.Where(p => p.BranchID == branchId && 
                p.IsActive == isActive && p.DeleteFlag == deleteFlag).ToList();
        }
        public List<Branch> GetListBranchAll(bool isActive, bool deleteFlag)
        {
            return dbContext.Branches.Where(p => p.IsActive == isActive && p.DeleteFlag == deleteFlag).ToList();
        }
        public List<sp_GetSeatCodeResult> GetListSeatCode(string name, int branchID, int officeID, int floorID, bool? isAvailable)
        {
            return dbContext.sp_GetSeatCode(name, branchID, officeID, floorID, isAvailable, true, false).ToList();
        }
        public SeatCode GetSeatCodeByID(int seatCodeID, bool isActive, bool deleteFlag)
        {
            return dbContext.SeatCodes.Where(p => p.ID == seatCodeID && 
                p.IsActive == isActive && p.DeleteFlag == deleteFlag).FirstOrDefault();
        }
        public List<sp_GetSeatCodeResult> Sort(List<sp_GetSeatCodeResult> list, string sortColumn, string sortOrder)
        {
            int order;

            if (sortOrder == "desc")
            {
                order = -1;
            }
            else
            {
                order = 1;
            }
            switch (sortColumn)
            {
                case "ID":
                    list.Sort(
                         delegate(sp_GetSeatCodeResult m1, sp_GetSeatCodeResult m2)
                         { return m1.SeatCodeID.CompareTo(m2.SeatCodeID) * order; });
                    break;
                case "Floor":
                    list.Sort(
                         delegate(sp_GetSeatCodeResult m1, sp_GetSeatCodeResult m2)
                         { return m1.FloorName.CompareTo(m2.FloorName) * order; });
                    break;
                case "Branch":
                    list.Sort(
                         delegate(sp_GetSeatCodeResult m1, sp_GetSeatCodeResult m2)
                         { return m1.BranchName.CompareTo(m2.BranchName) * order; });
                    break;
                case "Office":
                    list.Sort(
                         delegate(sp_GetSeatCodeResult m1, sp_GetSeatCodeResult m2)
                         { return m1.OfficeName.CompareTo(m2.OfficeName) * order; });
                    break;
            }

            return list;
        }

        public Office GetOfficeByID(int officeID, bool isActive, bool deleteFlag)
        {
            return dbContext.Offices.Where(p => p.ID == officeID && 
                p.IsActive == isActive && p.DeleteFlag == deleteFlag).FirstOrDefault();
        }

        public Office GetOfficeByOfficeID(int officeID)
        {
            return dbContext.Offices.Where(p => p.ID == officeID && p.DeleteFlag == false).FirstOrDefault();
        }

        public Branch GetBranchByID(int branchID)
        {
            return dbContext.Branches.Where(p => p.ID == branchID && p.DeleteFlag == false).FirstOrDefault();
        }

        public Floor GetFloorByID(int floorID)
        {
            return dbContext.Floors.Where(p => p.ID == floorID && p.DeleteFlag == false).FirstOrDefault();
        }

        public SeatCode GetSeatCodeByID(int seatCodeID)
        {
            return dbContext.SeatCodes.Where(p => p.ID == seatCodeID && p.DeleteFlag == false).FirstOrDefault();
        }

        public SeatCode GetSeatCodeByName(string name, int floorid)
        {
            return dbContext.SeatCodes.Where(p => p.Name == name && p.DeleteFlag == false && p.FloorID == floorid).FirstOrDefault();

        }

        public Floor GetFloorByName(string name)
        {
            return dbContext.Floors.Where(p => p.Name == name && p.DeleteFlag == false).FirstOrDefault();
        }

        public bool IsSeatCodeAvailableFor(int seatCodeID, string ownerID, bool isSTT)
        {
            Constants.OwnerModel owner = GetOwner(seatCodeID);
            if (owner == null || (owner.OwnerID.Equals(ownerID) && owner.IsSTT == isSTT))
                return true;
            return false;
            //if (!isSTT)
            //{
            //    var empList = dbContext.STTs.Where(p=> !p.DeleteFlag && 
            //        (!p.ID.Equals(ownerID) || string.IsNullOrEmpty(ownerID))).ToList();
            //    foreach(var emp in empList)
            //    {
            //        string tmpSeatCodeID = string.IsNullOrEmpty(emp.LocationCode) ? "" : 
            //            CommonFunc.GetLocation(emp.LocationCode, LocationType.SeatCode);
            //        if (!string.IsNullOrEmpty(tmpSeatCodeID) && int.Parse(tmpSeatCodeID) == seatCodeID)
            //            return false;
            //    }
            //}
            //else
            //{
            //    var sttList = dbContext.STTs.Where(p => !p.DeleteFlag &&
            //        (!p.ID.Equals(ownerID) || string.IsNullOrEmpty(ownerID))).ToList();
            //    foreach (var stt in sttList)
            //    {
            //        string tmpSeatCodeID = string.IsNullOrEmpty(stt.LocationCode) ? "" :
            //            CommonFunc.GetLocation(stt.LocationCode, LocationType.SeatCode);
            //        if (!string.IsNullOrEmpty(tmpSeatCodeID) && int.Parse(tmpSeatCodeID) == seatCodeID)
            //            return false;
            //    }
            //}
            //return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seatCodeID"></param>
        /// <returns>Dictionary(Employee/STT, is STT)</returns>
        public Constants.OwnerModel GetOwner(int seatCodeID)
        {
            //var empList = dbContext.Employees.Where(p=> p.LocationCode != null && p.LocationCode.Length > 0 ).ToList();
            //Employee emp = empList.Where(p => CommonFunc.GetLocation(p.LocationCode, LocationType.SeatCode).
            //    Equals(seatCodeID.ToString())).FirstOrDefault();
            //if (emp != null)
            //    return "<b>" + emp.ID + "</b> - " + CommonFunc.GetEmployeeFullName(emp, Constants.FullNameFormat.FirstMiddleLast) +
            //        " (Employee)";
            //var sttList = dbContext.STTs.Where(p => p.LocationCode != null && p.LocationCode.Length > 0).ToList();
            //STT stt = sttList.Where(p => CommonFunc.GetLocation(p.LocationCode, LocationType.SeatCode).
            //    Equals(seatCodeID.ToString())).FirstOrDefault();
            //if (stt != null)
            //    return "<b>" + stt.ID + "</b> - " + (string.IsNullOrEmpty(stt.MiddleName) ? (stt.FirstName + " " + stt.LastName) : 
            //        stt.FirstName + " " + stt.MiddleName + " " + stt.LastName) + " (STT)";
            //return null;

            var empList = dbContext.Employees.Where(p => p.LocationCode != null && p.LocationCode.Length > 0).ToList();
            Employee emp = empList.Where(p => CommonFunc.GetLocation(p.LocationCode, LocationType.SeatCode).
                Equals(seatCodeID.ToString())).FirstOrDefault();
            if (emp != null)
                return new Constants.OwnerModel() { 
                    OwnerID = emp.ID,
                    OwnerFullName = CommonFunc.GetEmployeeFullName(emp, Constants.FullNameFormat.FirstMiddleLast),
                    IsSTT = false
                };
            var sttList = dbContext.STTs.Where(p => p.LocationCode != null && p.LocationCode.Length > 0).ToList();
            STT stt = sttList.Where(p => CommonFunc.GetLocation(p.LocationCode, LocationType.SeatCode).
                Equals(seatCodeID.ToString())).FirstOrDefault();
            if (stt != null)
                return new Constants.OwnerModel()
                {
                    OwnerID = stt.ID,
                    OwnerFullName = string.IsNullOrEmpty(stt.MiddleName) ? (stt.FirstName + " " + stt.LastName) :
                                stt.FirstName + " " + stt.MiddleName + " " + stt.LastName ,
                    IsSTT = true
                };
            return null;
        }

        public List<sp_GetAllSeatCodeAndEmpSTTResult> GetListSeatCodeOfFloor(int floorId)
        {
            List<sp_GetAllSeatCodeAndEmpSTTResult> list = dbContext.sp_GetAllSeatCodeAndEmpSTT(floorId).ToList();
            List<sp_GetAllSeatCodeAndEmpSTTResult> list2 = new List<sp_GetAllSeatCodeAndEmpSTTResult>();
            List<int> ids = list.Select(c => c.SeatCodeID).Distinct().ToList();
                            
            foreach (int item in ids)
            {
                List<sp_GetAllSeatCodeAndEmpSTTResult> list3 = list.Where(c => c.SeatCodeID == item).ToList();
                if (list3.Count == 1)
                {
                    list2.Add(list3[0]);
                }
                else if(list3.Count >1)
                { 
                    if (list3[0].EmpId != null)
                        list2.Add(list3[0]);
                    else
                        list2.Add(list3[1]);
                }
            }

            return list2;
        }


        public Message Swap(string id1, string id2)
        {
            Message msg = null;
            STTDao sttDao = new STTDao ();
            EmployeeDao empDao =new EmployeeDao ();
            try
            {
                if (id1.IsSttId())
                {
                    STT obj1 = sttDao.GetById(id1);
                    string tmp = obj1.LocationCode;
                    if (id2.IsSttId())
                    {
                        STT obj2 = sttDao.GetById(id2);
                        obj1.LocationCode = obj2.LocationCode;
                        obj2.LocationCode = tmp;
                    }
                    else
                    {
                        Employee obj2 = empDao.GetById(id2);
                        obj1.LocationCode = obj2.LocationCode;
                        obj2.LocationCode = tmp;
                    }
                }
                else
                {
                    Employee obj1 = empDao.GetById(id1);
                    string tmp = obj1.LocationCode;
                    if (id2.IsSttId())
                    {
                        STT obj2 = sttDao.GetById(id2);
                        obj1.LocationCode = obj2.LocationCode;
                        obj2.LocationCode = tmp;
                    }
                    else
                    {
                        Employee obj2 = empDao.GetById(id2);
                        obj1.LocationCode = obj2.LocationCode;
                        obj2.LocationCode = tmp;
                    }
                }
                msg = new Message(MessageConstants.I0001, MessageType.Info, "Work Location", "swapped");
                dbContext.SubmitChanges();
            }
            catch
            { 
                msg = new Message(MessageConstants.E0033, MessageType.Error);
            }
            return msg;
        }
    }
}