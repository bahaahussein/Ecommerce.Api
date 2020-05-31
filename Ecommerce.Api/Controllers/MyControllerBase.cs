using Ecommerce.Models;
using Ecommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class MyControllerBase : ControllerBase
    {
        protected JsonResult ReturnResponse(EServiceActionStatus returnCode, object data = null, string message = null)
        {
            var returnModel = new ApiReturnModel
            {
                Data = data,
                Message = message
            };
            var result = new JsonResult(returnModel)
            {
                StatusCode = (int)returnCode
            };
            return result;
        }

        protected JsonResult ReturnResponse(ServiceResultModel model)
        {
            var returnModel = new ApiReturnModel
            {
                Data = model.Data,
                Message = model.Message
            };
            var result = new JsonResult(returnModel)
            {
                StatusCode = (int)model.Status
            };
            return result;
        }

        // for unit testing
        [ApiExplorerSettings(IgnoreApi = true)] // for not seen in swagger
        public void AddTestErrorToModelState()
        {
            ModelState.AddModelError("test", "test");
        }
    }
}