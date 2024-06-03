using Application.Interfaces;
using Application.ServiceResponse;
using Application.ViewModels.AccountDTOs;
using AutoMapper;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreateAccountDTO createdUserDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<IEnumerable<AccountDTO>>> GetUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<AccountDTO>> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<IEnumerable<AccountDTO>>> SearchUserByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<AccountDTO>> UpdateUserAsync(int id, AccountDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
