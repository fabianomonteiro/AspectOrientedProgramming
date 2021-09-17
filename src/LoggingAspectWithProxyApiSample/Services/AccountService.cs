using LoggingAspectWithProxyApiSample.Models;
using LoggingAspectWithProxyApiSample.Repositories;
using System.Threading.Tasks;

namespace LoggingAspectWithProxyApiSample.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> GetAccount(int id)
        {
            return await _accountRepository.GetAccount(id);
        }

        public async Task DisableAccount(int id)
        {
            var account = await _accountRepository.GetAccount(id);

            account.Disabled = true;

            await _accountRepository.SaveAccount(account);
        }
    }
}
