using Demo.Core.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressMapper;
using DemoModel.ViewModel;

namespace DemoService.Customer
{
   public class CustomerService
    {
        OnBoadTaskEntities _Context = new OnBoadTaskEntities();

        #region Public_Methods

        /// Get all Customers
        public List<CustomerViewModel> GetAllCustomer()
        {
            List<CustomerViewModel> entities = new List<CustomerViewModel>();
            // making values as trim 
            try
            {
                var list = _Context.tbl_Customer.Where(x => x.IsActive == true).ToList();
                Mapper.Map(list, entities);
            }
            catch (Exception ex)
            {
                return entities;
            }

            return entities;
        }               


        public bool SaveCustomers(CustomerViewModel customerViewModel)
        {
            bool status = false;

            tbl_Customer customer = new tbl_Customer();
            Mapper.Map(customerViewModel, customer);

            customer.IsActive = true;
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.CreatedBy = "101";
            customer.ModifiedBy = "101";
            _Context.tbl_Customer.Add(customer);
            _Context.Configuration.ValidateOnSaveEnabled = true;
            _Context.SaveChanges();
            status = true;
            return status;
            // for new users
        }


        /// Get Customers detail by Id
        public CustomerViewModel GetCustomerDetailById(long Id)
        {
            tbl_Customer customers = _Context.tbl_Customer.Where(x => x.Id == Id).FirstOrDefault();
            return Mapper.Map(customers, new CustomerViewModel());

        }


        public bool UpdateCustomer(CustomerViewModel customerViewModel)
        {
            bool status = false;
            try
            {

                var _customerDetail = _Context.tbl_Customer.Find(customerViewModel.Id);

                if (_customerDetail != null)
                {
                    Mapper.Map(customerViewModel, _customerDetail);
                    _customerDetail.ModifiedDate = DateTime.Now;
                    _Context.Configuration.ValidateOnSaveEnabled = false;
                    _Context.SaveChanges();
                    status = true;

                }

            }

            catch (Exception ex)
            {
                //ex.Message;
            }
            // for new users
            return status;
        }

        
        /// Delete Customers       
        public bool Delete(long Id)
        {
            try
            {
                var entity = _Context.tbl_Customer.Find(Id);
                if (entity != null)
                {
                    entity.IsActive = false;
                    _Context.Configuration.ValidateOnSaveEnabled = false;
                    _Context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }

            catch (Exception ex)
            {
                return false;
            }

        }


        /// Get all Customer for drop down (get only Id and Name)
        public List<CustomerViewModel> GetCustomersForDropDown()
        {
            return (from customer in GetAllCustomer()
                    orderby customer.Name
                    select new CustomerViewModel
                    {
                        Id = customer.Id,
                        Name = customer.Name
                    }).ToList();
        }



        #endregion
    }
}


    

