using MyApp.Application.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services.Base
{
    public interface IBaseService
    {
        Task<ServiceResponse<object>> HandleServiceOperationAsync<T>(Func<Task<ServiceResponse<object>>> operation);
    }
}
