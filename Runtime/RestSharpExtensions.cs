using System;
using System.Net;
using System.Text;

namespace RestSharp
{
    public static class RestSharpExtensions
    {
        /// <summary>
        ///     Returns the error string, if the response was not successful.
        /// </summary>
        public static string GetError(this IRestResponse response, string endPoint = "")
        {
            if (response.IsSuccessful())
                return string.Empty;

            var sb = new StringBuilder();

            var uri = string.IsNullOrEmpty(endPoint) ? response.ResponseUri.ToString() : endPoint;

            sb.AppendLine($"REST request {uri} failed with the following errors:");


            if (response.StatusCode != 0 && response.StatusCode.IsSuccess() == false)
            {
                sb.AppendLine($"Server responded with status code {response.StatusCode}");
                sb.AppendLine($"Description: {response.StatusDescription}");
                sb.AppendLine($"Content: {response.Content}");
            }

            if (response.ErrorException != null)
                sb.AppendLine("Exception: " + response.ErrorMessage);

            return sb.ToString();
        }

        /// <summary>
        ///     Returns a <see cref="RestSharpException" /> exception if the response was not successful
        /// </summary>
        public static Exception GetException(this IRestResponse response)
        {
            return response.IsSuccessful()
                ? null
                : new RestSharpException(response.StatusCode, response.ResponseUri, response.Content,
                    response.GetError(), response.ErrorException);
        }

        /// <summary>
        ///     Returns if the Status Code implies success
        /// </summary>
        /// <param name="responseCode"></param>
        /// <returns></returns>
        public static bool IsSuccess(this HttpStatusCode responseCode)
        {
            var numericResponse = (int)responseCode;
            const int statusCodeOk = (int)HttpStatusCode.OK;
            const int statusCodeBadRequest = (int)HttpStatusCode.BadRequest;

            return numericResponse >= statusCodeOk && numericResponse < statusCodeBadRequest;
        }

        /// <summary>
        ///     Returns if the response was successful.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool IsSuccessful(this IRestResponse response)
        {
            return response.StatusCode.IsSuccess() && response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}