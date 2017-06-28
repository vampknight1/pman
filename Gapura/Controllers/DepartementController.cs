using Gapura.BLL.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class DepartementController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /Departement/

        public ActionResult Index()
        {
            var departements = _dbConn.Departements.Include(d => d.Division);
            return View(departements.ToList());
        }

        //
        // GET: /Departement/Details/5

        public ActionResult Details(int id = 0)
        {
            Departement departement = _dbConn.Departements.Find(id);
            if (departement == null)
            {
                return HttpNotFound();
            }
            return View(departement);
        }

        //
        // GET: /Departement/Create

        public ActionResult Create()
        {
            ViewBag.DivisionID = new SelectList(_dbConn.Divisions, "DivisionID", "DivisionName");
            return View();
        }

        //
        // POST: /Departement/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Departement departement)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Departements.Add(departement);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DivisionID = new SelectList(_dbConn.Divisions, "DivisionID", "DivisionName", departement.DivisionID);
            return View(departement);
        }

        //
        // GET: /Departement/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Departement departement = _dbConn.Departements.Find(id);
            if (departement == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionID = new SelectList(_dbConn.Divisions, "DivisionID", "DivisionName", departement.DivisionID);
            return View(departement);
        }

        //
        // POST: /Departement/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Departement departement)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(departement).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionID = new SelectList(_dbConn.Divisions, "DivisionID", "DivisionName", departement.DivisionID);
            return View(departement);
        }

        //
        // GET: /Departement/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Departement departement = _dbConn.Departements.Find(id);
            if (departement == null)
            {
                return HttpNotFound();
            }
            return View(departement);
        }

        //
        // POST: /Departement/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departement departement = _dbConn.Departements.Find(id);
            _dbConn.Departements.Remove(departement);
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