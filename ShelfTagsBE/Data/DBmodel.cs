using System;
using Microsoft.EntityFrameworkCore;
using ShelfTagsBE.Models;

namespace ShelfTagsBE.Data;

    public class DBmodel:DbContext
{
        public DBmodel(DbContextOptions<DBmodel> options):base(options){}

        public DbSet<AuditLog> AuditLogs {get;set;}
        public DbSet<PriceHistory> PriceHistories {get;set;}
        public DbSet<PrintTemplate> PrintTemplates {get;set;}
        public DbSet<Product> Products {get;set;}
        public DbSet<ShelfTag> ShelfTags {get;set;}

        public DbSet<Account> Accounts {get;set;}
}
