using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Gapura.BLL.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /Categories/

        public ActionResult Index()
        {
            return View(_dbConn.Categories.ToList());
        }

        ///////////////-------------- Load Data by JQuery Fir 26102016 -----------------/////////////
        public ActionResult LoadData()
        {
            //using (YSIDGAEntitiesConn dc = new YSIDGAEntitiesConn())
            {
                _dbConn.Configuration.LazyLoadingEnabled = false;      // if your table is relational, contain foreign key
                //var data = dbConn.Categories.OrderBy(p => p.CategoryName).ToList();

                var data = _dbConn.SPI_Categories().ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }
        ///////////////-----------End Load Data by JQuery Fir 26102016 -----------------/////////////

        //
        // GET: /Categories/Details/5

        public ActionResult Details(int id = 0)
        {
            Category category = _dbConn.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Categories/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Categories/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbConn.Categories.Add(category);
                    _dbConn.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
                {
                    ModelState.AddModelError(string.Empty, "Ga bisa disimpan !!");
                }

            return View(category);
        }

        //
        // GET: /Categories/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category category = _dbConn.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Categories/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(category).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Categories/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category category = _dbConn.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Categories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _dbConn.Categories.Find(id);
            _dbConn.Categories.Remove(category);
            _dbConn.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _dbConn.Dispose();
            base.Dispose(disposing);
        }

        /*
        //////   Use  Stream  Method -> File will be download and not the open pop up window
        public ActionResult PrintListOfCategories()
        {
            using (ReportDocument rptC = new ReportDocument())
            {
                //List data = new List();
                //var data = dbConn.SPR_Suppliers().ToList();
                //rptC.SetDatabaseLogon("firman", "123", "(local)", "YSIDGA");
                //rptC.SetDataSource(data);

                rptC.Load(Path.Combine(Server.MapPath("~/App_Reports"), "CategoryList.rpt"));       // Alternative --> rptC.FileName = Server.MapPath("~/") + "/App_Reports/CategoryList.rpt";

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                try{
                    Stream stream = rptC.ExportToStream(ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "ListofCategories.pdf");
                }
                catch (Exception ex){
                    throw;
                }
            }
        }
*/

        public void PrintListOfCategories()
        {
            using (ReportDocument rptD = new ReportDocument())
            {
                try {
                    rptD.FileName = Server.MapPath("~/") + "/App_Reports/CategoryList.rpt";
                    //rptC.Load();
                    rptD.SetDatabaseLogon("firman", "123", "(local)", "YSIDGA");
                    rptD.ExportToHttpResponse(
                        ExportFormatType.PortableDocFormat,
                        System.Web.HttpContext.Current.Response, false, "ListofCategories"
                    );
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }
    }
}