using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Infra.Models;
using ElBayt.Common.Security;
using ElBayt.Infra.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElBayt.Infra.Context
{
    public class ElBaytContext : IdentityDbContext<AppUser>  
    {
        private readonly IUserIdentity _userIdentity;

        public ElBaytContext(DbContextOptions<ElBaytContext> options)
          : base(options)
        {
        }

        public virtual DbSet<ProductModel> Products { get; set; }
        public virtual DbSet<ProductCategoryModel> ProductCategories { get; set; }
        public virtual DbSet<ProductTypeModel> ProductTypes { get; set; }
        public virtual DbSet<AreaModel> Areas { get; set; }
        public virtual DbSet<CountryModel> Countries { get; set; }
        public virtual DbSet<GovernorateModel> Governorates { get; set; }
        public virtual DbSet<ProductDepartmentModel> ProductDepartments { get; set; }


        public virtual void MarkEntryAsModified(BaseGeneralModel entity, EntityState entityState = EntityState.Modified)
        {
            Entry(entity).State = entityState;
        }

        public virtual void MarkPropertyAsModified(BaseGeneralModel entity, string propertyName, bool isModified = true)
        {
            Entry(entity).Property(propertyName).IsModified = isModified;
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseGeneralModel
                            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var identityName = _userIdentity?.Name ?? "Unknown";
            var dateNow = DateTime.Now;
            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is BaseGeneralModel entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = entity.ModifiedBy = identityName;
                        entity.CreatedDate = entity.ModifiedDate = dateNow;
                        entity.IsDeleted = false;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.ModifiedBy = identityName;
                        entity.ModifiedDate = dateNow;
                    }
                }
            }

            return base.SaveChanges();
        }

       
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseGeneralModel
                            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var identityName = _userIdentity?.Name ?? "Unknown";
            var dateNow = DateTime.Now;
            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is BaseGeneralModel entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = entity.ModifiedBy = identityName;
                        entity.CreatedDate = entity.ModifiedDate = dateNow;
                        entity.IsDeleted = false;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.ModifiedBy = identityName;
                        entity.ModifiedDate = dateNow;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        public Task<int> SaveChangesAsync()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseGeneralModel
                            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var identityName = _userIdentity?.Name ?? "Unknown";
            var dateNow = DateTime.Now;
            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is BaseGeneralModel entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = entity.ModifiedBy = identityName;
                        entity.CreatedDate = entity.ModifiedDate = dateNow;
                        entity.IsDeleted = false;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.ModifiedBy = identityName;
                        entity.ModifiedDate = dateNow;
                    }
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
