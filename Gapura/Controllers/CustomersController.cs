using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Gapura.BLL.Models;
using Gapura.BLL.ViewModel;
using Gapura.BLL.DAL;

namespace Gapura.Controllers
{
    public class CustomersController : Controller
    {
        //private YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn();     //Fir 09052017
        private ICustomerRepo customerRepo;

        public CustomersController()
        {
            this.customerRepo = new CustomerRepo(new YSIDGAEntitiesConn());
        }

        public CustomersController(ICustomerRepo customerRepo)
        {
            this.customerRepo = customerRepo;
        }

        //
        // GET: /Customers/

        public ActionResult Index()
        {
            //return View(dbConn.Customers.ToList());

            List<Customer> customer = (List<Customer>)customerRepo.GetCustomers();
            return View(customer);
        }

        ///////////////-------------- Load Data by JQuery Fir 26102016 -----------------/////////////
        public ActionResult LoadData()
        {
            using (YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn())
            {
                dbConn.Configuration.LazyLoadingEnabled = false;    // if your table is relational, contain foreign key
                //var data = dbConn.Customers.OrderBy(c => c.CompanyName).ToList();
                //var data = from cu in dbConn.Customers
                //           orderby cu.CompanyName
                //           select new { cu.DepartemenID, cu.CompanyName, cu.ContactName, cu.ContactTitle, cu.Address, cu.Phone, cu.Fax };

                var data = dbConn.SPI_Customers().ToList();

                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }
        ///////////////-----------End Load Data by JQuery Fir 26102016 -----------------/////////////

        //
        // GET: /Customers/Details/5

        public ActionResult Details(string id)
        {
            //Customer customer = dbConn.Customers.Find(id);
            //if (customer == null)
            //{
            //    return HttpNotFound();
            //}

            Customer customer = customerRepo.GetCustomerById(id);
            return View(customer);
        }

        //
        // GET: /Customers/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVM customerVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

                    Customer customer = new Customer();

                    customer.CustomerID = customerVM.CustomerID;
                    customer.CompanyName = customerVM.CompanyName;
                    customer.ContactName = customerVM.ContactName;
                    customer.ContactTitle = customerVM.ContactTitle;
                    customer.Address = customerVM.Address;
                    customer.Phone = customerVM.Phone;
                    customer.Fax = customerVM.Fax;

                    customerRepo.InsertCustomer(customer);        // Real
                    customerRepo.Save();
                    return RedirectToAction("Create");
                    #region Previously
                    //YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn();

                    //dbConn.Customers.Add(customer);
                    //dbConn.SaveChanges();
                    //return RedirectToAction("Create");
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(customerVM);
        }

        //
        // GET: /Customers/Edit/5

        public ActionResult Edit(string id)
        {
            //Customer customer = dbConn.Customers.Find(id);
            //if (customer == null)
            //{
            //    return HttpNotFound();
            //}

            Customer customer = customerRepo.GetCustomerById(id);

            var customerVM = new CustomerVM();
            customerVM.CustomerID = id;
            customerVM.CompanyName = customer.CompanyName;
            customerVM.ContactName = customer.ContactName;
            customerVM.ContactTitle = customer.ContactTitle;
            customerVM.Address = customer.Address;
            customerVM.Phone = customer.Phone;
            customerVM.Fax = customer.Fax;
            
            return View(customerVM);
        }

        //
        // POST: /Customers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerVM customerVM)
        {
            try
            {
#region  -- Using Repository  (Previously) --
                //if (ModelState.IsValid)
                //{
                //    //dbConn.Entry(customer).State = EntityState.Modified;
                //    //dbConn.SaveChanges();
                //    //return RedirectToAction("Index");

                //    customerRepo.UpdateCustomer(customer);
                //    customerRepo.Save();
                //    ViewBag.ResultMessage = "Data has been changed.. ";
                //    return View(customer);
#endregion

                var _dbConn = new YSIDGAEntitiesConn();
                //var customer = _dbConn.Customers.FirstOrDefault(cust => string.Compare(cust.CustomerID, id, true) == 0);
                var customer = customerRepo.GetCustomerById(customerVM.CustomerID);
                if (customer != null)
                {
                    customer.CompanyName = customerVM.CompanyName;
                    customer.ContactName = customerVM.ContactName;
                    customer.ContactTitle = customerVM.ContactTitle;
                    customer.Address = customerVM.Address;
                    customer.Phone = customerVM.Phone;
                    customer.Fax = customerVM.Fax;

                    customerRepo.UpdateCustomer(customer);
                    customerRepo.Save();
                    ViewBag.ResultMessage = "Data has been changed.. ";
                    return View(customerVM);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(customerVM);
        }

        //
        // GET: /Customers/Delete/5

        public ActionResult Delete(string id = null, bool? saveChagesError = false)
        {
            #region Previous ways
            ////Customer customer = dbConn.Customers.Find(id);
            ////if (customer == null)
            ////{
            ////    return HttpNotFound();
            ////}

            //if (saveChagesError.GetValueOrDefault())
            //{
            //    ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            //}

            //Customer customer = customerRepo.GetCustomerById(id);
            //return View(customer);
            #endregion

            var customer = customerRepo.GetCustomerById(id);
            if (customer != null)
            {
                var customerVM = new CustomerVM();
                customerVM.CustomerID = id;
                customerVM.CompanyName = customer.CompanyName;
                customerVM.ContactName = customer.ContactName;
                customerVM.ContactTitle = customer.ContactTitle;
                customerVM.Address = customer.Address;
                customerVM.Phone = customer.Phone;
                customerVM.Fax = customer.Fax;

                return View(customerVM);
            }
            else
            {
                return HttpNotFound();
            }

            #region try use AutoMapper
            //Mapper.CreateMap<Gapura.ViewModel.CustomerVM, Gapura.Models.Customer>();
            //var customerVM = customerRepo.GetCustomerById(id);
            //var customer = Mapper.Map<Gapura.ViewModel.CustomerVM, Gapura.Models.Customer>(customerVM);
            //return View(customer);
            #endregion
        }

        //
        // POST: /Customers/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            bool status = false;

            try
            {
                #region Old Ways
                //Customer customer = dbConn.Customers.Find(id);
                //dbConn.Customers.Remove(customer);
                //dbConn.SaveChanges();
                //return RedirectToAction("Index");
                #endregion

                Customer customer = customerRepo.GetCustomerById(id);
                if (customer != null)
                {
                    customerRepo.DeleteCustomer(id);
                    customerRepo.Save();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new JsonResult { Data = new {status = status} };
        }

        protected override void Dispose(bool disposing)
        {
            //dbConn.Dispose();
            customerRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}