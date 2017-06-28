using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Gapura.BLL.Models;
using System.Web.Mvc;

namespace Gapura.BLL.ViewModel
{
    public class SupplierVM
    {
        [Key]
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Supplier Code")]
        public string SupplierCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Category Name")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Cell Phone")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "NPWP")]
        public string Npwp { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Terms")]
        public Nullable<short> TermID { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> TermOfPayList { get; set; }
    }
}