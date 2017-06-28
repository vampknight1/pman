using Gapura.BLL.Models;
using Gapura.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class RequestTypesController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /RequestTypes/

        public ActionResult Index()
        {
            return View(_dbConn.MasterRequestTypes.ToList());
        }

        //
        // GET: /RequestTypes/Details/5

        public ActionResult Details(short id = 0)
        {
            MasterRequestType masterRequestType = _dbConn.MasterRequestTypes.Find(id);
            if (masterRequestType == null)
            {
                return HttpNotFound();
            }
            return View(masterRequestType);
        }

        //
        // GET: /RequestTypes/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RequestTypes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterRequestType masterRequestType)
        {
            if (ModelState.IsValid)
            {
                _dbConn.MasterRequestTypes.Add(masterRequestType);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterRequestType);
        }

        //
        // GET: /RequestTypes/Edit/5

        public ActionResult Edit(short id = 0)
        {
            MasterRequestType masterRequestType = _dbConn.MasterRequestTypes.Find(id);
            if (masterRequestType == null)
            {
                return HttpNotFound();
            }
            return View(masterRequestType);
        }

        //
        // POST: /RequestTypes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterRequestType masterRequestType)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(masterRequestType).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterRequestType);
        }

        //
        // GET: /RequestTypes/Delete/5

        public ActionResult Delete(short id = 0)
        {
            MasterRequestType masterRequestType = _dbConn.MasterRequestTypes.Find(id);
            if (masterRequestType == null)
            {
                return HttpNotFound();
            }
            return View(masterRequestType);
        }

        //
        // POST: /RequestTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            MasterRequestType masterRequestType = _dbConn.MasterRequestTypes.Find(id);
            _dbConn.MasterRequestTypes.Remove(masterRequestType);
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