using CouponAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            this._couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO> list = new List<CouponDTO>();
            var response = await _couponService.GetAllCoupons<ResponseDto>();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            CouponDTO cDto = new CouponDTO();
            var response = await _couponService.GetCouponById<ResponseDto>(id);
            if(response != null && response.IsSuccess)
            {
                CouponDTO model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return View();
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.CreateCouponAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateCoupon(int couponId)
        {
            var response = await _couponService.GetCouponById<ResponseDto>(couponId);

            if (response != null && response.IsSuccess)
            {
                CouponDTO model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCoupon(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.UpdateCouponAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            var response = await _couponService.GetCouponById<ResponseDto>(couponId);

            if (response != null && response.IsSuccess)
            {
                CouponDTO model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.DeleteCouponAsync<ResponseDto>(model.ID);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return NotFound();
        }
    }
}