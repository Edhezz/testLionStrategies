using LionStrategiesTest.DTOs.Operations;
using LionStrategiesTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace LionStrategiesTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationsController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationDto>>> GetAll()
        {
            var userEmail = Request.Headers["email"].ToString();
            var userRole = HttpContext.Items["UserRole"]?.ToString();

            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userRole))
            {
                return Unauthorized();
            }

            var operations = await _operationService.GetAllAsync(userEmail, userRole);
            return Ok(operations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDto>> GetById(Guid id)
        {
            var operation = await _operationService.GetByIdAsync(id);
            if (operation == null)
            {
                return NotFound(); 
            }
            return Ok(operation);
        }

        [HttpGet("sales")]
        public async Task<ActionResult<IEnumerable<OperationDto>>> GetSales()
        {
            var sales = await _operationService.GetSalesAsync();
            return Ok(sales);
        }

        [HttpGet("purchases")]
        public async Task<ActionResult<IEnumerable<OperationDto>>> GetPurchases()
        {
            var purchases = await _operationService.GetPurchasesAsync();
            return Ok(purchases);
        }

        [HttpPost]
        public async Task<ActionResult<OperationDto>> Create(CreateOperationDto createDto)
        {
            // La validación del DTO (ej. [Required]) se hace automáticamente gracias a [ApiController]
            var newOperation = await _operationService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = newOperation.Id }, newOperation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateOperationDto updateDto)
        {
            var userRole = HttpContext.Items["UserRole"]?.ToString();
            if (userRole != "admin")
            {
                return  StatusCode(403);
            }

            var success = await _operationService.UpdateAsync(id, updateDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userRole = HttpContext.Items["UserRole"]?.ToString();
            if (userRole != "admin")
            {
                return  StatusCode(403); 
            }

            var success = await _operationService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}