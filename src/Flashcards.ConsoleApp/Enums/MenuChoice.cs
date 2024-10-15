using System.ComponentModel;

namespace Flashcards.ConsoleApp.Enums;

/// <summary>
/// Supported choices for all menu pages in the application.
/// </summary>
internal enum MenuChoice
{
    [Description("Default")]
    Default,
    [Description("Close application")]
    CloseApplication,
    [Description("Close page")]
    ClosePage,
    [Description("Study")]
    Study,
    [Description("View all study sessions")]
    ViewStudySessions,
    [Description("View study sessions report")]
    ViewStudySessionsReport,
    [Description("Manage stacks")]
    ManageStacks,
    [Description("Manage flashcards")]
    ManageFlashcards,
}
