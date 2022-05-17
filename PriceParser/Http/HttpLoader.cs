using System.Net;

namespace PriceParser.Sites.Http;

public class HttpLoader
{
    private readonly HttpClient _httpClient;
    private readonly HttpClientHandler _httpHandler;

    public HttpLoader()
    {
        _httpHandler = new HttpClientHandler();
        _httpHandler.CookieContainer = new CookieContainer();

        _httpClient = new HttpClient(_httpHandler);
    }

    public void AddCookie(Cookie cookie)
    {
        _httpHandler.CookieContainer.Add(cookie);
    }

    public async Task<string> Load(Uri url)
    {
        var response = await _httpClient.GetAsync(url);
        if (response.StatusCode != HttpStatusCode.OK)
            throw new HttpLoadException("Server response code != 200", response);

        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}