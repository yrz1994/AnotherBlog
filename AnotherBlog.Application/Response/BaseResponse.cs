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
    }
}
