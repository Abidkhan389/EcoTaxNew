using System;
using GreenLife.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GreenLife.Models
{
    public partial class GreenLifeFinalContext : IdentityDbContext<ApplicationUser>
    {
        //public GreenLifeFinalContext()
        //{
        //}

        public GreenLifeFinalContext(DbContextOptions<GreenLifeFinalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<AspNetRoles> UserRoles { get; set; }
        public virtual DbSet<ProductionBrand> ProductionBrands { get; set; }
        public virtual DbSet<ProductionCategory> ProductionCategories { get; set; }
        public virtual DbSet<ProductionProduct> ProductionProducts { get; set; }
        public virtual DbSet<ProductionStock> ProductionStocks { get; set; }
        public virtual DbSet<SalesCustomer> SalesCustomers { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public virtual DbSet<SalesStaff> SalesStaffs { get; set; }
        public virtual DbSet<SalesStore> SalesStores { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-RMDDIVJ\\SQLEXPRESS;Database=GreenLifeFinal;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("cities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cityName");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.ToTable("Messages");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstname");
                entity.Property(e => e.lastname)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("lastname");
                entity.Property(e => e.email)
                   .HasMaxLength(80)
                   .IsUnicode(false)
                   .HasColumnName("email");
                entity.Property(e => e.address)
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("address");
                entity.Property(e => e.phonenumber)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .HasColumnName("phonenumber");
                entity.Property(e => e.message)
                   .HasMaxLength(1000)
                   .IsUnicode(false)
                   .HasColumnName("message");
            });

            modelBuilder.Entity<ProductionBrand>(entity =>
            {
                entity.HasKey(e => e.BrandId)
                    .HasName("PK__producti__5E5A8E27DFB4992B");

                entity.ToTable("production_brands");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("brand_name");
            });

            modelBuilder.Entity<ProductionCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__producti__D54EE9B4C7A46B56");

                entity.ToTable("production_categories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<ProductionProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__producti__47027DF5B4D31F69");

                entity.ToTable("production_products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ListPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("list_price");
                entity.Property(e => e.actual_product_price)
                  .HasColumnType("decimal(10, 2)")
                  .HasColumnName("actual_product_price");

                entity.Property(e => e.ModelYear).HasColumnName("model_year");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ProductionProducts)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__productio__brand__32E0915F");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductionProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__productio__categ__31EC6D26");
            });

            modelBuilder.Entity<ProductionStock>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId })
                    .HasName("PK__producti__E68284D349181D93");

                entity.ToTable("production_stocks");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductionStocks)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__productio__produ__4316F928");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProductionStocks)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__productio__store__4222D4EF");
            });

            modelBuilder.Entity<SalesCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__sales_cu__CD65CB858B3303EB");

                entity.ToTable("sales_customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Cityid).HasColumnName("cityid");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.State)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.Street)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("street");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("zip_code");
                entity.Property(e => e.orderstatus)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("orderstatus");
                entity.Property(e => e.emailopt)
                   .HasMaxLength(5)
                   .IsUnicode(false)
                   .HasColumnName("emailopt");
                entity.Property(e => e.TransactionTID)
                  .HasMaxLength(30)
                  .IsUnicode(false)
                  .HasColumnName("TransactionTID");
                entity.Property(e => e.paymentmode)
                  .HasMaxLength(30)
                  .IsUnicode(false)
                  .HasColumnName("paymentmode");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.SalesCustomers)
                    .HasForeignKey(d => d.Cityid)
                    .HasConstraintName("FK__sales_cus__cityi__35BCFE0A");
            });

            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__sales_or__46596229C9FE6B72");

                entity.ToTable("sales_orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.RequiredDate)
                    .HasColumnType("date")
                    .HasColumnName("required_date");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("date")
                    .HasColumnName("shipped_date");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__sales_ord__custo__38996AB5");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__sales_ord__staff__3A81B327");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__sales_ord__store__398D8EEE");
            });

            modelBuilder.Entity<SalesOrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ItemId })
                    .HasName("PK__sales_or__837942D482EFCEDE");

                entity.ToTable("sales_order_items");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("discount");

                entity.Property(e => e.ListPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("list_price");
                entity.Property(e => e.actual_product_price)
                   .HasColumnType("decimal(10, 2)")
                   .HasColumnName("actual_product_price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.SalesOrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__sales_ord__order__3E52440B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesOrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__sales_ord__produ__3F466844");
            });

            modelBuilder.Entity<SalesStaff>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__sales_st__1963DD9C9A66C02D");

                entity.ToTable("sales_staffs");

                entity.HasIndex(e => e.Email, "UQ__sales_st__AB6E6164532E86A7")
                    .IsUnique();

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__sales_sta__manag__2B3F6F97");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SalesStaffs)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__sales_sta__store__2A4B4B5E");
            });

            modelBuilder.Entity<SalesStore>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK__sales_st__A2F2A30CEA58DAD6");

                entity.ToTable("sales_stores");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.Cityid).HasColumnName("cityid");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.State)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("store_name");

                entity.Property(e => e.Street)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("street");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("zip_code");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.SalesStores)
                    .HasForeignKey(d => d.Cityid)
                    .HasConstraintName("FK__sales_sto__cityi__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
