﻿using Gapura.BLL.Models;
using Gapura.BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    public class RequestOrderController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /RequestHeader/

        public ActionResult Index()
        {
            return View(_dbConn.RequestHeaders.ToList());
        }

        ///////////////-------------- Load Data by JQuery Fir 26102016 -----------------/////////////
        public ActionResult LoadData()
        {
            //using (YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn()) {
                _dbConn.Configuration.LazyLoadingEnabled = false;    // if your table is relational, contain foreign key

                var data = _dbConn.SPI_RequestHeader().ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            //}
        }
        ///////////////-----------End Load Data by JQuery Fir 26102016 -----------------/////////////

        public JsonResult Search(string term)
        {
            YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn();
            List<string> requester;

            requester = dbConn.Employees.Where(e => e.LastName.Contains(term)).Select(f => f.LastName).ToList();

            return Json(requester, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getRequester(string term)
        {
            var routeList = _dbConn.Employees.Where(e => e.LastName.Contains(term))
                    .Take(6)
                    .Select(e => new { value = e.EmployeeID, label = e.LastName });
            return Json(routeList, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /RequestHeader/Create

        public ActionResult Create()
        {
            RequestHDVM requestVM = new RequestHDVM();
            //request.RequestID = requestVM.vmRequestHeader.RequestID;

            int iSeq = _dbConn.RequestHeaders.Count();
            string sSeq;

            if (iSeq == 0)
            {
                sSeq = "001";
            }
            else
            {
                sSeq = (from rh in _dbConn.RequestHeaders
                        orderby rh.RequestID descending
                        select rh.ReqSeq
                        ).Take(1).SingleOrDefault();
            }

            int iMonth = DateTime.Now.Month;
            string sMonth = "";

            if (iMonth == 1) { sMonth = "I"; }
            else if (iMonth == 2) { sMonth = "II"; }
            else if (iMonth == 3) { sMonth = "III"; }
            else if (iMonth == 4) { sMonth = "IV"; }
            else if (iMonth == 5) { sMonth = "V"; }
            else if (iMonth == 6) { sMonth = "VI"; }
            else if (iMonth == 7) { sMonth = "VII"; }
            else if (iMonth == 8) { sMonth = "VIII"; }
            else if (iMonth == 9) { sMonth = "IX"; }
            else if (iMonth == 10) { sMonth = "X"; }
            else if (iMonth == 11) { sMonth = "XI"; }
            else if (iMonth == 12) { sMonth = "XII"; }

            //var vSeq = dbConn.RequestHeaders.SqlQuery("SELECT TOP (1) RIGHT('0000' + CAST(RequestID AS varchar(5)) , 3) AS SeqNo, * FROM RequestHeader").SingleOrDefault();
            ////                    001/YSID-ADM/VI/17
            ////                    004/RF/GA/VI/17
            ViewData["RequestNo"] = sSeq + "/RF/GA/" + sMonth + "/" + DateTime.Now.ToString("yy");

            ViewData["RequestDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            ViewData["RequiredDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "DepartemenName");
            //ViewBag.DepartemenID = from c in dbConn.Customers
            //                     select new
            //                     {
            //                         DepartemenID = c.DepartemenID,
            //                         Departemen = c.CompanyName
            //                     };
            ViewBag.RequestTypeID = new SelectList(_dbConn.MasterRequestTypes, "ID", "RequestType");
            ViewBag.CurrencyID = new SelectList(_dbConn.MasterCurrencies, "ID", "CurrencyCode");
            ViewBag.AssetsTypeID = new SelectList(_dbConn.MasterAssetsTypes, "ID", "AssetsType");
            ViewBag.EmployeeID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName");
            ViewBag.MgrID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName");

            return View(requestVM);
        }

        //
        // POST: /RequestHeader/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestHeader requestHeader)
        {
            if (ModelState.IsValid)
            {
                _dbConn.RequestHeaders.Add(requestHeader);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "CompanyName", requestHeader.DepartemenID);
            ViewBag.RequestTypeID = new SelectList(_dbConn.MasterRequestTypes, "ID", "RequestType", requestHeader.RequestTypeID);
            ViewBag.CurrencyID = new SelectList(_dbConn.MasterCurrencies, "ID", "CurrencyCode", requestHeader.CurrencyID);
            ViewBag.AssetsTypeID = new SelectList(_dbConn.MasterAssetsTypes, "ID", "AssetsType", requestHeader.AssetsTypeID);
            ViewBag.EmployeeID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", requestHeader.EmployeeID);
            ViewBag.MgrID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", requestHeader.MgrID);

            return View(requestHeader);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]                      // fir 20/01/2017
        public JsonResult Save(RequestHDVM requestVM)
        {   //System.Diagnostics.Debugger.Break();          
            try
            {
                bool status = false;
                var isValidModel = TryUpdateModel(requestVM);
                if (isValidModel)
                //if (ModelState.IsValid)
                {
                    //using (YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn())
                    //{
                        #region before
                        //RequestHeader reqH = new RequestHeader 
                        //{
                        //    RequestNo = request.RequestH.RequestNo,
                        //    RequestDate = request.RequestH.RequestDate,
                        //    RequiredDate = request.RequestH.RequiredDate,
                        //    ReffNo = request.RequestH.ReffNo,
                        //    RequestTypeID = request.RequestH.RequestTypeID,
                        //    AssetsTypeID = request.RequestH.AssetsTypeID,
                        //    EmployeeID = request.RequestH.EmployeeID,
                        //    MgrID = request.RequestH.MgrID,
                        //    DepartemenID = request.RequestH.DepartemenID,
                        //    CurrencyID = request.RequestH.CurrencyID,
                        //    Remarks = request.RequestH.Remarks,
                        //    TotalRequest = request.RequestH.TotalRequest,
                        //    TotalPrice = request.RequestH.TotalPrice
                        //};

                        //foreach (var i in request.RequestD)
                        //{
                        //    reqH.RequestDetails.Add(i);
                        //}

                        //dbConn.RequestHeaders.Add(reqH);
                        //dbConn.SaveChanges();
                        //status = true;
                        #endregion
                        #region reqD -using VM
                        //int latestRequestID = reqH.RequestID;

                        //RequestDetail reqD = new RequestDetail();
                        //reqD.RequestID = latestRequestID;
                        //reqD.ProductID = request.RequestD.ProductID;
                        //reqD.UnitPrice = request.RequestD.UnitPrice;
                        //reqD.UnitID = request.RequestD.UnitID;
                        //reqD.Quantity = request.RequestD.Quantity;
                        //reqD.Amount = request.RequestD.Amount;
                        //reqD.Remarks = request.RequestD.Remarks;

                        //dbConn.RequestDetails.Add(reqD);
                        //dbConn.SaveChanges();
                        //status = true;
                        #endregion
                        RequestHeader requestH = new RequestHeader();
                        ////request.RequestID = requestVM.vmRequestHeader.RequestID;
                        //requestH.RequestNo = requestVM.vmRequestHeader.RequestNo;
                        //requestH.RequestDate = requestVM.vmRequestHeader.RequestDate;
                        //requestH.RequiredDate = requestVM.vmRequestHeader.RequiredDate;
                        //requestH.ReffNo = requestVM.vmRequestHeader.ReffNo;
                        //requestH.RequestTypeID = requestVM.vmRequestHeader.RequestTypeID;
                        //requestH.AssetsTypeID = requestVM.vmRequestHeader.AssetsTypeID;
                        //requestH.EmployeeID = requestVM.vmRequestHeader.EmployeeID;
                        //requestH.MgrID = requestVM.vmRequestHeader.MgrID;
                        //requestH.DepartemenID = requestVM.vmRequestHeader.DepartemenID;
                        //requestH.CurrencyID = requestVM.vmRequestHeader.CurrencyID;
                        //requestH.Remarks = requestVM.vmRequestHeader.Remarks;
                        //requestH.TotalRequest = requestVM.vmRequestHeader.TotalRequest;
                        //requestH.TotalPrice = requestVM.vmRequestHeader.TotalPrice;
                        //requestH.RequestDetails = requestVM.vmRequestHeader.RequestDetails;

                        //requestVM.vmRequestHeader.RequestID = requestH.RequestID;
                        requestVM.vmRequestHeader.RequestNo = requestH.RequestNo;
                        requestVM.vmRequestHeader.RequestDate = requestH.RequestDate;
                        requestVM.vmRequestHeader.RequiredDate = requestH.RequiredDate;
                        requestVM.vmRequestHeader.ReffNo = requestH.ReffNo;
                        requestVM.vmRequestHeader.RequestTypeID = requestH.RequestTypeID;
                        requestVM.vmRequestHeader.AssetsTypeID = requestH.AssetsTypeID;
                        requestVM.vmRequestHeader.EmployeeID = requestH.EmployeeID;
                        requestVM.vmRequestHeader.MgrID = requestH.MgrID;
                        requestVM.vmRequestHeader.DepartemenID = requestH.DepartemenID;
                        requestVM.vmRequestHeader.CurrencyID = requestH.CurrencyID;
                        requestVM.vmRequestHeader.Remarks = requestH.Remarks;
                        requestVM.vmRequestHeader.TotalRequest = requestH.TotalRequest;
                        requestVM.vmRequestHeader.TotalPrice = requestH.TotalPrice;
                        requestVM.vmRequestHeader.RequestDetails = requestH.RequestDetails;
                        _dbConn.RequestHeaders.Add(requestH);
                        _dbConn.SaveChanges();
                        status = true;
                    //}
                }
                else
                {
                    status = false;
                }
                return new JsonResult { Data = new { status = status } };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //
        // GET: /RequestHeader/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RequestHeader requestHeader = _dbConn.RequestHeaders.Find(id);

            if (requestHeader == null)
            {
                return HttpNotFound();
            }

            ViewData["ReffNo"] = requestHeader.ReffNo;
            ViewBag.RequestNo = requestHeader.RequestNo;
            ViewBag.RequestDate = requestHeader.RequestDate.Date.ToString("yyyy/MM/dd");
            ViewBag.RequiredDate = requestHeader.RequiredDate.Date.ToString("yyyy/MM/dd");
            ViewBag.Remarks = requestHeader.Remarks;
            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "DepartemenName", requestHeader.DepartemenID);
            ViewBag.RequestTypeID = new SelectList(_dbConn.MasterRequestTypes, "ID", "RequestType", requestHeader.RequestTypeID);
            ViewBag.CurrencyID = new SelectList(_dbConn.MasterCurrencies, "ID", "CurrencyCode", requestHeader.CurrencyID);
            ViewBag.AssetsTypeID = new SelectList(_dbConn.MasterAssetsTypes, "ID", "AssetsType", requestHeader.AssetsTypeID);
            ViewBag.EmployeeID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", requestHeader.EmployeeID);
            ViewBag.MgrID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", requestHeader.MgrID);
            ViewData["TotalRequest"] = requestHeader.TotalRequest;
            ViewData["TotalPrice"] = requestHeader.TotalPrice;
            ViewData["RequestDetails"] = _dbConn.Database.SqlQuery<RequestDetailListVM>("EXEC SPI_RequestDetail {0}", id).ToList();

            return View(requestHeader);
        }
        
        //
        // POST: /RequestHeader/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RequestHeader requestHeader)
        {
            //reqHD.RequestDetails = reqHD.RequestDetails.ToList();

            //var requestHD = new RequestHD();
            //requestHD.RequestDetail = dbConn.RequestDetails.ToList();
            //requestHD.RequestHeader = new RequestHeader();

            if (ModelState.IsValid)
            {
                _dbConn.Entry(requestHeader).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ReffNo"] = requestHeader.ReffNo;
            ViewBag.RequestNo = requestHeader.RequestNo;
            ViewBag.RequestDate = requestHeader.RequestDate;
            ViewBag.RequiredDate = requestHeader.RequiredDate;
            ViewBag.Remarks = requestHeader.Remarks;
            ViewBag.DepartemenID = new SelectList(_dbConn.Departements, "DepartemenID", "DepartemenName", requestHeader.DepartemenID);
            ViewBag.RequestTypeID = new SelectList(_dbConn.MasterRequestTypes, "ID", "RequestType", requestHeader.RequestTypeID);
            ViewBag.CurrencyID = new SelectList(_dbConn.MasterCurrencies, "ID", "CurrencyCode", requestHeader.CurrencyID);
            ViewBag.AssetsTypeID = new SelectList(_dbConn.MasterAssetsTypes, "ID", "AssetsType", requestHeader.AssetsTypeID);
            ViewBag.EmployeeID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", requestHeader.EmployeeID);
            ViewBag.MgrID = new SelectList(_dbConn.Employees, "EmployeeID", "LastName", requestHeader.MgrID);
            ViewData["TotalRequest"] = requestHeader.TotalRequest;
            ViewData["TotalPrice"] = requestHeader.TotalPrice;
            ViewData["RequestDetails"] = _dbConn.Database.SqlQuery<RequestDetailListVM>("EXEC SPI_RequestDetail {0}", requestHeader.RequestID).ToList();

            return View(requestHeader);
        }

        #region Try Use RequestHD (ViewModel)
        /*
                public ActionResult Edit(int id = 0)
                {
                    var requestHD = new RequestHD();

                    RequestDetailListVM requestdetail = new RequestDetailListVM();
                    requestHD.RequestDetail = requestdetail;
                    //requestdetail = dbConn.Database.SqlQuery<RequestDetailListVM>("EXEC SPR_RequestDetail {0}", id).ToList();

                    RequestHeader requestheader = new RequestHeader();
                    requestHD.RequestHeader = requestheader;
                    requestheader = dbConn.RequestHeaders.Find(id);

                    if (requestheader == null)
                    {
                        return HttpNotFound();
                    }

                    ViewData["ReffNo"] = requestheader.ReffNo;
                    ViewBag.RequestNo = requestheader.RequestNo;
                    ViewBag.RequestDate = requestheader.RequestDate.Date.ToString("yyyy/MM/dd");
                    ViewBag.RequiredDate = requestheader.RequiredDate.Date.ToString("yyyy/MM/dd");
                    ViewBag.Remarks = requestheader.Remarks;
                    ViewBag.DepartemenID = new SelectList(dbConn.Customers, "DepartemenID", "CompanyName", requestheader.DepartemenID);
                    ViewBag.RequestTypeID = new SelectList(dbConn.MasterRequestTypes, "ID", "RequestType", requestheader.RequestTypeID);
                    ViewBag.CurrencyID = new SelectList(dbConn.MasterCurrencies, "ID", "CurrencyCode", requestheader.CurrencyID);
                    ViewBag.AssetsTypeID = new SelectList(dbConn.MasterAssetsTypes, "ID", "AssetsType", requestheader.AssetsTypeID);
                    ViewBag.EmployeeID = new SelectList(dbConn.Employees, "EmployeeID", "LastName", requestheader.EmployeeID);
                    ViewBag.MgrID = new SelectList(dbConn.Employees, "ReportsTo", "LastName", requestheader.MgrID);
                    ViewData["TotalRequest"] = requestheader.TotalRequest;
                    ViewData["TotalPrice"] = requestheader.TotalPrice;

                    //return View(requestheader);
                    return View(requestHD); // Update + List Req
                }
        */
        /*
                [HttpPost]                  //Try ViewModel in Update
                [ValidateAntiForgeryToken]
                public ActionResult Edit( RequestHD reqHD)
                {
                    if (ModelState.IsValid)
                    {
                        using (var context = new YSIDGAEntitiesConn())
                        {
                            reqHD.RequestHeader.RequestDetails = reqHD.RequestDetail.ToList();

                            var requestHD = new RequestHD();
                            requestHD.RequestDetail = dbConn.RequestDetails.ToList();
                            requestHD.RequestHeader = new RequestHeader();

                            var reqHeader = new RequestHeader
                            {
                                RequestID = reqHD.RequestHeader.RequestID,
                                RequestNo = reqHD.RequestHeader.RequestNo,
                                DepartemenID = reqHD.RequestHeader.DepartemenID,
                                RequestDate = reqHD.RequestHeader.RequestDate,
                                RequiredDate = reqHD.RequestHeader.RequiredDate,
                                TotalRequest = reqHD.RequestHeader.TotalRequest,
                                TotalPrice = reqHD.RequestHeader.TotalPrice,
                                ReffNo = reqHD.RequestHeader.ReffNo,
                                RequestTypeID = reqHD.RequestHeader.RequestTypeID,
                                EmployeeID = reqHD.RequestHeader.EmployeeID,
                                MgrID = reqHD.RequestHeader.MgrID,
                                CurrencyID = reqHD.RequestHeader.CurrencyID,
                                AssetsTypeID = reqHD.RequestHeader.AssetsTypeID,
                                Remarks = reqHD.RequestHeader.Remarks,
                                RequestDetails = reqHD.RequestHeader.RequestDetails.ToList()
                            };
                            var reqDetail = new RequestDetail();

                            //context.RequestHeaders.Add(reqHeader);
                            context.Entry(reqHeader).State = EntityState.Modified;
                            reqDetail.RequestID = reqHeader.RequestID;

                            //context.RequestDetails.Add(reqDetail);
                            context.Entry(reqHeader).State = EntityState.Modified;

                            //reqHD.RequestHeader.RequestDetails = reqHD.RequestDetail;
                            context.Entry(reqHD).State = EntityState.Modified;
                            context.SaveChanges();
                        }

                        //dbConn.RequestHeaders.Add(reqHeader);
                        //reqDetail.RequestID = reqHeader.RequestID;
                        //dbConn.RequestDetails.Add(reqDetail);

                        //dbConn.Entry(requestHeaderDetail).State = EntityState.Modified;
                        //dbConn.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    //ViewBag.RequestNo = reqHD.RequestHeader.RequestNo;
                    ViewData["ReffNo"] = reqHD.RequestHeader.RequestNo;
                    ViewData["RequestDate"] = reqHD.RequestHeader.RequestDate;
                    ViewData["RequiredDate"] = reqHD.RequestHeader.RequiredDate;
                    ViewBag.Remarks = reqHD.RequestHeader.Remarks;
                    ViewBag.DepartemenID = new SelectList(dbConn.Customers, "DepartemenID", "CompanyName", reqHD.RequestHeader.DepartemenID);
                    ViewBag.RequestTypeID = new SelectList(dbConn.MasterRequestTypes, "ID", "RequestType", reqHD.RequestHeader.RequestTypeID);
                    ViewBag.CurrencyID = new SelectList(dbConn.MasterCurrencies, "ID", "CurrencyCode", reqHD.RequestHeader.CurrencyID);
                    ViewBag.AssetsTypeID = new SelectList(dbConn.MasterAssetsTypes, "ID", "AssetsType", reqHD.RequestHeader.AssetsTypeID);
                    ViewBag.EmployeeID = new SelectList(dbConn.Employees, "EmployeeID", "LastName", reqHD.RequestHeader.EmployeeID);
                    ViewBag.MgrID = new SelectList(dbConn.Employees, "ReportsTo", "LastName", reqHD.RequestHeader.MgrID);
                    //ViewBag["TotalRequest"] = reqHD.RequestHeader.TotalRequest;
                    //ViewBag["TotalPrice"] = reqHD.RequestHeader.TotalPrice;
                    ViewBag.TotalRequest = reqHD.RequestHeader.TotalRequest;
                    ViewBag.TotalPrice = reqHD.RequestHeader.TotalPrice;

                    return View(reqHD);
                }
        */
        #endregion

        //
        // GET: /RequestHeader/Details/5

        [HttpPost]
        public ActionResult Details(int? RequestID)
        {
            //RequestHeader requestheader = dbConn.RequestHeaders.Find(id);
            //if (requestheader == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(requestheader);

            YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn();

            return PartialView("_Details", dbConn.RequestDetails.Find(RequestID));
        }

        //
        // GET: /RequestHeader/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RequestHeader requestHeader = _dbConn.RequestHeaders.Find(id);
            if (requestHeader == null)
            {
                return HttpNotFound();
            }
            return View(requestHeader);
        }

        //
        // POST: /RequestHeader/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                RequestHeader requestHeader = _dbConn.RequestHeaders.Find(id);
                _dbConn.RequestHeaders.Remove(requestHeader);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            _dbConn.Dispose();
            base.Dispose(disposing);
        }
    }
}