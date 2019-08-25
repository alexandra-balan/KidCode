using KidCode.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidCode.Controllers
{
    public class StudentController : Controller
    {

        private ApplicationDbContext db = ApplicationDbContext.Create();

        // GET: Student
        public ActionResult Index()
        {
            var students = db.Students.Include("Teacher");
            ViewBag.Students = students;
            return View();
        }

        public ActionResult Details(int id)
        {
            Student student = db.Students.Find(id);
            ViewBag.Student = student;
            ViewBag.Teacher = student.Teacher;
            ViewBag.School = student.Teacher.School;
            return View(student);
        }

        public ActionResult Create()
        {
            Student student = new Student();
            student.Teachers = GetAllTeachers();
            student.UserId = User.Identity.GetUserId();
            return View(student);
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllTeachers()
        {
            var selectList = new List<SelectListItem>();
            var teachers = from t in db.Teachers select t;
            foreach (var teacher in teachers)
            {
                selectList.Add(new SelectListItem
                {
                    Value = teacher.TeacherId.ToString(),
                    Text = teacher.FullDesc.ToString()
                });
            }
            return selectList;
        }


        [HttpPost]
        public ActionResult Create(Student student)
        {
            student.Teachers = GetAllTeachers();
            try
            {
                if (ModelState.IsValid)
                {
                    student.Score = 0;
                    db.Students.Add(student);
                    db.SaveChanges();
                    TempData["message"] = "Studentul a fost adaugat!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(student);
                }
            }
            catch (Exception e)
            {
                return View(student);
            }
        }

        public ActionResult Edit(int id)
        {
            Student student = db.Students.Find(id);
            student.Teachers = GetAllTeachers();
            ViewBag.Student = student ;
            return View(student);
        }

        [HttpPut]
        public ActionResult Edit(int id, Student requestStudent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Student student = db.Students.Find(id);
                    if (TryUpdateModel(student))
                    {
                        student.StudentFirstName = requestStudent.StudentFirstName;
                        student.StudentLastName = requestStudent.StudentLastName;
                        student.TeacherId = requestStudent.TeacherId;
                        db.SaveChanges();
                        TempData["message"] = "Elevul a fost modificat!";
                        

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
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            TempData["message"] = "Elevul a fost sters!";
            return RedirectToAction("Index");
        }

    }
}