using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.WebApi.Infrastructure.Extensions
{
    internal static class ValidationResultExt
    {
        public static ValidationResult<TData> WithValidationContext<TData>(this TData data) where TData : class
        {
            var parentResult = new OperationResult<TData>().SetData(data);

            return new ValidationResult<TData>(data, parentResult);
        }
        
        public static OperationResult MergeWith(this OperationResult data, OperationResult otherResult)
        {
            if (otherResult == null)
                throw new ArgumentNullException(nameof(otherResult));

            if (otherResult.HasError)
            {
                foreach (var errorGroup in otherResult.Errors)
                {
                    List<string> messages;

                    if (data.Errors.TryGetValue(errorGroup.Key, out messages))
                    {
                        messages.AddRange(errorGroup.Value);
                    }
                    else
                    {
                        data.Errors[errorGroup.Key] = errorGroup.Value.ToList();
                    }
                }
            }

            return data;
        }
    }
}