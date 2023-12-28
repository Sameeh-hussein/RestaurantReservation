using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories.Implementaion
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly RestaurantReservationDbContext _Context;

        public Repository(RestaurantReservationDbContext context)
        {
            _Context = context;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            _Context.Set<TEntity>().Add(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int entityId)
        {
            var entity = await _Context.Set<TEntity>().FindAsync(entityId);
            if (entity != null)
            {
                _Context.Set<TEntity>().Remove(entity);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _Context.Set<TEntity>().Update(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }
    }
}
