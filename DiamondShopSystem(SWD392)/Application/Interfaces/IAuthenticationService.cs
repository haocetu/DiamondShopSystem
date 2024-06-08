using Application.ServiceResponse;
using Application.ViewModels.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterAccountDTO registerAccountDTO);
        public Task<ServiceResponse<string>> LoginAsync(AuthenticationAccountDTO accountDTO);
    }
}
