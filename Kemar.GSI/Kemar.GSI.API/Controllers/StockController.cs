using Kemar.GSI.API.Helper.Common;
using Kemar.GSI.Business.Interface;
using Kemar.GSI.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.GSI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly IStockService _service;

        public StockController(IStockService service)
        {
            _service = service;
        }

        [HttpGet("All")]
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
                    Message = "Stock record not found",
                    Data = null
                };
                return CommonHelper.ReturnActionResultByStatus(notFound, this);
            }

            var result = ResultModel.Success(data);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] StockRequest request)
        {
            var data = await _service.AddStockAsync(request);
            var result = ResultModel.Success(data);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("ReduceFIFO")]
        public async Task<IActionResult> ReduceStock([FromQuery] int productId, [FromQuery] int quantity)
        {
            var success = await _service.ReduceStockFIFOAsync(productId, quantity);

            if (!success)
            {
                var failed = new ResultModel
                {
                    StatusCode = ResultCode.BadRequest,
                    Message = "Not enough stock available OR product not found",
                    Data = null
                };
                return CommonHelper.ReturnActionResultByStatus(failed, this);
            }

            var result = ResultModel.Success("Stock reduced successfully (FIFO applied)");
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
            {
                var notFound = new ResultModel
                {
                    StatusCode = ResultCode.RecordNotFound,
                    Message = "Stock record not found",
                    Data = null
                };
                return CommonHelper.ReturnActionResultByStatus(notFound, this);
            }

            var result = ResultModel.Success("Deleted successfully");
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }
    }
}
