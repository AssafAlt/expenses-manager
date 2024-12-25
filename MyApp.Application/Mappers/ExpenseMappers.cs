using MyApp.Application.DTOs.Auth;
using MyApp.Application.DTOs.Expense;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers
{
    public static class ExpenseMappers
    {
       
        public static Expense ToExpenseFromCreateDto(this CreateExpenseDto createExpenseDto, Guid AppUserId)
        {
            return new Expense
            {
                ExpenseName = createExpenseDto.ExpenseName,
                Price = createExpenseDto.Price,
                ExpenseCategory=createExpenseDto.ExpenseCategory,
                Date = createExpenseDto.Date,
                AppUserId = AppUserId
            };
        }
    }
}
