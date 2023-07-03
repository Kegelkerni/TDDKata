using RefactoringKata.InternalInterfaces;

namespace RefactoringKata;

internal class GameRunner
{
    private readonly IGame _game;

    public GameRunner(IGame game)
    {
        _game = game;
    }

    public void RunGame()
    {
        var notAWinner = true;

        _game.AddPlayer("Chet");
        _game.AddPlayer("Pat");
        _game.AddPlayer("Sue");

        var random = new Random();

        do
        {
            _game.Roll(random.Next(5) + 1);

            if (random.Next(9) == 7)
            {
                _game.SimulateQuestionWasAnsweredIncorrectly();
            }
            else
            {
                notAWinner = _game.SimulateQuestionWasAnsweredCorrectly();
            }
        } while (notAWinner);
    }
}