using KidCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidCode.Controllers
{
    public class TextSolutionController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        // GET: TextSolution
        public ActionResult Index()
        {
            var sol = db.TextSolutions;
            ViewBag.Solutions = sol;
            return View();
        }

        public ActionResult Details (int id)
        {
            TextSolution sol = db.TextSolutions.Find(id);
            Student student = db.Students.Find(sol.StudentId);
            ViewBag.Challenge = sol.TextChallenge;
            ViewBag.Student = student;
            ViewBag.Solution = sol;
            return View(sol);
        }

       

        public ActionResult Create(int id)
        {
            TextSolution solution = new TextSolution();
            Student student = db.Students.Find(3);
            solution.StudentId = student.StudentId;

            TextChallenge challenge = db.TextChallenges.Find(id);
            //challenge.TextSolutions.Add(solution);
            //solution.TextChallenge = challenge;
            solution.ChallengeId = challenge.ChallengeId;
           // ViewBag.Solution = solution;
            return View(solution);
        }

        [HttpPost]
        public ActionResult Create(TextSolution solution)
        {

            try
            {
                if(ModelState.IsValid)
                {
                    
                    solution.StatusTeacher = false;
                    solution.StatusStudent = false;
                    solution.TeacherAnswer = "";
                    
                    db.TextSolutions.Add(solution);
                    db.SaveChanges();
                    TempData["message"] = "Solutia a fost adaugata!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(solution);
                }
            }
            catch(Exception e)
            {
                return View(solution);
            }
        }

        public ActionResult Edit(int id)
        {
            TextSolution solution = db.TextSolutions.Find(id);
            ViewBag.Solution = solution;
            return View(solution);
        }

        [HttpPut]
        public ActionResult Edit(int id, TextSolution requestSolution)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    TextSolution solution = db.TextSolutions.Find(id);
                    if (TryUpdateModel(solution))
                    {
                        solution.SolutionScore = requestSolution.SolutionScore;
                        solution.StudentAnswer = requestSolution.StudentAnswer;
                        solution.TeacherAnswer = requestSolution.TeacherAnswer;
                        db.SaveChanges();
                        TempData["message"] = "Solutia a fost modificata";
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
            TextSolution solution = db.TextSolutions.Find(id);
            db.TextSolutions.Remove(solution);
            db.SaveChanges();
            TempData["message"] = "Solutia a fost stearsa";
            return RedirectToAction("Index");
        }
    }
}