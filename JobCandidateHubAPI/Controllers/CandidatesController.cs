using JobCandidateHubAPI.Models;
using JobCandidateHubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidateHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _service;

        public CandidatesController(ICandidateService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.AddOrUpdateCandidateAsync(candidateDto);
            return Ok();
        }
    }
}
