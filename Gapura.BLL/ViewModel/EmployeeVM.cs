using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gapura.BLL.Models;

namespace Gapura.BLL.ViewModel
{
    public class EmployeeVM
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string LastName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string TitleOfCourtesy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public System.DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public System.DateTime HireDate { get; set; }

        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        [Display(Name = "Zip Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }

        [Display(Name = "Phone")]
        public string HomePhone { get; set; }

        [Display(Name = "Ext.")]
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Manager")]
        public Nullable<int> ReportsTo { get; set; }

        [Display(Name = "Photo Path")]
        public string PhotoPath { get; set; }

        [Required]
        [Display(Name = "Office")]
        public int OfficeID { get; set; }

        [Required]
        [Display(Name = "Departemen")]
        public string DepartemenID { get; set; }

        /// <summary>
        ///     Fir 20022017    ///////////////////////////////////////////////////////////////////////
        /// </summary>
        public virtual IEnumerable<Customer> Customers { get; set; }

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