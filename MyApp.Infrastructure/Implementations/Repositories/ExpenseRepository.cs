using Microsoft.EntityFrameworkCore;
using MyApp.Application.DTOs.Expense;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Application.Responses;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Data;

namespace MyApp.Infrastructure.Implementations.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;
        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseByIdAsync(Guid expenseId)
        {
            var expenseToRemove = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == expenseId);
            if (expenseToRemove != null)
            {
                _context.Expenses.Remove(expenseToRemove);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<GetUserExpensesByCategoryResponse> GetUserExpensesByCategoriesAsync(Guid userId, DateOnly? startDate, DateOnly? endDate)
        {
            var expensesQuery = _context.Expenses.AsQueryable();
            expensesQuery = expensesQuery.Where(e => e.AppUserId == userId);

            if (startDate.HasValue) expensesQuery = expensesQuery.Where(e => e.Date >= startDate.Value);

            if (endDate.HasValue) expensesQuery = expensesQuery.Where(e => e.Date <= endDate.Value);

            var groupedExpenses = await expensesQuery
                .GroupBy(e => e.ExpenseCategory)
                .Select(g => new ExpensesSummaryByCategoryDto
                {
                    CategoryName = g.Key.ToString(),
                    TotalCategoryPrice = g.Sum(e => e.Price),
                    Expenses = g.Select(e => new ExpenseByCategoryDto
                    {
                        Id = e.Id,
                        ExpenseName = e.ExpenseName,
                        Date = e.Date,
                        Price = e.Price
                    }).ToList()
                })
                .ToListAsync();
            
            var grandTotalPrice = groupedExpenses.Sum(g => g.TotalCategoryPrice);

            return new GetUserExpensesByCategoryResponse
            {
                Data = groupedExpenses,
                TotalPrice = grandTotalPrice
            };
        }

        public async Task<GetUserExpensesByDateResponse> GetUserExpensesByDatesAsync(Guid userId, DateOnly? startDate, DateOnly? endDate)
        {
            var expensesQuery = _context.Expenses.AsQueryable();
            expensesQuery = expensesQuery.Where(e => e.AppUserId == userId);

            if (startDate.HasValue) expensesQuery = expensesQuery.Where(e => e.Date >= startDate.Value);

            if (endDate.HasValue) expensesQuery = expensesQuery.Where(e => e.Date <= endDate.Value);

            
            var groupedExpenses = await expensesQuery
                .GroupBy(e => e.Date)
                .Select(g => new ExpensesSummaryByDateDto
                {
                    Date = g.Key,  
                    TotalDatePrice = g.Sum(e => e.Price),
                    Expenses = g.Select(e => new ExpenseByDateDto
                    {
                        Id = e.Id,
                        ExpenseName = e.ExpenseName,
                        ExpenseCategory = e.ExpenseCategory.ToString(), 
                        Price = e.Price
                    }).ToList()
                })
                .ToListAsync();

            
            var grandTotalPrice = groupedExpenses.Sum(g => g.TotalDatePrice);

            return new GetUserExpensesByDateResponse
            {
                Data = groupedExpenses,
                TotalPrice = grandTotalPrice
            };
        }
    }
}
