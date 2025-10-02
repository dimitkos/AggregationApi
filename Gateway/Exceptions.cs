using System;
using System.Net;

namespace Gateway
{
    [Serializable]
    public class GatewayException : Exception
    {
        public GatewayException() { }
        public GatewayException(string message) : base(message) { }
        public GatewayException(string message, Exception inner) : base(message, inner) { }
        protected GatewayException(
          global::System.Runtime.Serialization.SerializationInfo info,
          global::System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class ApiClientException : GatewayException
    {
        public HttpStatusCode StatusCode { get; }
        public object? Request { get; }
        public string? RequestBody { get; }
        public string Response { get; }

        public ApiClientException(HttpStatusCode statusCode, string response)
            : base($"statusCode: {statusCode}, response: {response}")
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}
