using NUnit.Framework;
using Storage;
using System.Collections.Generic;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class CatchTheBallStorageTests
        {
            private const int DUMMY_TARGET_RADIUS = 5;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Tests</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test objects in the CatchTheBall storage.
            </li>
        </ul>
        ")]
            [Test]
            public void ObjectsAreSemanticallyEquivalent()
            {

                CatchTheBallStorage storage1 = new CatchTheBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<CatchTheBallStorage.BallRound>
                    {
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 5.3F,
                            InitialBallRadius = 3.9F,
                            PredictedTimeToContact = 6.8F
                        },
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 6.3F,
                            InitialBallRadius = 7.4F,
                            PredictedTimeToContact = 6.2F
                        },
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 9.2F,
                            InitialBallRadius = 4,
                            PredictedTimeToContact = 9.2F
                        },
                    }
                };

                CatchTheBallStorage storage2 = new CatchTheBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<CatchTheBallStorage.BallRound>
                    {
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 5.3F,
                            InitialBallRadius = 3.9F,
                            PredictedTimeToContact = 6.8F
                        },
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 6.3F,
                            InitialBallRadius = 7.4F,
                            PredictedTimeToContact = 6.2F
                        },
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 9.2F,
                            InitialBallRadius = 4,
                            PredictedTimeToContact = 9.2F
                        },
                    }
                };

                Assert.AreEqual(storage1, storage2);
            }
        }
    }
}