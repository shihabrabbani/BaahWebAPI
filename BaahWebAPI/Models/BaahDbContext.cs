using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace BaahWebAPI.Models
{
    public partial class BaahDbContext : DbContext
    {
        public BaahDbContext()
        {
        }

        public BaahDbContext(DbContextOptions<BaahDbContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<NewView> NewViews { get; set; } = null!;
        public virtual DbSet<ViewProcessingorder> ViewProcessingorders { get; set; } = null!;
        public virtual DbSet<ViewTodayssale> ViewTodayssales { get; set; } = null!;
        public virtual DbSet<ViewNetSalesreport> ViewNetSalesreports { get; set; } = null!;
        public virtual DbSet<ViewSalesreport> ViewSalesreports { get; set; } = null!;
        public virtual DbSet<ViewTopSellingProducts> ViewTopSellingProducts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string connectionString = "server=localhost;database=baahstore;user id=sa;password=123123";
                string connectionString = "server=54.183.92.125;database=baahstore;user id=baahstore;password=s#z%R79S$kYG25yp";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));


                //optionsBuilder.UseMySql("server=103.147.182.59;database=baahstore;user id=baahstore;password=ba@hS$otRelmT2027", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.39-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            //modelBuilder.Entity<NewView>(entity =>
            //{
            //    entity.HasNoKey();

            //    entity.ToView("new_view");

            //    entity.Property(e => e.Date)
            //        .HasColumnType("datetime")
            //        .HasDefaultValueSql("'0000-00-00 00:00:00'");

            //    entity.Property(e => e.ItemsSold)
            //        .HasColumnType("int(11)")
            //        .HasColumnName("Items Sold");

            //    entity.Property(e => e.NetSale).HasColumnName("Net Sale");

            //    entity.Property(e => e.ShippingCost)
            //        .HasColumnName("Shipping Cost")
            //        .UseCollation("utf8mb4_unicode_ci")
            //        .HasCharSet("utf8mb4");

            //    entity.Property(e => e.TotalSale).HasColumnName("Total Sale");
            //});

            modelBuilder.Entity<ViewProcessingorder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_processingorders");

                entity.Property(e => e.Count)
                    .HasColumnType("bigint(21)")
                    .HasColumnName("count");
            });

            modelBuilder.Entity<ViewTodayssale>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_todayssale");

                entity.Property(e => e.Count).HasColumnType("bigint(21)");
            });

            modelBuilder.Entity<ViewNetSalesreport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_netsalesreport");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.ItemsSold)
                    .HasColumnType("int(11)")
                    .HasColumnName("Items Sold");

                entity.Property(e => e.NetSale).HasColumnName("Net Sale");

                entity.Property(e => e.ShippingCost)
                    .HasColumnName("Shipping Cost")
                    .UseCollation("utf8mb4_unicode_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.TotalSale).HasColumnName("Total Sale");
            });

            modelBuilder.Entity<ViewSalesreport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_salesreport");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.ItemsSold)
                    .HasColumnType("int(11)")
                    .HasColumnName("Items Sold");

                entity.Property(e => e.TotalSale).HasColumnName("Total Sale");

                entity.Property(e => e.Status).HasColumnName("Status");
            });

            modelBuilder.Entity<ViewTopSellingProducts>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_topsellingproducts");

                entity.Property(e => e.value).HasColumnName("TotalSale");

                entity.Property(e => e.name).HasColumnName("ProductName");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
