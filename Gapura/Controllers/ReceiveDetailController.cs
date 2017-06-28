using Gapura.BLL.Models;
using Gapura.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class ReceiveDetailController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /ReceiveDetail/

        public ActionResult Index()
        {
            return View(_dbConn.ReceiveDetails.ToList());
        }

        //
        // GET: /ReceiveDetail/Details/5

        public ActionResult Details(int id = 0)
        {
            ReceiveDetail receiveDetail = _dbConn.ReceiveDetails.Find(id);
            if (receiveDetail == null)
            {
                return HttpNotFound();
            }
            return View(receiveDetail);
        }

        //
        // GET: /ReceiveDetail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReceiveDetail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReceiveDetail receiveDetail)
        {
            if (ModelState.IsValid)
            {
                _dbConn.ReceiveDetails.Add(receiveDetail);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(receiveDetail);
        }

        //
        // GET: /ReceiveDetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ReceiveDetail receiveDetail = _dbConn.ReceiveDetails.Find(id);
            if (receiveDetail == null)
            {
                return HttpNotFound();
            }
            return View(receiveDetail);
        }

        //
        // POST: /ReceiveDetail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReceiveDetail receiveDetail)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(receiveDetail).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(receiveDetail);
        }

        //
        // GET: /ReceiveDetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ReceiveDetail receiveDetail = _dbConn.ReceiveDetails.Find(id);
            if (receiveDetail == null)
            {
                return HttpNotFound();
            }
            return View(receiveDetail);
        }

        //
        // POST: /ReceiveDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReceiveDetail receiveDetail = _dbConn.ReceiveDetails.Find(id);
            _dbConn.ReceiveDetails.Remove(receiveDetail);
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