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
        private OnBoadTaskEntities _Context = new OnBoadTaskEntities();

        #region Public_Methods

        /// Get all Customers
        public List<StoreViewModel> GetAllStores()
        {
            List<StoreViewModel> entities = new List<StoreViewModel>();
            // making values as trim  

            var list = _Context.tbl_Store.Where(x => x.IsActive == true).ToList();
            Mapper.Map(list, entities);
            return entities;
        }


        /// Save Store        
        public bool SaveStores(StoreViewModel storeViewModel)
        {
            bool status = false;
            tbl_Store stores = new tbl_Store();
            Mapper.Map(storeViewModel, stores);

            stores.IsActive = true;
            stores.CreatedDate = DateTime.Now;
            stores.ModifiedDate = DateTime.Now;
            stores.CreatedBy = "101";
            stores.ModifiedBy = "101";
            _Context.tbl_Store.Add(stores);
            _Context.Configuration.ValidateOnSaveEnabled = true;
            _Context.SaveChanges();
            status = true;
            return status;
            // for new users
        }
        
        
        /// Update Store information        
        public bool UpdateStores(StoreViewModel storeViewModel)
        {
            bool status = false;
            try
            {
                //var _usrsaltdetails = _Context.Users.FirstOrDefault(x => x.Id == user.Id);
                var _storeDetails = _Context.tbl_Store.Find(storeViewModel.Id);

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
                var entity = _Context.tbl_Store.Find(Id);
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

        
        /// Get all Stores for drop down (get only Id and Name)
        public List<StoreViewModel> GetStoresForDropDown()
        {
            return (from store in GetAllStores()
                    orderby store.Name
                    select new StoreViewModel
                    {
                        Id = store.Id,
                        Name = store.Name
                    }).ToList();
        }

        #endregion
    }
}
