using LoggingAspectWithProxyApiSample.Models;
using System;
using System.Threading.Tasks;

namespace LoggingAspectWithProxyApiSample.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<Account> GetAccount(int id) => await Task.FromResult(new Account
                                                                                {
                                                                                    Id = id,
                                                                                    Name = "Test",
                                                                                    Disabled = false,
                                                                                    Created = DateTime.UtcNow
                                                                                });

        public async Task SaveAccount(Account account) => await Task.CompletedTask;
    }
}
