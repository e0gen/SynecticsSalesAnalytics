using SynecticsSalesGenerator;
using Microsoft.Extensions.Configuration;
using System.Globalization;

Console.WriteLine("=== Sales Stab Data Generation Tool ===\n");

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build().Get<AppConfig>();

string docPath = config.Path ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

var filesExists = Directory.GetFiles(docPath).Where(x => x.EndsWith("dat"));

if (filesExists.Any())
{
    Console.WriteLine($"There are already generated files in the directory: {docPath}");
    Console.WriteLine("=== End ===");
    Console.ReadLine();
    return;
}

Console.WriteLine("Generation started...");

DateTime startDate = DateTime.UtcNow.AddYears(-4);
Parallel.For(0, config.NumberOfFiles, x =>
{
    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"salepoint_{x}.dat")))
    {
        for (int i = 0; i < config.EntiresPerFile; i++)
        {
            var rnd = Random.Shared.Next(365 * 4);
            var date = startDate.AddDays(rnd);
            var price = Random.Shared.NextDouble() + rnd % 1000;

            outputFile.WriteLine($"{date.ToString(config.DateFormat, CultureInfo.InvariantCulture)}##{price.ToString(config.PriceFormat, CultureInfo.InvariantCulture)}");
        }
    }
});

Console.WriteLine($"Sales data successfully generated to {docPath}");
Console.WriteLine("=== End ===");
Console.ReadLine();