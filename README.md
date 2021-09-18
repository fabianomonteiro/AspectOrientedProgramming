# Aspect Oriented Programming

## Múltiplos Aspectos utilizando Interactor do FluentInteract:

Registrando Aspectos via injeção de dependência:

```csharp
services.AddSingleton<IAspectWeaver, AspectWeaver>((serviceProvider) =>
{
    var aspectWeaver = new AspectWeaver();

    aspectWeaver.AddAspect<AuthorizingAspect>();
    aspectWeaver.AddAspect<CachingAspect>();
    aspectWeaver.AddAspect<CanExecutingAspect>();
    aspectWeaver.AddAspect<ChangingExecuteAspect>();
    aspectWeaver.AddAspect<LoggingAspect>();
    aspectWeaver.AddAspect<ValidatingAspect>();

    return aspectWeaver;
});
```
Aspecto de Autorização:

```csharp
public class AuthorizingAspect : IAuthorizingAspect
{
    public bool IsMatch(IInteractor interactor, object input)
    {
        return true;
    }

    public Task<bool> Authorize(IInteractor interactor, object input)
    {
        return Task.FromResult(true);
    }
}
```
Aspecto de Cache:

```csharp
public class CachingAspect : CachingAspectBase
{
    public override bool IsMatch(IInteractor interactor, object input)
    {
        return false;
    }

    public async override Task<object> GetCache(IInteractor interactor, object input)
    {
        return await Task.FromResult(new object());
    }
}
```
Aspecto de CanExecute:

```csharp
public class CanExecutingAspect : ICanExecutingAspect
{
    public bool IsMatch(IInteractor interactor, object input)
    {
        return true;
    }

    public Task<bool> CanExecute(IInteractor interactor, object input)
    {
        return Task.FromResult(true);
    }
}
```
Aspecto de ChangingExecute:

```csharp
public class ChangingExecuteAspect : IChangingExecuteAspect
{
    public bool IsMatch(IInteractor interactor, object input)
    {
        return false;
    }

    public Task<object> Execute(IInteractor interactor, object input)
    {
        return Task.FromResult(new object());
    }
}
```
Aspecto de Logging:

```csharp
public class LoggingAspect : ILoggingAspect
{
    public bool IsMatch(IInteractor interactor, object input)
    {
        return true;
    }

    public Task LogStartExecute(DateTime dateTime, IInteractor interactor, object input, ICallerInstance callerInstance, string memberName, string sourceFilePath, int sourceLineNumber)
    {
        Console.WriteLine("Start Execute");

        return Task.CompletedTask;
    }

    public Task LogEndExecute(DateTime dateTime, IInteractor interactor, TimeSpan elapsed, bool executeFromAspect, IAspect aspectExecutedInstance)
    {
        Console.WriteLine("End Execute");

        return Task.CompletedTask;
    }

    public Task LogExceptionExecute(DateTime dateTime, IInteractor interactor, object input, Exception exception, ICallerInstance callerInstance, string memberName, string sourceFilePath, int sourceLineNumber)
    {
        Console.WriteLine("Exception Execute");

        return Task.CompletedTask;
    }
}
```
Aspecto de Validating:

```csharp
public class ValidatingAspect : IValidatingAspect
{
    public bool IsMatch(IInteractor interactor, object input)
    {
        return true;
    }

    public Task<bool> Validate(IInteractor interactor, object input)
    {
        return Task.FromResult(true);
    }
}
```

Github FluentInteract:

https://github.com/fabianomonteiro/FluentInteract

## Aspecto de Logging utilizando DynamicProxy do Castle.Core:

ServicesExtensions utilizando Castle.Core:

```csharp
public static class ServicesExtensions
{
    public static void AddProxiedScoped<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddScoped<TImplementation>();
        services.AddScoped(typeof(TInterface), serviceProvider =>
        {
            var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
            var actual = serviceProvider.GetRequiredService<TImplementation>();
            var interceptors = serviceProvider.GetServices<IInterceptor>().ToArray();
            return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptors);
        });
    }
}
```

Injeção de dependência utilizando o Castle.Core:

```csharp
services.AddSingleton(new ProxyGenerator());
services.AddScoped<IInterceptor, LoggingAspect>();
services.AddProxiedScoped<IAccountService, AccountService>();
services.AddProxiedScoped<IAccountRepository, AccountRepository>();
```

Aspecto de Logging utilizando Castle.Core:

```csharp
public class LoggingAspect : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Calling method {invocation.TargetType}.{invocation.Method.Name}.");
        invocation.Proceed();
    }
}
```

Github Castle.Core:

https://github.com/castleproject/Core

## Múltiplos Aspectos utilizando Decorate do Scrutor:

Injeção de dependência utilizando o Decorate:

```csharp
services.AddScoped<IAccountService, AccountService>();
services.Decorate<IAccountService, AccountServiceLogging>();
services.Decorate<IAccountService, AccountServiceValidation>();
            
services.AddScoped<IAccountRepository, AccountRepository>();
services.Decorate<IAccountRepository, AccountRepositoryLogging>();
```
Aspecto de Logging do AccountService:
 
```csharp
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
```

Aspecto de Logging do AccountRepository:

```csharp
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
```
Aspecto de Validação do AccountService:

```csharp
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
```
Github Scrutor:

https://github.com/khellang/Scrutor
