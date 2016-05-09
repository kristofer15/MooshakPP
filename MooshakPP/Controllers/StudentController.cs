﻿using MooshakPP.Models.Entities;//   ÞARF AÐ TAKA ÚT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"!#$%&/(&%$#"!"#$%&/(&%$#"!#$%&/()&%$#"!#$%&/()/&%$#"
using MooshakPP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MooshakPP.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace MooshakPP.Controllers
{
    [Authorize(Roles = "student")]
    public class StudentController : BaseController
    {
        private StudentService service = new StudentService();

        // GET: Student
        [HttpGet]
        public ActionResult Index(int? courseID, int? assignmentID)
        {
            IndexViewModel model = new IndexViewModel();

            if (courseID == null)
            {
                courseID = service.GetFirstCourse(User.Identity.GetUserId());
            }

            if (assignmentID == null)
            {
                assignmentID = service.GetFirstAssignment((int)courseID);
            }

            Course usingThisCourse = service.GetCourse((int)courseID);

            ViewBag.selectedCourseName = usingThisCourse.name;

            model = service.Index(User.Identity.GetUserId(), (int)courseID, (int)assignmentID);

            return View(model);
        }


        [HttpPost]
        public ActionResult Submit()
        {
            HttpPostedFileBase file = null;
            //if file submission is valid
            if (Request.Files.Count >= 0 && Request.Files[0].FileName != "")
            {
                file = Request.Files[0];

                //userID, mileID, HttpPostedFileBase
                //username must be passed because User is tied to http
                service.CreateSubmission(User.Identity.GetUserId(), User.Identity.Name, 1, file);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ViewDetails()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewSubmission()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewDescription()
        {
            return View();
        }
    }
}