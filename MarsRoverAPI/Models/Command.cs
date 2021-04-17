using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverAPI.Models
{
    public class Command
    {
        public string Plateau{ get; set; }
        public List<RoverCommand> Rovers { get; set; }
    }
}
