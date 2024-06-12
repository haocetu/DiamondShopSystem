using Application.Interfaces;
using Application.Services;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.DiamondDTOs;
using Application.ViewModels.ImageDTOs;
using Microsoft.AspNetCore.Mvc;

namespace DiamondShopSystem_SWD392_.Controllers
{
    public class DiamondController : BaseController
    {
        private readonly IDiamondService _diamondService;
        public DiamondController(IDiamondService diamondService)
        {
            _diamondService = diamondService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDiamondList()
        {
            var Diamond = await _diamondService.GetDiamondAsync();
            return Ok(Diamond);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDiamondById(int id)
        {
            var diamond = await _diamondService.GetDiamondByIdAsync(id);
            if (!diamond.Success)
            {
                return NotFound(diamond);
            }
            return Ok(diamond);
        }
        [HttpGet]
        [Route("{origin}")]
        public async Task<IActionResult> SearchDiamondByOrigin(string origin)
        {
            var result = await _diamondService.SearchDiamondByOriginAsync(origin);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiamond([FromBody] CreateDiamondDTO createdDiamondDTO)
        {
            //Dòng này kiểm tra xem dữ liệu đầu vào (trong trường hợp này là createdAccountDTO)
            //đã được kiểm tra tính hợp lệ bằng các quy tắc mô hình (model validation) hay chưa.
            //Nếu dữ liệu hợp lệ, nó tiếp tục kiểm tra và xử lý.
            if (ModelState.IsValid)
            {
                var response = await _diamondService.CreateDiamondAsync(createdDiamondDTO);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                return BadRequest("Invalid request data.");
            }
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> AddImage(int id,[FromBody] ImageDiamondDTO image)
        {
            if (ModelState.IsValid)
            {
                var response = await _diamondService.AddImageDiamondById(id, image);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                return BadRequest("Invalid request data.");
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDiamond(int id, [FromBody] UpdateDiamondDTO diamondDTO)
        {
            var updatedDiamond = await _diamondService.UpdateDiamondAsync(id, diamondDTO);
            if (!updatedDiamond.Success)
            {
                return NotFound(updatedDiamond);
            }
            return Ok(updatedDiamond);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiamond(int id)
        {
            var deletedDiamond = await _diamondService.DeleteDiamondAsync(id);
            if (!deletedDiamond.Success)
            {
                return NotFound(deletedDiamond);
            }

            return Ok(deletedDiamond);
        }
    }
}
