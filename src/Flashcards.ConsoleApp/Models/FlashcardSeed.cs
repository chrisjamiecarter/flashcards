namespace Flashcards.ConsoleApp.Models;

internal class FlashcardSeed
{
    internal FlashcardSeed(string question, string answer)
    {
        Question = question;
        Answer = answer;
    }

    internal string Question { get; init; }

    internal string Answer { get; init; }
}
