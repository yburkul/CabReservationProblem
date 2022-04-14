﻿using System;
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
        public InvoiceGenerator()
        {
            this.PricePerKilometer = 10;
            this.PricePerMinute = 1;
            this.MinimumFare = 5;
        }
        public double TotalFearForSingleRide(Ride ride)
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
    }
}