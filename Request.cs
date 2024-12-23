using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorManagement
{
    public class Request
    {
        public int SourceFloor { get; set; }
        public int DestinationFloor { get; set; }
        public DateTime Time { get; set; }

        public Request(int sourceFloor, int destinationFloor)
        {
            SourceFloor = sourceFloor;
            DestinationFloor = destinationFloor;
            Time = DateTime.Now;
        }
    }
}
