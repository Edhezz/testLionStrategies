using LionStrategiesTest.DTOs.Declarations;
using LionStrategiesTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace LionStrategiesTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class DeclarationsController : ControllerBase
    {
        private readonly IDeclarationService _declarationService;

        public DeclarationsController(IDeclarationService declarationService)
        {
            _declarationService = declarationService;
        }

        [HttpPost("{year:int}/{month:int}/generate")]
        public async Task<ActionResult<DeclarationDto>> Generate(int year, int month)
        {
            if (month < 1 || month > 12)
            {
                return BadRequest("Month must be between 1 and 12.");
            }

            var declaration = await _declarationService.GenerateDeclarationAsync(year, month);
            return Ok(declaration);
        }

        [HttpGet("{year:int}/{month:int}")]
        public async Task<ActionResult<DeclarationDto>> Get(int year, int month)
        {
            var declaration = await _declarationService.GetDeclarationAsync(year, month);
            if (declaration == null)
            {
                return NotFound();
            }
            return Ok(declaration);
        }

        [HttpGet("{year:int}/{month:int}/status")]
        public async Task<ActionResult<object>> GetStatus(int year, int month)
        {
            var status = await _declarationService.GetDeclarationStatusAsync(year, month);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(new { status = status });
        }

        [HttpPut("{year:int}/{month:int}/status/{status}")]
        public async Task<IActionResult> UpdateStatus(int year, int month, string status)
        {
            var userRole = HttpContext.Items["UserRole"]?.ToString();
            if (userRole != "admin")
            {
                return StatusCode(403); 
            }

            // Validación del estado
            var validStatuses = new[] { "pending", "submitted", "accepted" };
            if (!validStatuses.Contains(status.ToLower()))
            {
                return BadRequest("Invalid status provided.");
            }

            var success = await _declarationService.UpdateDeclarationStatusAsync(year, month, status);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{year:int}/{month:int}")]
        public async Task<IActionResult> Delete(int year, int month)
        {
            var userRole = HttpContext.Items["UserRole"]?.ToString();
            if (userRole != "admin")
            {
                return StatusCode(403); // Forbidden
            }

            var success = await _declarationService.DeleteDeclarationAsync(year, month);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}