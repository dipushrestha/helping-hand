using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using helping_hand.Models;
using helping_hand.Data.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace helping_hand.Server.Controllers
{
    [Route("api/help-requests")]
    [ApiController]
    public class HelpRequestsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HelpRequestsController> _logger;

        public HelpRequestsController(IUnitOfWork unitOfWork, ILogger<HelpRequestsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] RequestParams requestParams)
        {
            var requests = await _unitOfWork.HelpRequests.GetAll(requestParams);
            return Ok(requests);
        }

        [HttpGet("{id:int}", Name = "GetHelpRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var request = await _unitOfWork.HelpRequests.Get(q => q.Id == id);
            return Ok(request);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] HelpRequest helpRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid create request body.");
                return BadRequest();
            }

            await _unitOfWork.HelpRequests.Insert(helpRequest);
            await _unitOfWork.Save();


            return CreatedAtRoute("GetHelpRequest", new { helpRequest.Id }, helpRequest);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] HelpRequest helpRequest)
        {
            if (!ModelState.IsValid || helpRequest == null || id != helpRequest.Id)
            {
                _logger.LogError("Invalid update request body.");
                return BadRequest();
            }

            _unitOfWork.HelpRequests.Update(helpRequest);
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
            var request = await _unitOfWork.HelpRequests.Get(q => id == q.Id);

            if (request is null)
            {
                _logger.LogError($"Invalid DELETE attempt of request with id = {id}.");
                return BadRequest();
            }

            await _unitOfWork.HelpRequests.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
