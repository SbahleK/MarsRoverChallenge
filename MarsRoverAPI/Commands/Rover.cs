using MarsRoverAPI.Enums;
using MarsRoverAPI.Models;
using System;

namespace MarsRoverAPI.Commands
{
    public class Rover : IRover
    {
        public Position Position { get; set; }
        public Plateau LandingArea { get; set; }
        public Rover()
        {
            LandingArea = new Plateau { X = 0, Y = 0 };
        }
        public void Move(Movement movement)
        {
            switch (movement)
            {
                case Movement.L:
                    SpinLeft();
                    break;
                case Movement.R:
                     SpinRight();
                    break;
                case Movement.M:
                    MoveForward();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movement), movement, null);
            }
        }

        public void MoveForward()
        {
            if (Position.Direction == Direction.N && (Position.Y + 1 <= LandingArea.Y))
            {
                Position.Y += 1;
            }
            else if (Position.Direction == Direction.E && (Position.X + 1 <= LandingArea.X))
            {
                Position.X += 1;
            }
            else if (Position.Direction == Direction.S && (Position.Y - 1 >= 0))
            {
                Position.Y -= 1;
            }
            else if (Position.Direction == Direction.W && (Position.X - 1 >= 0))
            {
                Position.X -= 1;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }


        public void SpinLeft()
        {
            switch (Position.Direction)
            {
                case Direction.N:
                    Position.Direction = Direction.W;
                    break;

                case Direction.W:
                    Position.Direction = Direction.S;
                    break;

                case Direction.S:
                    Position.Direction = Direction.E;
                    break;

                case Direction.E:
                    Position.Direction = Direction.N;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SpinRight()
        {
            switch (Position.Direction)
            {
                case Direction.N:
                    Position.Direction = Direction.E;
                    break;

                case Direction.E:
                    Position.Direction = Direction.S;
                    break;

                case Direction.S:
                    Position.Direction = Direction.W;
                    break;

                case Direction.W:
                    Position.Direction = Direction.N;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
