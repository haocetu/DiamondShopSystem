using Application.Interfaces;
using Application.ViewModels.CertificateDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiamondShopSystem_SWD392_.Controllers
{
    public class CertificateController : BaseController
    {
        private readonly ICertificateService _certificateService;
        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCertificate()
        {
            var certificateList = await _certificateService.GetAllCertificateAsync();
            return Ok(certificateList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetCertificateById(int id)
        {
            var certificate = await _certificateService.GetCertificateByIdAsync(id);
            if (!certificate.Success)
            {
                return NotFound(certificate);
            }
            return Ok(certificate);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCertificate([FromBody]CreateCertificateDTO createCertificateDTO)
        {
            var result = await _certificateService.CreateCertificateAsync(createCertificateDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteCertificate(int id)
        {
            var result = await _certificateService.DeleteCertificate(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
