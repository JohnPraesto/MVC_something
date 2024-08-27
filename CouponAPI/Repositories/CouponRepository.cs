using CouponAPI.Data;
using CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _db;
        public CouponRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Coupon coupon)
        {
            _db.Coupons.Add(coupon);
        }

        public async Task<IEnumerable<Coupon>> GetAllAsync()
        {
            return await _db.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetAsync(int id)
        {
            return await _db.Coupons.FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<Coupon> GetAsync(string couponName)
        {
            return await _db.Coupons.FirstOrDefaultAsync(c => c.Name == couponName.ToLower());
        }

        public async Task RemoveAsync(Coupon coupon)
        {
            _db.Coupons.Remove(coupon);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsynz(Coupon coupon)
        {
            _db.Coupons.Update(coupon);
        }
    }
}
