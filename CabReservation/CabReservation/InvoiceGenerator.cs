using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabReservation
{
    public class InvoiceGenerator
    {
        readonly int PricePerKilometer;
        readonly int PricePerMinute;
        readonly int MinimumFare;
        readonly int premiumPricePerKm;
        readonly int premiumPricePerMin;
        readonly int premiumMinimumFare;
        public int NumOfRides;
        public double TotalFare;
        public double AveragePerRide;

        public InvoiceGenerator()
        {
            this.PricePerKilometer = 10;
            this.PricePerMinute = 1;
            this.MinimumFare = 5;
            this.premiumPricePerKm = 15;
            this.premiumPricePerMin = 2;
            this.premiumMinimumFare = 20;
        }
        public double TotalFareForSingleRide(Ride ride)
        {
            if(ride.distance < 0)
            {
                throw new CabReservationException(CabReservationException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
            }
            if(ride.time < 0)
            {
                throw new CabReservationException(CabReservationException.ExceptionType.INVALID_TIME, "Time is Invalid");
            }
            return Math.Max(MinimumFare, ride.distance * PricePerKilometer + ride.time * PricePerMinute);
        }
        public double TotalFareForMultipleRides(List<Ride> rides)
        {
            foreach(Ride ride in rides)
            {
                TotalFare += TotalFareForSingleRide(ride);
                NumOfRides++;
            }
            AveragePerRide = TotalFare / NumOfRides;
            return TotalFare;
        }
        public double TotalFareForPremiumSingleRide(Ride ride)
        {
            if (ride.distance < 0)
            {
                throw new CabReservationException(CabReservationException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
            }
            if (ride.time < 0)
            {
                throw new CabReservationException(CabReservationException.ExceptionType.INVALID_TIME, "Time is Invalid");
            }
            return Math.Max(premiumMinimumFare, ride.distance * premiumPricePerKm + ride.time * premiumPricePerMin);
        }
        public double TotalFareForPremiumMultipleRide(List<Ride> rides)
        {
            foreach(Ride ride in rides)
            {
                TotalFare += TotalFareForPremiumSingleRide(ride);
                NumOfRides++;
            }
            AveragePerRide = TotalFare / NumOfRides;
            return TotalFare;
        }
    }
}