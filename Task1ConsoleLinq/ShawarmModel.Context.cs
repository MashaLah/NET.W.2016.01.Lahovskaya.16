﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShawarmaEntities : DbContext
    {
        public ShawarmaEntities()
            : base("name=ShawarmaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<IngredientCategory> IngredientCategory { get; set; }
        public virtual DbSet<OrderHeader> OrderHeader { get; set; }
        public virtual DbSet<PriceController> PriceController { get; set; }
        public virtual DbSet<Seller> Seller { get; set; }
        public virtual DbSet<SellingPoint> SellingPoint { get; set; }
        public virtual DbSet<SellingPointCategory> SellingPointCategory { get; set; }
        public virtual DbSet<Shawarma> Shawarma { get; set; }
        public virtual DbSet<ShawarmaRecipe> ShawarmaRecipe { get; set; }
        public virtual DbSet<TimeController> TimeController { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
    }
}