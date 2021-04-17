using MarsRoverAPI.Enums;
using MarsRoverAPI.Models;
using System.Collections.Generic;

namespace MarsRoverAPI.Commands
{
    public interface ISquad
    {
        Position Deploy(Plateau plateau, Position position, List<Movement> movements);
    }
}
