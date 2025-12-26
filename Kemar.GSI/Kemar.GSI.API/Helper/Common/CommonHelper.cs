using Microsoft.AspNetCore.Mvc;

namespace Kemar.GSI.API.Helper.Common
{
    public static class CommonHelper
    {
        //    public static IActionResult ReturnActionResultByStatus(ResultModel result, ControllerBase cntbase)
        //    {
        //        if (result == null)
        //        {
        //            return cntbase.NotFound(result);
        //        }
        //        else if (result.StatusCode == ResultCode.SuccessfullyCreated)
        //        {
        //            return cntbase.Created("", result);
        //        }
        //        else if (result.StatusCode == ResultCode.SuccessfullyUpdated)
        //        {
        //            return cntbase.Ok(result);
        //        }
        //        else if (result.StatusCode == ResultCode.Success)
        //        {
        //            return cntbase.Ok(result);
        //        }
        //        else if (result.StatusCode == ResultCode.Unauthorized)
        //        {
        //            return cntbase.Unauthorized(result);
        //        }
        //        else if (result.StatusCode == ResultCode.DuplicateRecord)
        //        {
        //            return cntbase.Conflict(result);
        //        }
        //        else if (result.StatusCode == ResultCode.RecordNotFound)
        //        {
        //            return cntbase.NotFound(result);
        //        }
        //        else if (result.StatusCode == ResultCode.NotAllowed)
        //        {
        //            return cntbase.NotFound(result);
        //        }
        //        else if (result.StatusCode == ResultCode.Invalid)
        //        {
        //            return cntbase.NotFound(result);
        //        }
        //        else if (result.StatusCode == ResultCode.ExceptionThrown)
        //        {
        //            return cntbase.NotFound(result);
        //        }
        //        return null;
        //    }
        //}

        public static IActionResult ReturnActionResultByStatus(ResultModel result, ControllerBase cntbase)
        {
            if (result == null)
            {
                return cntbase.StatusCode(500, "Unexpected error");
            }

            return result.StatusCode switch
            {
                ResultCode.Success => cntbase.Ok(result),
                ResultCode.SuccessfullyCreated => cntbase.Created("", result),
                ResultCode.SuccessfullyUpdated => cntbase.Ok(result),

                ResultCode.BadRequest => cntbase.BadRequest(result),
                ResultCode.ValidationError => cntbase.UnprocessableEntity(result),
                ResultCode.Unauthorized => cntbase.Unauthorized(result),
                ResultCode.DuplicateRecord => cntbase.Conflict(result),
                ResultCode.RecordNotFound => cntbase.NotFound(result),
                ResultCode.NotAllowed => cntbase.StatusCode(405, result),

                ResultCode.ExceptionThrown => cntbase.StatusCode(500, result),

                _ => cntbase.StatusCode(500, result)
            };
        }
    }
}