using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.DiamondDTOs;
using AutoMapper;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    public class DiamondService : IDiamondService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public DiamondService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<ServiceResponse<ViewModels.DiamondDTOs.Product>> CreateDiamondAsync(CreateDiamondDTO createdDiamondDTO)
        {
            var response = new ServiceResponse<ViewModels.DiamondDTOs.Product>();
            try
            {
                var diamond = _mapper.Map<Diamond>(createdDiamondDTO);
                diamond.IsDeleted = false;
                diamond.Name = diamond.OriginName+ " " + diamond.CutName +" "+ diamond.ClarityName;
                await _unitOfWork.DiamondRepository.AddAsync(diamond);
                

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (!createdDiamondDTO.DiamondImages.IsNullOrEmpty())
                {
                    await _imageService.UploadDiamondImages(createdDiamondDTO.DiamondImages, diamond.Id);
                }
                if (isSuccess)
                {
                    var diamondDTO = _mapper.Map<ViewModels.DiamondDTOs.Product>(diamond);
                    response.Data = diamondDTO;
                    response.Success = true;
                    response.Message = "Diamond created successfully.";
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

        public async Task<ServiceResponse<bool>> DeleteDiamondAsync(int id)
        {
            var response = new ServiceResponse<bool>();

            var exist = await _unitOfWork.DiamondRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Diamond is not existed";
                return response;
            }
            else if (exist.IsDeleted == true)
            {
                response.Success = false;
                response.Message = "Diamond have been deleted from the system";
                return response;
            }

            try
            {
                _unitOfWork.DiamondRepository.SoftRemove(exist);
                exist.IsDeleted = true;
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Diamond deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting diamond.";
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
    
        public async Task<ServiceResponse<IEnumerable<ViewModels.DiamondDTOs.Product>>> GetDiamondAsync()
        {
            var _response = new ServiceResponse<IEnumerable<ViewModels.DiamondDTOs.Product>>();
            try
            {
                var diamonds = await _unitOfWork.DiamondRepository.GetAllAsync();

                var diamondDTOs = new List<ViewModels.DiamondDTOs.Product>();

                foreach (var diamond in diamonds)
                {

                    if (diamond.IsDeleted == false)
                    {
                        diamondDTOs.Add(_mapper.Map<ViewModels.DiamondDTOs.Product>(diamond));
                    }
                }

                if (diamondDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Diamonds retrieved successfully";
                    _response.Data = diamondDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Not have Diamond yet";
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

        public async Task<ServiceResponse<ViewModels.DiamondDTOs.Product>> GetDiamondByIdAsync(int id)
        {
            var response = new ServiceResponse<ViewModels.DiamondDTOs.Product>();

            var exist = await _unitOfWork.DiamondRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Diamond is not existed";
            }
            else if(exist.IsDeleted == true)
            {
                response.Success = false;
                response.Message = "Diamond have been deleted from the system";
            
            }
            else
            {
                response.Success = true;
                response.Message = "Diamond found";
                response.Data = _mapper.Map<ViewModels.DiamondDTOs.Product>(exist);
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ViewModels.DiamondDTOs.Product>>> SearchDiamondByOriginAsync(string origin)
        {
            var response = new ServiceResponse<IEnumerable<ViewModels.DiamondDTOs.Product>>();

            try
            {
                var diamonds = await _unitOfWork.DiamondRepository.SearchDiamondByOriginAsync(origin);

                var diamondDTOs = new List<ViewModels.DiamondDTOs.Product>();

                foreach (var diamond in diamonds)
                {
                    if (diamond.IsDeleted == false)
                    {
                        diamondDTOs.Add(_mapper.Map<ViewModels.DiamondDTOs.Product>(diamond));
                    }
                }

                if (diamondDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Diamond retrieved successfully";
                    response.Data = diamondDTOs;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not have Diamond yet";
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

        public async Task<ServiceResponse<ViewModels.DiamondDTOs.Product>> UpdateDiamondAsync(int id, UpdateDiamondDTO diamondDTO)
        {
            var response = new ServiceResponse<ViewModels.DiamondDTOs.Product>();

            try
            {
                var existingDiamond = await _unitOfWork.DiamondRepository.GetByIdAsync(id);

                if (existingDiamond == null)
                {
                    response.Success = false;
                    response.Message = "Diamond not found.";
                    return response;
                }

                if (existingDiamond.IsDeleted == true)
                {
                    response.Success = false;
                    response.Message = "Diamond has been deleted in system";
                    return response;
                }


                // Map accountDT0 => existingUser
                var updated = _mapper.Map(diamondDTO, existingDiamond);
                //updated.PasswordHash = Utils.HashPassword.HashWithSHA256(accountDTO.PasswordHash);

                _unitOfWork.DiamondRepository.Update(existingDiamond);

                var updatedDiamondDto = _mapper.Map<ViewModels.DiamondDTOs.Product>(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    response.Data = updatedDiamondDto;
                    response.Success = true;
                    response.Message = "Diamond updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the diamond.";
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
