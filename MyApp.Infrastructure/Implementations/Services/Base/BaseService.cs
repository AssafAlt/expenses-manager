using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces.Services.Base;
using MyApp.Application.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Implementations.Services.Base
{
    public class BaseService:IBaseService
    {
        public async Task<ServiceResponse<object>> HandleServiceOperationAsync<T>(Func<Task<ServiceResponse<object>>> operation)
        {
            try
            {
                return await operation();
            }
            catch (DbUpdateException dbEx)
            {
                var sqlException = dbEx.InnerException as SqlException;
                var dbErrorMessage = sqlException?.Message ?? "A database error occurred.";
                return new ServiceResponse<object>(
                    StatusCodes.Status500InternalServerError,
                    dbErrorMessage
                );
            }
            catch (Exception ex)
            {
                return new ServiceResponse<object>(
                    StatusCodes.Status500InternalServerError,
                    ex.Message
                );
            }
        }

        
    }

}
