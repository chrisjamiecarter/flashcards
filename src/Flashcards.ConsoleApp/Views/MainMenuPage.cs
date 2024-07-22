using Flashcards.ConsoleApp.Enums;
using Flashcards.ConsoleApp.Models;
using Flashcards.Controllers;
using Flashcards.Enums;
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
    #region Fields

    private readonly FlashcardController _flashcardController;
    private readonly StackController _stackController;
    private readonly StudySessionController _studySessionController;
    private readonly StudySessionReportController _studySessionReportController;

    #endregion
    #region Constructors

    public MainMenuPage(FlashcardController flashcardController, StackController stackController, StudySessionController studySessionController, StudySessionReportController studySessionReportController)
    {
        _flashcardController = flashcardController;
        _stackController = stackController;
        _studySessionController = studySessionController;
        _studySessionReportController = studySessionReportController;
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
    #region Methods

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
                ViewStudySessions();
                break;

            case 3:

                // View study sessions report.
                ViewStudySessionsReport();
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

    private void ViewStudySessions()
    {
        // Get raw data.
        var data = _studySessionController.GetStudySessions();
        var stacks = _stackController.GetStacks();

        // Configure table data.
        var table = new Table
        {
            Title = new TableTitle("Study Sessions")
        };
        table.AddColumn("ID");
        table.AddColumn("Stack");
        table.AddColumn("Studied");
        table.AddColumn("Score");

        foreach (var x in data)
        {
            table.AddRow(x.Id.ToString(), stacks.First(s => s.Id == x.StackId).Name, x.DateTime.ToString("yyyy-MM-dd HH:mm"), x.Score.ToString());
        }

        table.Caption = new TableTitle($"{data.Count} study sessions found.");

        // Fill up window.
        table.Expand();

        // Display report.
        MessagePage.Show("Study Sessions", table);
    }

    private void ViewStudySessionsReport()
    {
        // Which report, and for what year?
        var config = ConfigureStudySessionReportPage.Show();

        // If nothing is returned, user has opted to not proceed.
        if (config == null)
        {
            return;
        }

        // Get data.
        var data = config.Type switch
        {
            StudySessionReportType.Average => _studySessionReportController.GetStudySessionAverageReportByYear(config.Date),
            StudySessionReportType.Total => _studySessionReportController.GetStudySessionTotalReportByYear(config.Date),
            _ => throw new NotImplementedException("Invalid StudySessionReportType.")
        };

        // Configure table data.
        var table = new Table
        {
            Title = new TableTitle($"{config.Type:G} Study Sessions Report for {config.Date.Year}")
        };
        table.AddColumn("Stack");
        table.AddColumn("January");
        table.AddColumn("February");
        table.AddColumn("March");
        table.AddColumn("April");
        table.AddColumn("May");
        table.AddColumn("June");
        table.AddColumn("July");
        table.AddColumn("August");
        table.AddColumn("September");
        table.AddColumn("October");
        table.AddColumn("November");
        table.AddColumn("December");

        foreach (var x in data)
        {
            table.AddRow(
                x.StackName,
                x.January.ToString(),
                x.February.ToString(),
                x.March.ToString(),
                x.April.ToString(),
                x.May.ToString(),
                x.June.ToString(),
                x.July.ToString(),
                x.August.ToString(),
                x.September.ToString(),
                x.October.ToString(),
                x.November.ToString(),
                x.December.ToString()
                );
        }

        table.Caption = new TableTitle($"{data.Count} stacks with study sessions found.");

        // Fill up window.
        table.Expand();

        // Display report.
        MessagePage.Show("Study Sessions Report", table);
    }


    #endregion
}
