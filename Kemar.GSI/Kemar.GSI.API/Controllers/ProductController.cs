using Kemar.GSI.API.Helper.Common;
using Kemar.GSI.Business.Interface;
using Kemar.GSI.Model.Filter;
using Kemar.GSI.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.GSI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("AllProducts")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();

            var result = ResultModel.Success(data);

            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
            {
                var resultNotFound = new ResultModel
                {
                    StatusCode = ResultCode.RecordNotFound,
                    Message = "Product not found",
                    Data = null
                };

                return CommonHelper.ReturnActionResultByStatus(resultNotFound, this);
            }
            var result = ResultModel.Success(data);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromQuery] int? id, [FromBody] ProductRequest request)
        {
            var data = await _service.AddOrUpdateAsync(id, request);
            var result = ResultModel.Success(data);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [HttpPost("GetByFilter")]
        public async Task<IActionResult> GetByFilter([FromBody] ProductFilterModel filter)
        {
            var data = await _service.GetByFilterAsync(filter);
            var result = ResultModel.Success(data);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            var result = ResultModel.Success("Deleted Successfully");

            return CommonHelper.ReturnActionResultByStatus(result, this);
        }
    }
}