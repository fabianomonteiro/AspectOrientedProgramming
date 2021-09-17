using FluentInteract;
using FluentInteract.Aspects;

namespace LoggingAspectWithProxyApiSample.Aspects
{
    public class AuthorizingAspect : IAuthorizingAspect
    {
        public bool IsMatch(IInteractor interactor, object input)
        {
            return true;
        }

        public bool Authorize(IInteractor interactor, object input)
        {
            return true;
        }
    }
}
