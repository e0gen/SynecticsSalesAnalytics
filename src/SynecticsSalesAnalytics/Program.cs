using SynecticsSalesAnalytics.Contracts;
using SynecticsSalesAnalytics.Infrastructure;
using SynecticsSalesAnalytics.Service;
using SynecticsSalesAnalytics.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

AppDomain.CurrentDomain.UnhandledException += Utilities.GlobalExceptionHandler;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var config = builder.Build();

var services = new ServiceCollection();
services.AddOptions();
services.Configure<SalesDataFileContext.Configuration>(config.GetSection("SalesDataConfiguration"));
services.AddSingleton<ISalesDataContext, SalesDataFileContext>();
services.AddSingleton<ISalesAnalyticsService, SalesAnalyticsService>();
services.AddSingleton<ISalesAnalyticsCLI, SalesAnalyticsCLI>();
var serviceProvider = services.BuildServiceProvider();

var salesAnaliticsCLI = serviceProvider.GetService<ISalesAnalyticsCLI>();
if (salesAnaliticsCLI is null) throw new Exception("Sales Analytics CLI service was not resolved");

salesAnaliticsCLI.Run();