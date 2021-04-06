using System.Net;
using AutoMapper;
using Shop.DataLayer.Repositories;
using Shop.WebApi.Infrastructure;
using Shop.WebApi.Infrastructure.Extensions;
using Shop.WebApi.L10n;
using Shop.DataLayer.Repositories.Abstracts;
using ApiVM = Shop.Api.Contract;
using DbVm = Shop.DataLayer.Models;

namespace Shop.WebApi.Models
{
    public class OrderManager
    { 
        private IOrderRepository _repository;
        private IPostamatRepository _postamatRepository;
        private IMapper _mapper;

        public OrderManager(IMapper mapper, IOrderRepository repository, IPostamatRepository postamatRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _postamatRepository = postamatRepository;
        }

        public OperationResult<ApiVM.Order> Create(ApiVM.OrderCreate apiOrderCreate)
        {
            var result = new OperationResult<ApiVM.Order>();

            var validationResult = ValidateCreate(apiOrderCreate);
            if (validationResult.HasError)
                return result.MergeWith(validationResult);

            var order = _mapper.Map<DbVm.Order>(apiOrderCreate);

            order = _repository.Create(order);

            var apiOrder = _mapper.Map<ApiVM.Order>(order);

            result.SetData(apiOrder);
            
            return result;
        }
        
        public OperationResult<ApiVM.Order> Update(int id, ApiVM.OrderUpdate apiOrderUpdate)
        {
            var result = new OperationResult<ApiVM.Order>();

            var order = _repository.FindById(id);

            if (order == null)
            {
                result.AddError(Errors.NotFound, HttpStatusCode.NotFound);
                return result;
            }

            order.Products = apiOrderUpdate.Products;
            order.Cost = apiOrderUpdate.Cost;
            order.PhoneNumber = apiOrderUpdate.PhoneNumber;
            order.RecipientFullName = apiOrderUpdate.RecipientFullName;

            _repository.Update(order);
            
            return result;
        }
        
        public OperationResult Cancel(int id)
        {
            var result = new OperationResult();

            var order = _repository.FindById(id);

            if (order == null)
            {
                result.AddError(Errors.NotFound, HttpStatusCode.NotFound);
                return result;
            }

            order.State = DbVm.StateOrder.Canceled;

            _repository.Update(order);
            
            return result;
        }
        
        public OperationResult<ApiVM.Order> FindById(int id)
        {
            var result = new OperationResult<ApiVM.Order>();

            var order = _repository.FindById(id);

            if (order == null)
            {
                result.AddError(Errors.NotFound, HttpStatusCode.NotFound);
                return result;
            }

            var apiOrder = _mapper.Map<ApiVM.Order>(order);

            result.SetData(apiOrder);
            
            return result;
        }

        private OperationResult ValidateCreate(ApiVM.OrderCreate apiOrderCreate)
        {
            var validationResult = apiOrderCreate.WithValidationContext().ValidateModelNotNull().ValidateModelAnnotations();
            if (validationResult.HasError)
                return validationResult.Errors;

            switch (_postamatRepository.IsExistActive(apiOrderCreate.PostamatId))
            {
                case PostamatResult.Active: break;
                case PostamatResult.NotActive:
                {
                    validationResult.AddErrorForKey(nameof(apiOrderCreate.PostamatId), Errors.Forbidden, HttpStatusCode.Forbidden);
                    break;
                }
                case PostamatResult.NotExists:
                {
                    validationResult.AddErrorForKey(nameof(apiOrderCreate.PostamatId), Errors.NotFound, HttpStatusCode.NotFound);
                    break;
                }
            }
            
            return validationResult.Errors;
        }
    }
}