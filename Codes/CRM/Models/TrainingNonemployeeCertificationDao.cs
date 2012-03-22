using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;
using System.Data.Linq.SqlClient;
using CRM.Models.Entities;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace CRM.Models
{
    public class TrainingNonemployeeCertificationDao : BaseDao
    {
        NonEmployeeCerfiticationDao nonCertDao = new NonEmployeeCerfiticationDao();
        public List<Training_CertificationMaster> GetTypeList()
        {
            return dbContext.Training_CertificationMasters.ToList();
        }
        public Training_CertificationMaster GetCertificationMasterTypeByID(int id)
        {
            return dbContext.Training_CertificationMasters.Where(q => q.ID.Equals(id)&&q.DeleteFlag==false).FirstOrDefault();
        }
        public List<Training_NonEmpCertification> GetListNonCertByNonEmpID(int id)
        {
            return dbContext.Training_NonEmpCertifications.Where(q => q.NonEmpId.Equals(id) && q.DeleteFlag == false).ToList();
        }
        public List<Training_NonEmpCertification> GetOldListNonCertByID(int id)
        {
            return dbContext.Training_NonEmpCertifications.Where(q => q.NonEmpId.Equals(id) && q.DeleteFlag == false).ToList();
        }
        public Training_NonEmpCertification GetNonEmCertificationByNonEmpID(int id)
        {
            return dbContext.Training_NonEmpCertifications.Where(q => q.NonEmpId.Equals(id) && q.DeleteFlag == false).FirstOrDefault();
        }
        public Training_NonEmpCertification GetNonEmCertificationByID(int id)
        {
            return dbContext.Training_NonEmpCertifications.Where(q => q.ID.Equals(id) && q.DeleteFlag == false).FirstOrDefault();
        }
        public string GetTypeName(int typeId)
        {
            var type = dbContext.Training_CertificationMasters.FirstOrDefault(p => p.ID == typeId && p.DeleteFlag == false);
            return type == null ? "" : type.Name;
        }
        public List<NonEmpCertificationEntity> GetCertificationByNonEmmployeeID(int nonEmpID)
        {
            //return dbContext.Training_NonEmpCertifications.Where(p => p.NonEmpId.Equals(nonEmpID)).ToList();
            List<NonEmpCertificationEntity> sql = GetQueryNonEmployeeCertification(nonEmpID).ToList();
            return sql;
        }
        public IQueryable<NonEmpCertificationEntity> GetQueryNonEmployeeCertification(int id)
        {
            var sql = from nonCert in dbContext.Training_NonEmpCertifications
                      join certMast in dbContext.Training_CertificationMasters on nonCert.TypeId equals certMast.ID
                      where nonCert.NonEmpId == id && nonCert.DeleteFlag == false
                      select new NonEmpCertificationEntity
                      {
                          CertificationName = certMast.Name,
                          CreateDate = nonCert.CreateDate,
                          CreatedBy = nonCert.CreatedBy,
                          DeleteFlag = nonCert.DeleteFlag,
                          ExpireDate = nonCert.ExpireDate,
                          Id = nonCert.ID,
                          NonEmployeeID = nonCert.NonEmpId,
                          Notes = nonCert.Notes,
                          Score = nonCert.Score,
                          TypeID = nonCert.TypeId,
                          UpdateDate = nonCert.UpdateDate,
                          UpdatedBy = nonCert.UpdatedBy
                      };
            return sql;
        }

        public Message Insert(List<Training_NonEmpCertification> objUI)
        {
            Message msg = null;
            try
            {
                if (objUI.Count()>0)
                {
                    dbContext.Training_NonEmpCertifications.InsertAllOnSubmit(objUI);
                    dbContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }
        public Message InsertCert(Training_NonEmpCertification objUI)
        {
            Message msg = null;
            try
            {
                    dbContext.Training_NonEmpCertifications.InsertOnSubmit(objUI);
                    dbContext.SubmitChanges();
                    msg = new Message(MessageConstants.I0001, MessageType.Info, "Certification '" + objUI.NonEmpId + "'", "added");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }

        public Message Update(Training_NonEmpCertification objUI, List<Training_NonEmpCertification> oldList, List<Training_NonEmpCertification> newList)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                if (objUI != null)
                {
                    Training_NonEmpCertification objDB = GetNonEmCertificationByNonEmpID(objUI.ID);
                    if (objDB != null)
                    {
                        //if (oldList.Count > 0)
                        //{
                        string oldCertID = string.Empty;
                        foreach (Training_NonEmpCertification item in oldList)
                        {
                            UpdateNonEmployeeCert(item);
                            oldCertID += item.ID + ",";
                        }
                        foreach (Training_NonEmpCertification deleteItem in GetOldListNonCertByID(objDB.NonEmpId))
                        {
                            if (!oldCertID.TrimEnd(',').Split(',').Contains(deleteItem.ID.ToString()))
                            {
                                Delete(deleteItem);
                            }
                        }
                        dbContext.SubmitChanges();

                        msg = new Message(MessageConstants.I0001, MessageType.Info,
                            objUI.NonEmployee.FirstName + " "
                            + objUI.NonEmployee.MiddleName == null ? string.Empty : objUI.NonEmployee.MiddleName + " "
                            + objUI.NonEmployee.LastName + "'s certification", "updated");
                    }

                    if (newList.Count > 0)
                    {
                        Insert(newList);
                        msg = new Message(MessageConstants.I0001, MessageType.Info,
                            objUI.NonEmployee.FirstName + " "
                            + (objUI.NonEmployee.MiddleName == null ? string.Empty : objUI.NonEmployee.MiddleName) + " "
                            + objUI.NonEmployee.LastName + "'s certification", "updated");
                    }

                    trans.Commit();
                }
            }
            catch (Exception)
            {
                if (trans != null) trans.Rollback();
                // Show system error
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }

            return msg;
        }

        public void UpdateNonEmployeeCert(Training_NonEmpCertification objUI)
        { 
            Training_NonEmpCertification objDB = GetNonEmCertificationByID(objUI.ID);
            if (objDB != null)
            {
                //objDB.CreateDate = objUI.CreateDate;
                //objDB.CreatedBy = objUI.CreatedBy;
                objDB.DeleteFlag = objUI.DeleteFlag;
                objDB.ExpireDate = objUI.ExpireDate;
                objDB.NonEmpId = objUI.NonEmpId;
                objDB.Notes = objUI.Notes;
                objDB.Score = objUI.Score;
                objDB.TypeId = objUI.TypeId;
                objDB.UpdateDate = objUI.UpdateDate;
                objDB.UpdatedBy = objUI.UpdatedBy;
                dbContext.SubmitChanges();
            }
                            
        }
        public void Delete(Training_NonEmpCertification objUI)
        {
            if (objUI != null)
            {
                Training_NonEmpCertification objDb = GetNonEmCertificationByID(objUI.ID);
                if (objDb != null)
                {
                    objDb.DeleteFlag = true;
                    dbContext.SubmitChanges();

                }
            }
        }
        public Message DeleteList(string ids)
        {
            Message msg = null;
            DbTransaction trans = null;
            try
            {
                dbContext.Connection.Open();
                trans = dbContext.Connection.BeginTransaction();
                dbContext.Transaction = trans;
                bool isFalse = true;
                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.TrimEnd(',');
                    string[] idArr = ids.Split(',');
                    int total = idArr.Count();
                    foreach (string id in idArr)
                    {
                        string nonEmID = id;
                        Training_NonEmpCertification trainingCer = GetNonEmCertificationByID(ConvertUtil.ConvertToInt(nonEmID));
                        Employee_Certification obj = dbContext.Employee_Certifications.Where(q => q.CertificationId.Equals(ConvertUtil.ConvertToInt(nonEmID))).FirstOrDefault();
                        if (obj == null)
                        {
                            Delete(trainingCer);
                        }
                        else
                        {

                            isFalse = false;
                            break;
                        }
                    }
                    if (isFalse)
                    {
                        // Show succes message
                        msg = new Message(MessageConstants.I0001, MessageType.Info, total.ToString() + " Certification(s)", "deleted");

                    }
                    else
                    {
                        msg = new Message(MessageConstants.E0006, MessageType.Error, "delete  ", "this certification");
                        //msg = new Message(MessageConstants.E0051, MessageType.Error);
                    }
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                msg = new Message(MessageConstants.E0007, MessageType.Error);
            }
            return msg;
        }
    }
}