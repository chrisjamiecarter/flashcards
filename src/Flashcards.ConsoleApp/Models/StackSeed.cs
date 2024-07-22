namespace Flashcards.ConsoleApp.Models;

internal class StackSeed
{
    public StackSeed(string name)
    {
        Name = name;
        Flashcards = [];
        StudySessions = [];
    }

    internal string Name { get; init; }

    public List<FlashcardSeed> Flashcards { get; set; }

    public List<StudySessionSeed> StudySessions { get; set; }

}
