using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Gapura.BLL.Models;
using Gapura.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            var employees = _dbConn.Employees.Include(e => e.Employee1);
            return View(employees.ToList());
        }

        ///////////////-------------- Load Data by JQuery Fir 26102016 -----------------/////////////
        public ActionResult LoadData()
        {
            //using (YSIDGAEntitiesConn dc = new YSIDGAEntitiesConn())
            {
                // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
                _dbConn.Configuration.LazyLoadingEnabled = false;

                var data = _dbConn.SPI_Employee().ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }
        ///////////////-----------End Load Data by JQuery Fir 26102016 -----------------/////////////

        public JsonResult Upload(Employee employee)
        {

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;

                foreach (string filePhoto in Request.Files)
                {
                    //To save file, use SaveAs method
                    file.SaveAs(Server.MapPath("~/UploadFiles/Photo/") + fileName); //File will be saved in exp. root "~/"

                    var postedFile = Request.Files[filePhoto];
                    //postedFile.SaveAs(Server.MapPath("~/img/Photo/") + Path.GetFileName(postedFile.FileName));
                    employee.PhotoPath = "~/UploadFiles/Photo/" + Path.GetFileName(postedFile.FileName);
                }

                ////To save file, use SaveAs method
                //file.SaveAs(Server.MapPath("~/img/Photo/") + fileName); //File will be saved in exp. root "~/"
            }
            return Json("Uploaded " + Request.Files.Count + " files");
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = _dbConn.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            ViewData["PhotoPath"] = "~/UploadFiles/Photo/";
            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "DepartemenName");
            ViewBag.OfficeID = new SelectList(_dbConn.MasterOffices, "OfficeID", "OfficeCode");
            ViewBag.ReportsTo = new SelectList(_dbConn.Employees, "EmployeeID", "LastName");
            ViewBag.TitleID = new SelectList(_dbConn.MasterTitles, "TitleID", "TitleName");
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (Request.Files.Count > 0)
            {
                //HttpPostedFileBase file = Request.Files[0];
                foreach (string file in Request.Files)
                {
                    var postedFile = Request.Files[file];
                    //postedFile.SaveAs(Server.MapPath("~/img/Photo/") + Path.GetFileName(postedFile.FileName));
                    employee.PhotoPath = "~/UploadFiles/Photo/" + Path.GetFileName(postedFile.FileName);
                }
            }
            else
            {
                ViewBag.Message = "Please select the file !";
            }

            if (ModelState.IsValid)
            {
                _dbConn.Employees.Add(employee);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["RequestDate"] = employee.BirthDate.Value.Date.ToString("yyyy/MM/dd");
            ViewData["RequiredDate"] = employee.HireDate.Value.Date.ToString("yyyy/MM/dd");
            ViewBag.Address = employee.Address;
            ViewBag.Notes = employee.Notes;
            ViewBag.PhotoPath = employee.PhotoPath;
            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "DepartemenName", employee.DepartemenID);
            ViewBag.OfficeID = new SelectList(_dbConn.MasterOffices, "OfficeID", "OfficeCode", employee.OfficeID);
            ViewBag.ReportsTo = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", employee.ReportsTo);
            ViewBag.TitleID = new SelectList(_dbConn.MasterTitles, "TitleID", "TitleName", employee.TitleID);
            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employee employee = _dbConn.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.Departemen = (from e in _dbConn.Employees
                                  join ca in _dbConn.Departements on e.DepartemenID equals ca.DepartemenID
                                  where e.EmployeeID == id
                                  select new
                                  {
                                      DepartemenID = e.DepartemenID,
                                      CompanyName = ca.DepartemenName
                                  }).ToList();

            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "DepartemenName", employee.DepartemenID);
            ViewBag.OfficeID = new SelectList(_dbConn.MasterOffices, "OfficeID", "OfficeCode", employee.OfficeID);
            ViewBag.ReportsTo = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", employee.ReportsTo);
            ViewBag.TitleID = new SelectList(_dbConn.MasterTitles, "TitleID", "TitleName", employee.TitleID);

            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {    
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string oldPhotoPath = employee.PhotoPath;
                    HttpPostedFileBase file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/UploadFiles/Photo/"), fileName);
                        file.SaveAs(path);

                        employee.PhotoPath = "/UploadFiles/Photo/" + file.FileName;
                        string fullPath = Request.MapPath("~" + oldPhotoPath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                    else
                    {
                        ViewData["PhotoPath"] = employee.PhotoPath;
                    }
                }

                _dbConn.Entry(employee).State = EntityState.Modified;
                _dbConn.SaveChanges();
                //return RedirectToAction("Index");
                return Redirect(Request.UrlReferrer.ToString());
            }

            //ViewData["PhotoPath"] = employee.PhotoPath.ToString();
            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "DepartemenName", employee.DepartemenID);
            ViewBag.OfficeID = new SelectList(_dbConn.MasterOffices, "OfficeID", "OfficeCode", employee.OfficeID);
            ViewBag.ReportsTo = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", employee.ReportsTo);
            ViewBag.TitleID = new SelectList(_dbConn.MasterTitles, "TitleID", "TitleName", employee.TitleID);
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Employee employee = _dbConn.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _dbConn.Employees.Find(id);
            _dbConn.Employees.Remove(employee);
            _dbConn.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _dbConn.Dispose();
            base.Dispose(disposing);
        }

        public void PrintListOfEmployees()
        {
            using (ReportDocument rptD = new ReportDocument())
            {
                try
                {
                    rptD.FileName = Server.MapPath("~/") + "/App_Reports/EmployeeList.rpt";
                    //rptC.Load();
                    rptD.SetDatabaseLogon("firman", "123", "(local)", "YSIDGA");
                    rptD.ExportToHttpResponse(
                        ExportFormatType.PortableDocFormat,
                        System.Web.HttpContext.Current.Response, false, "ListofEmployees"
                    );
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }
    }
}