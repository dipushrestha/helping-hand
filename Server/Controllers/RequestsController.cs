using helping_hand.Models;
using helping_hand.Server.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helping_hand.Server.Controllers
{
    [Route("api/requests")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RequestsController> _logger;

        public RequestsController(IUnitOfWork unitOfWork, ILogger<RequestsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var requests = await _unitOfWork.Requests.GetAll();
                return Ok(requests);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAll)} of the {nameof(RequestsController)}.");
                return StatusCode(500, "Internal server error. Please try again later!");
            }
        }

        [HttpGet("{id:int}", Name = "GetRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var request = await _unitOfWork.Requests.Get(q => q.Id == id);
                return Ok(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Get)} of the {nameof(RequestsController)}.");
                return StatusCode(500, "Internal server error. Please try again later!");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Request request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid create request body.");
                return BadRequest();
            }

            try
            {
                await _unitOfWork.Requests.Insert(request);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetRequest", new { request.Id }, request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Create)} of the {nameof(RequestsController)}.");
                return StatusCode(500, "Internal server error. Please try again later!");
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] Request request)
        {
            if (!ModelState.IsValid || request == null || id != request.Id)
            {
                _logger.LogError("Invalid update request body.");
                return BadRequest();
            }

            try
            {
                _unitOfWork.Requests.Update(request);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Update)} of the {nameof(RequestsController)}.");
                return StatusCode(500, "Internal server error. Please try again later!");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var request = await _unitOfWork.Requests.Get(q => id == q.Id);

                if (request is null)
                {
                    _logger.LogError($"Invalid DELETE attempt of request with id = {id}.");
                    return BadRequest();
                }

                await _unitOfWork.Requests.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Delete)} of the {nameof(RequestsController)}.");
                return StatusCode(500, "Internal server error. Please try again later!");
            }
        }
    }
}
