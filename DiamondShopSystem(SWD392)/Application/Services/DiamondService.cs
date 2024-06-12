using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.DiamondDTOs;
using Application.ViewModels.ImageDTOs;
using AutoMapper;
using Domain.Entities;
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

        public DiamondService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<DiamondDTO>> CreateDiamondAsync(CreateDiamondDTO createdDiamondDTO)
        {
            var response = new ServiceResponse<DiamondDTO>();
            try
            {
                var diamond = _mapper.Map<Diamond>(createdDiamondDTO);
                diamond.IsDeleted = false;
                await _unitOfWork.DiamondRepository.AddAsync(diamond);
                

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var diamondDTO = _mapper.Map<DiamondDTO>(diamond);
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
    
        public async Task<ServiceResponse<IEnumerable<DiamondDTO>>> GetDiamondAsync()
        {
            var _response = new ServiceResponse<IEnumerable<DiamondDTO>>();
            try
            {
                var diamonds = await _unitOfWork.DiamondRepository.GetAllAsync();

                var diamondDTOs = new List<DiamondDTO>();

                foreach (var diamond in diamonds)
                {
                    if (diamond.IsDeleted == false)
                    {
                        diamondDTOs.Add(_mapper.Map<DiamondDTO>(diamond));
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

        public async Task<ServiceResponse<DiamondDTO>> GetDiamondByIdAsync(int id)
        {
            var response = new ServiceResponse<DiamondDTO>();

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
                response.Data = _mapper.Map<DiamondDTO>(exist);
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<DiamondDTO>>> SearchDiamondByOriginAsync(string origin)
        {
            var response = new ServiceResponse<IEnumerable<DiamondDTO>>();

            try
            {
                var diamonds = await _unitOfWork.DiamondRepository.SearchDiamondByOriginAsync(origin);

                var diamondDTOs = new List<DiamondDTO>();

                foreach (var diamond in diamonds)
                {
                    if (diamond.IsDeleted == false)
                    {
                        diamondDTOs.Add(_mapper.Map<DiamondDTO>(diamond));
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

        public async Task<ServiceResponse<DiamondDTO>> UpdateDiamondAsync(int id, UpdateDiamondDTO diamondDTO)
        {
            var response = new ServiceResponse<DiamondDTO>();

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

                var updatedDiamondDto = _mapper.Map<DiamondDTO>(updated);
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
        public async Task<ServiceResponse<ImageDiamondDTO>>AddImageDiamondById(int id, ImageDiamondDTO imageDiamondDTO)
        {
            var response = new ServiceResponse<ImageDiamondDTO>();
            //check exist
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
            //Add
            try
            {
                //Mapping : imageDiamondDTO -> Image
                var image = _mapper.Map<Image>(imageDiamondDTO);
                image.DiamondId = id;
                await _unitOfWork.ImageRepository.AddAsync(image);


                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var diamondDTO = _mapper.Map<Image>(imageDiamondDTO);
                    response.Data = imageDiamondDTO;
                    response.Success = true;
                    response.Message = "Add image successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving.";
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
    }
}
