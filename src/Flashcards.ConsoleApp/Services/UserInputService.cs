using System.Globalization;
using Flashcards.Models;
using Flashcards.ConsoleApp.Enums;
using Flashcards.ConsoleApp.Extensions;
using Flashcards.ConsoleApp.Models;
using Spectre.Console;
using Flashcards.Extensions;

namespace Flashcards.ConsoleApp.Services;

/// <summary>
/// Helper service for getting valid user inputs of different types.
/// </summary>
internal static class UserInputService
{
    #region Methods
    
    internal static DateTime? GetDateTime(string prompt, string format, Func<string, UserInputValidationResult> validate)
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>(prompt);
            if (input == "0")
            {
                return null;
            }

            var validationResult = validate(input);
            if (validationResult.IsValid)
            {
                return DateTime.ParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }

            AnsiConsole.WriteLine(validationResult.Message);
        }
    }

    internal static MenuChoice GetMenuChoice(string prompt, IEnumerable<MenuChoice> choices)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<MenuChoice>()
                .Title(prompt)
                .AddChoices(choices)
                .UseConverter(c => c.GetDescription())
                );
    }

    internal static SelectionChoice GetSelectionChoice(string prompt, IEnumerable<SelectionChoice> choices)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<SelectionChoice>()
                .Title(prompt)
                .AddChoices(choices)
                .UseConverter(c => c.Name!)
                );
    }

    internal static string GetString(string prompt)
    {
        return AnsiConsole.Ask<string>(prompt);
    }

    #endregion
}
