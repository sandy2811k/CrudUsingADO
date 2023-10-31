using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudUsingADO.Models;
using Humanizer.Localisation.TimeToClockNotation;

namespace CrudUsingADO.Controllers
{
    public class CourseController : Controller
    {
        CourseDAL db;
        IConfiguration configuration;
        public CourseController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new CourseDAL(this.configuration);
        }
        // GET: CourseController
        public ActionResult Index()
        {
            return View(db.GetCourses());
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            var course = db.GetCourseByid(id);
            return View(course);
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            try
            {
                int result = db.AddCourse(course);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));//List
                }
                else
                {
                    return View();//remain on create page
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            var course = db.GetCourseByid(id);
            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            try
            {
                int result = db.UpdateStudent(course);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));//List
                }
                else
                {
                    return View();//remain on Edit page
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            var course = db.GetCourseByid(id);

            return View(course);
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = db.DeleteCourse(id);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));//List
                }
                else
                {
                    return View();//remain on Edit page
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
