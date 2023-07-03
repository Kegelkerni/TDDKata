namespace RefactoringKata.InternalInterfaces;

internal interface IQuestionsSection
{
    void ShuffleAllDecks();
    string DrawPopQuestion();
    string DrawScienceQuestion();
    string DrawSportsQuestion();
    string DrawRockQuestion();
}