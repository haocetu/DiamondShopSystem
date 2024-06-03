using Application.Interfaces;
using Application.ServiceResponse;
using Application.ViewModels.AccountDTOs;
using AutoMapper;
using Domain.Entities;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        public async Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreateAccountDTO createdAccountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();

            var exist = await _unitOfWork.AccountRepository.CheckEmailNameExited(createdAccountDTO.Email);
            var existed = await _unitOfWork.AccountRepository.CheckPhoneNumberExited(createdAccountDTO.PhoneNumber);

            if (exist)
            {
                response.Success = false;
                response.Message = "Email is existed";
                return response;
            }
            else if (existed)
            {
                response.Success = false;
                response.Message = "Phone is existed";
                return response;
            }
            try
            {
                var account = _mapper.Map<Account>(createdAccountDTO);
                account.Password = Utils.HashPassword.HashWithSHA256(createdAccountDTO.Password);

                
                account.IsDeleted = false;

                await _unitOfWork.AccountRepository.AddAsync(account);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var accountDTO = _mapper.Map<AccountDTO>(account);
                    response.Data = accountDTO; // Chuyển đổi sang AccountDTO
                    response.Success = true;
                    response.Message = "User created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the user.";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            var response = new ServiceResponse<bool>();

            var exist = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (exist == null || exist.IsDeleted == true)
            {
                response.Success = false;
                response.Message = "Account is not existed";
                return response;
            }

            try
            {
                _unitOfWork.AccountRepository.SoftRemove(exist);
                exist.IsDeleted = true;
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Account deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetUserAsync()
        {
            var _response = new ServiceResponse<IEnumerable<AccountDTO>>();
            try
            {
                var users = await _unitOfWork.AccountRepository.GetAllAsync();

                var userDTOs = new List<AccountDTO>();

                foreach (var user in users)
                {
                    if (user.IsDeleted == false)
                    {
                        userDTOs.Add(_mapper.Map<AccountDTO>(user));
                    }
                }

                if (userDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Account retrieved successfully";
                    _response.Data = userDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Not have Account";
                }

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }

        public async Task<ServiceResponse<AccountDTO>> GetUserByIdAsync(int id)
        {
            var response = new ServiceResponse<AccountDTO>();

            var exist = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (exist == null || exist.IsDeleted == true)
            {
                response.Success = false;
                response.Message = "Account is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Account found";
                response.Data = _mapper.Map<AccountDTO>(exist);
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> SearchUserByNameAsync(string name)
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var users = await _unitOfWork.AccountRepository.SearchAccountByNameAsync(name);

                var userDTOs = new List<AccountDTO>();

                foreach (var acc in users)
                {
                    if (acc.IsDeleted == false)
                    {
                        userDTOs.Add(_mapper.Map<AccountDTO>(acc));
                    }
                }

                if (userDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account retrieved successfully";
                    response.Data = userDTOs;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not have Account";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<AccountDTO>> UpdateUserAsync(int id, AccountDTO userDTO)
        {
            var response = new ServiceResponse<AccountDTO>();

            try
            {
                var existingUser = await _unitOfWork.AccountRepository.GetByIdAsync(id);

                if (existingUser == null)
                {
                    response.Success = false;
                    response.Message = "Account not found.";
                    return response;
                }

                if (existingUser.IsDeleted == true)
                {
                    response.Success = false;
                    response.Message = "Account is deleted in system";
                    return response;
                }


                // Map accountDT0 => existingUser
                var updated = _mapper.Map(userDTO, existingUser);
                //updated.PasswordHash = Utils.HashPassword.HashWithSHA256(accountDTO.PasswordHash);

                _unitOfWork.AccountRepository.Update(existingUser);

                var updatedUserDto = _mapper.Map<AccountDTO>(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    response.Data = updatedUserDto;
                    response.Success = true;
                    response.Message = "Account updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
