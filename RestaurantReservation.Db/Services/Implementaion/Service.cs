using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.Implementaion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Services.Implementaion
{
    public class Service<TEntity>: IService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "cannot be null.");

            return await _repository.Create(entity);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "cannot be null.");

            return await _repository.Update(entity);
        }

        public async Task<bool> Delete(int entityId)
        {
            return await _repository.Delete(entityId);
        }
    }
}
