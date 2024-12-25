using MyApp.Application.DTOs.Expense;


namespace MyApp.Application.Responses
{
    public class GetUserExpensesByCategoryResponse
    {
        public List<ExpensesSummaryByCategoryDto> Data { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
