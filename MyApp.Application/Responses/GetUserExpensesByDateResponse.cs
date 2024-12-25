using MyApp.Application.DTOs.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Responses
{
    public class GetUserExpensesByDateResponse
    {
            public List<ExpensesSummaryByDateDto> Data { get; set; }
            public decimal TotalPrice { get; set; }
        
    }
}
