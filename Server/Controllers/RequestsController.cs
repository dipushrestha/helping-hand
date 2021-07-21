using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using helping_hand.Models;
using helping_hand.Data.IRepository;
using helping_hand.Server.Configurations;

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
        public async Task<IActionResult> GetAll([FromQuery] RequestParams requestParams)
        {
            var requests = await _unitOfWork.Requests.GetAll(requestParams);
            return Ok(requests);
        }

        [HttpGet("{id:int}", Name = "GetRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var request = await _unitOfWork.Requests.Get(q => q.Id == id);

            if (request is null)
            {
                return NotFound();
            }

            return Ok(request);
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

            await _unitOfWork.Requests.Insert(request);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetRequest", new { request.Id }, request);
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

            _unitOfWork.Requests.Update(request);
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
    }
}
