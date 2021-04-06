using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.WebApi.Infrastructure;

namespace Shop.WebApi.Controllers
{
    public abstract class ShopControllerBase : ControllerBase
    {
        protected readonly IMapper Mapper;

        protected ShopControllerBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IActionResult OkOrErrorResult(OperationResult operationResult) =>
            operationResult.HasError ? GetErrorResult(operationResult) : Ok();

        protected IActionResult OkOrErrorResult<TData>(OperationResult<TData> operationResult) where TData : class =>
            operationResult.HasError
                ? GetErrorResult(operationResult) : (operationResult.Data == null ? Ok() : Ok(operationResult.Data));
        
        protected IActionResult GetErrorResult(OperationResult operationResult) => 
            StatusCode((int) operationResult.StatusCode, operationResult.Errors);
    }
}