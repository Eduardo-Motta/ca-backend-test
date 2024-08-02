namespace Nexer.Finance.Shared.Utils
{
    public class PaginationParameters
    {
        public PaginationParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
    }
}
