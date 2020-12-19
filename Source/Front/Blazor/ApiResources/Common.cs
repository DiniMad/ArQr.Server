using System.Collections.Generic;

namespace Blazor.ApiResources
{
    public sealed record ApiResponse<T>(int Status, bool Success, T? Data, object? Error, bool DetailedError)
        where T : class;

    public sealed record PaginationInputResource(int PageSize = 5, int PageNumber = 1);

    public sealed record PaginationResultResource<TData> (IEnumerable<TData> Collection,
                                                          int                TotalCount,
                                                          string?            Next,
                                                          string?            Previous);
}