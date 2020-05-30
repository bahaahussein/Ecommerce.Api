using Ecommerce.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class ServiceResultModel
    {
        public object Data { get; set; }
        public EServiceActionStatus Status { get; set; }
        public string Message { get; set; }
    }
}
