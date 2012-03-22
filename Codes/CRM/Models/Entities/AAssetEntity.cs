using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Entities
{
    public class AAssetEntity
    {
        public A_Asset AAsset { get; set; }
        public Employee Employee { get; set; }
        public Employee Manager { get; set; }
        public List<A_AssetPropertyValue> Properties { get; set; }
        public List<A_AssetPropertyValue> PropertiesByPermission { get; set; }
    }

    /// <summary>
    /// An entity to store filtering conditions in AAsset page
    /// </summary>
    public class AConditions
    {
        public string Keyword { get; set; }

        public int? Category { get; set; }

        public int? Status { get; set; }

        public int? Branch { get; set; }

        public int? Dept { get; set; }

        public string Project { get; set; }

        public string Manager { get; set; }


        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }


        public int? Owner { get; set; }

        public List<APropertyParam> AdvanceParams { get; set; }

        public AConditions()
        {
            Keyword = null;
            Category = null;
            Status = null;
            Branch = null;
            Dept = null;
            Project = null;
            Manager = null;

            FromDate = null;
            ToDate = null;

            Owner = null;
            AdvanceParams = null;
        }
    }

    public class APropertyParam
    {
        public long Id { get; set; }
        public string Value { get; set; }     
  
        public APropertyParam(long id, string val)
        {
            Id = id;
            Value = val;
        }
    }
}