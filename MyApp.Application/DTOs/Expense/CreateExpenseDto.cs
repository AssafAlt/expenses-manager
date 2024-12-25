using MyApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace MyApp.Application.DTOs.Expense
{
    public class CreateExpenseDto
    {
        [Required(ErrorMessage = "Expense name is required.")]
        [StringLength(30, ErrorMessage = "Expense name cannot exceed 30 characters.")]
        public string ExpenseName { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between 0.01 and 1,000,000.00.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Expense category is required.")]
        public ExpenseCategory ExpenseCategory { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);


    }
}
