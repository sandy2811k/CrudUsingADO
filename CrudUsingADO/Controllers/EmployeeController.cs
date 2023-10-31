using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudUsingADO.Models;

namespace CrudUsingADO.Controllers
{
    public class EmployeeController : Controller
    {

        IConfiguration configuration;
        EmployeeDAL db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public EmployeeController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.configuration = configuration;
            db = new EmployeeDAL(this.configuration);
            this.env = env;
        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            var model = db.GetEmployees();
            return View(model);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            ViewBag.Dept = db.GetDepts();
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee, IFormFile file)
        {
            try
            {
                using (var fs = new FileStream(env.WebRootPath + "\\Images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }
                employee.ImageUrl = "~/Images/" + file.FileName;
                int result = db.AddEmployee(employee);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Dept = db.GetDepts();
            var model = db.GetEmployeeById(id);
            TempData["oldurl"] = model.ImageUrl;
            TempData.Keep("oldurl");
            return View(model);

        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee, IFormFile file)
        {
            try
            {
                string oldimageurl = TempData["oldurl"].ToString();
                if (file != null) // add new image
                {
                    using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                    }
                    employee.ImageUrl = "~/images/" + file.FileName;


                    string[] str = oldimageurl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = env.WebRootPath + "\\images\\" + str1;
                    System.IO.File.Delete(path);
                }
                else
                {
                    employee.ImageUrl = oldimageurl;
                }


                int result = db.UpdateEmployee(employee);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }

        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = db.GetEmployeeById(id);
            return View(model);
        }


        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]


        //deleteConfirm


        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = db.DeleteEmployee(id);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }

        }
    }
}
