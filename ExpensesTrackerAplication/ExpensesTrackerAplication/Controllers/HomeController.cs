using ExpensesTrackerAplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpensesTrackerAplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetExpenses()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var expense = dc.Expenses.OrderBy(a => a.ProyectID).ToList();
                return Json(new { data = expense }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();

        }

        [HttpGet]
        public ActionResult Clear()
        {
            return View();

        }

        [HttpPost]
        [ActionName("Clear")]
        public ActionResult ClearTable()
        {
            bool status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                foreach (var item in dc.Expenses.ToList()){
                    dc.Expenses.Remove(item);
                    dc.SaveChanges();
                    status = true;
                }
           
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Expenses.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost]
        [ActionName("Add")]
        public ActionResult Add(Expens emp)
        {
            bool status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                //Save
                dc.Expenses.Add(emp);
                try
                {
                    dc.SaveChanges();
                    status = true;
                }
                catch (DbEntityValidationException e)
                {

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;

                }
            }

            return new JsonResult { Data = new { status = status } };


        }

        [HttpPost]
        public ActionResult Save(Expens emp)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {

                    if (emp.Id > 0)
                    {
                        //Edit 
                        var v = dc.Expenses.Where(a => a.Id == emp.Id).FirstOrDefault();
                        if (v != null)
                        {
                           // v.ProyectID = emp.ProyectID;
                            v.Name = emp.Name;
                            v.ExpenseDate = emp.ExpenseDate;
                            v.Amount = emp.Amount;
                            v.Description = emp.Description;
                            
                        }
                    }
                    else
                    {
                        //Save
                        dc.Expenses.Add(emp);
                    }
                    try
                    {
                        dc.SaveChanges();
                        status = true;
                    }
                    catch (DbEntityValidationException e)
                    {

                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;

                    }
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Expenses.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteExpenses(int id)
        {
            bool status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Expenses.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    dc.Expenses.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}