using Spectre.Console;

internal static class Utilities
{
    internal static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
    {
        AnsiConsole.MarkupLine($"\n[red]ERR[/] [gray]{((Exception)e.ExceptionObject).Message}[/]");
        AnsiConsole.MarkupLine($"Press [green]Enter[/] to [blue]Exit[/]");
        Console.ReadLine();
        Environment.Exit(0);
    }
}
