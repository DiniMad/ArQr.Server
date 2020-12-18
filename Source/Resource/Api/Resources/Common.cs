using System.Collections.Generic;

namespace Resource.Api.Resources
{
    public sealed record ApiResponse<T>(int Status, bool Success, T? Data, object? Error) where T : class
    {
        public bool DetailedError => Error is null or not string;
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

    internal enum ProductType : byte
    {
        QrCode,
        MediaContent
    }
}