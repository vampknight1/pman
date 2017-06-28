using Gapura.BLL.Models;
using Gapura.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class TransTypesController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /TransTypes/

        public ActionResult Index()
        {
            return View(_dbConn.MasterTransTypes.ToList());
        }

        //
        // GET: /TransTypes/Details/5

        public ActionResult Details(short id = 0)
        {
            MasterTransType masterTransType = _dbConn.MasterTransTypes.Find(id);
            if (masterTransType == null)
            {
                return HttpNotFound();
            }
            return View(masterTransType);
        }

        //
        // GET: /TransTypes/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TransTypes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterTransType masterTransType)
        {
            if (ModelState.IsValid)
            {
                _dbConn.MasterTransTypes.Add(masterTransType);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterTransType);
        }

        //
        // GET: /TransTypes/Edit/5

        public ActionResult Edit(short id = 0)
        {
            MasterTransType masterTransType = _dbConn.MasterTransTypes.Find(id);
            if (masterTransType == null)
            {
                return HttpNotFound();
            }
            return View(masterTransType);
        }

        //
        // POST: /TransTypes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterTransType masterTransType)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(masterTransType).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterTransType);
        }

        //
        // GET: /TransTypes/Delete/5

        public ActionResult Delete(short id = 0)
        {
            MasterTransType masterTransType = _dbConn.MasterTransTypes.Find(id);
            if (masterTransType == null)
            {
                return HttpNotFound();
            }
            return View(masterTransType);
        }

        //
        // POST: /TransTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            MasterTransType masterTransType = _dbConn.MasterTransTypes.Find(id);
            _dbConn.MasterTransTypes.Remove(masterTransType);
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