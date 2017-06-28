using Gapura.BLL.Models;
using Gapura.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class DivisionController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /Division/

        public ActionResult Index()
        {
            return View(_dbConn.Divisions.ToList());
        }

        //
        // GET: /Division/Details/5

        public ActionResult Details(int id = 0)
        {
            Division division = _dbConn.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        //
        // GET: /Division/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Division/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Division division)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Divisions.Add(division);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(division);
        }

        //
        // GET: /Division/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Division division = _dbConn.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        //
        // POST: /Division/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Division division)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(division).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(division);
        }

        //
        // GET: /Division/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Division division = _dbConn.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        //
        // POST: /Division/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Division division = _dbConn.Divisions.Find(id);
            _dbConn.Divisions.Remove(division);
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