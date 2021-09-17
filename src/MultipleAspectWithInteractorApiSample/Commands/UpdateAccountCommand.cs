using FluentInteract;
using LoggingAspectWithProxyApiSample.Models;
using System.Threading.Tasks;

namespace MultipleAspectWithInteractorApiSample.Commands
{
    public class UpdateAccountCommand : Interactor<Account, VoidOutput>, IUpdateAccountCommand
    {
        protected async override Task<VoidOutput> ImplementExecute(Account input)
        {
            return await Task.FromResult(VoidOutput.Instance);
        }
    }
}
