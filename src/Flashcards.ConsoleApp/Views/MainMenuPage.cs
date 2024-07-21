using Flashcards.ConsoleApp.Enums;
using Flashcards.ConsoleApp.Models;
using Spectre.Console;

namespace Flashcards.ConsoleApp.Views;

/// <summary>
/// The main menu page of the application.
/// </summary>
internal class MainMenuPage : BasePage
{
    #region Constants

    private const string PageTitle = "Main Menu";

    #endregion
    #region Constructors

    public MainMenuPage()
    {
        
    }

    #endregion
    #region Properties

    internal static IEnumerable<UserChoice> PageChoices
    {
        get
        {
            return
            [
                new(1, "Study"),
                new(2, "View all study sessions"),
                new(3, "View study sessions report"),
                new(4, "Manage stacks"),
                new(5, "Manage flashcards"),
                new(0, "Close application")
            ];
        }
    }

    #endregion
    #region Methods - Internal

    internal void Show()
    {
        var status = PageStatus.Opened;

        while (status != PageStatus.Closed)
        {
            AnsiConsole.Clear();

            WriteHeader(PageTitle);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<UserChoice>()
                .Title(PromptTitle)
                .AddChoices(PageChoices)
                .UseConverter(c => c.Name!)
                );

            status = PerformOption(option);
        }
    }

    #endregion
    #region Methods - Private

    private PageStatus PerformOption(UserChoice option)
    {
        switch (option.Id)
        {
            case 0:

                // Close application.
                return PageStatus.Closed;

            case 1:

                // Study.
                
                break;

            case 2:
                // View all study sessions.
                
                break;

            case 3:

                // View study sessions report.
                
                break;

            case 4:

                // Manage stacks.
                
                break;

            case 5:

                // Manage flashcards.
                
                break;

            default:

                // Do nothing, but remain on this page.
                break;
        }

        return PageStatus.Opened;
    }

    #endregion
}
