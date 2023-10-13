#pragma warning disable CS8618 

using WDA.ApiDotNet.Application.Helpers;

namespace WDA.ApiDotNet.Business.Helpers
{
    public class PaginationResponse<T>
    {
        public PaginationHeader<T> Header { get; set; }
        public CustomHeaders<T> CustomHeader { get; set; }
        public List<T> Data { get; set; }
    }
}
