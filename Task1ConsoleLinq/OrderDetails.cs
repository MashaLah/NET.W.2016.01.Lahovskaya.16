//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Task1ConsoleLinq
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetails
    {
        public int OrderHeaderID { get; set; }
        public int ShawarmaID { get; set; }
        public int Quantity { get; set; }
    
        public virtual OrderHeader OrderHeader { get; set; }
        public virtual Shawarma Shawarma { get; set; }
    }
}
