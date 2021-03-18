using System;
using System.Net;

namespace AnotherBlog.Application.Response
{
    public class BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public String ErrorMsg { get; set; }

        public BaseResponse(HttpStatusCode statusCode =  HttpStatusCode.OK, string errorMsg = null)
        {
            StatusCode = statusCode;
            ErrorMsg = errorMsg;
        }

        public static BaseResponse Error(string errorMsg)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMsg = errorMsg
            };
        }

        public static BaseResponse BadRequest(string errorMsg)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMsg = errorMsg
            };
        }

        public static BaseResponse Success()
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
