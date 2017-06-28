using Gapura.BLL.Models;
using Gapura.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class CurrenciesController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /Currencies/

        public ActionResult Index()
        {
            return View(_dbConn.MasterCurrencies.ToList());
        }

        //
        // GET: /Currencies/Details/5

        public ActionResult Details(short id = 0)
        {
            MasterCurrency masterCurrency = _dbConn.MasterCurrencies.Find(id);
            if (masterCurrency == null)
            {
                return HttpNotFound();
            }
            return View(masterCurrency);
        }

        //
        // GET: /Currencies/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Currencies/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterCurrency masterCurrency)
        {
            if (ModelState.IsValid)
            {
                _dbConn.MasterCurrencies.Add(masterCurrency);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterCurrency);
        }

        //
        // GET: /Currencies/Edit/5

        public ActionResult Edit(short id = 0)
        {
            MasterCurrency masterCurrency = _dbConn.MasterCurrencies.Find(id);
            if (masterCurrency == null)
            {
                return HttpNotFound();
            }
            return View(masterCurrency);
        }

        //
        // POST: /Currencies/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterCurrency masterCurrency)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(masterCurrency).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterCurrency);
        }

        //
        // GET: /Currencies/Delete/5

        public ActionResult Delete(short id = 0)
        {
            MasterCurrency masterCurrency = _dbConn.MasterCurrencies.Find(id);
            if (masterCurrency == null)
            {
                return HttpNotFound();
            }
            return View(masterCurrency);
        }

        //
        // POST: /Currencies/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            MasterCurrency masterCurrency = _dbConn.MasterCurrencies.Find(id);
            _dbConn.MasterCurrencies.Remove(masterCurrency);
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