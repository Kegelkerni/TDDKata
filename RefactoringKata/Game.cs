using RefactoringKata.InternalInterfaces;

namespace RefactoringKata
{
    internal sealed class Game : IGame
    {
        private readonly IQuestionsSection _questionsSection;

        private readonly List<string> _players = new List<string>();

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        private readonly bool[] _inPenaltyBox = new bool[6];

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game(IQuestionsSection questionsSection)
        {
            _questionsSection = questionsSection;
            _questionsSection.ShuffleAllDecks();
        }

        public void AddPlayer(string playerName)
        {
            _players.Add(playerName);
            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine($"'{playerName}' was added as player number {_players.Count}");
        }

        private int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(_players[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");

                    _places[_currentPlayer] += roll;
                    if (_places[_currentPlayer] > 11)
                    {
                        _places[_currentPlayer] -= 12;
                    }

                    Console.WriteLine(_players[_currentPlayer]
                                      + "'s new location is "
                                      + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + GetCurrentQuestionCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] += roll;
                if (_places[_currentPlayer] > 11)
                {
                    _places[_currentPlayer] -= 12;
                }

                Console.WriteLine(_players[_currentPlayer]
                                  + "'s new location is "
                                  + _places[_currentPlayer]);
                Console.WriteLine("The category is " + GetCurrentQuestionCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (GetCurrentQuestionCategory() == "Pop")
            {
                var popQuestion = _questionsSection.DrawPopQuestion();
                Console.WriteLine(popQuestion);
            }

            if (GetCurrentQuestionCategory() == "Science")
            {
                var scienceQuestion = _questionsSection.DrawScienceQuestion();
                Console.WriteLine(scienceQuestion);
            }

            if (GetCurrentQuestionCategory() == "Sports")
            {
                var sportsQuestion = _questionsSection.DrawSportsQuestion();
                Console.WriteLine(sportsQuestion);
            }

            if (GetCurrentQuestionCategory() == "Rock")
            {
                var rockQuestion = _questionsSection.DrawRockQuestion();
                Console.WriteLine(rockQuestion);
            }
        }


        private string GetCurrentQuestionCategory()
        {
            switch (_places[_currentPlayer])
            {
                case 0:
                case 4:
                case 8:
                    return "Pop";
                case 1:
                case 5:
                case 9:
                    return "Science";
                case 2:
                case 6:
                case 10:
                    return "Sports";
                default:
                    return "Rock";
            }
        }

        public bool SimulateQuestionWasAnsweredCorrectly()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    Console.WriteLine(_players[_currentPlayer]
                                      + " now has "
                                      + _purses[_currentPlayer]
                                      + " Gold Coins.");

                    bool winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner;
                }

                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;
                return true;
            }

            {
                Console.WriteLine("Answer was correct!!!!");
                _purses[_currentPlayer]++;
                Console.WriteLine(_players[_currentPlayer]
                                  + " now has "
                                  + _purses[_currentPlayer]
                                  + " Gold Coins.");

                bool winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;

                return winner;
            }
        }

        public void SimulateQuestionWasAnsweredIncorrectly()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count)
            {
                _currentPlayer = 0;
            }
        }

        private bool DidPlayerWin()
        {
            return _purses[_currentPlayer] != 6;
        }
    }
}