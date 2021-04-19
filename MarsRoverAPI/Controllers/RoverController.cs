using MarsRoverAPI.Commands;
using MarsRoverAPI.Helpers;
using MarsRoverAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MarsRoverAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoverController : ControllerBase
    {
        private readonly ILogger<RoverController> _logger;

        private readonly ISquad _squad;
        private readonly IParse _parse;
        public RoverController(ILogger<RoverController> logger, ISquad squad, IParse parse)
        {
            _logger = logger;
            _squad = squad;
            _parse = parse;
        }

        [HttpPost]
        [Route("SendCommands")]
        public IEnumerable<string> SendCommands([FromBody]Command command)
        {
            try
            {
                var plateau = _parse.ToPlateau(command.Plateau);
                var rovers = new List<string>();

                foreach (var rover in command.Rovers)
                {
                    var position = _parse.ToRover(rover.Position);
                    var movements = _parse.ToMovemenent(rover.Movement);
                    var result =_squad.Deploy(plateau, position, movements);

                    rovers.Add(_parse.ToString(result));
                }

                return rovers;
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to deploy rover!", ex);
                return null;
            }
        }

    }
}