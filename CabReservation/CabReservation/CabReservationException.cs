using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabReservation
{
    public class CabReservationException : Exception
    {
        public ExceptionType type;
        public enum ExceptionType
        {
            INVALID_DISTANCE, INVALID_TIME
        }
        public CabReservationException(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
