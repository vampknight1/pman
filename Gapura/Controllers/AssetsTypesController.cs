using Gapura.BLL.Models;
using Gapura.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
   [Authorize(Roles = "Administrator")]
    public class AssetsTypesController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /AssetsTypes/

        public ActionResult Index()
        {
            return View(_dbConn.MasterAssetsTypes.ToList());
        }

        //
        // GET: /AssetsTypes/Details/5

        public ActionResult Details(short id = 0)
        {
            MasterAssetsType masterassetstype = _dbConn.MasterAssetsTypes.Find(id);
            if (masterassetstype == null)
            {
                return HttpNotFound();
            }
            return View(masterassetstype);
        }

        //
        // GET: /AssetsTypes/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AssetsTypes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterAssetsType masterAssetsType)
        {
            if (ModelState.IsValid)
            {
                _dbConn.MasterAssetsTypes.Add(masterAssetsType);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterAssetsType);
        }

        //
        // GET: /AssetsTypes/Edit/5

        public ActionResult Edit(short id = 0)
        {
            MasterAssetsType masterAssetsType = _dbConn.MasterAssetsTypes.Find(id);
            if (masterAssetsType == null)
            {
                return HttpNotFound();
            }
            return View(masterAssetsType);
        }

        //
        // POST: /AssetsTypes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterAssetsType masterAssetsType)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(masterAssetsType).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterAssetsType);
        }

        //
        // GET: /AssetsTypes/Delete/5

        public ActionResult Delete(short id = 0)
        {
            MasterAssetsType masterAssetsType = _dbConn.MasterAssetsTypes.Find(id);
            if (masterAssetsType == null)
            {
                return HttpNotFound();
            }
            return View(masterAssetsType);
        }

        //
        // POST: /AssetsTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            MasterAssetsType masterAssetsType = _dbConn.MasterAssetsTypes.Find(id);
            _dbConn.MasterAssetsTypes.Remove(masterAssetsType);
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