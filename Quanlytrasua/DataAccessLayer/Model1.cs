using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Quanlytrasua.DataAccessLayer
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillInfo> BillInfoes { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodCategory> FoodCategories { get; set; }
        public virtual DbSet<SUMBILLDATE> SUMBILLDATEs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TableFood> TableFoods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasMany(e => e.BillInfoes)
                .WithRequired(e => e.Bill)
                .HasForeignKey(e => e.idBill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.BillInfoes)
                .WithRequired(e => e.Food)
                .HasForeignKey(e => e.idFood)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodCategory>()
                .HasMany(e => e.BillInfoes)
                .WithOptional(e => e.FoodCategory)
                .HasForeignKey(e => e.idCategory);

            modelBuilder.Entity<FoodCategory>()
                .HasMany(e => e.Foods)
                .WithRequired(e => e.FoodCategory)
                .HasForeignKey(e => e.idCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUMBILLDATE>()
                .HasMany(e => e.Bills)
                .WithOptional(e => e.SUMBILLDATE)
                .HasForeignKey(e => e.idSumBill);

            modelBuilder.Entity<TableFood>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.TableFood)
                .HasForeignKey(e => e.idTable)
                .WillCascadeOnDelete(false);
        }
    }
}
