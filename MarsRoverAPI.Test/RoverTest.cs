using MarsRoverAPI.Commands;
using MarsRoverAPI.Controllers;
using MarsRoverAPI.Enums;
using MarsRoverAPI.Helpers;
using MarsRoverAPI.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarsRoverAPI.Test
{
    public class Tests
    {
        private  IRover _rover;
        private  IParse _helper;

        [SetUp]
        public void Setup()
        {
            _rover = new Rover();
            _helper = new Parse();
        }

        [Test]
        [TestCase("5 5")]
        [TestCase("10 10")]
        public void PlateauShouldBeTwoDigitCommandSeparatedBySpace(string command)
        {
            //Act
            Plateau plateau = _helper.ToPlateau(command);

            //Assert
            Assert.IsNotNull(plateau);
            Assert.AreEqual($"{plateau.X} {plateau.Y}", command);
        }

        [Test]
        [TestCase("1 2 N")]
        [TestCase("3 3 E")]
        public void RoverPostionShouldBeTwoDigitFollowedByOneCharacterCommandSeparatedBySpace(string command)
        {
            //Act
            Position position = _helper.ToRover(command);

            //Assert
            Assert.IsNotNull(position);
            Assert.AreEqual(_helper.ToString(position), command);
        }

        [Test]
        [TestCase("1 2 N")]
        public void RoverShouldSpinLeft(string command)
        {
            //Arrange
            _rover.Position = _helper.ToRover(command);

            //Act
            _rover.SpinLeft();

            //Assert
            Assert.IsNotNull(_rover);
            Assert.IsNotNull(_rover.Position);
            Assert.AreEqual(_rover.Position.Direction, Direction.W);
        }

        [Test]
        [TestCase("1 2 N")]
        public void RoverShouldSpinRight(string command)
        {
            //Arrange
            _rover.Position = _helper.ToRover(command);

            //Act
            _rover.SpinRight();

            //Assert
            Assert.IsNotNull(_rover);
            Assert.IsNotNull(_rover.Position);
            Assert.AreEqual(_rover.Position.Direction, Direction.E);
        }

        [Test]
        [TestCase("5 5","1 2 N")]
        public void RoverShouldMoveYForward(string landingArea, string position)
        {
            //Arrange
            _rover.Position = _helper.ToRover(position);
            _rover.LandingArea = _helper.ToPlateau(landingArea);

            //Act
            _rover.MoveForward();

            //Assert
            Assert.IsNotNull(_rover);
            Assert.IsNotNull(_rover.Position);
            Assert.IsNotNull(_rover.LandingArea);
            Assert.AreEqual(_rover.Position.Y, 3);
        }

        [Test]
        [TestCase("5 5", "3 3 E")]
        public void RoverShouldMoveXForward(string landingArea, string position)
        {
            //Arrange
            _rover.Position = _helper.ToRover(position);
            _rover.LandingArea = _helper.ToPlateau(landingArea);

            //Act
            _rover.MoveForward();

            //Assert
            Assert.IsNotNull(_rover);
            Assert.IsNotNull(_rover.Position);
            Assert.IsNotNull(_rover.LandingArea);
            Assert.AreEqual(_rover.Position.X, 4);
        }

        [Test]
        [TestCase("5 5", "1 2 N", "LMLMLMLMM", ExpectedResult = "1 3 N")]
        [TestCase("5 5", "3 3 E", "MMRMMRMRRM",ExpectedResult = "5 1 E")]
        public string RoverShouldDeployToExpectedCordinates(string landingArea, string position, string instructions)
        {
            // Arrange
            _rover.Position = _helper.ToRover(position);
            _rover.LandingArea = _helper.ToPlateau(landingArea);
            var movements = _helper.ToMovemenent(instructions);
            var squad = new Squad(_rover);

            // Act
            squad.Deploy(_rover.LandingArea, _rover.Position, movements);

            // Assert
            Assert.IsNotNull(_rover);
            Assert.IsNotNull(_rover.Position);
            Assert.IsNotNull(_rover.LandingArea);
            return _helper.ToString(_rover.Position);
        }

        [Test]
        [TestCase("5 5", "1 10 N", "LMLMLMLMM")]
        [TestCase("5 5", "6 3 E", "MMRMMRMRRM")]
        public void RoverShouldBeDeployedWithinTheCorrectPlateauBoundaries(string landingArea, string position, string instructions)
        {
            // Arrange
            _rover.Position = _helper.ToRover(position);
            _rover.LandingArea = _helper.ToPlateau(landingArea);
            var movements = _helper.ToMovemenent(instructions);
            var squad = new Squad(_rover);

            // Act
            var ex = Assert.Throws<Exception>(() => squad.Deploy(_rover.LandingArea, _rover.Position, movements));


            // Assert
            Assert.That(ex.Message, Is.EqualTo("Rover is outside the defined boundaries"));
        }

        [Test]
        public void SquadShouldBeAbleToSendMoreThanOneRovers()
        {
            //Arrange
            var command = new Command();
            command.Rovers = new List<RoverCommand>();

            command.Plateau = ("5 5");
            command.Rovers.Add(new RoverCommand { Position = ("1 2 N"), Movement = ("LMLMLMLMM") });
            command.Rovers.Add(new RoverCommand { Position = ("3 3 E"), Movement = ("MMRMMRMRRM") });

            var squad = new Squad(_rover);
            var logger = new Mock<ILogger<RoverController>>();
            var controller = new RoverController(logger.Object, squad, _helper);
            //Act
            var rovers = (List<string>)controller.SendCommands(command);

            //Assert
            Assert.AreEqual("1 3 N", rovers[0]);
            Assert.AreEqual("5 1 E", rovers[1]);
        }                  
    }
}