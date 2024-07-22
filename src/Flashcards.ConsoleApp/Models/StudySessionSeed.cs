namespace Flashcards.ConsoleApp.Models;

internal class StudySessionSeed
{
    internal StudySessionSeed(DateTime dateTime, int score)
    {
        DateTime = dateTime;
        Score = score;
    }

    internal DateTime DateTime { get; init; }

    internal int Score { get; init; }
}
