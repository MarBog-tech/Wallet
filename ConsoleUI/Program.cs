using ConsoleUI;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wallet.Service.Implementations;
using Wallet.Service.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.Add(AccountService);
    })
    .Build();

await host.RunAsync();