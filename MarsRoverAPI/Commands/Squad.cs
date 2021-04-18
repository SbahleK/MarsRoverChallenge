using System;
using System.Collections.Generic;
using MarsRoverAPI.Enums;
using MarsRoverAPI.Models;

namespace MarsRoverAPI.Commands
{
    public class Squad:ISquad
    {
        private readonly IRover _rover;

        public Squad(IRover rover)
        {
            _rover = rover;
        }

        public Position Deploy(Plateau plateau, Position position, List<Movement> movements)
        {
            if (!CanDeployRover(plateau, position))
            {
                throw new Exception("Rover is outside the defined boundaries");
            }

            _rover.LandingArea = plateau;
            _rover.Position = position;
            foreach (var instruction in movements)
            {
                _rover.Move(instruction);
            }
            return _rover.Position;
        }

        private bool CanDeployRover(Plateau landingSpace, Position position)
        {
            return position.X >= 0 && position.X <= landingSpace.X &&
                   position.Y >= 0 && position.Y <= landingSpace.Y;
        }

    
    }
}
