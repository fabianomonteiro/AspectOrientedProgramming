using MultipleAspectsWithDecorateApiSample.Models;
using MultipleAspectsWithDecorateApiSample.Services;
using System;
using System.Threading.Tasks;

namespace LoggingAspectWithDecorateApiSample.Loggings
{
    public class AccountServiceLogging : IAccountService
    {
        private readonly IAccountService _accountService;

        public AccountServiceLogging(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task DisableAccount(int id)
        {
            Console.Write($"AccountService.DisableAccount {id}");

            await _accountService.DisableAccount(id);
        }

        public async Task<Account> GetAccount(int id)
        {
            Console.Write($"AccountService.GetAccount {id}");

            return await _accountService.GetAccount(id);
        }
    }
}
