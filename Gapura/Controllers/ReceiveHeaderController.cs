using Gapura.BLL.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class ReceiveHeaderController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /ReceiveHeader/

        public ActionResult Index()
        {
            return View(_dbConn.ReceiveHeaders.ToList());
        }

        //
        // GET: /ReceiveHeader/Details/5

        public ActionResult Details(int id = 0)
        {
            ReceiveHeader receiveHeader = _dbConn.ReceiveHeaders.Find(id);
            if (receiveHeader == null)
            {
                return HttpNotFound();
            }
            return View(receiveHeader);
        }

        //
        // GET: /ReceiveHeader/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReceiveHeader/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReceiveHeader receiveHeader)
        {
            if (ModelState.IsValid)
            {
                _dbConn.ReceiveHeaders.Add(receiveHeader);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(receiveHeader);
        }

        //
        // GET: /ReceiveHeader/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ReceiveHeader receiveHeader = _dbConn.ReceiveHeaders.Find(id);
            if (receiveHeader == null)
            {
                return HttpNotFound();
            }
            return View(receiveHeader);
        }

        //
        // POST: /ReceiveHeader/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReceiveHeader receiveHeader)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(receiveHeader).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(receiveHeader);
        }

        //
        // GET: /ReceiveHeader/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ReceiveHeader receiveHeader = _dbConn.ReceiveHeaders.Find(id);
            if (receiveHeader == null)
            {
                return HttpNotFound();
            }
            return View(receiveHeader);
        }

        //
        // POST: /ReceiveHeader/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReceiveHeader receiveHeader = _dbConn.ReceiveHeaders.Find(id);
            _dbConn.ReceiveHeaders.Remove(receiveHeader);
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