using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Resource.Api.Resources
{
    public sealed record ApiResponse<T>(int Status, bool Success, T? Data, object? Error) where T : class
    {
        public bool DetailedError => Error is null or not string;

        public static ApiResponse<T> Parse(ObjectResult objectResult)
        {
            var status  = objectResult.StatusCode!.Value;
            var success = status < 400;
            var data    = success is true ? objectResult.Value as T : null;
            var error   = success is false ? objectResult.Value : null;
            return new(status, success, data, error);
        }
    }

    public sealed record PaginationInputResource(int PageSize = 5, int PageNumber = 1)
    {
        public int After => (PageNumber - 1) * PageSize;
    }

    public sealed record PaginationResultResource<TData>
    {
        public IEnumerable<TData> Collection { get; }
        public int                TotalCount { get; }
        public string?            Next       { get; }
        public string?            Previous   { get; }

        private readonly string                  _baseUrl;
        private readonly PaginationInputResource _paginationInput;

        public PaginationResultResource(IEnumerable<TData>      collection,
                                        int                     totalCount,
                                        string                  baseUrl,
                                        PaginationInputResource paginationInput)
        {
            Collection       = collection;
            TotalCount       = totalCount;
            _baseUrl         = baseUrl;
            _paginationInput = paginationInput;

            var hasNext = paginationInput.After + paginationInput.PageSize < totalCount;
            Next = hasNext ? CreateNavigationLink(1) : null;

            var hasPrevious = paginationInput.After > 0;
            Previous = hasPrevious ? CreateNavigationLink(-1) : null;
        }

        private string CreateNavigationLink(sbyte amount)
        {
            return
                $"{_baseUrl}?{nameof(_paginationInput.PageNumber)}={_paginationInput.PageNumber + amount}&{nameof(_paginationInput.PageSize)}={_paginationInput.PageSize}";
        }
    }
}