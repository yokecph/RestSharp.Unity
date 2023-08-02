using System;
using System.Net;

namespace RestSharp
{
    /// <summary>
    ///     Represents a RestSharp exception with status code, URI and
    /// </summary>
    public class RestSharpException : Exception
    {
        public RestSharpException(HttpStatusCode httpStatusCode, Uri requestUri, string content, string error,
                                  Exception      innerException)
            : base(error, innerException)
        {
            HttpStatusCode = httpStatusCode;
            Uri = requestUri;
            Error = content;
        }


        public string         Error          { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }
        public Uri            Uri            { get; private set; }
    }
}