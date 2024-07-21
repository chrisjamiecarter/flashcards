using System.Configuration;
using Flashcards.ConsoleApp.Extensions;
using Flashcards.ConsoleApp.Views;
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
            // TODO: bool seedDatabase = ConfigurationManager.AppSettings.GetBoolean("SeedDatabase");

            // Create the required services.
            // TODO: var controller = new Controller(databaseConnectionString);
            
            // Generate seed data if required.
            // TODO: 
            //if (seedDatabase)
            //{
            //    // Could be a long(ish) process, so show a spinner while it works.
            //    AnsiConsole.Status()
            //        .Spinner(Spinner.Known.Aesthetic)
            //        .Start("Generating seed data. Please wait...", ctx =>
            //        {
            //            codingSessionController.SeedDatabase();
            //        });
            //    AnsiConsole.WriteLine("Seed data generated.");
            //}

            // Show the main menu.
            var mainMenu = new MainMenuPage();
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
