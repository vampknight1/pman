using Gapura.BLL.Models;
using Gapura.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class MasterTitleController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /MasterTitle/

        public ActionResult Index()
        {
            return View(_dbConn.MasterTitles.ToList());
        }

        //
        // GET: /MasterTitle/Details/5

        public ActionResult Details(int id = 0)
        {
            MasterTitle masterTitle = _dbConn.MasterTitles.Find(id);
            if (masterTitle == null)
            {
                return HttpNotFound();
            }
            return View(masterTitle);
        }

        //
        // GET: /MasterTitle/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MasterTitle/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterTitle masterTitle)
        {
            if (ModelState.IsValid)
            {
                _dbConn.MasterTitles.Add(masterTitle);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterTitle);
        }

        //
        // GET: /MasterTitle/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MasterTitle masterTitle = _dbConn.MasterTitles.Find(id);
            if (masterTitle == null)
            {
                return HttpNotFound();
            }
            return View(masterTitle);
        }

        //
        // POST: /MasterTitle/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterTitle masterTitle)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(masterTitle).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterTitle);
        }

        //
        // GET: /MasterTitle/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MasterTitle masterTitle = _dbConn.MasterTitles.Find(id);
            if (masterTitle == null)
            {
                return HttpNotFound();
            }
            return View(masterTitle);
        }

        //
        // POST: /MasterTitle/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterTitle masterTitle = _dbConn.MasterTitles.Find(id);
            _dbConn.MasterTitles.Remove(masterTitle);
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