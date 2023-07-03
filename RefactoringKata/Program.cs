namespace RefactoringKata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game(new QuestionsSection());
            new GameRunner(game).RunGame();
        }
    }
}