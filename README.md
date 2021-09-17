# Aspect Oriented Programming

## Aspecto de Logging utilizando DynamicProxy do Castle.Core:


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
