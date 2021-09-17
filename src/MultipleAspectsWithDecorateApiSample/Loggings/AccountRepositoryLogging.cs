using MultipleAspectsWithDecorateApiSample.Models;
using MultipleAspectsWithDecorateApiSample.Repositories;
using System;
using System.Threading.Tasks;

namespace LoggingAspectWithDecorateApiSample.Loggings
{
    public class AccountRepositoryLogging : IAccountRepository
    {
        private readonly IAccountRepository _accountRepository;

        public AccountRepositoryLogging(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task SaveAccount(Account account)
        {
            Console.Write($"AccountRepository.SaveAccount");

            await _accountRepository.SaveAccount(account);
        }

        public async Task<Account> GetAccount(int id)
        {
            Console.Write($"AccountRepository.DisableAccount {id}");

            return await _accountRepository.GetAccount(id);
        }
    }
}
