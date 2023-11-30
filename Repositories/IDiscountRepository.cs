using DIscount.Spa.API.Models;

namespace DIscount.Spa.API.Repositories
{
    public interface IDiscountRepository
    {
        public Task<Coupon> GetDiscount(string productName);
        public Task<Coupon> CreateDiscount(Coupon coupon);
        public Task<bool> UpdateDiscount(Coupon coupon);
        public Task<bool> DeleteDiscount(string productName);
    }
}
