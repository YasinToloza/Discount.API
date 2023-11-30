using DIscount.Spa.API.Models;
using DIscount.Spa.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DIscount.Spa.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository discountRepository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        [HttpGet]
        [Route("[action]/{productName}")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            return Ok(await discountRepository.GetDiscount(productName));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            return StatusCode(StatusCodes.Status201Created, await discountRepository.CreateDiscount(coupon));
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult<bool>> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await discountRepository.UpdateDiscount(coupon));
        }

        [HttpDelete]
        [Route("[action]/{productName}")]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return Ok(await discountRepository.DeleteDiscount(productName));
        }
    }
}
