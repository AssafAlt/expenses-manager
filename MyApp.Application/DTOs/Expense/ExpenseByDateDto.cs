using MyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Expense
{
    public class ExpenseByDateDto
    {
        public Guid Id { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseCategory { get; set; }
        public decimal Price { get; set; }
    }
}
