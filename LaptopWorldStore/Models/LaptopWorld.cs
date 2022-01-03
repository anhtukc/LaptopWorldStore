using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LaptopWorldStore.Models
{
    public partial class LaptopWorld : DbContext
    {
        public LaptopWorld()
            : base("name=LaptopWorld")
        {
        }

        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<_new> news { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<provider> providers { get; set; }
        public virtual DbSet<receiptbill> receiptbills { get; set; }
        public virtual DbSet<receiptbilldetail> receiptbilldetails { get; set; }
        public virtual DbSet<sellbill> sellbills { get; set; }
        public virtual DbSet<sellbilldetail> sellbilldetails { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<laptop> laptops { get; set; }
        public virtual DbSet<ram> rams { get; set; }
        public virtual DbSet<ssd> ssds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>()
                .Property(e => e.employee_id)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.category_id)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.sellbills)
                .WithRequired(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.employee_id)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.cmnd)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .HasMany(e => e.receiptbills)
                .WithRequired(e => e.employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<_new>()
                .Property(e => e.New_id)
                .IsUnicode(false);

            modelBuilder.Entity<_new>()
                .Property(e => e.category_id)
                .IsUnicode(false);

            modelBuilder.Entity<_new>()
                .Property(e => e.picture)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.product_id)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.category_id)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.provider_id)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price)
                .HasPrecision(18, 0);


            modelBuilder.Entity<product>()
                .HasMany(e => e.sellbilldetails)
                .WithRequired(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<provider>()
                .Property(e => e.provider_id)
                .IsUnicode(false);

            modelBuilder.Entity<provider>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<provider>()
                .HasMany(e => e.receiptbills)
                .WithRequired(e => e.provider)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receiptbill>()
                .Property(e => e.employee_id)
                .IsUnicode(false);

            modelBuilder.Entity<receiptbill>()
                .Property(e => e.provider_id)
                .IsUnicode(false);

            modelBuilder.Entity<receiptbill>()
                .Property(e => e.total_paid)
                .HasPrecision(18, 0);

            modelBuilder.Entity<receiptbill>()
                .HasMany(e => e.receiptbilldetails)
                .WithRequired(e => e.receiptbill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<receiptbilldetail>()
                .Property(e => e.product_id)
                .IsUnicode(false);

            modelBuilder.Entity<receiptbilldetail>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<receiptbilldetail>()
                .Property(e => e.grand_paid)
                .HasPrecision(18, 0);

            modelBuilder.Entity<sellbill>()
                .Property(e => e.shippingtype)
                .IsUnicode(false);

            modelBuilder.Entity<sellbill>()
                        .Property(e => e.customer_id).IsRequired();

            modelBuilder.Entity<sellbill>()
                .Property(e => e.total_paid)
                .HasPrecision(18, 0);

            modelBuilder.Entity<sellbill>()
                .HasMany(e => e.sellbilldetails)
                .WithRequired(e => e.sellbill)
                .HasForeignKey(e => e.sellbill_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sellbill>()
                .HasMany(e => e.sellbilldetails1)
                .WithRequired(e => e.sellbill1)
                .HasForeignKey(e => e.sellbill_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sellbilldetail>()
                .Property(e => e.product_id)
                .IsUnicode(false);

            modelBuilder.Entity<sellbilldetail>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<sellbilldetail>()
                .Property(e => e.grand_paid)
                .HasPrecision(18, 0);

            modelBuilder.Entity<laptop>()
                .Property(e => e.product_id)
                .IsUnicode(false);

            modelBuilder.Entity<ram>()
                .Property(e => e.product_id)
                .IsUnicode(false);

            modelBuilder.Entity<ssd>()
                .Property(e => e.product_id)
                .IsUnicode(false);
        }
    }
}
