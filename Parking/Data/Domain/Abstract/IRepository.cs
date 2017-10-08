using System;
using System.Threading.Tasks;

namespace ParkingApp.Data.Domain.Abstract
{
    public interface IRepository : IReadRepository, IParkingsRepository, IPlaceRepository, IVechicleRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
        where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Save();

        Task<bool> SaveAsync();

    }
}
