using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Shop.WebApi.Infrastructure
{
    public sealed class ValidationResult<TData>
    {
        public ValidationResult(TData data, OperationResult errors)
        {
            Data = data;
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }


        public TData Data { get; }
        
        public OperationResult Errors { get; }
        
        public bool IsValid => Errors.IsSuccess;
        
        public bool HasError => Errors.HasError;
        
        public ValidationResult<TData> ValidateModelNotNull(string messageIfNull = null)
        {
            if (Data == null) Errors.AddError(messageIfNull ?? "Model is null");

            return this;
        }
        
        public ValidationResult<TData> ValidateModelAnnotations()
        {
            if (HasError)
                return this;
            
            var model = Data;

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
            var errors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, validationContext, errors, validateAllProperties: true);

            foreach (var error in errors)
                Errors.AddErrorForKey(error.MemberNames.First(), error.ErrorMessage);

            return this;
        }
        
        public ValidationResult<TData> AddError(string message,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            Errors.AddError(message, httpStatusCode);

            return this;
        }


        public ValidationResult<TData> AddErrorForKey(string key, string message,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            Errors.AddErrorForKey(key, message, httpStatusCode);

            return this;
        }
    }
}