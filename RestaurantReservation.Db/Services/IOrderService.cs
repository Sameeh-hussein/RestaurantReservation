﻿using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrderInReservationAsync(int reservationId);
        Task<Order?> GetOrderByIdInReservationAsync(int reservationId, int orderId);
        Task<Order> CreateOrderInReservationAsync(int reservationId, Order order);
        Task DeleteAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<bool> OrderExist(int reservationId, int orderId);
    }
}
