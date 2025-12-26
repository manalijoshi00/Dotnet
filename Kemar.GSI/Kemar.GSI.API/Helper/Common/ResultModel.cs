namespace Kemar.GSI.API.Helper.Common
{
    public enum ResultCode
    {
        Success = 200,
        SuccessfullyCreated = 201,
        SuccessfullyUpdated = 202,

        BadRequest = 400,
        ValidationError = 422,

        Unauthorized = 401,

        RecordNotFound = 404,
        NotAllowed = 405,
        DuplicateRecord = 409,

        Invalid = 204,
        ExceptionThrown = 500
    }
    public class ResultModel
    {
        public ResultCode StatusCode { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public static ResultModel Success(object? data = null)
        {
            return new ResultModel
            {
                StatusCode = ResultCode.Success,
                Message = "Success",
                Data = data
            };
        }

        public static ResultModel Created(object? data)
        {
            return new ResultModel
            {
                StatusCode = ResultCode.SuccessfullyCreated,
                Message = "Created Successfully",
                Data = data
            };
        }

        public static ResultModel Updated(object? data)
        {
            return new ResultModel
            {
                StatusCode = ResultCode.SuccessfullyUpdated,
                Message = "Updated Successfully",
                Data = data
            };
        }

        public static ResultModel Failure(string msg)
        {
            return new ResultModel
            {
                StatusCode = ResultCode.ExceptionThrown,
                Message = msg,
                Data = null
            };
        }
    }
}