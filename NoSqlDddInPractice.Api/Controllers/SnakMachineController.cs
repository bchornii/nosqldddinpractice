using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoSqlDddInPractice.Api.Models;
using NoSqlDddInPractice.Commands.Commands;
using NoSqlDddInPractice.Domain.ReadModels;
using NoSqlDddInPractice.Queries.Queries;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NoSqlDddInPractice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnakMachineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;        

        public SnakMachineController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("{id:length(24)}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSnackMachine(string id)
        {
            var result = await _mediator
                .Send(new SnackMachineQuery
                {
                    Id = id
                });
            return Ok(result);
        }

        [Route("initialize")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InitializeSnakMachine(
            [FromBody] InitializeSnakMachineDto model)
        {
            var command = _mapper.Map<InitializeSnakMachineCommand>(model);
            command.UserId = null;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("{id:length(24)}/addSlot")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddSlot(
            [FromRoute] string id,
            [FromBody] AddSnackMachineSlotDto model)
        {
            var command = _mapper.Map<AddSnackMachineSlotCommand>(model);
            command.UserId = null;
            command.SnackMachineId = id;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("{id:length(24)}/buySnack")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddSlot(
            [FromRoute] string id,
            [FromBody] BuySnackDto model)
        {
            var command = _mapper.Map<BuySnackCommand>(model);
            command.UserId = null;
            command.SnackMachineId = id;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("snackTypes")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SnakTypeReadModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSnackTypes()
        {
            var snackTypes = await _mediator
                .Send(new SnackTypesQuery());
            return Ok(snackTypes);
        }
    }
}
