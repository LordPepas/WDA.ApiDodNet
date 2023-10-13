namespace WDA.ApiDotNet.Application.Helpers
{
    public class CustomHeaders<T>
    {
        public CustomHeaders(string? orderBy, string? searchValue)
        {
            OrderBy = orderBy;
            SearchValue = searchValue;
        }

        public string? OrderBy { get; set; }
        public string? SearchValue { get; set; }
    }
}