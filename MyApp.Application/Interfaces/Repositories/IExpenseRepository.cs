using MyApp.Application.Responses;
using MyApp.Domain.Entities;


namespace MyApp.Application.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        Task CreateExpenseAsync(Expense expense);
        Task DeleteExpenseByIdAsync(Guid expenseId);
        Task<GetUserExpensesByCategoryResponse> GetUserExpensesByCategoriesAsync(Guid userId, DateOnly? startDate, DateOnly? endDate);
        Task<GetUserExpensesByDateResponse> GetUserExpensesByDatesAsync(Guid userId, DateOnly? startDate, DateOnly? endDate);
    }
}
