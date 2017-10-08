using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingApp.Data.Domain.Abstract;

namespace ParkingApp.Data.Domain
{
    public partial class Repository<TContext> : ReadRepository<TContext>, IRepository
     where TContext : DbContext
    {
        public Repository(TContext context)
            : base(context)
        {
        }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = createdBy;
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual async Task<bool> SaveAsync()
        {
            
            return (await context.SaveChangesAsync()) > 0;
            
        }

        protected virtual void ThrowEnhancedValidationException(Exception e)
        {
            var errorMessages = e.Message;

            var fullErrorMessage = string.Join("; ", errorMessages);
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            throw new Exception(exceptionMessage, e.InnerException);
        }
    }
}
