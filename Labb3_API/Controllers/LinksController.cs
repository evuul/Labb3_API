using Labb3_API.Models.DTO;
using Labb3_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Labb3_API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LinksController : ControllerBase
{
    private readonly ILinksService _linksService;
    
    public LinksController(ILinksService linksService)
    {
        _linksService = linksService;
    }
    
    // GET: api/links
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LinkDTO>>> GetAllLinks()
    {
        var links = await _linksService.GetAllLinksAsync();
        return Ok(links);
    }

    // POST: api/links
    [HttpPost]
    public async Task<ActionResult<LinkDTO>> CreateLink([FromBody] CreateLinkDTO dto)
    {
        var created = await _linksService.CreateLinkAsync(dto);
        return Created($"/api/links/{created.Id}", created);
    }
    
    // DELETE: api/links/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLink(int id)
    {
        var deleted = await _linksService.DeleteLinkAsync(id);
        return deleted ? NoContent() : NotFound();
    }
    
    
}