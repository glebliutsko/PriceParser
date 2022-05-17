namespace PriceParser.Http;

public class HttpLoadException : Exception
{
    public HttpLoadException(string msg, HttpResponseMessage response) : base(msg)
    {
        Response = response;
    }

    public HttpResponseMessage Response { get; }
}