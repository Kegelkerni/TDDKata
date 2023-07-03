namespace BowlingKata
{
    public class Game
    {
        private int _currentFrameIndex;

        private readonly List<Frame> _frames = new List<Frame>
        {
            new Frame(), new Frame(), new Frame(), new Frame(), new Frame(),
            new Frame(), new Frame(), new Frame(), new Frame(), new LastFrame()
        };

        public void Roll(int pins)
        {
            if (_frames[_currentFrameIndex].GetType() != typeof(LastFrame) &&
                _frames[_currentFrameIndex].ResultType == FrameResultType.SumOfTwoRollsEntered ||
                _frames[_currentFrameIndex].ResultType == FrameResultType.Spare ||
                _frames[_currentFrameIndex].ResultType == FrameResultType.Strike)
            {
                _currentFrameIndex += 1;
            }

            _frames[_currentFrameIndex].Add(pins);
        }

        public virtual int Score()
        {
            var sum = 0;
            for (var index = 0; index < _frames.Count; index++)
            {
                var frame = _frames[index];

                if (index > 0)
                {
                    if (TheFrameBeforeWasASpare(index))
                    {
                        sum += frame.FirstValue;
                    }
                    else if (TheFrameBeforeWasAStrike(index))
                    {
                        if (index > 1 && TheFrameTwoFramesBeforeWasAStrike(index))
                        {
                            sum += frame.FirstValue;
                        }

                        sum += frame.Sum;
                    }
                }

                sum += frame.Sum;
            }

            return sum;
        }

        private bool TheFrameTwoFramesBeforeWasAStrike(int index)
        {
            return _frames[index - 2].ResultType == FrameResultType.Strike;
        }

        private bool TheFrameBeforeWasASpare(int index)
        {
            return LastFrameHadASpecificFrameResultType(index, FrameResultType.Spare);
        }

        private bool TheFrameBeforeWasAStrike(int index)
        {
            return LastFrameHadASpecificFrameResultType(index, FrameResultType.Strike);
        }

        private bool LastFrameHadASpecificFrameResultType(int index, FrameResultType frameResultType)
        {
            return _frames[index - 1].ResultType == frameResultType;
        }

        private class Frame
        {
            protected int _sum;
            private int _firstValue;
            private FrameResultType _frameResultType;

            public virtual void Add(int pins)
            {
                if (_frameResultType == FrameResultType.OneRollEntered)
                {
                    _sum += pins;

                    _frameResultType = _sum == 10 ? FrameResultType.Spare : FrameResultType.SumOfTwoRollsEntered;

                    return;
                }

                _firstValue = pins;
                _sum += pins;

                _frameResultType = _sum == 10 ? FrameResultType.Strike : FrameResultType.OneRollEntered;
            }

            public FrameResultType ResultType => _frameResultType;
            public int Sum => _sum;
            public int FirstValue => _firstValue;
        }

        private class LastFrame : Frame
        {
            public override void Add(int pins)
            {
                _sum += pins;
            }
        }

        private enum FrameResultType
        {
            Undefined,
            OneRollEntered,
            SumOfTwoRollsEntered,
            Spare,
            Strike
        }
    }
}