using offerStation.Core.Wrappers;

namespace offerStation.Core.Wrappers
{
    public static class ToPagedResponseHelper
    {
        public static PagedResponse<T> ToPagedResponse<T>(this IEnumerable<T> queryable, PagingParameters pagingParameters)
        {
            var count = queryable.Count();
            var items = queryable.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize).ToList();
            var previousPageLink = pagingParameters.PageNumber > 1 ? CreatePagingLink(pagingParameters.PageNumber - 1, pagingParameters.PageSize) : null;
            var nextPageLink = pagingParameters.PageNumber < (count / (double)pagingParameters.PageSize) ? CreatePagingLink(pagingParameters.PageNumber + 1, pagingParameters.PageSize) : null;

            return new PagedResponse<T>(items, pagingParameters.PageNumber, pagingParameters.PageSize, count)
            {
                FirstPage = CreatePagingLink(1, pagingParameters.PageSize),
                LastPage = CreatePagingLink((int)Math.Ceiling(count / (double)pagingParameters.PageSize), pagingParameters.PageSize),
                NextPage = nextPageLink,
                PreviousPage = previousPageLink
            };
        }
        private static Uri CreatePagingLink(int pageNumber, int pageSize)
        {
            return new Uri(string.Format("?pageNumber={0}&pageSize={1}", pageNumber, pageSize), UriKind.Relative);
        }
    }
}