using MarsRoverAPI.Enums;
using MarsRoverAPI.Models;
using System.Collections.Generic;

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
