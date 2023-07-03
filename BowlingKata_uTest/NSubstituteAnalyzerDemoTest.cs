using BowlingKata;
using NSubstitute;
using NUnit.Framework;

namespace BowlingKata_uTest
{
    [TestFixture]
    public sealed class NSubstituteAnalyzerDemoTest
    {
        [Test]
        public void ShowThatMemberCanNotBeIntercepted()
        {
            var game = Substitute.For<Game>();
            game.Score().Returns(7);
        }
    }
}