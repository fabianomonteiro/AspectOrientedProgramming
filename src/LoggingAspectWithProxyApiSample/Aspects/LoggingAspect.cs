using Castle.DynamicProxy;
using System;

namespace LoggingAspectWithProxyApiSample.Interceptors
{
    public class LoggingAspect : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Calling method {invocation.TargetType}.{invocation.Method.Name}.");
            invocation.Proceed();
        }
    }
}
