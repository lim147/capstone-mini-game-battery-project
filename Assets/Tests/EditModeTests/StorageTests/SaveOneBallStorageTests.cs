using NUnit.Framework;
using Storage;
using System.Collections.Generic;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class SaveOneBallStorageTests
        {
            private const int DUMMY_TARGET_RADIUS = 5;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing that objects in the storage are as expected.
            </li>
        </ul>
        ")]
            [Test]
            public void ObjectsAreSemanticallyEquivalent()
            {

                SaveOneBallStorage storage1 = new SaveOneBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<SaveOneBallStorage.BallRound>
                    {
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 5.3F,
                            RightActualTimeToContact = 3.5F,
                            DidPredictFirstArrivingBall = true,
                            InitialLeftBallRadius = 3.9F,
                            InitialRightBallRadius = 3.9F,
                            PredictedTimeToContact = 6.8F
                        },
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 6.3F,
                            RightActualTimeToContact = 3.6F,
                            DidPredictFirstArrivingBall = false,
                            InitialLeftBallRadius = 7.4F,
                            InitialRightBallRadius = 7.4F,
                            PredictedTimeToContact = 6.2F
                        },
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 9.2F,
                            RightActualTimeToContact = 2.9F,
                            DidPredictFirstArrivingBall = true,
                            InitialLeftBallRadius = 4,
                            InitialRightBallRadius = 4,
                            PredictedTimeToContact = 9.2F
                        },
                    }
                };

                SaveOneBallStorage storage2 = new SaveOneBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<SaveOneBallStorage.BallRound>
                    {
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 5.3F,
                            RightActualTimeToContact = 3.5F,
                            DidPredictFirstArrivingBall = true,
                            InitialLeftBallRadius = 3.9F,
                            InitialRightBallRadius = 3.9F,
                            PredictedTimeToContact = 6.8F
                        },
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 6.3F,
                            RightActualTimeToContact = 3.6F,
                            DidPredictFirstArrivingBall = false,
                            InitialLeftBallRadius = 7.4F,
                            InitialRightBallRadius = 7.4F,
                            PredictedTimeToContact = 6.2F
                        },
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 9.2F,
                            RightActualTimeToContact = 2.9F,
                            DidPredictFirstArrivingBall = true,
                            InitialLeftBallRadius = 4,
                            InitialRightBallRadius = 4,
                            PredictedTimeToContact = 9.2F
                        },
                    }
                };

                Assert.AreEqual(storage1, storage2);
            }
        }
    }
}