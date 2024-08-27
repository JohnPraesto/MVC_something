using CouponAPI.Models.DTOs;

namespace WebApplication1.Services
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            this._httpClientFactory = clientFactory;
        }
        public Task<T> CreateCouponAsync<T>(CouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteCouponAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAllCoupons<T>()
        {
            return this.SendAsync<T>(new Models.ApiRequest()
            {
                apiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponApiBase + "/api/coupons",
                AccessToken = ""
            });
        }

        public async Task<T> GetCouponById<T>(int id)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                apiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponApiBase + "/api/coupon/" + id
            });
        }

        public Task<T> UpdateCouponAsync<T>(CouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }
    }
}
