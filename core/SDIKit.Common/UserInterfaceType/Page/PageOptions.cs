namespace SDIKit.Common.UserInterfaceType.Page
{
    public class PageOptions : IPageOptions
    {
        public PageOptions()
        {
            RowCount = 15;
            SortDirection = SortDirection.ASC;
        }

        public int PageNumber { get; set; }
        public int RowCount { get; set; }
        public string SortColumn { get; set; }
        public SortDirection SortDirection { get; set; }
    }

    public interface IPageOptions
    {
        int PageNumber { get; set; }
        int RowCount { get; set; }
        string SortColumn { get; set; }
        SortDirection SortDirection { get; set; }
    }

    public enum SortDirection
    {
        ASC = 0,
        DESC = 1
    }

    public abstract class PageOptionBase : IPageOptions
    {
        public PageOptionBase(IPageOptions options)
        {
            PageNumber = options.PageNumber;
            RowCount = options.RowCount;
            SortColumn = options.SortColumn;
            SortDirection = options.SortDirection;
        }

        public int PageNumber { get; set; }
        public int RowCount { get; set; }
        public string SortColumn { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}