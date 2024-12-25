using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Expense
{
    public class ExpensesSummaryByDateDto
    {
        public DateOnly Date { get; set; }
        public decimal TotalDatePrice { get; set; }
        public List<ExpenseByDateDto> Expenses { get; set; }
    }
}
