using KidCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidCode.Controllers
{
    public class TextChallengeController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        // GET: TextChallenge
        public ActionResult Index()
        {
            var chl = db.TextChallenges;
            ViewBag.Challenges = chl;
            return View();

        }

        public ActionResult Details (int id)
        {
            TextChallenge tc = db.TextChallenges.Find(id);
            ViewBag.Challenge = tc;
            Teacher teacher = db.Teachers.Find(tc.TeacherId);
            ViewBag.Teacher = teacher;
            return View(tc);
        }

        public ActionResult Create()
        {
            TextChallenge tc = new TextChallenge();
           
            return View(tc);

        }

        [HttpPost]
        public ActionResult Create(TextChallenge tc)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Teacher teacher = db.Teachers.Find(2);
                    tc.TeacherId = teacher.TeacherId;
                    db.TextChallenges.Add(tc);
                    db.SaveChanges();
                    TempData["message"] = "Problema a fost adaugata!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tc);
                }
            }
            catch (Exception e)
            {
                return View(tc);
            }
        }

        public ActionResult Edit(int id)
        {
            TextChallenge tc = db.TextChallenges.Find(id);
            ViewBag.Challenge = tc;
            return View(tc);
        }

        [HttpPut]
        public ActionResult Edit(int id, TextChallenge requestChallenge)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    TextChallenge tc = db.TextChallenges.Find(id);
                    if(TryUpdateModel(tc))
                    {
                        tc.ChallengeScore = requestChallenge.ChallengeScore;
                        tc.Question = requestChallenge.Question;
                        db.SaveChanges();
                        TempData["message"] = "Problema a fost modificata!";
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch(Exception e)
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            TextChallenge tc = db.TextChallenges.Find(id);
            db.TextChallenges.Remove(tc);
            db.SaveChanges();
            TempData["message"] = "Problema a fost stearsa";
            return RedirectToAction("Index");
        }
    }
}