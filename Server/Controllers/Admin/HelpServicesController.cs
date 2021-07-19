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
    [Route("api/admin/help-services")]
    [ApiController]
    public class HelpServicesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HelpServicesController> _logger;

        public HelpServicesController(IUnitOfWork unitOfWork, ILogger<HelpServicesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] RequestParams requestParams)
        {
            var requests = await _unitOfWork.HelpServices.GetAll(requestParams);
            return Ok(requests);
        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetHelpService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var request = await _unitOfWork.HelpServices.Get(q => q.Id == id);
            return Ok(request);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] HelpService helpService)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid create request body.");
                return BadRequest();
            }

            await _unitOfWork.HelpServices.Insert(helpService);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetHelpService", new { helpService.Id }, helpService);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] HelpService helpService)
        {
            if (!ModelState.IsValid || helpService == null || id != helpService.Id)
            {
                _logger.LogError("Invalid update request body.");
                return BadRequest();
            }

            _unitOfWork.HelpServices.Update(helpService);
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
            var request = await _unitOfWork.HelpServices.Get(q => id == q.Id);

            if (request is null)
            {
                _logger.LogError($"Invalid DELETE attempt of request with id = {id}.");
                return BadRequest();
            }

            await _unitOfWork.HelpServices.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
