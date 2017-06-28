using Gapura.BLL.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class TermOfPaysController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /TermOfPays/

        public ActionResult Index()
        {
            return View(_dbConn.TermOfPays.ToList());
        }

        //
        // GET: /TermOfPays/Details/5

        public ActionResult Details(short id = 0)
        {
            TermOfPay termOfPay = _dbConn.TermOfPays.Find(id);
            if (termOfPay == null)
            {
                return HttpNotFound();
            }
            return View(termOfPay);
        }

        //
        // GET: /TermOfPays/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TermOfPays/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TermOfPay termOfPay)
        {
            if (ModelState.IsValid)
            {
                _dbConn.TermOfPays.Add(termOfPay);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(termOfPay);
        }

        //
        // GET: /TermOfPays/Edit/5

        public ActionResult Edit(short id = 0)
        {
            TermOfPay termOfPay = _dbConn.TermOfPays.Find(id);
            if (termOfPay == null)
            {
                return HttpNotFound();
            }
            return View(termOfPay);
        }

        //
        // POST: /TermOfPays/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TermOfPay termOfPay)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(termOfPay).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(termOfPay);
        }

        //
        // GET: /TermOfPays/Delete/5

        public ActionResult Delete(short id = 0)
        {
            TermOfPay termOfPay = _dbConn.TermOfPays.Find(id);
            if (termOfPay == null)
            {
                return HttpNotFound();
            }
            return View(termOfPay);
        }

        //
        // POST: /TermOfPays/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            TermOfPay termOfPay = _dbConn.TermOfPays.Find(id);
            _dbConn.TermOfPays.Remove(termOfPay);
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