using CabReservation;
using NUnit.Framework;

namespace CabReservationTest
{
    public class Tests
    {
        InvoiceGenerator generator;
        [SetUp]
        public void Setup()
        {
            generator = new InvoiceGenerator();
        }
        /// <summary>
        /// UC 1- Total Fare for Single Ride
        /// </summary>
        [Test]
        [TestCase(5,3)]
        public void Given_TimeAndDistance_CalculateFare(double distance, double time)
        {
            Ride ride = new Ride(distance, time);
            int expected = 53;
            Assert.AreEqual(expected, generator.TotalFearForSingleRide(ride));
        }
        /// <summary>
        /// TC1.1 - Check for Invalid Distance
        /// </summary>
        [Test]
        public void Given_InvalidDistance_ThrowException()
        {
            Ride ride = new Ride(-1, 1);
            CabReservationException cabReservationException = Assert.Throws<CabReservationException>(() => generator.TotalFearForSingleRide(ride));
            Assert.AreEqual(CabReservationException.ExceptionType.INVALID_DISTANCE, cabReservationException.type);
        }
        /// <summary>
        /// TC1.2- Check for Invalid Time
        /// </summary>
        [Test]
        public void Given_InvalidTime_ThrowException()
        {
            Ride ride = new Ride(1, -1);
            CabReservationException cabReservationException = Assert.Throws<CabReservationException>(() => generator.TotalFearForSingleRide(ride));
            Assert.AreEqual(CabReservationException.ExceptionType.INVALID_TIME, cabReservationException.type);
        }
    }
}