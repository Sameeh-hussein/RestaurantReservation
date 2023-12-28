using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<TEntity> Create(TEntity entity);
        Task<bool> Delete(int entityId);
        Task<TEntity> Update(TEntity entity);
    }
}
