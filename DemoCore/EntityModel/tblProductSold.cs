
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Demo.Core.EntityModel
{

using System;
    using System.Collections.Generic;
    
public partial class tblProductSold
{

    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public int StoreId { get; set; }

    public bool IsActive { get; set; }

    public System.DateTime CreatedDate { get; set; }

    public System.DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public System.DateTime DateSold { get; set; }



    public virtual tblCustomer tblCustomer { get; set; }

    public virtual tblProduct tblProduct { get; set; }

    public virtual tblStore tblStore { get; set; }

}

}
