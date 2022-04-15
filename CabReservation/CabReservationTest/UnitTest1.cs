using CabReservation;
using NUnit.Framework;
using System.Collections.Generic;

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
            Assert.AreEqual(expected, generator.TotalFareForSingleRide(ride));
        }
        /// <summary>
        /// TC1.1 - Check for Invalid Distance
        /// </summary>
        [Test]
        public void Given_InvalidDistance_ThrowException()
        {
            Ride ride = new Ride(-1, 1);
            CabReservationException cabReservationException = Assert.Throws<CabReservationException>(() => generator.TotalFareForSingleRide(ride));
            Assert.AreEqual(CabReservationException.ExceptionType.INVALID_DISTANCE, cabReservationException.type);
        }
        /// <summary>
        /// TC1.2- Check for Invalid Time
        /// </summary>
        [Test]
        public void Given_InvalidTime_ThrowException()
        {
            Ride ride = new Ride(1, -1);
            CabReservationException cabReservationException = Assert.Throws<CabReservationException>(() => generator.TotalFareForSingleRide(ride));
            Assert.AreEqual(CabReservationException.ExceptionType.INVALID_TIME, cabReservationException.type);
        }
        /// <summary>
        /// UC2 - Total fare for Multiple rides 
        /// </summary>
        [Test]
        public void Given_ListOfRides_GenerateInvoice()
        {
            Ride rideOne = new Ride(4, 4);
            Ride rideTwo = new Ride(2, 1);
            Ride rideThree = new Ride(3, 1);    

            List<Ride> rides = new List<Ride>();
            rides.Add(rideOne);
            rides.Add(rideTwo);
            rides.Add(rideThree);

            Assert.AreEqual(96.0d, generator.TotalFareForMultipleRides(rides));            
        }
    }
}