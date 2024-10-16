﻿using Spectre.Console;

namespace Flashcards.ConsoleApp.Views;

/// <summary>
/// A page which displays a parameterised message and title.
/// </summary>
internal class MessagePage : BasePage
{
    #region Methods

    internal static void Show(string title, string message)
    {
        WriteHeader(title);

        AnsiConsole.WriteLine(message);

        WriteFooter();

        // Await user confirmation to continue.
        Console.ReadKey();
    }

    internal static void Show(string title, Exception exception)
    {
        WriteHeader(title);

        AnsiConsole.WriteException(exception, ExceptionFormats.NoStackTrace);

        WriteFooter();

        // Await user confirmation to continue.
        Console.ReadKey();
    }

    internal static void Show(string title, Table table)
    {
        WriteHeader(title);

        AnsiConsole.Write(table);

        WriteFooter();

        // Await user confirmation to continue.
        Console.ReadKey();
    }

    #endregion
}
