using AutoMapper;
using ZwajApp.API.Data.Infrastructure;
using ZwajApp.API.Service.Interfaces;
using ZwajApp.API.ViewModels;

namespace ZwajApp.API.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UserService(IUnitOfWork _unitOfWork, IMapper _mapper) 
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public void Add(UserAddVM userAddVM)
        {
            throw new System.NotImplementedException();
        }
    }
}
