﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealBranch.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FlowerbedDBEntities : DbContext
    {
        public FlowerbedDBEntities()
            : base("name=FlowerbedDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<Flower> Flowers { get; set; }
        public virtual DbSet<PlantationFlower> PlantationFlowers { get; set; }
        public virtual DbSet<Plantation> Plantations { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<SupplyFlower> SupplyFlowers { get; set; }
        public virtual DbSet<WarehouseFlower> WarehouseFlowers { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
    }
}
