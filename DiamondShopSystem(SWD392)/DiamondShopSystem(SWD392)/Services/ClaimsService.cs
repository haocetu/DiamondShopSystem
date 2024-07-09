using Application.Interfaces;
using System.Security.Claims;

namespace DiamondShopSystem_SWD392_.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            // todo implementation to get the current userId
            var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("id");
            GetCurrentUserId = string.IsNullOrEmpty(Id) ? 0 : int.Parse(Id);
        }

        public int? GetCurrentUserId { get; }

    }

}
