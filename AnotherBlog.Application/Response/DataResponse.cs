using System.Net;

namespace AnotherBlog.Application.Response
{
    public class DataResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public DataResponse() { }

        public DataResponse(T data, HttpStatusCode statusCode = HttpStatusCode.OK, string errorMsg = null) : base(statusCode, errorMsg)
        {
            Data = data;
        }

        public static DataResponse<T> Success(T data, string msg = null)
        {
            return new DataResponse<T>
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
                ErrorMsg = msg
            };
        }

        public static DataResponse<T> NotFond(string msg = null)
        {
            return new DataResponse<T>
            {
                Data = default(T),
                StatusCode = HttpStatusCode.NotFound,
                ErrorMsg = msg
            };
        }
    }
}
