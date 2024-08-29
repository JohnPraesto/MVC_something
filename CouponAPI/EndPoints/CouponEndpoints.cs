using AutoMapper;
using CouponAPI.Data;
using CouponAPI.Models;
using CouponAPI.Models.DTOs;
using CouponAPI.Repositories;

namespace CouponAPI.EndPoints
{
    public static class CouponEndpoints
    {


        public static void ConfigurationCouponEndpoint(this WebApplication app)
        {
            app.MapGet("/api/coupons", GetAllCoupons).WithName("GetCoupons").Produces<APIResponse>();
            app.MapGet("/api/coupon/{id:int}", GetCoupon).WithName("GetCoupon").Produces<APIResponse>();
            app.MapPost("/api/coupon", CreateCoupon)
                .WithName("CreateCoupon")
                .Accepts<CouponCreateDTO>("application/json")
                .Produces(201)
                .Produces(400);
        }

        private async static Task<IResult> GetAllCoupons(ICouponRepository _couponRepo)
        {
            APIResponse response = new APIResponse();

            response.Result = await _couponRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return Results.Ok(response);
        }


        private async static Task<IResult> GetCoupon(ICouponRepository _couponRepo, int id)
        {
            APIResponse response = new APIResponse();
            response.Result = await _couponRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> CreateCoupon(ICouponRepository _couponRepo, IMapper _mapper, CouponCreateDTO coupon_C_DTO)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if(_couponRepo.GetAsync(coupon_C_DTO.Name).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Coupon Name already exists");
                return Results.BadRequest(response);
            }

            Coupon coupon = _mapper.Map<Coupon>(coupon_C_DTO);
            await _couponRepo.CreateAsync(coupon);
            await _couponRepo.SaveAsync();
            CouponDTO couponDto = _mapper.Map<CouponDTO>(coupon);

            response.Result = couponDto;
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return Results.Ok(response);
        }
    }
}
