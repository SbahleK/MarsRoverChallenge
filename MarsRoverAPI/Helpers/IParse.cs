using MarsRoverAPI.Enums;
using MarsRoverAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverAPI.Helpers
{
    public interface IParse
    {
        List<Movement> ToMovemenent(string instructions);
        Plateau ToPlateau(string coordinates);
        Position ToRover(string command);
        string ToString(Position position);
    }
}
