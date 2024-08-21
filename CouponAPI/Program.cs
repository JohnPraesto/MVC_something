
using AutoMapper;
using CouponAPI.Data;
using CouponAPI.Models;
using CouponAPI.Models.DTOs;
using FluentValidation;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            //app.MapGet("/Hellosut23", () => "hello från minimal api");

            app.MapGet("/api/coupons", () =>
            {
                return Results.Ok(CouponStore.couponList);
            }).WithName("GetCoupons").Produces<IEnumerable<Coupon>>(200);

            app.MapGet("/api/coupon/{id:int}", (int id) =>
            {
                return Results.Ok(CouponStore.couponList.FirstOrDefault(c => c.ID == id));
            }).WithName("GetCoupon").Produces<Coupon>(200);

            app.MapPost("/api/coupon", async (IValidator<CouponCreateDTO> validator, IMapper _mapper, CouponCreateDTO coupon_C_DTO) =>
            {
                var validatorResult = await validator.ValidateAsync(coupon_C_DTO);
                // validation
                if (!validatorResult.IsValid) // förut: om id eller name är null
                {
                    return Results.BadRequest("Invalid ID or Coupon Name");
                }
                if (CouponStore.couponList.FirstOrDefault(c => c.Name.ToLower() == coupon_C_DTO.Name.ToLower()) != null)
                {
                    return Results.BadRequest("Coupon name already exists");
                }

                //// Utan automapper
                //Coupon coupon = new Coupon()
                //{
                //    Name = coupon_C_DTO.Name,
                //    Percent = coupon_C_DTO.Percent,
                //    IsActive = coupon_C_DTO.IsActive,
                //};

                // Med AutoMapper
                Coupon coupon2 = _mapper.Map<Coupon>(coupon_C_DTO);
                coupon2.ID = CouponStore.couponList.OrderByDescending(c => c.ID).FirstOrDefault().ID + 1;
                CouponStore.couponList.Add(coupon2);

                CouponDTO couponDTO = _mapper.Map<CouponDTO>(coupon2);

                return Results.CreatedAtRoute("GetCoupon", new { id = coupon2.ID }, coupon2);
            }).WithName("CreateCoupon").Produces<CouponCreateDTO>(201).Accepts<Coupon>("application/json").Produces(400);

            app.MapPut("/api/coupon", (CouponUpdateDTO coupon_U_DTO) =>
            {

            });

            app.Run();
        }
    }
}
