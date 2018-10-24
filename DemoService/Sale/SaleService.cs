using Demo.Core.EntityModel;
using DemoModel.ViewModel;
using ExpressMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoService.Sale
{
   public class SaleService
    {
        private OnBoadTaskEntities _Context = new OnBoadTaskEntities();

        #region Public_Methods
        public bool SaveSales(SaleViewModel salesViewModel)
        {
            bool status = false;

            tblProductSold product = new tblProductSold();
            Mapper.Map(salesViewModel, product);
            product.ProductId = salesViewModel.ProductId;
            product.CustomerId = salesViewModel.CustomerId;
            product.StoreId = salesViewModel.StoreId;
            product.DateSold = salesViewModel.DateSold;
            product.IsActive = true;
            product.CreatedDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;
            product.CreatedBy = "101";
            product.ModifiedBy = "101";
            _Context.tblProductSolds.Add(product);
            _Context.Configuration.ValidateOnSaveEnabled = true;
            _Context.SaveChanges();
            status = true;


            return status;
            // for new users
        }

        /// Get all Sales      
        public List<SaleViewModel> getbyId(int Id)
        {
            List<SaleViewModel> entities = new List<SaleViewModel>();
            // making values as trim  
            //var list = _Context.tblProductSolds.Include("tblCustomer").Include("tblProduct").Include("tblStore").Where(x=>x.IsActive==true).ToList();
            var list = _Context.GetSalesDetail(Id).ToList();
            
            Mapper.Map(list, entities);
            //            foreach (var item in entities)
            //            {
            //                item.DateSold=item.DateSold.ToString();
            //            }


            return entities;
        }


        /// Update Sales information       
        public bool UpdateSales(SaleViewModel salesViewModel)
        {
            bool status = false;
            try
            {
                //var saledetails = _Context.tblProductSolds.FirstOrDefault(x => x.Id == user.Id);
                var _saleDetails = _Context.tblProductSolds.Find(salesViewModel.Id);

                if (_saleDetails != null)
                {
                    Mapper.Map(salesViewModel, _saleDetails);
                    _saleDetails.ProductId = salesViewModel.ProductId;
                    _saleDetails.CustomerId = salesViewModel.CustomerId;
                    _saleDetails.StoreId = salesViewModel.StoreId;
                    _saleDetails.ModifiedDate = DateTime.Now;
                    _saleDetails.DateSold = salesViewModel.DateSold;
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


        /// Delete SalesRecord        
        public bool Delete(int Id)
        {
            try
            {
                var entity = _Context.tblCustomers.Find(Id);
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

