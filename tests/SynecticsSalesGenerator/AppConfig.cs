namespace SynecticsSalesGenerator;

internal class AppConfig
{
    public string DateFormat { get; set; } = "dd/MM/yyyy";
    public string PriceFormat { get; set; } = "0.00";
    public string Delimiter { get; set; } = "##";
    public int NumberOfFiles { get; set; } = 1;
    public int EntiresPerFile { get; set; } = 1_000_000;
    public string? Path { get; set; }
}
