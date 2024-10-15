using Flashcards.ConsoleApp.Enums;
using Flashcards.ConsoleApp.Services;
using Flashcards.Controllers;
using Flashcards.Enums;
using Flashcards.Extensions;
using Flashcards.Models;
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

    private readonly MenuChoice[] _pageChoices =
    [
        MenuChoice.Study,
        MenuChoice.ViewStudySessions,
        MenuChoice.ViewStudySessionsReport,
        MenuChoice.ManageStacks,
        MenuChoice.ManageFlashcards,
        MenuChoice.CloseApplication,
    ];


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
    #region Methods - Internal

    internal void Show()
    {
        var choice = MenuChoice.Default;

        while (choice != MenuChoice.CloseApplication)
        {
            WriteHeader(PageTitle);

            choice = UserInputService.GetMenuChoice(PromptTitle, _pageChoices);
            switch (choice)
            {
                case MenuChoice.Study:
                    Study();
                    break;
                case MenuChoice.ViewStudySessions:
                    ViewStudySessions();
                    break;
                case MenuChoice.ViewStudySessionsReport:
                    ViewStudySessionsReport();
                    break;
                case MenuChoice.ManageStacks:
                    ManageStacks();
                    break;
                case MenuChoice.ManageFlashcards:
                    ManageFlashcards();
                    break;
                default:
                    break;
            }
        }
    }

    #endregion
    #region Methods - Private

    private void ManageFlashcards()
    {
        // Get raw data.
        var stacks = _stackController.GetStacks();

        // Get stack to manage, or stop.
        StackDto? stack = SelectStackPage.Show(stacks);
        if (stack == null)
        {
            return;
        }

        var manageFlashcardsPage = new ManageFlashcardsPage(_flashcardController, stack);
        manageFlashcardsPage.Show();
    }

    private void ManageStacks()
    {
        var manageStackPage = new ManageStacksPage(_stackController);
        manageStackPage.Show();
    }

    private void Study()
    {
        // Get raw data.
        var stacks = _stackController.GetStacks();

        // Leave this page open until user is ready to stop studying.
        while (true)
        {
            // Get stack to study, or stop studying.
            StackDto? stack = SelectStackPage.Show(stacks);
            if (stack == null)
            {
                return;
            }

            // Get flashcards for selected stack.
            var flashcards = _flashcardController.GetFlashcards(stack.Id);

            // Can only study a stack with flashcards.
            if (flashcards.Count == 0)
            {
                MessagePage.Show("Error", $"The '{stack.Name}' stack contains no flashcards to study.");
                break;
            }

            // Study and get a score.
            int score = StudyStackPage.Show(stack, flashcards);

            // Add session to database.
            _studySessionController.AddStudySession(stack.Id, score);

            // Display score.
            MessagePage.Show("Study", $"You scored {score} out of {flashcards.Count}!");
        }
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
            StudySessionReportType.Average => _studySessionReportController.GetAverageStudySessionScoreReportByYear(config.Date),
            StudySessionReportType.Total => _studySessionReportController.GetTotalStudySessionsReportByYear(config.Date),
            _ => throw new NotImplementedException("Invalid StudySessionReportType.")
        };

        // Configure table data.
        var tableTitle = config.Type switch
        {
            StudySessionReportType.Average => $"Average study session score per month for {config.Date.Year}",
            StudySessionReportType.Total => $"Total study sessions per month for {config.Date.Year}",
            _ => throw new NotImplementedException("Invalid StudySessionReportType.")
        };

        var table = new Table
        {
            Title = new TableTitle(tableTitle)
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
