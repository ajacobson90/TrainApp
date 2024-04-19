using TrainApp.Domain;

namespace TrainApp.UnitTests
{
    public class TrainTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(10, 5, 5)]
        [TestCase(0, 0, 0)]
        public void NewTrain_WhenGivenEvenSeatCount_SplitsEvenlyBetweenAisleAndWindow(int totalSeats, int expectedAisle, int expectedWindow)
        {
            var train = new Train("Atlanta", TimeOnly.Parse("12:00"), totalSeats);
            Assert.That(train.AisleSeatsAvailable, Is.EqualTo(expectedAisle));
            Assert.That(train.WindowSeatsAvailable, Is.EqualTo(expectedWindow));
        }

        [Test]
        [TestCase(11, 6, 5)]
        [TestCase(1, 1, 0)]
        public void NewTrain_WhenGivenOddSeatCount_SplitsWithAisleGivenOneMoreSeatThanWindow(int totalSeats, int expectedAisle, int expectedWindow)
        {
            var train = new Train("Atlanta", TimeOnly.Parse("12:00"), totalSeats);
            Assert.That(train.AisleSeatsAvailable, Is.EqualTo(expectedAisle));
            Assert.That(train.WindowSeatsAvailable, Is.EqualTo(expectedWindow));
        }

        [Test]
        public void BookWindowSeat_IfNoWindowSeatsLeft_ReturnFalseAndCountStaysAtZero()
        {
            var train = new Train("Atlanta", TimeOnly.Parse("12:00"), 0);
            Assert.IsFalse(train.BookWindowSeat());
            Assert.That(train.WindowSeatsAvailable, Is.EqualTo(0));
        }

        [Test]
        public void BookAisleSeat_IfNoAisleSeatsLeft_ReturnFalseAndCountStaysAtZero()
        {
            var train = new Train("Atlanta", TimeOnly.Parse("12:00"), 0);
            Assert.IsFalse(train.BookAisleSeat());
            Assert.That(train.AisleSeatsAvailable, Is.EqualTo(0));
        }
    }
}