using Gapura.BLL.Models;
using Gapura.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class PeriodsController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        public JsonResult IsPeriodNameAvailable(string periodName)
        {
            return Json(!_dbConn.MasterPeriods.Any(PerName => PerName.PeriodName == periodName), JsonRequestBehavior.AllowGet);   
        }

        //
        // GET: /Periods/
        
        public ActionResult Index()
        {
            return View(_dbConn.MasterPeriods.ToList());
        }

        //
        // GET: /Periods/Details/5

        public ActionResult Details(int id = 0)
        {
            MasterPeriod masterPeriod = _dbConn.MasterPeriods.Find(id);
            if (masterPeriod == null)
            {
                return HttpNotFound();
            }
            return View(masterPeriod);
        }

        //
        // GET: /Periods/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Periods/Create
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterPeriod masterPeriod)
        {
            if (ModelState.IsValid)
            {
                _dbConn.MasterPeriods.Add(masterPeriod);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterPeriod);
        }

        //
        // GET: /Periods/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            MasterPeriod masterPeriod = _dbConn.MasterPeriods.Find(id);
            if (masterPeriod == null)
            {
                return HttpNotFound();
            }
            return View(masterPeriod);
        }

        //
        // POST: /Periods/Edit/5
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterPeriod masterPeriod)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(masterPeriod).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterPeriod);
        }

        //
        // GET: /Periods/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            MasterPeriod masterPeriod = _dbConn.MasterPeriods.Find(id);
            if (masterPeriod == null)
            {
                return HttpNotFound();
            }
            return View(masterPeriod);
        }

        //
        // POST: /Periods/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterPeriod masterPeriod = _dbConn.MasterPeriods.Find(id);
            _dbConn.MasterPeriods.Remove(masterPeriod);
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