using Gapura.BLL.Models;
using Gapura.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class PODetailController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /PODetail/

        public ActionResult Index()
        {
            var poDetails = _dbConn.PODetails.Include(p => p.POHeader);
            return View(poDetails.ToList());
        }

        //
        // GET: /PODetail/Details/5

        public ActionResult Details(int id = 0)
        {
            PODetail poDetails = _dbConn.PODetails.Find(id);
            if (poDetails == null)
            {
                return HttpNotFound();
            }
            return View(poDetails);
        }

        //
        // GET: /PODetail/Create

        public ActionResult Create()
        {
            ViewBag.POID = new SelectList(_dbConn.POHeaders, "POID", "RequestID");
            return View();
        }

        //
        // POST: /PODetail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PODetail poDetails)
        {
            if (ModelState.IsValid)
            {
                _dbConn.PODetails.Add(poDetails);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.POID = new SelectList(_dbConn.POHeaders, "POID", "RequestID", poDetails.POID);
            return View(poDetails);
        }

        //
        // GET: /PODetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PODetail poDetails = _dbConn.PODetails.Find(id);
            if (poDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.POID = new SelectList(_dbConn.POHeaders, "POID", "RequestID", poDetails.POID);
            return View(poDetails);
        }

        //
        // POST: /PODetail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PODetail poDetails)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(poDetails).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.POID = new SelectList(_dbConn.POHeaders, "POID", "RequestID", poDetails.POID);
            return View(poDetails);
        }

        //
        // GET: /PODetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PODetail poDetails = _dbConn.PODetails.Find(id);
            if (poDetails == null)
            {
                return HttpNotFound();
            }
            return View(poDetails);
        }

        //
        // POST: /PODetail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PODetail poDetails = _dbConn.PODetails.Find(id);
            _dbConn.PODetails.Remove(poDetails);
            _dbConn.SaveChanges();
            //return RedirectToAction("Index");
            //return Redirect(Request.UrlReferrer.ToString());
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            _dbConn.Dispose();
            base.Dispose(disposing);
        }
    }
}