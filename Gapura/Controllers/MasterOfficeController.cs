using Gapura.BLL.Models;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class MasterOfficeController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /MasterOffice/

        public ActionResult Index()
        {
            return View(_dbConn.MasterOffices.ToList());
        }

        //
        // GET: /MasterOffice/Details/5

        public ActionResult Details(int id = 0)
        {
            MasterOffice masterOffice = _dbConn.MasterOffices.Find(id);
            if (masterOffice == null)
            {
                return HttpNotFound();
            }
            return View(masterOffice);
        }

        //
        // GET: /MasterOffice/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MasterOffice/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterOffice masterOffice)
        {
            if (ModelState.IsValid)
            {
                _dbConn.MasterOffices.Add(masterOffice);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterOffice);
        }

        //
        // GET: /MasterOffice/Edit/5

        public ActionResult Edit(int? id)
        {
            MasterOffice masterOffice = _dbConn.MasterOffices.Find(id);
            if (masterOffice == null)
            {
                return HttpNotFound();
            }
            return View(masterOffice);
        }

        //
        // POST: /MasterOffice/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(MasterOffice masteroffice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        dbConn.Entry(masteroffice).State = EntityState.Modified;
        //        dbConn.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(masteroffice);
        //}

        [HttpPost]
        public ActionResult Edit(int? id, MasterOffice masterOfficeDetails)
        {
            try
            {
                var dbConn = new YSIDGAEntitiesConn();
                var masterOffice = dbConn.MasterOffices.FirstOrDefault(mo => mo.OfficeID == id);
                if (masterOffice != null)
                {
                    masterOffice.OfficeCode = masterOfficeDetails.OfficeCode;
                    masterOffice.OfficeName = masterOfficeDetails.OfficeName;
                    masterOffice.Address = masterOfficeDetails.Address;
                    masterOffice.City = masterOfficeDetails.City;
                    masterOffice.Region = masterOfficeDetails.Region;
                    masterOffice.Phone = masterOfficeDetails.Phone;
                    dbConn.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MasterOffice/Delete/5
        /*
                public ActionResult Delete(int id = 0)
                {
                    MasterOffice masteroffice = dbConn.MasterOffices.Find(id);
                    if (masteroffice == null)
                    {
                        return HttpNotFound();
                    }
                    return View(masteroffice);
                }
        */
        //
        // POST: /MasterOffice/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                MasterOffice masterOffice = _dbConn.MasterOffices.Find(id);
                _dbConn.MasterOffices.Remove(masterOffice);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { 
                return View(); 
            }
        }

        protected override void Dispose(bool disposing)
        {
            _dbConn.Dispose();
            base.Dispose(disposing);
        }
    }
}