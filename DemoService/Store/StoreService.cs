using Demo.Core.EntityModel;
using DemoModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressMapper;

namespace DemoService.Store
{
  public class StoreService
    {
        private TransportManagementSystemEntities _Context = new TransportManagementSystemEntities();

        #region Public_Methods

        /// Get all Customers
        public List<StoreViewModel> GetAllStores()
        {
            List<StoreViewModel> entities = new List<StoreViewModel>();
            // making values as trim  

            var list = _Context.tblStores.Where(x => x.IsActive == true).ToList();
            Mapper.Map(list, entities);
            return entities;
        }


        /// Save Store        
        public bool SaveStores(StoreViewModel customerViewModel)
        {
            bool status = false;
            tblStore stores = new tblStore();
            Mapper.Map(customerViewModel, stores);

            stores.IsActive = true;
            stores.CreatedDate = DateTime.Now;
            stores.ModifiedDate = DateTime.Now;
            stores.CreatedBy = "101";
            stores.ModifiedBy = "101";
            _Context.tblStores.Add(stores);
            _Context.Configuration.ValidateOnSaveEnabled = true;
            _Context.SaveChanges();
            status = true;
            return status;
            // for new users
        }


        public bool UpdateProducts(ProductViewModel productViewModel)
        {
            bool status = false;
            try
            {
                //var _usrsaltdetails = _Context.Users.FirstOrDefault(x => x.Id == user.Id);
                var _productDetails = _Context.tblProducts.Find(productViewModel.Id);

                if (_productDetails != null)
                {
                    Mapper.Map(productViewModel, _productDetails);
                    _productDetails.ModifiedDate = DateTime.Now;
                    _Context.Configuration.ValidateOnSaveEnabled = false;
                    _Context.SaveChanges();
                    status = true;

                }

            }

            catch (Exception ex)
            {

            }
            // for new product
            return status;
        }




        /// Update Store information        
        public bool UpdateStores(StoreViewModel storeViewModel)
        {
            bool status = false;
            try
            {
                //var _usrsaltdetails = _Context.Users.FirstOrDefault(x => x.Id == user.Id);
                var _storeDetails = _Context.tblStores.Find(storeViewModel.Id);

                if (_storeDetails != null)
                {
                    Mapper.Map(storeViewModel, _storeDetails);
                    _storeDetails.ModifiedDate = DateTime.Now;
                    _Context.Configuration.ValidateOnSaveEnabled = false;
                    _Context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
            }
            // for new users
            return status;
        }

        
        /// Delete Store
      public bool Delete(long Id)
        {
            try
            {
                var entity = _Context.tblStores.Find(Id);
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
     
                 
        #endregion
    }
}
