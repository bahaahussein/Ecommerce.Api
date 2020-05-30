using Ecommerce.Api.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ECommerce.Api.UnitTests
{
    public abstract class ControllerTestsBase
    {
        protected bool IsModelStateValid(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            return isModelStateValid;
        }
    }
}