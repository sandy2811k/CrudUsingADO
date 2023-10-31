using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudUsingADO.Models;
using Humanizer.Localisation.TimeToClockNotation;

namespace CrudUsingADO.Controllers
{
    public class StudentController : Controller
    {
        StudentDAL db;
        IConfiguration configuration;
        public StudentController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new StudentDAL(this.configuration);
        }
        // GET: StudentController
        public ActionResult Index()
        {
            return View(db.GetStudents());
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var student = db.GetStudentByRollNo(id);
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                int result = db.AddStudent(student);
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

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = db.GetStudentByRollNo(id);
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            try
            {
                int result = db.UpdateStudent(student);
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

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var student = db.GetStudentByRollNo(id);

            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = db.DeleteStudent(id);
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
