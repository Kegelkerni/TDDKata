using FluentAssertions;
using NUnit.Framework;
using RefactoringKata;

namespace RefactoringKata_uTest;

[TestFixture]
public class QuestionsSectionTest
{
    [Test]
    public void DrawingAnyQuestion_BeforeShufflingTheDecks_ThrowsAnException()
    {
        var questionsSection = CreateClassUnderTest();

        ((Action)(() => questionsSection.DrawPopQuestion())).Should().Throw<Exception>();
        ((Action)(() => questionsSection.DrawScienceQuestion())).Should().Throw<Exception>();
        ((Action)(() => questionsSection.DrawSportsQuestion())).Should().Throw<Exception>();
        ((Action)(() => questionsSection.DrawRockQuestion())).Should().Throw<Exception>();
    }

    [Test]
    public void DrawPopQuestion_ReturnsFirstPopQuestion_AfterFirstDraw()
    {
        var questionsSection = CreateClassUnderTest();
        questionsSection.ShuffleAllDecks();

        var question = questionsSection.DrawPopQuestion();

        question.Should()
            .Contain(QuestionCategory.Pop.ToString()).And.Contain("1");
    }

    [Test]
    public void DrawPopQuestion_Returns50thPopQuestion_After50thDraw()
    {
        var questionsSection = CreateClassUnderTest();
        questionsSection.ShuffleAllDecks();
        for (int i = 1; i <= 49; i++)
            questionsSection.DrawPopQuestion();

        var fiftiethQuestion = questionsSection.DrawPopQuestion();

        fiftiethQuestion.Should()
            .Contain(QuestionCategory.Pop.ToString()).And.Contain("50");
    }

    [Test]
    public void DrawScienceQuestion_ReturnsFirstScienceQuestion_AfterFirstDraw()
    {
        var questionsSection = CreateClassUnderTest();
        questionsSection.ShuffleAllDecks();

        var question = questionsSection.DrawScienceQuestion();

        question.Should()
            .Contain(QuestionCategory.Science.ToString()).And.Contain("1");
    }

    [Test]
    public void DrawSportsQuestion_ReturnsSportsQuestion_AfterFirstDraw()
    {
        var questionsSection = CreateClassUnderTest();
        questionsSection.ShuffleAllDecks();

        var question = questionsSection.DrawSportsQuestion();

        question.Should()
            .Contain(QuestionCategory.Sports.ToString()).And.Contain("1");
    }

    [Test]
    public void DrawRockQuestion_ReturnsFirstRockQuestion_AfterFirstDraw()
    {
        var questionsSection = CreateClassUnderTest();
        questionsSection.ShuffleAllDecks();

        var question = questionsSection.DrawRockQuestion();

        question.Should()
            .Contain(QuestionCategory.Rock.ToString()).And.Contain("1");
    }


    private static QuestionsSection CreateClassUnderTest()
    {
        return new QuestionsSection();
    }
}