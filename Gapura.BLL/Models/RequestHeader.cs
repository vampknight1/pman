//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gapura.BLL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequestHeader
    {
        public RequestHeader()
        {
            this.RequestDetails = new HashSet<RequestDetail>();
        }
    
        public int RequestID { get; set; }
        public string RequestNo { get; set; }
        public System.DateTime RequestDate { get; set; }
        public System.DateTime RequiredDate { get; set; }
        public int TotalRequest { get; set; }
        public string ReffNo { get; set; }
        public short RequestTypeID { get; set; }
        public int EmployeeID { get; set; }
        public Nullable<int> MgrID { get; set; }
        public Nullable<int> GAMgrID { get; set; }
        public Nullable<int> GMID { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public short CurrencyID { get; set; }
        public short AssetsTypeID { get; set; }
        public string Remarks { get; set; }
        public string ReqSeq { get; set; }
        public string UserID { get; set; }
        public int DepartemenID { get; set; }
    
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }
    }
}
