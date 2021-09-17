using MultipleAspectsWithDecorateApiSample.Models;
using System.Threading.Tasks;

namespace MultipleAspectsWithDecorateApiSample.Services
{
    public interface IAccountService
    {
        Task DisableAccount(int id);

        Task<Account> GetAccount(int id);
    }
}
