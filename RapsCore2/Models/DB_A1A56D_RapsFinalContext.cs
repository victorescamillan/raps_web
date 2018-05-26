using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RapsCore2.Models
{
    public partial class DB_A1A56D_RapsFinalContext : DbContext
    {
        public virtual DbSet<Commission> Commission { get; set; }
        public virtual DbSet<EncashmentRequest> EncashmentRequest { get; set; }
        public virtual DbSet<EntryCode> EntryCode { get; set; }
        public virtual DbSet<Genealogy> Genealogy { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<Program> Program { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<PurchaseType> PurchaseType { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<RepeatPurchaseCode> RepeatPurchaseCode { get; set; }
        public virtual DbSet<RequestType> RequestType { get; set; }
        public virtual DbSet<Stockist> Stockist { get; set; }
        public virtual DbSet<StockistDiscount> StockistDiscount { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        // Unable to generate entity type for table 'dbo.change_logs'. Please see the warning messages.

        public DB_A1A56D_RapsFinalContext(DbContextOptions<DB_A1A56D_RapsFinalContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commission>(entity =>
            {
                entity.ToTable("commission");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.DateRequested)
                    .HasColumnName("date_requested")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate()+(0.625))");

                entity.Property(e => e.IsApproved)
                    .HasColumnName("is_approved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsClaimed)
                    .HasColumnName("is_claimed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRequested)
                    .HasColumnName("is_requested")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.ProgramId).HasColumnName("program_id");

                entity.Property(e => e.SponsorId).HasColumnName("sponsor_id");

                entity.Property(e => e.SponsorId).HasColumnName("encash_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate()+(0.625))");
            });

            modelBuilder.Entity<EncashmentRequest>(entity =>
            {
                entity.ToTable("encashment_request");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");

                entity.Property(e => e.DateApproved)
                    .HasColumnName("date_approved")
                    .HasColumnType("date");

                entity.Property(e => e.DateRequested)
                    .HasColumnName("date_requested")
                    .HasColumnType("date");

                entity.Property(e => e.Gross)
                    .HasColumnName("gross")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsApproved)
                    .HasColumnName("is_approved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Net)
                    .HasColumnName("net")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RequestTypeId).HasColumnName("request_type_id");

                entity.Property(e => e.Tax)
                    .HasColumnName("tax")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate()+(1))");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<EntryCode>(entity =>
            {
                entity.ToTable("entry_code");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CenterId).HasColumnName("center_id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.IsUsed)
                    .HasColumnName("is_used")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.MobileId).HasColumnName("mobile_id");

                entity.Property(e => e.PinCode)
                    .IsRequired()
                    .HasColumnName("pin_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasColumnName("product_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Genealogy>(entity =>
            {
                entity.ToTable("genealogy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.SponsorId).HasColumnName("sponsor_id");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccount)
                    .HasColumnName("bank_account")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Beneficiary)
                    .HasColumnName("beneficiary")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .HasColumnName("contact_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateJoin)
                    .HasColumnName("date_join")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .HasColumnName("fname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsMaintenance)
                    .HasColumnName("is_maintenance")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Lname)
                    .HasColumnName("lname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mi)
                    .HasColumnName("mi")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RankId)
                    .HasColumnName("rank_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasDefaultValueSql("((5))");

                entity.Property(e => e.SponsorId)
                    .HasColumnName("sponsor_id")
                    .HasDefaultValueSql("((1000001))");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TinNo)
                    .HasColumnName("tin_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descrip)
                    .HasColumnName("descrip")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Details)
                    .HasColumnName("details")
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsEnabled)
                    .HasColumnName("is_enabled")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MemberPrice).HasColumnName("member_price");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeId).HasColumnName("product_type_id");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Srp).HasColumnName("srp");

                entity.Property(e => e.Tagline)
                    .HasColumnName("tagline")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("product_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("program");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("purchase");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PinCode)
                    .IsRequired()
                    .HasColumnName("pin_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasColumnName("product_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.PurchaseTypeId).HasColumnName("purchase_type_id");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate()+(0.625))");
            });

            modelBuilder.Entity<PurchaseType>(entity =>
            {
                entity.ToTable("purchase_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsEnabled)
                    .HasColumnName("is_enabled")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("rank");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Requirements)
                    .HasColumnName("requirements")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RepeatPurchaseCode>(entity =>
            {
                entity.ToTable("repeat_purchase_code");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CenterId).HasColumnName("center_id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.IsUsed)
                    .HasColumnName("is_used")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.MobileId).HasColumnName("mobile_id");

                entity.Property(e => e.PinCode)
                    .IsRequired()
                    .HasColumnName("pin_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasColumnName("product_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.ToTable("request_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stockist>(entity =>
            {
                entity.ToTable("stockist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate()+(0.625))");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<StockistDiscount>(entity =>
            {
                entity.ToTable("stockist_discount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CenterMembership)
                    .HasColumnName("center_membership")
                    .HasColumnType("decimal(18, 3)");

                entity.Property(e => e.CenterRepeatPurchase)
                    .HasColumnName("center_repeat_purchase")
                    .HasColumnType("decimal(18, 3)");

                entity.Property(e => e.MobileMembership)
                    .HasColumnName("mobile_membership")
                    .HasColumnType("decimal(18, 3)");

                entity.Property(e => e.MobileRepeatPurchase)
                    .HasColumnName("mobile_repeat_purchase")
                    .HasColumnType("decimal(18, 3)");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
