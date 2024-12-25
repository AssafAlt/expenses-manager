using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.Auth;
using MyApp.Application.DTOs.Expense;
using MyApp.Application.Interfaces.Services;

namespace MyApp.Api.Controllers
{
    [Authorize]
    [Route("api/expenses")]
    [ApiController]
    public class ExpenseController: ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExpenseController(IExpenseService expenseService, IHttpContextAccessor httpContextAccessor)
        {
            _expenseService = expenseService;
            _httpContextAccessor = httpContextAccessor;
         
        }

        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateExpenseDto createExpenseDto)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwt_token"];
            var results = await _expenseService.CreateAsync(createExpenseDto,token);

            return StatusCode(results.StatusCode, results.Data);
        }

        [HttpGet("category-report")]
        public async Task<IActionResult> GetExpenseReportByCategory([FromQuery] DateOnly? startDate,[FromQuery] DateOnly? endDate)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwt_token"];
            var results = await _expenseService.GetUserExpenseReportByCategoryAsync(token,startDate,endDate);

            return StatusCode(results.StatusCode,results.Data);

        }
        [HttpGet("dates-report")]
        public async Task<IActionResult> GetExpenseReportByDate([FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwt_token"];
            var results = await _expenseService.GetUserExpenseReportByDateAsync(token, startDate, endDate);

            return StatusCode(results.StatusCode, results.Data);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var results = await _expenseService.DeleteExpenseByIdAsync(id);
            return StatusCode(results.StatusCode, results.Data);

        }
    }
}
