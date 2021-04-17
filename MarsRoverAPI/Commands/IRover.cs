using MarsRoverAPI.Enums;
using MarsRoverAPI.Models;

namespace MarsRoverAPI.Commands
{
    public interface IRover
    {
        Position Position { get; set; }
        Plateau LandingArea { get; set; }
        void Move(Movement movement);
         void SpinRight();
         void SpinLeft();
        void MoveForward();
    }
}
