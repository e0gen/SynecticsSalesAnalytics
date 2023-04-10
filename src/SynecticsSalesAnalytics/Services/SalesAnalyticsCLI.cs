using SynecticsSalesAnalytics.Contracts;
using Spectre.Console;

namespace SynecticsSalesAnalytics.Services;

internal class SalesAnalyticsCLI : ISalesAnalyticsCLI
{
    private readonly ISalesAnalyticsService _salesAnalyticsService;

    public SalesAnalyticsCLI(ISalesAnalyticsService salesAnalyticsService)
    {
        _salesAnalyticsService = salesAnalyticsService;
    }

    public void Run()
    {
        AnsiConsole.MarkupLine("[orange3][[[/] [bold]Batch Synectics Sale Analitics Tool[/] [orange3]]][/]\n");

        ShowMainMenu();

        AnsiConsole.MarkupLine("[orange3][[[/] [bold]End[/] [orange3]]][/]");
        Console.ReadLine();
    }

    public void ShowMainMenu()
    {
        var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select the statistical report to calculate?")
            .AddChoices(new[]
            {
                    "1. Average of earnings for a range of years",
                    "2. Standard deviation of earnings within a specific year",
                    "3. Standard deviation of earnings for a range of years",
                    "4. Exit",
            }));

        var index = choice[0] - 48; //Encoding shift

        switch (index)
        {
            case 1:
                ShowAverageErningsForPeriod();
                break;
            case 2:
                ShowStandardDerivation();
                break;
            case 3:
                ShowStandardDerivationForPeriod();
                break;
            case 4:
                return;
            default:
                Console.WriteLine("Invalid input");
                break;
        }

        ShowMainMenu();
    }

    public int[] PromptYearsPeriod()
    {
        var input = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Specify the request period?")
                .Required()
                .InstructionsText(
                    "[grey](Select [blue]start[/] and [blue]end[/] year of period, " +
                    "Press [blue]<space>[/] to toggle a year, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(new[] {
                            "2022", "2021", "2020", "2019", "2018"
                }));

        if (input.Count != 2)
        {
            AnsiConsole.MarkupLine($"[red]ERR[/] [gray]Wrong years have been selected. Please select only two years to avoid an irrelevant statistics[/]");
            return PromptYearsPeriod();
        }

        return input.Select(x => int.Parse(x)).Order().ToArray();
    }

    public int PromptYear()
    {
        var input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the request year?")
                .AddChoices(new[] {
                                "2022", "2021", "2020", "2019", "2018"
                    }));

        return int.Parse(input);
    }

    public void ShowAverageErningsForPeriod()
    {
        var years = PromptYearsPeriod();

        var avgResult = _salesAnalyticsService.CalculateAverageEarnings(years[0], years[1]);

        var table = new Table();
        table.Title = new TableTitle($"Average Earnings for period {years[0]} - {years[1]}");
        table.AddColumn("Year");
        table.AddColumn("Average");
        table.Columns[0].Width(15);
        table.Columns[1].Width(45);
        foreach (var rec in avgResult)
        {
            table.AddRow(rec.Year.ToString(), rec.AverageSale.ToString("0.00"));
        }
        AnsiConsole.Write(table);
    }

    public void ShowStandardDerivation()
    {
        var year = PromptYear();

        var stdDevResult = _salesAnalyticsService.CalculateStandartDeviationOfEarnings(year, year).First();

        Console.WriteLine($"Standard Deviation of Earnings for {year}\n");
        Console.WriteLine(stdDevResult);
        Console.WriteLine();

        var table = new Table();
        table.Title = new TableTitle($"Standard Deviation of Earnings for {year}");
        table.AddColumn("Year");
        table.AddColumn("Standard Deviation");
        table.Columns[0].Width(15);
        table.Columns[1].Width(45);
        table.AddRow(stdDevResult.Year.ToString(), stdDevResult.StandardDeviation.ToString("0.00"));
        AnsiConsole.Write(table);
    }

    public void ShowStandardDerivationForPeriod()
    {
        var years = PromptYearsPeriod();

        var stdDevResults = _salesAnalyticsService.CalculateStandartDeviationOfEarnings(years[0], years[1]);

        var table = new Table();
        table.Title = new TableTitle($"Standard Deviation of Earnings for period {years[0]} - {years[1]}");
        table.AddColumn("Year");
        table.AddColumn("Standard Deviation");
        table.Columns[0].Width(15);
        table.Columns[1].Width(45);
        foreach (var stdDev in stdDevResults)
        {
            table.AddRow(stdDev.Year.ToString(), stdDev.StandardDeviation.ToString("0.00"));
        }
        AnsiConsole.Write(table);
    }
}
