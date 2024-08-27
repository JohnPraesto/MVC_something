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
        public async Task<T> CreateCouponAsync<T>(CouponDTO couponDTO)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                apiType = StaticDetails.ApiType.POST,
                Data = couponDTO,
                Url = StaticDetails.CouponApiBase + "/api/coupon",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteCouponAsync<T>(int id)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                apiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.CouponApiBase + "api/cupone/" + id,
                AccessToken = ""
            });
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
                Url = StaticDetails.CouponApiBase + "/api/coupon/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateCouponAsync<T>(CouponDTO couponDTO)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                apiType = StaticDetails.ApiType.PUT,
                Data = couponDTO,
                Url = StaticDetails.CouponApiBase + "/api/coupon",
                AccessToken = ""
            });
        }
    }
}