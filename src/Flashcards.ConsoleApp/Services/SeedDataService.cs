using Flashcards.ConsoleApp.Models;

namespace Flashcards.ConsoleApp.Services;
internal static class SeedDataService
{
    internal static List<StackSeed> GenerateSeedData()
    {
        return
        [
            GenerateFrenchStack(),
            GenerateGermanStack(),
            GenerateItalianStack(),
            GenerateSpanishStack()
        ];
    }

    private static StackSeed GenerateFrenchStack()
    {
        var stack = new StackSeed("French");
        stack.Flashcards.Add(new FlashcardSeed("Hello", "Bonjour"));
        stack.Flashcards.Add(new FlashcardSeed("My name is", "Mon nom est"));
        stack.Flashcards.Add(new FlashcardSeed("Please", "S il vous plait"));
        stack.Flashcards.Add(new FlashcardSeed("Thank you", "Merci"));
        stack.Flashcards.Add(new FlashcardSeed("Sorry", "Pardon"));
        stack.Flashcards.Add(new FlashcardSeed("Yes", "Oui"));
        stack.Flashcards.Add(new FlashcardSeed("No", "Non"));
        stack.Flashcards.Add(new FlashcardSeed("Good", "Bien"));
        stack.Flashcards.Add(new FlashcardSeed("Bad", "Mal"));

        return stack;
    }

    private static StackSeed GenerateGermanStack()
    {
        var stack = new StackSeed("German");
        stack.Flashcards.Add(new FlashcardSeed("Hello", "Hallo"));
        stack.Flashcards.Add(new FlashcardSeed("My name is", "Ich heisse"));
        stack.Flashcards.Add(new FlashcardSeed("Please", "Bitte"));
        stack.Flashcards.Add(new FlashcardSeed("Thank you", "Vielen Dank"));
        stack.Flashcards.Add(new FlashcardSeed("Sorry", "Es tut uns leid"));
        stack.Flashcards.Add(new FlashcardSeed("Yes", "Ja"));
        stack.Flashcards.Add(new FlashcardSeed("No", "Nein"));
        stack.Flashcards.Add(new FlashcardSeed("Good", "Gut"));
        stack.Flashcards.Add(new FlashcardSeed("Bad", "Schlecht"));

        return stack;
    }

    private static StackSeed GenerateItalianStack()
    {
        var stack = new StackSeed("Italian");
        stack.Flashcards.Add(new FlashcardSeed("Hello", "Ciao"));
        stack.Flashcards.Add(new FlashcardSeed("My name is", "Il mio nome e"));
        stack.Flashcards.Add(new FlashcardSeed("Please", "Per favore"));
        stack.Flashcards.Add(new FlashcardSeed("Thank you", "Grazie"));
        stack.Flashcards.Add(new FlashcardSeed("Sorry", "Scusate"));
        stack.Flashcards.Add(new FlashcardSeed("Yes", "Si"));
        stack.Flashcards.Add(new FlashcardSeed("No", "No"));
        stack.Flashcards.Add(new FlashcardSeed("Good", "Bene"));
        stack.Flashcards.Add(new FlashcardSeed("Bad", "Male"));

        return stack;
    }

    private static StackSeed GenerateSpanishStack()
    {
        var stack = new StackSeed("Spanish");
        stack.Flashcards.Add(new FlashcardSeed("Hello", "Hola"));
        stack.Flashcards.Add(new FlashcardSeed("My name is", "Me llamo"));
        stack.Flashcards.Add(new FlashcardSeed("Please", "Por favor"));
        stack.Flashcards.Add(new FlashcardSeed("Thank you", "Gracias"));
        stack.Flashcards.Add(new FlashcardSeed("Sorry", "Lo siento"));
        stack.Flashcards.Add(new FlashcardSeed("Yes", "Si"));
        stack.Flashcards.Add(new FlashcardSeed("No", "No"));
        stack.Flashcards.Add(new FlashcardSeed("Good", "Bueno"));
        stack.Flashcards.Add(new FlashcardSeed("Bad", "Malo"));

        return stack;
    }
}
