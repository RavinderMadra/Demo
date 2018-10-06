using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoModel.ViewModel;
using DemoService.Customer;


namespace OnboardTask.Controllers
{
    public class CustomerController : Controller
    {
        CustomerService objcustomer = new CustomerService();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            return Json(objcustomer.GetAllCustomer(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(CustomerViewModel obj)
        {
            return Json(objcustomer.SaveCustomers(obj), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int ID)
        {
            return Json(objcustomer.GetAllCustomer().Find(x => x.Id.Equals(ID)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(CustomerViewModel emp)
        {
            if (objcustomer.UpdateCustomer(emp))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }

        }
        public JsonResult Delete(int ID)
        {
            return Json(objcustomer.Delete(ID), JsonRequestBehavior.AllowGet);
        }

    }
}