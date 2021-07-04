using helping_hand.Data.IRepository;
using helping_hand.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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


            return Created($"/{helpRequest.Id}", helpRequest);
            //return CreatedAtRoute("GetRequest", new { helpRequest.Id }, helpRequest);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
