using System.Collections.Generic;

namespace MarsRoverAPI.Models
{
    public class Command
    {
        public string Plateau{ get; set; }
        public List<RoverCommand> Rovers { get; set; }
    }
}
