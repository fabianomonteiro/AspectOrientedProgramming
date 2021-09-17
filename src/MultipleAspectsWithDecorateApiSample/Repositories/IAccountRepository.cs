using MultipleAspectsWithDecorateApiSample.Models;
using System.Threading.Tasks;

namespace MultipleAspectsWithDecorateApiSample.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetAccount(int id);

        Task SaveAccount(Account account);
    }
}
