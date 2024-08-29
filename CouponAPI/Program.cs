
using AutoMapper;
using CouponAPI.Data;
using CouponAPI.EndPoints;
using CouponAPI.Models;
using CouponAPI.Models.DTOs;
using CouponAPI.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionToDb")));

            builder.Services.AddScoped<ICouponRepository, CouponRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.MapGet("/api/coupons", () =>
            //{
            //    APIResponse response = new APIResponse();
            //    response.Result = CouponStore.couponList;
            //    response.IsSuccess = true;
            //    response.StatusCode = System.Net.HttpStatusCode.OK;

            //    return Results.Ok(response);
            //}).WithName("GetCoupons").Produces(200);

            //app.MapGet("/api/coupon/{id:int}", (int id) =>
            //{
            //    APIResponse response = new APIResponse();
            //    response.Result = CouponStore.couponList.FirstOrDefault(c => c.ID == id);
            //    response.IsSuccess = true;
            //    response.StatusCode = System.Net.HttpStatusCode.OK;

            //    return Results.Ok(response);
            //}).WithName("GetCoupon").Produces(200);

            //app.MapPost("/api/coupon", async (IValidator<CouponCreateDTO> validator, IMapper _mapper, CouponCreateDTO coupon_C_DTO) =>
            //{
            //    APIResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            //    var validatorResult = await validator.ValidateAsync(coupon_C_DTO);
            //    // validation
            //    if (!validatorResult.IsValid) // förut: om id eller name är null
            //    {
            //        return Results.BadRequest(response);
            //    }
            //    if (CouponStore.couponList.FirstOrDefault(c => c.Name.ToLower() == coupon_C_DTO.Name.ToLower()) != null)
            //    {
            //        response.ErrorMessages.Add("Coupon Name already exists");
            //        return Results.BadRequest(response);
            //    }

            //    //// Utan automapper
            //    //Coupon coupon = new Coupon()
            //    //{
            //    //    Name = coupon_C_DTO.Name,
            //    //    Percent = coupon_C_DTO.Percent,
            //    //    IsActive = coupon_C_DTO.IsActive,
            //    //};

            //    // Med AutoMapper
            //    Coupon coupon2 = _mapper.Map<Coupon>(coupon_C_DTO);
            //    coupon2.ID = CouponStore.couponList.OrderByDescending(c => c.ID).FirstOrDefault().ID + 1;
            //    CouponStore.couponList.Add(coupon2);

            //    CouponDTO couponDTO = _mapper.Map<CouponDTO>(coupon2);
            //    response.Result = couponDTO;
            //    response.IsSuccess = true;
            //    response.StatusCode = System.Net.HttpStatusCode.Created;

            //    return Results.Ok(response);
            //}).WithName("CreateCoupon").Produces<CouponCreateDTO>(201).Accepts<APIResponse>("application/json").Produces(400);

            app.MapPut("/api/coupon", async (IMapper _mapper, IValidator<CouponUpdateDTO> _validator, CouponUpdateDTO coupon_U_DTO) =>
            {
                APIResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

                // add validation
                var validateResult = await _validator.ValidateAsync(coupon_U_DTO);
                if (!validateResult.IsValid)
                {
                    response.ErrorMessages.Add(validateResult.Errors.FirstOrDefault().ToString());
                }

                Coupon couponFromStore = CouponStore.couponList.FirstOrDefault(c => c.ID == coupon_U_DTO.ID);
                couponFromStore.IsActive = coupon_U_DTO.IsActive;
                couponFromStore.Name = coupon_U_DTO.Name;
                couponFromStore.Percent = coupon_U_DTO.Percent;
                couponFromStore.LastUpdate = DateTime.Now;

                // automapper
                Coupon coupon = _mapper.Map<Coupon>(coupon_U_DTO);

                response.Result = _mapper.Map<CouponDTO>(couponFromStore);
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);
            })
            .WithName("UpdateCoupon")
            .Accepts<CouponUpdateDTO>("application/json")
            .Produces<CouponUpdateDTO>(200).Produces(400);

            app.MapDelete("/api/coupon/{id:int}", (int id) =>
            {
                APIResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

                Coupon couponFromStore = CouponStore.couponList.FirstOrDefault(c => c.ID == id);

                if(couponFromStore != null)
                {
                    CouponStore.couponList.Remove(couponFromStore);
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.NoContent;
                    return Results.Ok(response);
                }
                else
                {
                    response.ErrorMessages.Add("Invalid ID");
                    return Results.BadRequest(response);
                }
            }).WithName("DeleteCoupon");

            app.ConfigurationCouponEndpoint();
            app.Run();
        }
    }
}
