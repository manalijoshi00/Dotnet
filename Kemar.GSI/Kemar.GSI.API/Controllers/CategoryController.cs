using Kemar.GSI.API.Helper.Common;
using Kemar.GSI.Business.Interface;
using Kemar.GSI.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.GSI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Manager")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
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
                var notFound = new ResultModel
                {
                    StatusCode = ResultCode.RecordNotFound,
                    Message = "Category not found",
                    Data = null
                };
                return CommonHelper.ReturnActionResultByStatus(notFound, this);
            }

            var result = ResultModel.Success(data);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromQuery] int? id, [FromBody] CategoryRequest request)
        {
            var data = await _service.AddOrUpdateAsync(id, request);
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
