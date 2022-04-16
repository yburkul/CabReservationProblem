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
        /// <summary>
        /// UC 5.1 - Total fare for Premium ride
        /// </summary>
        [Test]
        [TestCase(7, 3)]
        public void Given_DistanceAndTime_CalculatePremiumFare(double distance, double time)
        {
            Ride ride = new Ride(distance, time);
            int expected = 111;
            Assert.AreEqual(expected, generator.TotalFareForPremiumSingleRide(ride));
        }
        /// <summary>
        /// TC 5.2 - Given the Invalid Distance Throws Exception
        /// </summary>
        [Test]
        public void Given_InvalidDistance_ThrowsException()
        {
            Ride ride = new Ride(-1, 1);
            CabReservationException cabReservationException = Assert.Throws<CabReservationException>(() => generator.TotalFareForPremiumSingleRide(ride));
            Assert.AreEqual(CabReservationException.ExceptionType.INVALID_DISTANCE, cabReservationException.type);
        }
        /// <summary>
        /// TC 5.3 - Given The Invalid Time Throws Exception
        /// </summary>
        [Test]
        public void Given_InvalidTime_ThrowsException()
        {
            Ride ride = new Ride(1, -1);
            CabReservationException cabReservationException = Assert.Throws<CabReservationException>(() => generator.TotalFareForPremiumSingleRide(ride));
            Assert.AreEqual(CabReservationException.ExceptionType.INVALID_TIME, cabReservationException.type);
        }
        /// <summary>
        /// TC 5.4 - Total fare for Premium Multiple rides
        /// </summary>
        [Test]
        public void Given_DistanceAndTime_CalculteFareForPremiumMultipleRide()
        {
            Ride rideOne = new Ride(7, 5);
            Ride rideTwo = new Ride(8, 6);
            List<Ride> rides = new List<Ride>();    
            rides.Add(rideOne);
            rides.Add(rideTwo);

            Assert.AreEqual(247.0d, generator.TotalFareForPremiumMultipleRide(rides));
        }
        /// <summary>
        /// TC 5.5 - Calculte The Avg and Number of Rides for Multiple Rides
        /// </summary>
        [Test]
        public void Given_DistanceAndTime_Calculte_NumOfRidesAndAvg_PremumMultipleRides()
        {
            Ride rideOne = new Ride(7, 5);
            Ride rideTwo = new Ride(8, 6);
            List<Ride> rides = new List<Ride>();
            rides.Add(rideOne);
            rides.Add(rideTwo);

            Assert.AreEqual(247.0d, generator.TotalFareForPremiumMultipleRide(rides));
            Assert.AreEqual(123.5d, generator.AveragePerRide);
            Assert.AreEqual(2, generator.NumOfRides);
        }
        /// <summary>
        /// UC 5.6 - Given valid User Id For Premium Rides
        /// </summary>
        [Test]
        public void Given_ValidUserId_InvoiceService()
        {
            Ride rideOne = new Ride(9, 4);
            Ride rideTwo = new Ride(5, 3);
            rideRepository.AddRide("ABC", rideOne);
            rideRepository.AddRide("ABC", rideTwo);

            Assert.AreEqual(224.0d, generator.TotalFareForPremiumMultipleRide(rideRepository.getListByUserId("ABC")));
            Assert.AreEqual(112.0d, generator.AveragePerRide);
            Assert.AreEqual(2, generator.NumOfRides);
        }
        /// <summary>
        /// UC 5.7 - Given Invalid User Id For Premium Rides Throws Exception
        /// </summary>
        [Test]
        public void Given_InvalidUserId_ThrowsException()
        {
            Ride rideOne = new Ride(9, 4);
            Ride rideTwo = new Ride(5, 3);
            rideRepository.AddRide("ABC", rideOne);
            rideRepository.AddRide("ABC", rideTwo);

            CabReservationException cabReservationException = Assert.Throws<CabReservationException>(() => generator.TotalFareForPremiumMultipleRide(rideRepository.getListByUserId("xyz")));
            Assert.AreEqual(CabReservationException.ExceptionType.INVALID_USER_ID, cabReservationException.type);
        }
    }
}