using Gapura.BLL.Models;
using Gapura.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class UnitsController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /Units/

        public ActionResult Index()
        {
            return View(_dbConn.MasterUnits.ToList());
        }

        public JsonResult getUnits()
        {
            List<MasterUnit> units = new List<MasterUnit>();
            using (YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn())
            {
                units = dbConn.MasterUnits.OrderBy(u => u.UnitName).ToList();
            }
            return new JsonResult { Data = units, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //
        // GET: /Units/Details/5

        public ActionResult Details(int id = 0)
        {
            MasterUnit masterUnit = _dbConn.MasterUnits.Find(id);
            if (masterUnit == null)
            {
                return HttpNotFound();
            }
            return View(masterUnit);
        }

        //
        // GET: /Units/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Units/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterUnit masterUnit)
        {
            if (ModelState.IsValid)
            {
                _dbConn.MasterUnits.Add(masterUnit);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterUnit);
        }

        //
        // GET: /Units/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MasterUnit masterUnit = _dbConn.MasterUnits.Find(id);
            if (masterUnit == null)
            {
                return HttpNotFound();
            }
            return View(masterUnit);
        }

        //
        // POST: /Units/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterUnit masterUnit)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(masterUnit).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterUnit);
        }

        //
        // GET: /Units/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MasterUnit masterUnit = _dbConn.MasterUnits.Find(id);
            if (masterUnit == null)
            {
                return HttpNotFound();
            }
            return View(masterUnit);
        }

        //
        // POST: /Units/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterUnit masterUnit = _dbConn.MasterUnits.Find(id);
            _dbConn.MasterUnits.Remove(masterUnit);
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