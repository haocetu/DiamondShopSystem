using Application.Commons;
using Application.ViewModels.CertificateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICertificateService
    {
        Task<ServiceResponse<IEnumerable<CertificateDTO>>> GetAllCertificateAsync();
        Task<ServiceResponse<CertificateDTO>> GetCertificateByIdAsync(int id);
        Task<ServiceResponse<CertificateDTO>> CreateCertificateAsync(CreateCertificateDTO createCertificateDTO);
        Task<ServiceResponse<bool>> DeleteCertificate(int id);
    }
}
