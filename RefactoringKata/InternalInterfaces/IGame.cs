namespace RefactoringKata.InternalInterfaces;

internal interface IGame
{
    void AddPlayer(string playerName);
    void Roll(int number);
    void SimulateQuestionWasAnsweredIncorrectly();
    bool SimulateQuestionWasAnsweredCorrectly();
}