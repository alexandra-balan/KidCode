using KidCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KidCode.Controllers
{
    public class SchoolController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();


        // GET: School
        public ActionResult Index()
        {
            var schools = db.Schools;
            ViewBag.Schools = schools;
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            ViewBag.School = school;
            var teachers = school.Teachers;
            ViewBag.Teachers = teachers;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(School school)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                    db.Schools.Add(school);
                    db.SaveChanges();
                    TempData["message"] = "Scoala a fost adaugata";
                    return View();

                }
                else return View(school);

            }
            catch (Exception e)
            {
                return View(school);
            }
        }

        public ActionResult Edit (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            ViewBag.School = school;
            return View(school);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, School requestSchool)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    School school = db.Schools.Find(id);
                    if (TryUpdateModel(school))
                    {
                        school.SchoolName = requestSchool.SchoolName;
                        db.SaveChanges();
                        TempData["message"] = "Scoala a fost modificata";

                    }
                }
                return RedirectToAction("Index");
            }
            catch { return View();
            }
            
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            School school = db.Schools.Find(id);
            db.Schools.Remove(school);
            db.SaveChanges();
            TempData["message"] = "Scoala a fost stearsa";

            return RedirectToAction("Index");
        }
       
    }
}