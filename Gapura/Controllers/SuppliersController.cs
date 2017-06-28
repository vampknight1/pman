using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Gapura.BLL.Models;
using Gapura.BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class SuppliersController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /Suppliers/

        public ActionResult Index()
        {
            return View(_dbConn.Suppliers.ToList());
        }

        ///////////////-------------- Load Data by JQuery Fir 26102016 -----------------/////////////
        public ActionResult LoadData()
        {
            //using (YSIDGAEntitiesConn dc = new YSIDGAEntitiesConn())
            {
                _dbConn.Configuration.LazyLoadingEnabled = false;   // if your table is relational, contain foreign key
                #region Various Ways
                //var data = dbConn.Suppliers.OrderBy(s => s.CompanyName).ToList();             //default Way

                //var data = from s in dbConn.Suppliers                                             //Using LinQ
                //           join ca in dbConn.Categories on s.CategoryID equals ca.CategoryID
                //           orderby s.CompanyName
                //           select new { s.SupplierID, s.SupplierCode, ca.CategoryName, s.CompanyName, s.Address, s.City, s.Region, 
                //                        s.ContactName, s.Phone, s.CellPhone, s.Npwp, s.TermOfPayment };
                #endregion

                var data = _dbConn.SPI_Suppliers().ToList();                                 //Using SP
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }
        ///////////////-----------End Load Data by JQuery Fir 26102016 -----------------/////////////

        //
        // GET: /Suppliers/Details/5

        public ActionResult Details(int id = 0)
        {
            Supplier supplier = _dbConn.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // GET: /Suppliers/Create

        public ActionResult Create()
        {
            //ViewBag.CategoryID = new SelectList(_dbConn.Categories, "CategoryID", "CategoryName");
            //ViewBag.TermID = new SelectList(_dbConn.TermOfPays, "TermID", "TermDays");

            //List<Category> categoryList = _dbConn.Categories.ToList();
            //ViewBag.CategoryList = new SelectList(categoryList, "CategoryID", "CategoryName");

            //List<TermOfPay> termOfPayList = _dbConn.TermOfPays.ToList();
            //ViewBag.TermList = new SelectList(termOfPayList, "TermID", "TermDays");

            SupplierVM supplierVM = new SupplierVM();
            supplierVM.CategoryList = new SelectList(_dbConn.Categories, "CategoryID", "CategoryName");
            supplierVM.TermOfPayList = new SelectList(_dbConn.TermOfPays, "TermID", "TermDays");

            return View(supplierVM);
        }

        //
        // POST: /Suppliers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierVM supplierVM)
        {
            try {
                if (ModelState.IsValid)
                {
                    Supplier supplier = new Supplier();
                    supplier.SupplierCode = supplierVM.SupplierCode;
                    supplier.CategoryID = supplierVM.CategoryID;
                    supplier.CompanyName = supplierVM.CompanyName;
                    supplier.Address = supplierVM.Address;
                    supplier.City = supplierVM.City;
                    supplier.Region = supplierVM.Region;
                    supplier.ContactName = supplierVM.ContactName;
                    supplier.Phone = supplierVM.Phone;
                    supplier.CellPhone = supplierVM.CellPhone;
                    supplier.Npwp = supplierVM.Npwp;
                    supplier.TermID = supplierVM.TermID;

                    int latestSupplierID = supplier.SupplierID;

                    _dbConn.Suppliers.Add(supplier);
                    _dbConn.SaveChanges();
                    //return RedirectToAction("Index");
                }

                supplierVM.CategoryList = new SelectList(_dbConn.Categories, "CategoryID", "CategoryName");
                supplierVM.TermOfPayList = new SelectList(_dbConn.TermOfPays, "TermID", "TermDays");

                return View(supplierVM);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        //
        // GET: /Suppliers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //var supplier = _dbConn.Suppliers.FirstOrDefault(supplierID => supplierID.SupplierID == id);
            Supplier supplier = _dbConn.Suppliers.Find(id);

            var supplierVM = new SupplierVM();
            supplierVM.SupplierID = id;
            supplierVM.SupplierCode = supplier.SupplierCode;
            supplierVM.CategoryID = supplier.CategoryID;
            supplierVM.CompanyName = supplier.CompanyName;
            supplierVM.Address = supplier.Address;
            supplierVM.City = supplier.City;
            supplierVM.Region = supplier.Region;
            supplierVM.ContactName = supplier.ContactName;
            supplierVM.Phone = supplier.Phone;
            supplierVM.CellPhone = supplier.CellPhone;
            supplierVM.Npwp = supplier.Npwp;
            supplierVM.TermID = supplier.TermID;

            if (supplier == null)
            {
                return HttpNotFound();
            }

            supplierVM.CategoryList = new SelectList(_dbConn.Categories, "CategoryID", "CategoryName");
            supplierVM.TermOfPayList = new SelectList(_dbConn.TermOfPays, "TermID", "TermDays");

            return View(supplierVM);
        }

        //
        // POST: /Suppliers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierVM supplierVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Supplier supplier = new Supplier();
                    supplier.SupplierID = supplierVM.SupplierID;
                    supplier.SupplierCode = supplierVM.SupplierCode;
                    supplier.CategoryID = supplierVM.CategoryID;
                    supplier.CompanyName = supplierVM.CompanyName;
                    supplier.Address = supplierVM.Address;
                    supplier.City = supplierVM.City;
                    supplier.Region = supplierVM.Region;
                    supplier.ContactName = supplierVM.ContactName;
                    supplier.Phone = supplierVM.Phone;
                    supplier.CellPhone = supplierVM.CellPhone;
                    supplier.Npwp = supplierVM.Npwp;
                    supplier.TermID = supplierVM.TermID;

                    int latestSupplierID = supplier.SupplierID;

                    _dbConn.Entry(supplier).State = EntityState.Modified;
                    _dbConn.SaveChanges();
                    //return RedirectToAction("Index");
                }

                supplierVM.CategoryList = new SelectList(_dbConn.Categories, "CategoryID", "CategoryName");
                supplierVM.TermOfPayList = new SelectList(_dbConn.TermOfPays, "TermID", "TermDays");

                return View(supplierVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // GET: /Suppliers/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Supplier supplier = _dbConn.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // POST: /Suppliers/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = _dbConn.Suppliers.Find(id);
            _dbConn.Suppliers.Remove(supplier);
            _dbConn.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _dbConn.Dispose();
            base.Dispose(disposing);
        }

        #region Other ways to Print
        /*
        //Use  Stream  Method -> File will be download and not the open pop up window
        public ActionResult PrintListOfSuppliers()
        {
            using (ReportDocument rptC = new ReportDocument())
            {
                //List data = new List();
                //var data = dbConn.SPR_Suppliers().ToList();
                //rptC.SetDatabaseLogon("firman", "123", "(local)", "YSIDGA");
                //rptC.SetDataSource(data);

                rptC.Load(Path.Combine(Server.MapPath("~/App_Reports"), "SupplierList.rpt"));

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                try{
                    Stream stream = rptC.ExportToStream(ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "ListofSuppliers.pdf");
                }
                catch (Exception ex){
                    throw;
                }
            }
        }
*/
        #endregion

        public void PrintListOfSuppliers()
        {
            using (ReportDocument rptD = new ReportDocument())
            {
                try {
                    rptD.FileName = Server.MapPath("~/") + "/App_Reports/SupplierList.rpt";
                    //rptC.Load();
                    rptD.SetDatabaseLogon("firman", "123", "(local)", "YSIDGA");
                    rptD.ExportToHttpResponse(
                        ExportFormatType.PortableDocFormat,
                        System.Web.HttpContext.Current.Response, false, "ListofSuppliers"
                    );
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }
    }
}