using BowlingKata;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingKata_uTest
{
    [TestFixture]
    public sealed class GameTest
    {
        [Test]
        public void Score_ReturnsNumberOfScoredPins_AfterOneRoll()
        {
            var game = new Game();
            game.Roll(5);

            var score = game.Score();

            score.Should().Be(5);
        }

        [TestCase(3, 5, 8)]
        [TestCase(9, 1, 10)]
        public void Score_ReturnsNumberOfScoredPins_AfterTwoRolls(int roll1, int roll2, int expected)
        {
            var game = new Game();
            game.Roll(roll1);
            game.Roll(roll2);

            var score = game.Score();

            score.Should().Be(expected);
        }

        [Test]
        public void Score_DoublesNumberOfScoredPins_ForTheFirstRollAfterASpare()
        {
            var game = new Game();
            game.Roll(9);
            game.Roll(1);
            game.Roll(5);
            game.Roll(4);

            var score = game.Score();

            score.Should().Be(24);
        }

        [Test]
        public void Score_DoublesNumberOfScoredPins_ForTheNextTwoRollsAfterAStrike()
        {
            var game = new Game();
            game.Roll(10);
            game.Roll(5);
            game.Roll(4);

            var score = game.Score();

            score.Should().Be(28);
        }

        [Test]
        public void Score_ReturnsZero_ForANewlyCreatedGame()
        {
            var game = new Game();

            var score = game.Score();

            score.Should().Be(0);
        }

        [Test]
        public void Score_ReturnsZero_ForANewGameAfterAnotherOneWasStartedBefore()
        {
            var game = new Game();
            game.Roll(7);
            game = new Game();

            var score = game.Score();

            score.Should().Be(0);
        }

        [Test]
        public void Score_Returns300_ForAPerfectGame()
        {
            var game = new Game();
            for (int i = 0; i < 12; i++)
            {
                game.Roll(10);
            }

            var score = game.Score();

            score.Should().Be(300);
        }

        [Test]
        public void Score_Returns286_ForANearlyPerfectGame()
        {
            var game = new Game();
            for (int i = 0; i < 10; i++)
            {
                game.Roll(10);
            }

            game.Roll(7);
            game.Roll(2);

            var score = game.Score();

            score.Should().Be(286);
        }
    }
}