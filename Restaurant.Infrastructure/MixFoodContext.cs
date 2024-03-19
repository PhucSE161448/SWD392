using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Restaurant.Domain.Entities;

namespace Restaurant.Infrastructure
{
    public partial class MixFoodContext : DbContext
    {
        public MixFoodContext()
        {
        }

        public MixFoodContext(DbContextOptions<MixFoodContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<FeedBack> FeedBacks { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<IngredientProduct> IngredientProducts { get; set; } = null!;
        public virtual DbSet<IngredientType> IngredientTypes { get; set; } = null!;
        public virtual DbSet<IngredientTypeTemplateStep> IngredientTypeTemplateSteps { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Nutrition> Nutritions { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductTemplate> ProductTemplates { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<TemplateStep> TemplateSteps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           /* if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=MixFood;TrustServerCertificate=True");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmationToken).IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Gender).HasMaxLength(255);

                entity.Property(e => e.IsConfirmed).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FeedBack>(entity =>
            {
                entity.ToTable("FeedBack");

                entity.Property(e => e.AccountId).HasColumnName("Account_Id");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FeedBacks)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__FeedBack__Accoun__06CD04F7");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("Image_Url");

                entity.Property(e => e.IngredientTypeId).HasColumnName("IngredientType_id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.IngredientType)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.IngredientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_ingredienttype_id_foreign");
            });

            modelBuilder.Entity<IngredientProduct>(entity =>
            {
                entity.HasKey(e => new { e.IngredientId, e.ProductId })
                    .HasName("PK__Ingredie__F080A71ADF39F2B5");

                entity.ToTable("IngredientProduct");

                entity.Property(e => e.IngredientId).HasColumnName("Ingredient_id");

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientProducts)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredien__Ingre__5EBF139D");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.IngredientProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredien__Produ__5FB337D6");
            });

            modelBuilder.Entity<IngredientType>(entity =>
            {
                entity.ToTable("IngredientType");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<IngredientTypeTemplateStep>(entity =>
            {
                entity.HasKey(e => new { e.IngredientTypeId, e.TemplateStepId })
                    .HasName("PK__Ingredie__D37B8AD7132C9711");

                entity.ToTable("IngredientType_TemplateStep");

                entity.Property(e => e.IngredientTypeId).HasColumnName("IngredientType_Id");

                entity.Property(e => e.TemplateStepId).HasColumnName("TemplateStep_Id");

                entity.Property(e => e.QuantityMax).HasColumnName("Quantity_Max");

                entity.Property(e => e.QuantityMin).HasColumnName("Quantity_Min");

                entity.HasOne(d => d.IngredientType)
                    .WithMany(p => p.IngredientTypeTemplateSteps)
                    .HasForeignKey(d => d.IngredientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("IngredientType_templateStep_ingredienttype_id_foreign");

                entity.HasOne(d => d.TemplateStep)
                    .WithMany(p => p.IngredientTypeTemplateSteps)
                    .HasForeignKey(d => d.TemplateStepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredienttype_templatestep_templatestep_id_foreign");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.AccountId).HasColumnName("Account_Id");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_account_id_foreign");
            });

            modelBuilder.Entity<Nutrition>(entity =>
            {
                entity.ToTable("Nutrition");

                entity.HasIndex(e => e.IngredientId, "UQ__Nutritio__C90398E29135F4A4")
                    .IsUnique();

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.HealthValue).HasMaxLength(255);

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.Property(e => e.IngredientId).HasColumnName("Ingredient_id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Nutrition1)
                    .HasMaxLength(255)
                    .HasColumnName("Nutrition");

                entity.Property(e => e.Vitamin).HasMaxLength(255);

                entity.HasOne(d => d.Ingredient)
                    .WithOne(p => p.Nutrition)
                    .HasForeignKey<Nutrition>(d => d.IngredientId)
                    .HasConstraintName("FK__Nutrition__Nutri__03F0984C");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.AccountId).HasColumnName("Account_id");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.PaymentMethodId).HasColumnName("Payment_method_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("Store_id");

                entity.Property(e => e.TotalPrice).HasColumnName("Total_price");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_account_id_foreign");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_payment_method_id_foreign");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_store_id_foreign");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK__OrderPro__08D097C182F2AEC7");

                entity.ToTable("OrderProduct");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Order__2A164134");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Produ__2B0A656D");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.IsDelete)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Payment_type");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ProductTemplateId).HasColumnName("ProductTemplate_Id");

                entity.HasOne(d => d.ProductTemplate)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTemplate_ID");
            });

            modelBuilder.Entity<ProductTemplate>(entity =>
            {
                entity.ToTable("ProductTemplate");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("Image_url");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Size)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("Store_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductTemplates)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("producttemplate_categoryid_foreign");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProductTemplates)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("producttemplate_store_id_foreign");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.EndTime).HasColumnName("End_Time");

                entity.Property(e => e.IngredientId).HasColumnName("Ingredient_Id");

                entity.Property(e => e.IsDelete)
                    .IsRequired()
                    .HasColumnName("Is_Delete")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.StartTime).HasColumnName("Start_Time");

                entity.Property(e => e.StoreId).HasColumnName("Store_Id");

                entity.HasMany(d => d.Ingredients)
                    .WithMany(p => p.Sessions)
                    .UsingEntity<Dictionary<string, object>>(
                        "IngredientSession",
                        l => l.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Ingredien__Ingre__60A75C0F"),
                        r => r.HasOne<Session>().WithMany().HasForeignKey("SessionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Ingredien__Sessi__619B8048"),
                        j =>
                        {
                            j.HasKey("SessionId", "IngredientId").HasName("PK__Ingredie__455B9AFE7F7A4C49");

                            j.ToTable("IngredientSession");

                            j.IndexerProperty<int>("SessionId").HasColumnName("Session_Id");

                            j.IndexerProperty<int>("IngredientId").HasColumnName("Ingredient_Id");
                        });
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<TemplateStep>(entity =>
            {
                entity.ToTable("TemplateStep");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ProuctTemplateId).HasColumnName("ProuctTemplate_Id");

                entity.HasOne(d => d.ProuctTemplate)
                    .WithMany(p => p.TemplateSteps)
                    .HasForeignKey(d => d.ProuctTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("templatestep_proucttemplate_id_foreign");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
