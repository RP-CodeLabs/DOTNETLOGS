﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DOTNET.LOGS.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DotNetLogEntities : DbContext
    {
        public DotNetLogEntities() : base("name=DotNetLogEntities")
        {
        }

        public DotNetLogEntities(string connectionName) : base(connectionName)
        {
            
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LogDetail> LogDetails { get; set; }
        public virtual DbSet<LogMain> LogMains { get; set; }
        public virtual DbSet<MIAdditionalItemLog> MIAdditionalItemLogs { get; set; }
        public virtual DbSet<MILog> MILogs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Version> Versions { get; set; }
        public virtual DbSet<tbl_BGLNetLogErrors> tbl_BGLNetLogErrors { get; set; }
    }
}