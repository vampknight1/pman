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
    
    public partial class PODetail
    {
        public int PODetailID { get; set; }
        public int POID { get; set; }
        public int RequestID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitID { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }
    
        public virtual POHeader POHeader { get; set; }
    }
}
