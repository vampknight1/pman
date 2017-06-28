using Gapura.BLL.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class ReleaseHeaderController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /ReleaseHeader/

        public ActionResult Index()
        {
            return View(_dbConn.ReleaseHeaders.ToList());
        }

        //
        // GET: /ReleaseHeader/Details/5

        public ActionResult Details(int id = 0)
        {
            ReleaseHeader releaseHeader = _dbConn.ReleaseHeaders.Find(id);
            if (releaseHeader == null)
            {
                return HttpNotFound();
            }
            return View(releaseHeader);
        }

        //
        // GET: /ReleaseHeader/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReleaseHeader/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReleaseHeader releaseHeader)
        {
            if (ModelState.IsValid)
            {
                _dbConn.ReleaseHeaders.Add(releaseHeader);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(releaseHeader);
        }

        //
        // GET: /ReleaseHeader/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ReleaseHeader releaseHeader = _dbConn.ReleaseHeaders.Find(id);
            if (releaseHeader == null)
            {
                return HttpNotFound();
            }
            return View(releaseHeader);
        }

        //
        // POST: /ReleaseHeader/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReleaseHeader releaseHeader)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(releaseHeader).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(releaseHeader);
        }

        //
        // GET: /ReleaseHeader/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ReleaseHeader releaseHeader = _dbConn.ReleaseHeaders.Find(id);
            if (releaseHeader == null)
            {
                return HttpNotFound();
            }
            return View(releaseHeader);
        }

        //
        // POST: /ReleaseHeader/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReleaseHeader releaseheader = _dbConn.ReleaseHeaders.Find(id);
            _dbConn.ReleaseHeaders.Remove(releaseheader);
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