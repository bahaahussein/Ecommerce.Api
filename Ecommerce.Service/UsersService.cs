using AutoMapper;
using Ecommerce.Entites.Sql;
using Ecommerce.Models;
using Ecommerce.Models.Enums;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResultModel> AddUser(CreateUserModel model)
        {
            model.EmailAddress = model.EmailAddress.ToLower();
            var user = _mapper.Map<User>(model);
            var addedUser = _repository.AddUser(user);
            await _unitOfWork.SaveChangesAsync();
            var returnUser = _mapper.Map<UserReturnModel>(addedUser);
            return new ServiceResultModel
            {
                Data = returnUser,
                Status = EServiceActionStatus.Created
            };
        }

        public async Task<ServiceResultModel> GetUserById(long id)
        {
            var user = await _repository.GetUserById(id);
            if(user == null)
            {
                return new ServiceResultModel
                {
                    Status = EServiceActionStatus.NotFound
                };
            }
            var returnUser = _mapper.Map<UserReturnModel>(user);
            return new ServiceResultModel
            {
                Status = EServiceActionStatus.Ok,
                Data = returnUser
            };
        }

        public async Task<ServiceResultModel> SignIn(SignInModel model)
        {
            var emailAddress = model.EmailAddress.ToLower();
            var user = await _repository.GetUserByEmailAddressAndPassword(emailAddress, model.Password);
            if (user == null)
            {
                return new ServiceResultModel
                {
                    Status = EServiceActionStatus.Unauthorized
                };
            }

            var returnUser = _mapper.Map<UserReturnModel>(user);
            return new ServiceResultModel
            {
                Status = EServiceActionStatus.Ok,
                Data = returnUser
            };
        }
    }
}