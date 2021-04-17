using MarsRoverAPI.Enums;
using MarsRoverAPI.Models;
using System;
using System.Collections.Generic;

namespace MarsRoverAPI.Helpers
{
    public  class Parse: IParse
    {
        public string ToString(Position position) 
        {
            return $"{position.X} {position.Y} {position.Direction}";
        }

        public  List<Movement> ToMovemenent(string instructions)
        {
            var movements = new List<Movement>();
            foreach (var move in instructions)
            {
                movements.Add(Enum.Parse<Movement>(move.ToString()));
            }
            return movements; ;
        }

        public Plateau ToPlateau(string coordinates)
        {
            var plateau = new Plateau();
            var split = coordinates.Split(' ');

            plateau.X = int.Parse(split[0]);
            plateau.Y = int.Parse(split[1]);

            return plateau;
        }

        public Position ToRover(string command)
        {
            var position = new Position();
            var split = command.Split(' ');

            position.X = int.Parse(split[0]);
            position.Y = int.Parse(split[1]);
            position.Direction = Enum.Parse<Direction>(split[2]);

            return position;
        }
    }
}
