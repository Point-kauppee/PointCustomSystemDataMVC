﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PointCustomSystemDataMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JohaMeriSQL1Entities : DbContext
    {
        public JohaMeriSQL1Entities()
            : base("name=JohaMeriSQL1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Personnel> Personnel { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<PostOffices> PostOffices { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Treatment> Treatment { get; set; }
        public virtual DbSet<TreatmentPlace> TreatmentPlace { get; set; }
        public virtual DbSet<Studentx> Studentx { get; set; }
        public virtual DbSet<TreatmentOffice> TreatmentOffice { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<TreatmentReport> TreatmentReport { get; set; }
    }
}
