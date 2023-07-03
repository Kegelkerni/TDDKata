using RefactoringKata.InternalInterfaces;

namespace RefactoringKata;

internal enum QuestionCategory
{
    Pop,
    Science,
    Sports,
    Rock
}

internal sealed class QuestionsSection : IQuestionsSection
{
    private readonly IDictionary<string, LinkedList<string>> _questionDecks =
        new Dictionary<string, LinkedList<string>>();

    private readonly List<string> _questionCategories = Enum.GetNames(typeof(QuestionCategory)).ToList();

    public void ShuffleAllDecks()
    {
        var numberOfCardsPerDeck = 50;
        _questionCategories.ForEach(category =>
            _questionDecks.Add(category, CreateQuestions(category, numberOfCardsPerDeck)));
    }

    public string DrawPopQuestion()
    {
        return DrawQuestion(QuestionCategory.Pop.ToString());
    }

    public string DrawScienceQuestion()
    {
        return DrawQuestion(QuestionCategory.Science.ToString());
    }

    public string DrawSportsQuestion()
    {
        return DrawQuestion(QuestionCategory.Sports.ToString());
    }

    public string DrawRockQuestion()
    {
        return DrawQuestion(QuestionCategory.Rock.ToString());
    }

    private string DrawQuestion(string category)
    {
        var nextQuestion = _questionDecks[category].First();
        _questionDecks[category].RemoveFirst();
        return nextQuestion;
    }

    private static LinkedList<string> CreateQuestions(string category, int count)
    {
        var questions = new LinkedList<string>();

        Enumerable.Range(1, count).ToList()
            .ForEach(number => questions.AddLast($"{category} Question " + number));

        return questions;
    }
}