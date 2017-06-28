using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gapura.BLL.Models;

namespace Gapura.BLL.ViewModel
{
    public class CustomerVM
    {
        [Key]
        [Required(ErrorMessage = "*")]
        [StringLength(5, ErrorMessage = "Max 5 Char")]
        [Display(Name = "Customer Code")]
        public string CustomerID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Customer Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Contact")]
        public string ContactName { get; set; }

        //[RegularExpression("[a-zA-Z]")]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Contact Title")]
        public string ContactTitle { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        #region Properties not use
        //public string City { get; set; }
        //public string Region { get; set; }
        //public string PostalCode { get; set; }
        //public string Country { get; set; }
        #endregion

        [Required(ErrorMessage = "*")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Fax")]
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        /// <summary>
        ///     Fir 20022017    ///////////////////////////////////////////////////////////////////////
        /// </summary>

        //public IEnumerable<SelectListItem> Departemen (string defaultId = "")
        //{
        //    return Departemen.ToSelectList(
        //        e => string.Format("{0} ({1} DepartemenName)"),
        //        e => e.DepartemenID.ToString(),
        //        defaultId
        //        );
        //}
        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}