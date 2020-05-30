using Ecommerce.Models;
using Ecommerce.Models.Enums;
using Ecommerce.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Controllers
{
    public class UsersController : MyControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<JsonResult> GetUserById(long id)
        {
            var result = await _usersService.GetUserById(id);
            return ReturnResponse(result);
        }

        [HttpPost]
        public async Task<JsonResult> CreateUser([FromBody]CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return ReturnResponse(EServiceActionStatus.BadRequest);
            }
            var result = await _usersService.AddUser(model);
            return ReturnResponse(result);
        }

        [HttpPost]
        public async Task<JsonResult> SignIn([FromBody]SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return ReturnResponse(EServiceActionStatus.BadRequest);
            }
            var result = await _usersService.SignIn(model);
            return ReturnResponse(result);
        }
    }
}