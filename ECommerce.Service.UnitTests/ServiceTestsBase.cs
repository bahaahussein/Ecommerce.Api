using AutoMapper;
using Ecommerce.Entites.Sql;
using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.AutoMapper.Profiles;

namespace ECommerce.Service.UnitTests
{
    public abstract class ServiceTestsBase
    {
        protected Mock<IUsersRepository> _usersRepositoryMock { get; set; }
        protected IMapper _mapper { get; set; }
        protected Mock<IUnitOfWork> _unitOfWorkMock { get; set; }

        public ServiceTestsBase()
        {
            InitializeUsersRepositoryMock();
            InitializeUnitOfWorkMock();
            InitializeMapperMock();
        }

        #region private methods
        private void InitializeUsersRepositoryMock()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _usersRepositoryMock
                .Setup(x => x.AddUser(It.IsAny<User>())).Returns((User u) => u);
        }

        private void InitializeMapperMock()
        {
            var profile = new UserProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(profile));
            _mapper = new Mapper(configuration);
        }

        private void InitializeUnitOfWorkMock()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(default(int));
        }
        #endregion
    }
}