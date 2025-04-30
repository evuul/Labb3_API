using Labb3_API.Models.DTO;
using Labb3_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Labb3_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterestController : ControllerBase
{
    private readonly IInterestService _interestService;

    public InterestController(IInterestService interestService)
    {
        _interestService = interestService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InterestDTO>>> GetAllInterests()
    {
        var interests = await _interestService.GetAllInterestsAsync();
        return Ok(interests);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInterest([FromBody] CreateInterestDTO dto)
    {
        var interest = await _interestService.CreateInterestAsync(dto);
        return Created($"/api/interest/{interest.Id}", interest);
    }
}