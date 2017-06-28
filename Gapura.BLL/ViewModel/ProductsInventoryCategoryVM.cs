using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Gapura.BLL.Models;

namespace Gapura.BLL.ViewModel
{
    public class ProductsInventoryCategoryVM
    {
        public int ProductID { get; set; }

        //[Required]
        //[Display(Name = "Departemen")]
        //public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Item Code")]
        public string ProductCode { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Current Stock")]
        public Nullable<short> UnitsInStock { get; set; }

        [Display(Name = "Units On Order")]
        public Nullable<short> UnitsOnOrder { get; set; }
    }
}