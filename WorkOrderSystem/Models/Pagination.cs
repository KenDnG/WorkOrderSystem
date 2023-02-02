namespace WorkOrderSystem.Models
{
    public class Pagination<T>
    {
        public string? SearchTerm { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int? RecordCount { get; set; }
        public List<T> Model { get; set; }
    }
}
