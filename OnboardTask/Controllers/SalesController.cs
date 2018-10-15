using DemoService.Customer;
using DemoService.Product;
using DemoService.Store;
using DemoService.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoModel.ViewModel;

namespace OnboardTask.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        ProductService objProduct = new ProductService();
        CustomerService objcustomer = new CustomerService();
        StoreService objStore = new StoreService();
        SaleService objSale = new SaleService();

        public ActionResult Index()
        {
            List<SaleViewModel> list = new List<SaleViewModel>();
            list = objSale.getbyId(0);
            //ViewBag.Product = new SelectList(objProduct.GetProductsForDropDown(), "Id", "Name", 0);
            ViewBag.Product = objProduct.GetProductsForDropDown();
            ViewBag.Customers = objcustomer.GetCustomersForDropDown();
            ViewBag.Stores = objStore.GetStoresForDropDown();
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("_SalesList", list) : View(list);
        }
        public JsonResult Add(SaleViewModel objsales)
        {
            return Json(objSale.SaveSales(objsales), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int ID)
        {
            return Json(objSale.getbyId(ID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(SaleViewModel objsale)
        {
            return Json(objSale.UpdateSales(objsale), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRec(int ID)
        {
            return Json(objSale.Delete(ID), JsonRequestBehavior.AllowGet);
        }
    }
}


