using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using Gapura.Models;

namespace Gapura.Controllers
{
    public class CategoriesController : Controller
    {
        private YSIDGAEntitiesConn dbConn = new YSIDGAEntitiesConn();

        //
        // GET: /Categories/

        public ActionResult Index()
        {
            return View(dbConn.Categories.ToList());
        }

        //YSIDGAEntitiesConn db = new YSIDGAEntitiesConn();
        public JsonResult GetCategories(string sidx, string sort, int page, int rows)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var CategoryList = dbConn.Categories.Select(
                    c => new
                    {
                        c.CategoryID,
                        c.CategoryName,
                        c.Description,
                        //c.Picture
                    });
            

            int totalRecords = CategoryList.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sort.ToUpper() == "DESC")
            {
                CategoryList = CategoryList.OrderByDescending(c => c.CategoryName);
                CategoryList = CategoryList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                CategoryList = CategoryList.OrderBy(c => c.CategoryName);
                CategoryList = CategoryList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = CategoryList
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Categories/Details/5

        public ActionResult Details(int id = 0)
        {
            Category Category = dbConn.Categories.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }

        /*    Create - Old ( by Scafold Default )    */
        /*
        //
        // GET: /Categories/Create

        public ActionResult Create()
        {
            return View();
        }

                //
                // POST: /Categories/Create

                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create(Category Category)
                {
                    if (ModelState.IsValid)
                    {
                        db.Categories.Add(Category);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(Category);
                }
        */

        [HttpPost]
        public string Create([Bind(Exclude = "CategoryID")] Category objCategory)
        {
            //YSIDGAEntitiesConn db = new YSIDGAEntitiesConn();
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    //Model.CategoryID = Guid.NewGuid().ToString();
                    dbConn.Categories.Add(objCategory);

                    dbConn.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        /*      Update - Old (by Scafold Default)     */
        /*
        //
        // GET: /Categories/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category Category = db.Categories.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }

                //
                // POST: /Categories/Edit/5

                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit(Category Category)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(Category).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(Category);
                }
        */
        public string Edit(Category objCategory)
        {
            //YSIDGAEntitiesConn db = new YSIDGAEntitiesConn();
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    Category Category = dbConn.Categories.Find(objCategory);
                    if (objCategory == null)
                    {
                        dbConn.Entry(objCategory).State = EntityState.Modified;
                        dbConn.SaveChanges();
                        msg = "Saved Successfully";
                    }
                    else
                    {
                        msg = "Error. Update Data Error !!";
                    }
                    
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        /*      Delete - Old (by Scafold Default)     */
        /*
        //
        // GET: /Categories/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category Category = db.Categories.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }

        //
        // POST: /Categories/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category Category = db.Categories.Find(id);
            db.Categories.Remove(Category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);

        }
*/
        public string Delete(int Id)
        {
            //YSIDGAEntitiesConn db = new YSIDGAEntitiesConn();
            Category Categories = dbConn.Categories.Find(Id);
            dbConn.Categories.Remove(Categories);
            dbConn.SaveChanges();
            return "Deleted successfully";
        }
/*
        public ActionResult GetCategory(string sidx, string sort, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            List<Category> groups = service.Category();
            List<Category> results;
            if (_search)
                results = groups.Where(c => c.CategoryName.Contains(searchString)).ToList();
            else
                results = groups.Skip(page * rows).Take(rows).ToList();

            int i = 1;

            var jsonData = new
            {
                total = groups.Count / 20,
                page = page,
                records = groups.Count,
                rows = (
                    from Category in results
                    select new
                    {
                        i = i++,
                        cell = new string[] {
                         Category.CategoryName,
                         Category.Description
                     }
                    }).ToArray()
            };

            return Json(jsonData);
        }
 */ 
    }
}