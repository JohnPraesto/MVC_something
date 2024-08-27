using CouponAPI.Models.DTOs;

namespace WebApplication1.Services
{
    public interface ICouponService
    {
        Task<T> GetAllCoupons<T>();
        Task<T> GetCouponById<T>(int id);
        Task<T> CreateCouponAsync<T>(CouponDTO couponDTO);
        Task<T> UpdateCouponAsync<T>(CouponDTO couponDTO);
        Task<T> DeleteCouponAsync<T>(int id);
    }
}
