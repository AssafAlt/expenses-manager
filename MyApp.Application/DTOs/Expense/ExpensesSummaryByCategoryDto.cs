using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Expense
{
    public class ExpensesSummaryByCategoryDto
    {
        public string CategoryName { get; set; }
        public decimal TotalCategoryPrice { get; set; }
        public List<ExpenseByCategoryDto> Expenses { get; set; }
    }
}
