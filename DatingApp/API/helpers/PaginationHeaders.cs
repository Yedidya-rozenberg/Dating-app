namespace API.helpers
{
    public class PaginationHeaders
    {
        public PaginationHeaders(int currentPage, int itemPerPage, int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemPerPage = itemPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public int CurrentPage { get; }
        public int ItemPerPage { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }
    }
}