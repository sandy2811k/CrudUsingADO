using CrudUsingADO.Models;
using CrudUsingAOD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CrudUsingAOD.Controllers
{
    public class ProductsController : Controller
    {
        IConfiguration configuration;
        ProductDAL db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public ProductsController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.configuration = configuration;
            db = new ProductDAL(this.configuration);
            this.env = env;
        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            var model = db.GetProduct();
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.Category = db.GetCategory();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, IFormFile file)
        {
            try
            {
                using (var fs = new FileStream(env.WebRootPath + "\\Images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }
                product.ImageUrl = "~/Images/" + file.FileName;
                int result = db.AddProducts(product);
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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Category = db.GetCategory();
            var model = db.GetProductById(id);
            TempData["oldurl"] = model.ImageUrl;
            TempData.Keep("oldurl");
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, IFormFile file)
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
                    product.ImageUrl = "~/images/" + file.FileName;


                    string[] str = oldimageurl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = env.WebRootPath + "\\images\\" + str1;
                    System.IO.File.Delete(path);
                }
                else
                {
                    product.ImageUrl = oldimageurl;
                }


                int result = db.UpdateProduct(product);
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = db.GetProductById(id);
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {

                int result = db.DeleteProduct(id);
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