using Gapura.BLL.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class RequestDetailController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /RequestDetail/

        public ActionResult Index(int? id)      // int? => Param. will Optional / Nullable
        {
            ////return View(dbConn.RequestDetails.Where(rd => rd.RequestID == id).ToList());

            ViewData["ProductName"] = (from rd in _dbConn.RequestDetails
                                       join p in _dbConn.Products on rd.ProductID equals p.ProductID
                                       where rd.RequestID == id
                                       select p.ProductName).ToList();

            ViewData["UnitName"] = (from rd in _dbConn.RequestDetails
                                    join p in _dbConn.MasterUnits on rd.UnitID equals p.UnitID
                                    where rd.RequestID == id
                                    select p.UnitName).ToList();

            //var RequestDetailList = (from rd in dbConn.RequestDetails                                 // Using Linq
            //                         join p in dbConn.Products on rd.ProductID equals p.ProductID
            //                         join mu in dbConn.MasterUnits on rd.UnitID equals mu.UnitID
            //                         where rd.RequestID == id
            //                         select new
            //                         {
            //                             RequestDetailID = rd.RequestDetailID,
            //                             RequestID = rd.RequestID,
            //                             ProductName = p.ProductName,
            //                             UnitPrice = rd.UnitPrice,
            //                             UnitName = mu.UnitName,
            //                             Quantity = rd.Quantity,
            //                             Amount = rd.Amount,
            //                             Remarks = rd.Remarks
            //                         }).Select(a => new RequestDetailListVM
            //                         {
            //                             RequestDetailID = a.RequestDetailID,
            //                             RequestID = a.RequestID,
            //                             ProductName = a.ProductName,
            //                             UnitPrice = a.UnitPrice,
            //                             UnitName = a.UnitName,
            //                             Quantity = a.Quantity,
            //                             Amount = a.Amount,
            //                             Remarks = a.Remarks
            //                         }).ToList();

            //return View(RequestDetailList);
            
            return View     // using SP
            (
                _dbConn.Database.SqlQuery<RequestDetailListVM>("EXEC SPR_RequestDetail {0}", id).ToList()
            );
        }

        //
        // GET: /RequestDetail/Details/5

        public ActionResult Details(int id = 0)
        {
            RequestDetail requestDetail = _dbConn.RequestDetails.Find(id);
            if (requestDetail == null)
            {
                return HttpNotFound();
            }
            return View(requestDetail);
        }

        //
        // GET: /RequestDetail/Create

        public ActionResult Create()
        {
            //return View();
            // Test Bulk Req. Detail
            List<RequestDetail> reqDet = new List<RequestDetail> { new RequestDetail { RequestDetailID = 0, RequestID = 0, ProductID = 0, UnitPrice = 0, UnitID = 0, Quantity = 0, Amount = 0, Remarks = "" } };
            return View(reqDet);
        }

        //
        // POST: /RequestDetail/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(RequestDetail requestdetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        dbConn.RequestDetails.Add(requestdetail);
        //        dbConn.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(requestdetail);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<RequestDetail> reqDet)
        {
            if (ModelState.IsValid)
            {
                using (YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn())
                {
                    foreach (var i in reqDet)
                    {
                        dbConn.RequestDetails.Add(i);
                    }
                    dbConn.SaveChanges();
                    ViewBag.Message = "Request Item Successfully saved !!";
                    ModelState.Clear();
                    reqDet = new List<RequestDetail> { new RequestDetail { RequestDetailID = 0, RequestID = 0, ProductID = 0, UnitPrice = 0, UnitID = 0, Quantity = 0, Amount = 0, Remarks = "" } };
                }
            }
            return View(reqDet);
        }

        //
        // GET: /RequestDetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RequestDetail requestDetail = _dbConn.RequestDetails.Find(id);
            if (requestDetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductID = new SelectList(_dbConn.Products, "ProductID", "ProductName", requestDetail.ProductID);
            ViewBag.UnitID = new SelectList(_dbConn.MasterUnits, "UnitID", "UnitName", requestDetail.UnitID);

            return View(requestDetail);
        }

        //
        // POST: /RequestDetail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RequestDetail requestDetail)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(requestDetail).State = EntityState.Modified;
                _dbConn.SaveChanges();
                //return RedirectToAction("Index");
                return Redirect(Request.UrlReferrer.ToString());
            }

            ViewBag.ProductID = new SelectList(_dbConn.Products, "ProductID", "ProductName", requestDetail.ProductID);
            ViewBag.UnitID = new SelectList(_dbConn.MasterUnits, "UnitID", "UnitName", requestDetail.UnitID);

            return View(requestDetail);
        }

        //
        // GET: /RequestDetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RequestDetail requestDetail = _dbConn.RequestDetails.Find(id);
            if (requestDetail == null)
            {
                return HttpNotFound();
            }
            return View(requestDetail);
        }

        //
        // POST: /RequestDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestDetail requestDetail = _dbConn.RequestDetails.Find(id);
            _dbConn.RequestDetails.Remove(requestDetail);
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