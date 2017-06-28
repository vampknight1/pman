using Gapura.BLL.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class ReleaseDetailController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /ReleaseDetail/

        public ActionResult Index()
        {
            return View(_dbConn.ReleaseDetails.ToList());
        }

        //
        // GET: /ReleaseDetail/Details/5

        public ActionResult Details(int id = 0)
        {
            ReleaseDetail releaseDetail = _dbConn.ReleaseDetails.Find(id);
            if (releaseDetail == null)
            {
                return HttpNotFound();
            }
            return View(releaseDetail);
        }

        //
        // GET: /ReleaseDetail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReleaseDetail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReleaseDetail releaseDetail)
        {
            if (ModelState.IsValid)
            {
                _dbConn.ReleaseDetails.Add(releaseDetail);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(releaseDetail);
        }

        //
        // GET: /ReleaseDetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ReleaseDetail releaseDetail = _dbConn.ReleaseDetails.Find(id);
            if (releaseDetail == null)
            {
                return HttpNotFound();
            }
            return View(releaseDetail);
        }

        //
        // POST: /ReleaseDetail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReleaseDetail releaseDetail)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(releaseDetail).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(releaseDetail);
        }

        //
        // GET: /ReleaseDetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ReleaseDetail releaseDetail = _dbConn.ReleaseDetails.Find(id);
            if (releaseDetail == null)
            {
                return HttpNotFound();
            }
            return View(releaseDetail);
        }

        //
        // POST: /ReleaseDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReleaseDetail releaseDetail = _dbConn.ReleaseDetails.Find(id);
            _dbConn.ReleaseDetails.Remove(releaseDetail);
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