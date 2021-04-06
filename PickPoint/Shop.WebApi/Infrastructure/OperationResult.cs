using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Shop.WebApi.Infrastructure
{ 
    public class OperationResult<TData> : OperationResult where TData : class
    {
        public TData Data { get; private set; }

        public OperationResult<TData> SetData(TData data)
        {
            Data = data;

            return this;
        }

        public new OperationResult<TData> MergeWith(OperationResult otherResult)
        {
            base.MergeWith(otherResult);

            return this;
        }
    }

    public class OperationResult
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> Errors => _errors;

        public bool IsSuccess => StatusCode == HttpStatusCode.OK;

        public bool HasError => !IsSuccess;

        public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.OK;
        
        public OperationResult AddError(string message,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            SetError(string.Empty, message, httpStatusCode);

            return this;
        }


        public OperationResult AddErrorForKey(string key, string message,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            SetError(key, message, httpStatusCode);

            return this;
        }
        
        public OperationResult MergeWith(OperationResult otherResult)
        {
            if (otherResult == null)
                throw new ArgumentNullException(nameof(otherResult));

            if (otherResult.HasError)
            {
                foreach (var errorGroup in otherResult.Errors)
                {
                    List<string> messages;

                    if (_errors.TryGetValue(errorGroup.Key, out messages))
                    {
                        messages.AddRange(errorGroup.Value);
                    }
                    else
                    {
                        _errors[errorGroup.Key] = errorGroup.Value.ToList();
                    }
                }

                StatusCode = otherResult.StatusCode;
            }

            return this;
        }
        
        private OperationResult SetError(string key, string error, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (error == null)
                throw new ArgumentNullException(nameof(error));

            List<string> messages;

            if (_errors.TryGetValue(key, out messages))
                messages.Add(error);
            else
                _errors[key] = new List<string> {error};

            StatusCode = httpStatusCode;

            return this;
        }
    }
}