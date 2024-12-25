using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Expense;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Interfaces.Services.Base;
using MyApp.Application.Mappers;
using MyApp.Application.Responses.Base;

namespace MyApp.Infrastructure.Implementations.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBaseService _baseService;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<ExpenseService> _logger;

        public ExpenseService(IUserRepository userRepository, IBaseService baseService, IExpenseRepository expenseRepository, ITokenService tokenService,ILogger<ExpenseService> logger)
        {
            _userRepository = userRepository;
            _baseService = baseService;
            _expenseRepository = expenseRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<ServiceResponse<object>> CreateAsync(CreateExpenseDto createExpenseDto, string token)
        {
            return await _baseService.HandleServiceOperationAsync<object>(async () =>
            {
              
                var user = await _userRepository.GetUserByEmailAsync(_tokenService.GetEmailFromClaims(token));
            
                var newExpense = createExpenseDto.ToExpenseFromCreateDto(user.Id);

                await _expenseRepository.CreateExpenseAsync(newExpense);

                return new ServiceResponse<object>(StatusCodes.Status201Created, "Expense was created!");

            });
        }

        public async Task<ServiceResponse<object>> DeleteExpenseByIdAsync(Guid expenseId)
        {
            return await _baseService.HandleServiceOperationAsync<object>(async () =>
            {

                await _expenseRepository.DeleteExpenseByIdAsync(expenseId);

                return new ServiceResponse<object>(StatusCodes.Status204NoContent, "Expense was deleted!");

            });
        }

        public async Task<ServiceResponse<object>> GetUserExpenseReportByCategoryAsync(string token, DateOnly? startDate, DateOnly? endDate)
        {
            return await _baseService.HandleServiceOperationAsync<object>(async () =>
            {

                var user = await _userRepository.GetUserByEmailAsync(_tokenService.GetEmailFromClaims(token));

                var newReport = await _expenseRepository.GetUserExpensesByCategoriesAsync(user.Id, startDate, endDate); 

                
                return new ServiceResponse<object>(StatusCodes.Status200OK, newReport);

            });
        }

        public async Task<ServiceResponse<object>> GetUserExpenseReportByDateAsync(string token, DateOnly? startDate, DateOnly? endDate)
        {
            return await _baseService.HandleServiceOperationAsync<object>(async () =>
            {

                var user = await _userRepository.GetUserByEmailAsync(_tokenService.GetEmailFromClaims(token));

                var newReport = await _expenseRepository.GetUserExpensesByDatesAsync(user.Id, startDate, endDate);


                return new ServiceResponse<object>(StatusCodes.Status200OK, newReport);

            });
        }
    }
}
