using Kemar.GSI.API.Helper.Common;
using Kemar.GSI.Model.Exceptions;
using Kemar.GSI.Business.Interface;
using Kemar.GSI.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemar.GSI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            var result = ResultModel.Success(data);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
            {
                var notFound = new ResultModel
                {
                    StatusCode = ResultCode.RecordNotFound,
                    Message = "Order not found"
                };
                return CommonHelper.ReturnActionResultByStatus(notFound, this);
            }

            var result = ResultModel.Success(data);
            return CommonHelper.ReturnActionResultByStatus(result, this);
        }

        [Authorize(Roles = "User")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] OrderRequest request)
        {
            try
            {
                var data = await _service.CreateOrderAsync(request);
                var result = ResultModel.Created(data);
                return CommonHelper.ReturnActionResultByStatus(result, this);
            }
            catch (BusinessException ex)
            {
                var error = new ResultModel
                {
                    StatusCode = ResultCode.ValidationError,
                    Message = ex.Message
                };
                return CommonHelper.ReturnActionResultByStatus(error, this);
            }
            catch (Exception ex)
            {
                var error = new ResultModel
                {
                    StatusCode = ResultCode.ExceptionThrown,
                    Message = "Internal server error"
                };
                return CommonHelper.ReturnActionResultByStatus(error, this);
            }
        }
        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var success = await _service.DeleteOrderAsyn(id);

        //    if (!success)
        //        return CommonHelper.ReturnActionResultByStatus(
        //            new ResultModel
        //            {
        //                StatusCode = ResultCode.RecordNotFound,
        //                Message = "Order not found"
        //            }, this);

        //    return CommonHelper.ReturnActionResultByStatus(
        //        ResultModel.Success("Order deleted successfully"), this);
        //}


    }
}
