using Business.Abstract;
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
    public class UserFindexController : ControllerBase
    {
        IUserFindexService _userFindexService;
        public UserFindexController(IUserFindexService userFindexService)
        {
            _userFindexService = userFindexService;
        }


        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _userFindexService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _userFindexService.GetByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("findexcontrol")]
        public IActionResult FindexControl(int userId, int carFindex)
        {
            var result = _userFindexService.FindexControl(userId,carFindex);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
