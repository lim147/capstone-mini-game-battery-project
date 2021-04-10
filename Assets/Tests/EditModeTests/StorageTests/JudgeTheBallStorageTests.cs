using NUnit.Framework;
using Storage;
using System.Collections.Generic;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class JudgeTheBallStorageTests
        {
            private const int DUMMY_TARGET_RADIUS = 5;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that objects in the storage are as expected.
            </li>
        </ul>
        ")]
            [Test]
            public void ObjectsAreSemanticallyEquivalent()
            {

                JudgeTheBallStorage storage1 = new JudgeTheBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<JudgeTheBallStorage.BallRound>
                    {
                        new JudgeTheBallStorage.BallRound
                        {
                            SlowBallActualTimeToContact = 5.3F,
                            PlayerBallActualTimeToContact = 6.0F,
                            FastBallActualTimeToContact = 7,
                            InitialSlowBallRadius = 3.9F,
                            InitialPlayerBallRadius = 3.9F,
                            InitialFastBallRadius = 3.9F,
                            PredictedTimeToContact = 6.8F
                        },
                        new JudgeTheBallStorage.BallRound
                        {
                            SlowBallActualTimeToContact = 6.3F,
                            PlayerBallActualTimeToContact = 7,
                            FastBallActualTimeToContact = 7.5F,
                            InitialSlowBallRadius = 7.4F,
                            InitialPlayerBallRadius = 7.4F,
                            InitialFastBallRadius = 7.4F,
                            PredictedTimeToContact = 6.2F
                        },
                        new JudgeTheBallStorage.BallRound
                        {
                            SlowBallActualTimeToContact = 9.2F,
                            PlayerBallActualTimeToContact = 9.6F,
                            FastBallActualTimeToContact = 10,
                            InitialSlowBallRadius = 4,
                            InitialPlayerBallRadius = 4,
                            InitialFastBallRadius = 4,
                            PredictedTimeToContact = 9.2F
                        },
                    }
                };

                JudgeTheBallStorage storage2 = new JudgeTheBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<JudgeTheBallStorage.BallRound>
                    {
                        new JudgeTheBallStorage.BallRound
                        {
                            SlowBallActualTimeToContact = 5.3F,
                            PlayerBallActualTimeToContact = 6.0F,
                            FastBallActualTimeToContact = 7,
                            InitialSlowBallRadius = 3.9F,
                            InitialPlayerBallRadius = 3.9F,
                            InitialFastBallRadius = 3.9F,
                            PredictedTimeToContact = 6.8F
                        },
                        new JudgeTheBallStorage.BallRound
                        {
                            SlowBallActualTimeToContact = 6.3F,
                            PlayerBallActualTimeToContact = 7,
                            FastBallActualTimeToContact = 7.5F,
                            InitialSlowBallRadius = 7.4F,
                            InitialPlayerBallRadius = 7.4F,
                            InitialFastBallRadius = 7.4F,
                            PredictedTimeToContact = 6.2F
                        },
                        new JudgeTheBallStorage.BallRound
                        {
                            SlowBallActualTimeToContact = 9.2F,
                            PlayerBallActualTimeToContact = 9.6F,
                            FastBallActualTimeToContact = 10,
                            InitialSlowBallRadius = 4,
                            InitialPlayerBallRadius = 4,
                            InitialFastBallRadius = 4,
                            PredictedTimeToContact = 9.2F
                        },
                    }
                };

                Assert.AreEqual(storage1, storage2);
            }
        }
    }
}