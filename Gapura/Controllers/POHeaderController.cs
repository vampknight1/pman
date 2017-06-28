using Gapura.BLL.Models;
using Gapura.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class POHeaderController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /POHeader/

        public ActionResult Index()
        {
            return View(_dbConn.POHeaders.ToList());
        }

        ///////////////-------------- Load Data by JQuery Fir 26102016 -----------------/////////////
        public ActionResult LoadData()
        {
            //using (YSIDGAEntitiesConn dc = new YSIDGAEntitiesConn())
            {
                _dbConn.Configuration.LazyLoadingEnabled = false;    // if your table is relational, contain foreign key

                var data = _dbConn.SPI_POHeader().ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }
        ///////////////-----------End Load Data by JQuery Fir 26102016 -----------------/////////////

        //
        // GET: /POHeader/Details/5

        public ActionResult Details(int id = 0)
        {
            POHeader poHeader = _dbConn.POHeaders.Find(id);
            if (poHeader == null)
            {
                return HttpNotFound();
            }
            return View(poHeader);
        }

        //
        // GET: /POHeader/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /POHeader/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(POHeader poHeader)
        {
            if (ModelState.IsValid)
            {
                _dbConn.POHeaders.Add(poHeader);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poHeader);
        }

        //
        // GET: /POHeader/Edit/5

        public ActionResult Edit(int id = 0)
        {
            POHeader poHeader = _dbConn.POHeaders.Find(id);
            if (poHeader == null)
            {
                return HttpNotFound();
            }

            ViewBag.PONo = poHeader.PONo;
            ViewData["PODate"] = DateTime.Now.ToString("yyyy/MM/dd");
            ViewData["RequestNo"] = (from rh in _dbConn.RequestHeaders
                                                    where rh.RequestID == id
                                                    select rh.RequestNo
                                                   ).Take(1).SingleOrDefault();
            ViewData["ReffNo"] = poHeader.ReffNo;
            ViewData["RequestDate"] = poHeader.RequestDate.Date.ToString("yyyy/MM/dd");
            ViewData["RequiredDate"] = poHeader.RequiredDate.Date.ToString("yyyy/MM/dd");
            ViewBag.Remarks = poHeader.Remarks;
            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "DepartemenName", poHeader.DepartemenID);
            ViewBag.RequestTypeID = new SelectList(_dbConn.MasterRequestTypes, "ID", "RequestType", poHeader.RequestTypeID);
            ViewBag.CurrencyID = new SelectList(_dbConn.MasterCurrencies, "ID", "CurrencyCode", poHeader.CurrencyID);
            ViewBag.AssetsTypeID = new SelectList(_dbConn.MasterAssetsTypes, "ID", "AssetsType", poHeader.AssetsTypeID);
            ViewBag.EmployeeID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", poHeader.EmployeeID);
            ViewBag.MgrID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", poHeader.MgrID);
            ViewBag.GAMgrID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", poHeader.GAMgrID);
            ViewBag.GMID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", poHeader.GMID);

            ViewData["PODetails"] = _dbConn.Database.SqlQuery<PODetailListVM>("EXEC SPI_PODetail {0}", id).ToList();

            if (User.IsInRole("Administrator"))
            {
                return View(poHeader);
            }
            else if (User.IsInRole("GM"))
            {
                return View("EditGM", poHeader);
            }
            else if (User.IsInRole("GAM"))
            {
                return View("EditGAM", poHeader);
            }
            //else if (User.IsInRole("MGR"))
            //{
            //    return View("EditMgr", poHeader);
            //}
            else
            {
                return View("EditUser", poHeader);
            }
        }

        //
        // POST: /POHeader/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(POHeader poHeader)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(poHeader).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.PONo = poheader.PONo;
            //ViewData["PODate"] = DateTime.Now.ToString("yyyy/MM/dd");
            //ViewData["RequestNo"] = (from rh in dbConn.RequestHeaders
            //                         where rh.RequestID == id
            //                         select rh.RequestNo
            //                        ).Take(1).SingleOrDefault();
            //ViewData["ReffNo"] = poheader.ReffNo;
            //ViewData["RequestDate"] = poheader.RequestDate.Date.ToString("yyyy/MM/dd");
            //ViewData["RequiredDate"] = poheader.RequiredDate.Date.ToString("yyyy/MM/dd");
            //ViewBag.Remarks = poheader.Remarks;
            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "DepartemenName", poHeader.DepartemenID);
            ViewBag.RequestTypeID = new SelectList(_dbConn.MasterRequestTypes, "ID", "RequestType", poHeader.RequestTypeID);
            ViewBag.CurrencyID = new SelectList(_dbConn.MasterCurrencies, "ID", "CurrencyCode", poHeader.CurrencyID);
            ViewBag.AssetsTypeID = new SelectList(_dbConn.MasterAssetsTypes, "ID", "AssetsType", poHeader.AssetsTypeID);
            ViewBag.EmployeeID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", poHeader.EmployeeID);
            ViewBag.MgrID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", poHeader.MgrID);
            ViewBag.GAMgrID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", poHeader.GAMgrID);
            ViewBag.GMID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", poHeader.GMID);

            ViewData["PODetails"] = _dbConn.Database.SqlQuery<PODetailListVM>("EXEC SPI_PODetail {0}", poHeader.POID).ToList();

            return View(poHeader);
        }

        //
        // GET: /POHeader/Delete/5

        public ActionResult Delete(int id = 0)
        {
            POHeader poHeader = _dbConn.POHeaders.Find(id);
            if (poHeader == null)
            {
                return HttpNotFound();
            }
            return View(poHeader);
        }

        //
        // POST: /POHeader/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POHeader poHeader = _dbConn.POHeaders.Find(id);
            _dbConn.POHeaders.Remove(poHeader);
            _dbConn.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _dbConn.Dispose();
            base.Dispose(disposing);
        }
    }
}