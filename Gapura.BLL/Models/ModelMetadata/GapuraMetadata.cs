using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gapura.BLL.Models
{
    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {

    }
    public class CategoryMetaData
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }

        public ICollection<Product> Products { get; set; }
    }

    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer
    {

    }
    public class CustomerMetaData
    {
        [Key]
        [Required(ErrorMessage = "*")]
        [StringLength(5)]
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

        [Required(ErrorMessage = "*")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Fax")]
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }
        #region Not Used
        //[Display(Name = "City")]
        //public string City { get; set; }

        //[Display(Name = "Region")]
        //public string Region { get; set; }
        //public string PostalCode { get; set; }

        //[Display(Name = "Country")]
        //public string Country { get; set; }
        #endregion
        public ICollection<Order> Orders { get; set; }
        public ICollection<CustomerDemographic> CustomerDemographics { get; set; }
    }

    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {

    }
    public class ProductMetadata
    {
        // Apply RequiredAttribute
        public int ProductID { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(15)]
        [Display(Name = "Item Code")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Category Name")]
        public Nullable<int> CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Company Name")]
        public Nullable<int> SupplierID { get; set; }

        [Display(Name = "Qty PerUnit")]
        public string QuantityPerUnit { get; set; }

        [Required(ErrorMessage = "*")]
        //[DataType(DataType.Currency)]
        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public decimal? UnitPrice { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Unit Name")]
        public Nullable<int> UnitID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Specs.")]
        public string Specs { get; set; }

        [Display(Name = "ReOrder Level")]
        public Nullable<short> ReorderLevel { get; set; }

        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Input Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FirstInputDate { get; set; }

        [Display(Name = "UnitsInStock")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public Nullable<short> UnitsInStock { get; set; }

        [Display(Name = "UnitsOnOrder")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public Nullable<short> UnitsOnOrder { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
    }

    [MetadataType(typeof(SupplierMetadata))]
    public partial class Supplier
    {

    }
    public class SupplierMetadata
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
    }

    [MetadataType(typeof(ProductsInventoryMetadata))]
    public partial class ProductsInventory
    {

    }
    public class ProductsInventoryMetadata
    {
        [Key]
        [Required(ErrorMessage = "*")]
        public int ProductID { get; set; }

        //[Column("DepartemenID")]
        //[Key]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Departement")]
        public string DepartemenID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Current Stock")]
        public Nullable<short> UnitsInStock { get; set; }

        [Display(Name = "Units On Order")]
        public Nullable<short> UnitsOnOrder { get; set; }
    }

    [MetadataType(typeof(MasterPeriodMetadata))]
    public partial class MasterPeriod
    {

    }
    public class MasterPeriodMetadata
    {
        [Key]
        public int PeriodID { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Period")]
        public System.DateTime StartPeriod { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Period")]
        public System.DateTime EndPeriod { get; set; }

        [Required(ErrorMessage = "*")]
        [Remote("IsPeriodNameAvailable", "Periods", ErrorMessage = "Period Name already used !")]
        //[MaxLength(6)]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Period Format 'mmyyyy' exp. 052017")]
        [Display(Name = "Period Name")]
        public string PeriodName { get; set; }

        [Display(Name = "Remarks")]
        public Nullable<bool> ClosePeriod { get; set; }
    }

    [MetadataType(typeof(MasterUnitMetadata))]
    public partial class MasterUnit
    {

    }
    public class MasterUnitMetadata
    {
        [Key]
        public int UnitID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        [Display(Name = "Description")]
        public string UnitDesc { get; set; }
    }

    [MetadataType(typeof(MasterOfficeMetadata))]
    public partial class MasterOffice
    {

    }
    public class MasterOfficeMetadata
    {
        [Key]
        public int OfficeID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Office Code")]
        public string OfficeCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Office")]
        public string OfficeName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }

    [MetadataType(typeof(TermOfPayMetadata))]
    public partial class TermOfPay
    {

    }
    public class TermOfPayMetadata
    {
        [Key]
        public int TermID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Term Days")]
        public int TermDays { get; set; }
    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {

    }
    public class EmployeeMetadata
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Name")]
        public string LastName { get; set; }
        //public string FirstName { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Job Title")]
        public int TitleID { get; set; }

        [Required(ErrorMessage = "*")]
        public string TitleOfCourtesy { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public System.DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public System.DateTime HireDate { get; set; }

        [Required(ErrorMessage = "*")]
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        public string Country { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }

        [Display(Name = "Ext.")]
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Manager")]
        public Nullable<int> ReportsTo { get; set; }

        [Display(Name = "Photo Path")]
        public string PhotoPath { get; set; }

        public string PhotoName
        {
            //get{
            //    var path = "~/Img/Photo/";
            //    return path;
            //}
            get; set;
        }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Office")]
        public int OfficeID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Departement")]
        public string DepartemenID { get; set; }
    }

    [MetadataType(typeof(MasterAssetsTypeMetadata))]
    public partial class MasterAssetsType
    {

    }
    public class MasterAssetsTypeMetadata
    {
        [Key]
        public short id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Asset Type")]
        public string AssetsType { get; set; }

        [Display(Name = "Note")]
        public string AssetsNote { get; set; }
    }

    [MetadataType(typeof(MasterCurrencyMetadata))]
    public partial class MasterCurrency
    {

    }
    public class MasterCurrencyMetadata
    {
        [Key]
        public short id { get; set; }

        [Required(ErrorMessage = "*")]
        //[MaxLength(80)]
        [StringLength(3)]
        [Display(Name = "Currency Code")]
        public string CurrencyCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Currency Name")]
        public string CurrencyName { get; set; }
    }

    [MetadataType(typeof(MasterRequestTypeMetadata))]
    public partial class MasterRequestType
    {

    }
    public class MasterRequestTypeMetadata
    {
        [Key]
        public short id { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(20)]
        [Display(Name = "Request Type")]
        public string RequestType { get; set; }

        [Display(Name = "Note")]
        public string RequestNote { get; set; }
    }

    [MetadataType(typeof(MasterTransTypeMetadata))]
    public partial class MasterTransType
    {

    }
    public class MasterTransTypeMetadata
    {
        [Key]
        public short id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Transaction Code")]
        public string TransCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Transaction Name")]
        public string TransName { get; set; }
    }

    [MetadataType(typeof(PODetailMetadata))]
    public partial class PODetail
    {

    }
    public class PODetailMetadata
    {
        [Key]
        public int PODetailID { get; set; }

        public int POID { get; set; }

        public int RequestID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Name")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Unit Price")]
        //[DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Unit Name")]
        public int UnitID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Quantity")]
        //[DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Amount")]
        //[DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Amount { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }

    [MetadataType(typeof(POHeaderMetadata))]
    public partial class POHeader
    {

    }
    public class POHeaderMetadata
    {
        [Key]
        public int POID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Request No")]
        public string RequestID { get; set; }

        [Required(ErrorMessage = "*")]
        //[Remote("IsPeriodNameAvailable", "Periods", ErrorMessage = "Period Name already in use !")]
        [Display(Name = "PO No")]
        public string PONo { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "PO Date")]
        public System.DateTime PODate { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Departement")]
        public string DepartemenID { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Request Date")]
        public System.DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Required Date")]
        public System.DateTime RequiredDate { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = false)]
        [Display(Name = "Total Request")]
        public int TotalRequest { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Wrong Reff No Format")]
        [Display(Name = "Reff. No")]
        public string ReffNo { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Request Type")]
        public short RequestTypeID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Requester")]
        public int EmployeeID { get; set; }

        [Display(Name = "Manager")]
        public Nullable<int> MgrID { get; set; }

        [Display(Name = "GA Manager")]
        public Nullable<int> GAMgrID { get; set; }

        [Display(Name = "General Manager")]
        public Nullable<int> GMID { get; set; }

        [Display(Name = "Total Prices")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> TotalPrice { get; set; }
        
        [Required(ErrorMessage = "*")]
        [Display(Name = "Currency")]
        public short CurrencyID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Type of Asset")]
        public short AssetsTypeID { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "User Name")]
        public string UserID { get; set; }
    }

    [MetadataType(typeof(ReceiveDetailMetadata))]
    public partial class ReceiveDetail
    {

    }
    public class ReceiveDetailMetadata
    {
        [Key]
        public int ReceiveDetailID { get; set; }

        public int ReceiveID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Name")]
        public int ProductID { get; set; }

        [Display(Name = "Begin Stock")]
        public Nullable<int> StockBegin { get; set; }

        [Display(Name = "Final Stock")]
        public Nullable<int> StockFinal { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Qty Receive")]
        public Nullable<int> QtyReceive { get; set; }
    }

    [MetadataType(typeof(ReceiveHeaderMetadata))]
    public partial class ReceiveHeader
    {

    }
    public class ReceiveHeaderMetadata
    {
        [Key]
        public int ReceiveID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Receive No")]
        public string ReceiveNo { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receive Date")]
        public System.DateTime ReceiveDate { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Receive Reff.")]
        public string ReceiptReff { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Supplier Name")]
        public string SupplierCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Total Receive")]
        public Nullable<int> TotalReceive { get; set; }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Receiver User")]
        public string UserID { get; set; }
    }

    [MetadataType(typeof(ReleaseDetailMetadata))]
    public partial class ReleaseDetail
    {

    }
    public class ReleaseDetailMetadata
    {
        [Key]
        public int ReleaseDetailID { get; set; }

        public int ReleaseID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Name")]
        public int ProductID { get; set; }

        [Display(Name = "Stock Begin")]
        public Nullable<int> StockBegin { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Qty Request")]
        public Nullable<int> QtyRequest { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Qty Release")]
        public Nullable<int> QtyRelease { get; set; }

        [Display(Name = "Stock Final")]
        public Nullable<int> StockFinal { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }
    }

    [MetadataType(typeof(ReleaseHeaderMetadata))]
    public partial class ReleaseHeader
    {

    }
    public class ReleaseHeaderMetadata
    {
        [Key]
        public int ReleaseID { get; set; }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Request No")]
        public int RequestID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Release No")]
        public string ReleaseNo { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public System.DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Departement")]
        public string DepartemenID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Total Request")]
        public Nullable<int> TotalRequest { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Total Release")]
        public Nullable<int> TotalRelease { get; set; }

        [Display(Name = "Release Note")]
        public string ReleaseNote { get; set; }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "User Release")]
        public string UserID { get; set; }
    }

    [MetadataType(typeof(RequestDetailMetadata))]
    public partial class RequestDetail
    {

    }
    public class RequestDetailMetadata
    {
        [Key]
        public int RequestDetailID { get; set; }

        public int RequestID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Name")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Currency)]
        //[RegularExpression(@"(?:\d*\.)?\d+")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Unit Name")]
        public int UnitID { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Currency)]
        //[RegularExpression(@"(?:\d*\.)?\d+")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        //[Required(ErrorMessage = "*")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Amount")]
        public Nullable<decimal> Amount { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        //public virtual Product Product { get; set; }
        //public virtual MasterUnit MasterUnit { get; set; }
    }

    [MetadataType(typeof(RequestHeaderMetadata))]
    public partial class RequestHeader
    {

    }
    public class RequestHeaderMetadata
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }

        [Key]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Request No")]
        public string RequestNo { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Departement")]
        public string DepartemenID { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Request Date")]
        public System.DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Required Date")]
        public System.DateTime RequiredDate { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = false)]       //Fir 04062017
        [Display(Name = "Total Request")]
        public int TotalRequest { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Wrong Reff No Format")]
        [Display(Name = "Reff. No")]
        public string ReffNo { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Request Type")]
        public short RequestTypeID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Requester")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Manager")]
        public Nullable<int> MgrID { get; set; }

        [Display(Name = "GA Manager")]
        public Nullable<int> GAMgrID { get; set; }

        [Display(Name = "General Manager")]
        public Nullable<int> GMID { get; set; }

        [Required(ErrorMessage = "*")]
        //[DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Prices")]
        public Nullable<decimal> TotalPrice { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Currency")]
        public short CurrencyID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Type of Asset")]
        public short AssetsTypeID { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please fill in !")]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
/*
        public string SeqRequestNo
        {
            get {
                    // 001/RF/GA/I/17
                    var seq = RequestID.ToString() + "/RF/GA/";
                    return seq;
            }
        }                                   */
        public int ReqSeq { get; set; }

        [Display(Name = "User Name")]
        public string UserID { get; set; }
    }

    [MetadataType(typeof(StockCardMetadata))]
    public partial class StockCard
    {

    }
    public class StockCardMetadata
    {
        [Key]
        public int TransactionID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Transaction No")]
        public string TransactionNo { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Transaction Name")]
        public string TransCode { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = false)]
        [Display(Name = "Transaction Date")]
        public System.DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Departement")]
        public string DepartemenID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Name")]
        public int ProductID { get; set; }
        
        [Display(Name = "Note")]
        public string Note { get; set; }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Qty In")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public Nullable<long> TransactionIN { get; set; }

        //[Required(ErrorMessage = "*")]
        [Display(Name = "Qty Out")]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public Nullable<long> TransactionOUT { get; set; }
    }

    public class RequestDetailListVM
    {
        [Key]
        public int RequestDetailID { get; set; }
        public int RequestID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "*")]
        //[DisplayFormat(DataFormatString = "{0:n}", ApplyFormatInEditMode = false)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        [Required(ErrorMessage = "*")]
        //[DisplayFormat(DataFormatString = "{0:n}", ApplyFormatInEditMode = false)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "*")]
        //[DisplayFormat(DataFormatString = "{0:n}", ApplyFormatInEditMode = false)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Amount")]
        public Nullable<decimal> Amount { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; } 
    }

    public class PODetailListVM
    {
        [Key]
        public int PODetailID { get; set; }
        public int POID { get; set; }
        public int RequestID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Item Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "*")]
        //[DisplayFormat(DataFormatString = "{0:n}", ApplyFormatInEditMode = false)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        [Required(ErrorMessage = "*")]
        //[DisplayFormat(DataFormatString = "{0:n}", ApplyFormatInEditMode = false)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "*")]
        //[DisplayFormat(DataFormatString = "{0:n}", ApplyFormatInEditMode = false)]
        [DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Amount")]
        public Nullable<decimal> Amount { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }

    [MetadataType(typeof(DivisionMetadata))]
    public partial class Division
    {

    }
    public class DivisionMetadata
    {
        [Key]
        public int DivisionID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Division Name")]
        public string DivisionName { get; set; }

        public string Note { get; set; }
    
        public virtual ICollection<Departement> Departements { get; set; }
    }

    [MetadataType(typeof(DepartementMetadata))]
    public partial class Departement
    {

    }
    public class DepartementMetadata
    {
        [Key]
        public int DepartemenID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Departement Name")]
        public string DepartemenName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Division Name")]
        public int DivisionID { get; set; }

        public string Note { get; set; }

        public virtual Division Division { get; set; }
    }

    [MetadataType(typeof(MasterTitleMetadata))]
    public partial class MasterTitle
    {

    }
    public class MasterTitleMetadata
    {
        [Key]
        public int TitleID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Title Name")]
        public string TitleName { get; set; }

        public string Note { get; set; }
    }

    //[Table("RBAC_UserProfile")]
    [MetadataType(typeof(RBAC_UserProfileMetadata))]
    public partial class RBAC_UserProfile
    {

    }
    public class RBAC_UserProfileMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}