using SynecticsSalesAnalytics.Contracts;
using SynecticsSalesAnalytics.Models;
using Microsoft.Extensions.Options;

namespace SynecticsSalesAnalytics.Infrastructure;

public class SalesDataFileContext : ISalesDataContext
{
    private IList<SaleRecord>? _cacheData;
    private Configuration _config;
    private object _lockObj = new object();

    public SalesDataFileContext(IOptions<Configuration> config)
    {
        _config = config.Value;

        if (string.IsNullOrEmpty(_config.Path))
            _config.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    public IEnumerable<SaleRecord> GetAllSales()
    {
        if (_cacheData is null)
            RefreshDataCache();

        return _cacheData!;
    }

    public void RefreshDataCache()
    {
        if (!Directory.Exists(_config.Path))
            throw new Exception($"Specified directory does not exist: {_config.Path}");

        var saleFiles = Directory.GetFiles(_config.Path).Where(x => x.EndsWith("dat")).ToArray();

        if (!saleFiles.Any())
            throw new Exception($"There is no data in the directory {_config.Path} for analysis.");

        var totalSales = new List<SaleRecord>();
        Parallel.ForEach(saleFiles, file =>
        {
            var sales = new List<SaleRecord>();

            try
            {
                var lines = File.ReadAllLines(file);
                for (var i = 0; i < lines.Length; i++)
                {
                    var saleRecord = SaleRecord.Parse(lines[i], _config.Delimiter, _config.DateFormat, _config.DecimalSymbol);
                    sales.Add(saleRecord);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERR: Unable to parse file {file} with specified format. Exception: {ex.Message}");
            }

            lock (_lockObj)
            {
                totalSales.AddRange(sales);
            }
        });

        _cacheData = totalSales;
        Console.WriteLine($"DBG: Sales data context successfully loaded with {totalSales.Count} records\n");
    }

    public class Configuration
    {
        public string DateFormat { get; set; } = "dd/MM/yyyy";
        public char DecimalSymbol { get; set; } = '.';
        public string Delimiter { get; set; } = "##";
        public string? Path { get; set; }
    }
}
