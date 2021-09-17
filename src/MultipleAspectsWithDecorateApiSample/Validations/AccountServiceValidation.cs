using MultipleAspectsWithDecorateApiSample.Models;
using MultipleAspectsWithDecorateApiSample.Services;
using System;
using System.Threading.Tasks;

namespace LoggingAspectWithDecorateApiSample.Validations
{
    public class AccountServiceValidation : IAccountService
    {
        private readonly IAccountService _accountService;

        public AccountServiceValidation(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Account> GetAccount(int id)
        {
            if (id <= 0)
                throw new Exception("Id cannot be less than or equal to zero");

            return await _accountService.GetAccount(id);
        }

        public async Task DisableAccount(int id)
        {
            if (id <= 0)
                throw new Exception("Id cannot be less than or equal to zero");

            await _accountService.DisableAccount(id);
        }
    }
}
