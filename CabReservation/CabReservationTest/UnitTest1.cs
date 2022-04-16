using CabReservation;
using NUnit.Framework;
using System.Collections.Generic;

namespace CabReservationTest
{
    public class Tests
    {
        InvoiceGenerator generator;
        RideRepository rideRepository;
        [SetUp]
        public void Setup()
        {
            generator = new InvoiceGenerator();
            rideRepository = new RideRepository();
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
        public void Given_DistanceAndTime_CalculteFareForMultipleRide()
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
        /// <summary>
        /// UC3- Calculte Total number of Rides and Average Fare per Ride
        /// </summary>
        [Test]
        public void Given_DistanceAndTime_Calculte_NumOfRidesAndAvgFarePerRide()
        {
            Ride rideOne = new Ride(4, 4);
            Ride rideTwo = new Ride(2, 1);
            Ride rideThree = new Ride(3, 1);

            List<Ride> rides = new List<Ride>();
            rides.Add(rideOne);
            rides.Add(rideTwo);
            rides.Add(rideThree);

            Assert.AreEqual(96.0d, generator.TotalFareForMultipleRides(rides));
            Assert.AreEqual(32.0d, generator.AveragePerRide);
            Assert.AreEqual(3, generator.NumOfRides);
        }
        /// <summary>
        /// UC4.1- Check fare of user using valid UserID
        /// </summary>
        [Test]
        public void Given_ValidUserID_InvoicService()
        {
            Ride rideOne = new Ride(4, 4);
            Ride rideTwo = new Ride(2, 1);
            rideRepository.AddRide("YB", rideOne);
            rideRepository.AddRide("YB", rideTwo);

            Assert.AreEqual(65.0d, generator.TotalFareForMultipleRides(rideRepository.getListByUserId("YB")));
            Assert.AreEqual(32.5d, generator.AveragePerRide);
            Assert.AreEqual(2, generator.NumOfRides);
        }
        /// <summary>
        /// UC 4.2 - Given Invalid User Id Throws Exception
        /// </summary>
        [Test]
        public void Given_InvalidUserID_ThrowsException()
        {
            Ride rideOne = new Ride(4, 4);
            Ride rideTwo = new Ride(2, 1);
            rideRepository.AddRide("YB", rideOne);
            rideRepository.AddRide("YB", rideTwo);

            CabReservationException cabReservationException = Assert.Throws<CabReservationException>(() => generator.TotalFareForMultipleRides(rideRepository.getListByUserId("YMB")));
            Assert.AreEqual(CabReservationException.ExceptionType.INVALID_USER_ID, cabReservationException.type);
        }
    }
}