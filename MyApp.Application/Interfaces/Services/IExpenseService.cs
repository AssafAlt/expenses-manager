
using MyApp.Application.DTOs.Expense;
using MyApp.Application.Responses.Base;


namespace MyApp.Application.Interfaces.Services
{
    public interface IExpenseService
    {
        public Task<ServiceResponse<object>> CreateAsync(CreateExpenseDto createExpenseDto,string token);
        public Task<ServiceResponse<object>> DeleteExpenseByIdAsync(Guid expenseId);
        public Task<ServiceResponse<object>> GetUserExpenseReportByCategoryAsync(string token, DateOnly? startDate, DateOnly? endDate);
        public Task<ServiceResponse<object>> GetUserExpenseReportByDateAsync(string token, DateOnly? startDate, DateOnly? endDate);

    }
}
