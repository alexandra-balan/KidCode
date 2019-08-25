using KidCode.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidCode.Controllers
{
    public class TeacherController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();


        // GET: Teacher
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include("School");
            ViewBag.Teachers = teachers;
            return View();
        }

        public ActionResult Details(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            ViewBag.Teacher = teacher;
            ViewBag.School = teacher.School;
            var students = teacher.Students;
            ViewBag.Students = students;
            return View(teacher);

        }

        public ActionResult Create()
        {
            Teacher teacher = new Teacher();
            teacher.Schools = GetAllSchools();
            teacher.UserId = User.Identity.GetUserId();
            
            return View(teacher);
        }


        [NonAction]
        public IEnumerable<SelectListItem> GetAllSchools()
        {
            var selectList = new List<SelectListItem>();
            
            var schools = from sch in db.Schools select sch;
            foreach(var school in schools)
            {
                selectList.Add(new SelectListItem
                {
                    Value = school.SchoolId.ToString(),
                    Text = school.SchoolName.ToString()
                });

            }
            return selectList;
        }
        

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            teacher.Schools = GetAllSchools();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Teachers.Add(teacher);
                    db.SaveChanges();
                    TempData["message"] = "Profesorul a fost adaugat!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(teacher);
                }
            }
            catch(Exception e)
            {
                return View(teacher);
            }
        }
        
        public ActionResult Edit(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            teacher.Schools = GetAllSchools();
            ViewBag.Teacher = teacher;
            
            return View(teacher);
        }

        [HttpPut]
        public ActionResult Edit(int id, Teacher requestTeacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Teacher teacher = db.Teachers.Find(id);
                    if(TryUpdateModel(teacher))
                    {
                        teacher.TeacherFirstName = requestTeacher.TeacherFirstName;
                        teacher.TeacherLastName = requestTeacher.TeacherLastName;
                        teacher.SchoolId = requestTeacher.SchoolId;
                        db.SaveChanges();
                        TempData["message"] = "Profesorul a fost modificat!";

                    }
                    return RedirectToAction("Index");
                }
                else 
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            TempData["message"] = "Profesorul a fost sters";
            return RedirectToAction("Index");
        }


        
    }
}