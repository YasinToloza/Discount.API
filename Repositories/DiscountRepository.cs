using Dapper;
using DIscount.Spa.API.Models;
using Npgsql;

namespace DIscount.Spa.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration Configuration;
        private readonly string ConnectionString;
        public DiscountRepository(IConfiguration Configuration) 
        {
            this.Configuration = Configuration;
            ConnectionString = Configuration.GetValue<string>("PostgreDBSettings:ConnectionString");
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var queryString = "SELECT * FROM Coupon Where ProductName=@ProductName";
            using var pgConnection = new NpgsqlConnection(ConnectionString);
            var coupon = await pgConnection.QueryFirstOrDefaultAsync<Coupon>(queryString,
             new { ProductName = productName });
            if (coupon == null) 
            {
                return new Coupon
                {
                    Id = 0,
                    ProductName = "Not Found",
                    Description = "",
                    Amount = 0
                };
            }
            return coupon;
        }

        public async Task<Coupon> CreateDiscount(Coupon coupon)
        {
            var queryString = "INSERT INTO  Coupon(ProductName, Description, Amount) VALUES(@ProductName, @Description, @Amount) returning id;";
            using var pgConnection = new NpgsqlConnection(ConnectionString);
            var id = await pgConnection.ExecuteScalarAsync<int>(queryString, coupon);
            coupon.Id = id;
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var queryString = "UPDATE Coupon SET ProductName=@ProductName, Description=@Description,Amount=@Amount WHERE Id=@Id";
            using var pgConnection = new NpgsqlConnection(ConnectionString);
            var rowCount = await pgConnection.ExecuteAsync(queryString, coupon);
            return rowCount > 0 ? true : false;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var queryString = "DELETE FROM Coupon WHERE ProductName=@ProductName";
            using var pgConnection = new NpgsqlConnection(ConnectionString);
            var rowCount = await pgConnection.ExecuteAsync(queryString, new
            {
                ProductName = productName
            });
            return rowCount > 0 ? true : false;
        }
    }
}
