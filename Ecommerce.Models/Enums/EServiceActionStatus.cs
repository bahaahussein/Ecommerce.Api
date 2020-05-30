using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Enums
{
    public enum EServiceActionStatus
    {
        Ok = 200,
        Created = 201,
        NotModified = 304,
        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        NotFound = 404,
        Timeout = 408,
        Conflict = 409,
        RequestEntityTooLarge = 413,
        UnsupportedMediaType = 415,
        UpgradeRequired = 426,
        GeneralError = 500,
        Deleted = 502,
        NothingModified = 503,
        Error = 504,
        AlreadyExisting = 505,
        NotValid = 506,
        Duplicated = 507
    }
}
