using ConsoleUI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wallet.DAL;
using Wallet.Service.Implementations;
using Wallet.Service.Interfaces;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlite("DataSource=Dbwallet.db"));
        services.AddHostedService<Worker>();
        services.AddScoped<IAccountService,AccountService>();
        services.AddScoped<IUserCardService, UserCardService>();
        services.AddScoped<IProfitService, ProfitService>();
        services.AddScoped<IExpensesService,ExpensesService>();
    })
    .Build();

await host.RunAsync();