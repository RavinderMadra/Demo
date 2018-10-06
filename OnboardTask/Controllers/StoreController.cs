using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoModel.ViewModel;
using DemoService.Store;
namespace OnboardTask.Controllers
{
    public class StoreController : Controller
    {
        StoreService objstore = new StoreService();
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {

            return Json(objstore.GetAllStores(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(StoreViewModel obj)
        {
            return Json(objstore.SaveStores(obj), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(objstore.GetAllStores().Find(x => x.Id.Equals(ID)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(StoreViewModel obej)
        {
            return Json(objstore.UpdateStores(obej), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(objstore.Delete(ID), JsonRequestBehavior.AllowGet);
        }
    }
}