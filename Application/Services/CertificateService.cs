using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.CertificateDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace Application.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CertificateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IEnumerable<CertificateDTO>>> GetAllCertificateAsync()
        {
            var response = new ServiceResponse<IEnumerable<CertificateDTO>>();
            try
            {
                var getAllCertificate = await _unitOfWork.CertificateRepository.GetAllAsync();
                var newList = new List<CertificateDTO>();
                foreach (var certificate in getAllCertificate)
                {
                    if(certificate.IsDeleted == false)
                    {
                        newList.Add(_mapper.Map<CertificateDTO>(certificate));
                    }
                }
                if (newList.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Certificate retrieved successfully";
                    response.Data = newList;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have certificate";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;

        }
        public async Task<ServiceResponse<CertificateDTO>> GetCertificateByIdAsync(int id)
        {
            var response = new ServiceResponse<CertificateDTO>();
            try
            {
                var getCertificateById = await _unitOfWork.CertificateRepository.GetByIdAsync(id);
                if(getCertificateById == null || getCertificateById.IsDeleted == true) 
                {
                    response.Success = false;
                    response.Message = "Certificate id not exist";
                    return response;
                }
                response.Success = true;
                response.Message = "Certificate found";
                response.Data = _mapper.Map<CertificateDTO>(getCertificateById);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }
        public async Task<ServiceResponse<CertificateDTO>> CreateCertificateAsync(CreateCertificateDTO createCertificateDTO)
        {
            var response = new ServiceResponse<CertificateDTO>();
            try
            {
                var IsReportNumberExist = await _unitOfWork.CertificateRepository.IsReportNumberExist(createCertificateDTO.ReportNumber);
                if(IsReportNumberExist == true)
                {
                    response.Success = false;
                    response.Message = "ReportNumber exist in database, please use another ReportNumber";
                    return response;
                }
                var certificate = _mapper.Map<Certificates>(createCertificateDTO);
                certificate.IsDeleted = false;
                await _unitOfWork.CertificateRepository.AddAsync(certificate);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var CertificateDTO = _mapper.Map<CertificateDTO>(certificate);
                    response.Data = CertificateDTO; 
                    response.Success = true;
                    response.Message = "certificate created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving certificate.";
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
        public async Task<ServiceResponse<bool>> DeleteCertificate(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var checkIdExist = await _unitOfWork.CertificateRepository.GetByIdAsync(id);
                if (checkIdExist == null || checkIdExist.IsDeleted == true)
                {
                    response.Success = false;
                    response.Message = "Id not exist";
                    return response;
                }
                _unitOfWork.CertificateRepository.SoftRemove(checkIdExist);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "certificate deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the certificate.";
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
