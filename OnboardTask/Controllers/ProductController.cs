using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoModel.ViewModel;
using DemoService.Product;

namespace OnboardTask.Controllers
{
    public class ProductController : Controller
    {
        ProductService objproduct = new ProductService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(objproduct.GetAllProducts(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(ProductViewModel obej)
        {
            return Json(objproduct.SaveProducts(obej), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(objproduct.GetAllProducts().Find(x => x.Id.Equals(ID)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ProductViewModel pdut)
        {
            return Json(objproduct.UpdateProducts(pdut), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            return Json(objproduct.Delete(ID), JsonRequestBehavior.AllowGet);
        }
    }
}