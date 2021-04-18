using MarsRoverAPI.Enums;
using MarsRoverAPI.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            var expression = new Regex("^[LMR]+$");
            if (!expression.IsMatch(instructions))
            {
                throw new Exception($"Movements {instructions} does not match format LRM");
            }
            var movements = new List<Movement>();
            foreach (var move in instructions)
            {
                movements.Add(Enum.Parse<Movement>(move.ToString()));
            }
            return movements; ;
        }

        public Plateau ToPlateau(string coordinates)
        {
            var expression = new Regex("^\\d+ \\d+");
            if (!expression.IsMatch(coordinates))
            {
                throw new Exception($"Plateau {coordinates} does not match format 0 0");
            }
            var plateau = new Plateau();
            var split = coordinates.Split(' ');

            plateau.X = int.Parse(split[0]);
            plateau.Y = int.Parse(split[1]);

            return plateau;
        }

        public Position ToRover(string command)
        {
            var expression = new Regex("^\\d+ \\d+ [NSWE]$");
            if (!expression.IsMatch(command))
            {
                throw new Exception($"Position {command} does not match format 0 0 N");
            }
            var position = new Position();
            var split = command.Split(' ');

            position.X = int.Parse(split[0]);
            position.Y = int.Parse(split[1]);
            position.Direction = Enum.Parse<Direction>(split[2]);

            return position;
        }
    }
}
