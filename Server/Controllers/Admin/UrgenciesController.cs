using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using helping_hand.Models;
using helping_hand.Data.IRepository;

namespace helping_hand.Server.Controllers.Admin
{
    [Route("api/admin/urgencies")]
    [ApiController]
    public class UrgenciesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UrgenciesController> _logger;

        public UrgenciesController(IUnitOfWork unitOfWork, ILogger<UrgenciesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] RequestParams requestParams)
        {
            var requests = await _unitOfWork.Urgencies.GetAll(requestParams);
            return Ok(requests);
        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetUrgency")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var request = await _unitOfWork.Urgencies.Get(q => q.Id == id);
            return Ok(request);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Urgency urgency)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid create request body.");
                return BadRequest();
            }

            await _unitOfWork.Urgencies.Insert(urgency);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetUrgency", new { urgency.Id }, urgency);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] Urgency urgency)
        {
            if (!ModelState.IsValid || urgency == null || id != urgency.Id)
            {
                _logger.LogError("Invalid update request body.");
                return BadRequest();
            }

            _unitOfWork.Urgencies.Update(urgency);
            await _unitOfWork.Save();

            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _unitOfWork.Urgencies.Get(q => id == q.Id);

            if (request is null)
            {
                _logger.LogError($"Invalid DELETE attempt of request with id = {id}.");
                return BadRequest();
            }

            await _unitOfWork.Urgencies.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
