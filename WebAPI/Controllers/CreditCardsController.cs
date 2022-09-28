using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private readonly ICreditCardService _creditCartService;
        public CreditCardsController(ICreditCardService creditCartService)
        {
            _creditCartService = creditCartService;
        }

        [HttpGet("getall")]
        public ActionResult Get()
        {
            var result = _creditCartService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("creditcartcontrol")]
        public IActionResult RentalDateControl(CreditCard creditCard)
        {
            var result = _creditCartService.CheckCreditCart(creditCard);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
