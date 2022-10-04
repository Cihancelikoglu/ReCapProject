using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private IColorService _colorService;
        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("getall")]
        public ActionResult Get()
        {
            var result = _colorService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public ActionResult GetById(int colorId)
        {
            var result = _colorService.GetById(colorId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public ActionResult Add(Color color)
        {
            var result = _colorService.Add(color);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public ActionResult Update(Color color)
        {
            var result = _colorService.Update(color);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public ActionResult Delete(Color color)
        {
            var result = _colorService.Delete(color);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
