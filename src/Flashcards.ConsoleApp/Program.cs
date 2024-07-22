using System.Configuration;
using Flashcards.ConsoleApp.Extensions;
using Flashcards.ConsoleApp.Models;
using Flashcards.ConsoleApp.Services;
using Flashcards.ConsoleApp.Views;
using Flashcards.Controllers;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards.ConsoleApp;

/// <summary>
/// Main insertion point for the console application.
/// Configures the required application settings and launches the main menu view.
/// </summary>
internal class Program
{
    #region Methods

    private static void Main()
    {
        try
        {
            // Read required configuration settings.
            string databaseConnectionString = ConfigurationManager.AppSettings.GetString("DatabaseConnectionString");
            bool seedDatabase = ConfigurationManager.AppSettings.GetBoolean("SeedDatabase");

            // Create the required services.
            var flashcardController = new FlashcardController(databaseConnectionString);
            var stackController = new StackController(databaseConnectionString);
            var studySessionController = new StudySessionController(databaseConnectionString);
            var studySessionReportController = new StudySessionReportController(databaseConnectionString);

            // Generate seed data if required (config value set and empty database).
            if (seedDatabase && stackController.GetStacks().Count == 0)
            {
                // Could be a long(ish) process, so show a spinner while it works.
                AnsiConsole.Status()
                    .Spinner(Spinner.Known.Aesthetic)
                    .Start("Generating seed data. Please wait...", ctx =>
                    {
                        var seedData = SeedDataService.GenerateSeedData();
                        foreach (var stack in seedData)
                        {
                            stackController.AddStack(new StackDto(stack.Name));
                            StackDto stackDto = stackController.GetStack(stack.Name);

                            foreach (var flashcard in stack.Flashcards)
                            {
                                flashcardController.AddFlashcard(new FlashcardDto(stackDto.Id, flashcard.Question, flashcard.Answer));
                            }
                        }

                        // Generate a hundred random study session datetimes within this and the last year.
                        var studySessionDates = new List<DateTime>();
                        var startOfLastYear = new DateTime(DateTime.Now.Year - 1, 1, 1);
                        int range = (DateTime.Now - startOfLastYear).Days;
                        for (int i = 0; i < 100; i++)
                        {
                            var dateTime = startOfLastYear.AddDays(Random.Shared.Next(range)).AddHours(Random.Shared.Next(0, 24)).AddMinutes(Random.Shared.Next(0, 60)).AddSeconds(Random.Shared.Next(0, 60));
                            studySessionDates.Add(dateTime);
                        }

                        // Order by date, and then apply a random score to a random stack.
                        studySessionDates.Sort();
                        var stacks = stackController.GetStacks();
                        var flashcards = flashcardController.GetFlashcards();
                        foreach(var dateTime in studySessionDates)
                        {
                            var stack = stacks[Random.Shared.Next(0, stacks.Count)];
                            var flashcardsCount = flashcards.Count(c => c.StackId == stack.Id);
                            int score = Random.Shared.Next(0, flashcardsCount + 1);
                            studySessionController.AddStudySession(new StudySessionDto(stack.Id, dateTime, score));

                        }
                    });
                AnsiConsole.WriteLine("Seed data generated.");
            }

            // Show the main menu.
            var mainMenu = new MainMenuPage(flashcardController, stackController, studySessionController, studySessionReportController);
            mainMenu.Show();
        }
        catch (Exception exception)
        {
            MessagePage.Show("Error", exception);
        }
        finally
        {
            Environment.Exit(0);
        }
    }

    #endregion
}
