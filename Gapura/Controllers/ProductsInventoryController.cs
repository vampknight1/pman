using Gapura.BLL.Models;
using Gapura.BLL.ViewModel;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Gapura.Controllers
{
    [Authorize]
    public class ProductsInventoryController : Controller
    {
        private YSIDGAEntitiesConn _dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /ProductsInventory/

        public ActionResult Index()
        {
            return View(_dbConn.ProductsInventories.ToList());
        }

        ///////////////-------------- Load Data by JQuery Fir 26102016 -----------------/////////////
        public ActionResult LoadData()
        {
            //using (YSIDGAEntitiesConn dc = new YSIDGAEntitiesConn())
            {
                _dbConn.Configuration.LazyLoadingEnabled = false;    // if your table is relational, contain foreign key
                #region Other Ways
                ////var data = dbConn.Products.OrderBy(p => p.ProductName).ToList();
                //var data = from pi in dbConn.ProductsInventories
                //           join p in dbConn.Products on pi.ProductID equals p.ProductID
                //           join cu in dbConn.Customers on pi.DepartemenID equals cu.DepartemenID
                //           join ca in dbConn.Categories on pi.CategoryID equals ca.CategoryID
                //           orderby p.ProductCode
                //           select new { pi.ProductID, pi.DepartemenID, p.ProductCode, cu.CompanyName, p.ProductName, ca.CategoryName, pi.UnitsInStock, pi.UnitsOnOrder };
                //return Json(new { data = data }, JsonRequestBehavior.AllowGet);
                #endregion

                var data = _dbConn.SPI_ProductsInventory().ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }
        ///////////////-----------End Load Data by JQuery Fir 26102016 -----------------/////////////

        public ActionResult ListProductsInventoryProductVM()
        {
            var data = (from pi in _dbConn.ProductsInventories
                        join p in _dbConn.Products on pi.ProductID equals p.ProductID
                        join d in _dbConn.Departements on pi.DepartemenID equals d.DepartemenID
                        into ThisList
                        orderby p.ProductCode
                        select new
                     {
                         productID = p.ProductID,
                         //CompanyName = c.DepartemenID,
                         productCode = p.ProductCode,
                         productName = p.ProductName,
                         unitsInStock = pi.UnitsInStock,
                         unitsOnOrder = pi.UnitsOnOrder
                     }).ToList()
                       .Select(x => new ProductsInventoryProductVM()
                       {
                           ProductID = x.productID,
                           //CompanyName = x.CompanyName,
                           ProductCode = x.productCode,
                           ProductName = x.productName,
                           UnitsInStock = x.unitsInStock,
                           UnitsOnOrder = x.unitsOnOrder
                       });

            return View(data);
        }

        //
        // GET: /ProductsInventory/Details/5

        public ActionResult Details(int productID = 0, int departemenID = 0)
        {
            ProductsInventory productsInventory = _dbConn.ProductsInventories.Find(productID, departemenID);
            if (productsInventory == null)
            {
                return HttpNotFound();
            }
            return View(productsInventory);
        }

        //
        // GET: /ProductsInventory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ProductsInventory/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductsInventory productsInventory)
        {
            if (ModelState.IsValid)
            {
                _dbConn.ProductsInventories.Add(productsInventory);
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productsInventory);
        }

        //
        // GET: /ProductsInventory/Edit/5

        public ActionResult Edit(int productID = 0, int departemenID = 0)
        {
            ProductsInventory productsInventory = _dbConn.ProductsInventories.Find(productID, departemenID);
            if (productsInventory == null)
            {
                return HttpNotFound();
            }

            // Test Using Class Tuple
            //var ProductInventoryTuple = new Tuple<List<ProductsInventory>, List<Product>>
            //    (dbConn.ProductsInventories.OrderBy(pi => pi.ProductID).ToList(), dbConn.Products.OrderBy(p => p.ProductID).ToList()) { };

            //ViewBag.DepartemenID = new SelectList(dbConn.Customers, "DepartemenID", "CompanyName", productsinventory.DepartemenID);
            ViewBag.DepartemenID = from pi in _dbConn.ProductsInventories
                                   join c in _dbConn.Departements on pi.DepartemenID equals c.DepartemenID
                                   select new
                                   {
                                       departemenID = pi.DepartemenID,
                                       Departemen = c.DepartemenName
                                   };

            //ViewBag.ProductID = new SelectList(dbConn.Products, "ProductID", "ProductName", productsinventory.ProductID);
            ViewBag.ProductID = from pi in _dbConn.ProductsInventories
                                join p in _dbConn.Products on pi.ProductID equals p.ProductID
                                select new
                                {
                                    productID = pi.ProductID,
                                    codeName = p.ProductCode + " - " + p.ProductName
                                };

            //var query = "SELECT pi.ProductID, ca.CategoryName "
            //    + "FROM ProductsInventory pi LEFT JOIN Categories ca "
            //    + "ON pi.ProductID = ca.ProductID "
            //    + "WHERE pi.ProductID =  " + ProductID
            //    ;
            //ViewData["Category"] = dbConn.Database.SqlQuery<ProductsInventoryCategoryVM>(query);

            ViewBag.PhotoPath = (from pi in _dbConn.ProductsInventories
                                 join p in _dbConn.Products on pi.ProductID equals p.ProductID
                                 where p.ProductID == productID
                                 select p.PhotoPath)
                                            .Take(1).SingleOrDefault();

            ViewBag.CategoryID = (from pi in _dbConn.ProductsInventories
                                  join ca in _dbConn.Categories on pi.CategoryID equals ca.CategoryID
                                  where pi.ProductID == productID
                                  select new
                                  {
                                      categoryID = pi.CategoryID,
                                      categoryName = ca.CategoryName
                                  }).ToList();

            //ViewBag.CategoryID = new SelectList(dbConn.Categories, "CategoryID", "CategoryName", productsinventory.CategoryID);

            return View(productsInventory);
            //return View(ProductInventoryTuple);     // Test Tuple
        }

        //
        // POST: /ProductsInventory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductsInventory productsInventory)
        {
            if (ModelState.IsValid)
            {
                _dbConn.Entry(productsInventory).State = EntityState.Modified;
                _dbConn.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productsInventory);
        }

        //
        // GET: /ProductsInventory/Delete/5

        public ActionResult Delete(int productID = 0, int departemenID = 0)
        {
            ProductsInventory productsInventory = _dbConn.ProductsInventories.Find(productID, departemenID);
            if (productsInventory == null)
            {
                return HttpNotFound();
            }
            return View(productsInventory);
        }

        //
        // POST: /ProductsInventory/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int productID = 0, int departemenID = 0)
        {
            ProductsInventory productsInventory = _dbConn.ProductsInventories.Find(productID, departemenID);
            _dbConn.ProductsInventories.Remove(productsInventory);
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