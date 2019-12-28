using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.EntityModel;
using ExpressMapper;
using DemoModel.ViewModel;

namespace DemoService.Product
{
    
   public  class ProductService
    {
        OnBoadTaskEntities _Context = new OnBoadTaskEntities();

        public List<ProductViewModel> GetAllProducts()
        {
            List<ProductViewModel> entities = new List<ProductViewModel>();
            
            var list = _Context.tbl_Product.Where(x => x.IsActive == true).ToList();

            Mapper.Map(list, entities);

            return entities;
        }

        public bool SaveProducts(ProductViewModel productViewModel)
        {
            bool status = false;

            tbl_Product product = new tbl_Product();
            Mapper.Map(productViewModel, product);

            product.IsActive = true;
            product.CreatedDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;
            product.CreatedBy = "101";
            product.ModifiedBy = "101";
            _Context.tbl_Product.Add(product);
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
                var _productDetails = _Context.tbl_Product.Find(productViewModel.Id);

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

        public bool Delete(long Id)
        {
            try
            {
                var entity = _Context.tbl_Product.Find(Id);
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


        /// Get all Product for drop down (get only Id and Name)

        public List<ProductViewModel> GetProductsForDropDown()
        {
            return (from product in GetAllProducts()
                    orderby product.Name
                    select new ProductViewModel
                    {
                        Id = product.Id,
                        Name = product.Name
                    }).ToList();

        }
    }
}
