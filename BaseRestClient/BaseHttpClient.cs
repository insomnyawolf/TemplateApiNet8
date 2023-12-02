using System.Net.Http.Json;
using System.Net;
using System.Text;

namespace BaseRestClient;

public abstract class BaseHttpClient
{
    private readonly Uri BaseUrl;
    private readonly HttpClient HttpClient;
    private readonly IHttpClientFactory IHttpClientFactory;

    protected abstract int AuthRetryCount { get; set; }

    public BaseHttpClient(Uri BaseUrl, IHttpClientFactory IHttpClientFactory) : this(BaseUrl)
    {
        this.IHttpClientFactory = IHttpClientFactory;
    }

    public BaseHttpClient(Uri BaseUrl, HttpClient HttpClient) : this(BaseUrl)
    {
        this.HttpClient = HttpClient;
    }

    private BaseHttpClient(Uri BaseUrl)
    {
        this.BaseUrl = BaseUrl;
    }

    protected abstract void SetupHttpClient(HttpClient httpClient);

    protected async Task<TResponse?> SendAsync<TResponse, TContext>(RequestConfig<TContext> requestConfiguration, CancellationToken cancellationToken)
    {
        var response = await SendAsync(requestConfiguration, cancellationToken);

        var result = await response.ReadFromJsonAsync<TResponse>(cancellationToken);

        return result;
    }

    protected async Task<HttpContent> SendAsync<TContext>(RequestConfig<TContext> requestConfiguration, CancellationToken cancellationToken)
    {
        var localRetryCount = AuthRetryCount;

        var client = GetHttpClient();

    retry:

        SetupHttpClient(client);

        var message = requestConfiguration.MessageBuilder(requestConfiguration.Context);

        message.RequestUri ??= GetUri(requestConfiguration.Endpoint, requestConfiguration.QueryParams);

        var response = await client.SendAsync(message, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (localRetryCount > 0)
            {
                localRetryCount--;
                SetupHttpClient(client);
                goto retry;
            }
        }

        response.EnsureSuccessStatusCode();

        return response.Content;
    }

    private Uri GetUri(string endpoint, List<KeyValuePair<string, dynamic>>? param = null)
    {
        if (param is null)
        {
            return new Uri(BaseUrl, endpoint);
        }

        var query = new StringBuilder();

        query.Append(endpoint);

        foreach (var pair in param)
        {
            if (string.IsNullOrWhiteSpace(pair.Key))
            {
                continue;
            }

            if (query.Length > endpoint.Length)
            {
                query.Append("&");
            }

            query.Append(pair.Key);

            if (string.IsNullOrWhiteSpace(pair.Key))
            {
                continue;
            }

            query.Append('=');

            query.Append(pair.Value);
        }

        var relativeUri = query.ToString();

        var uri = new Uri(BaseUrl, relativeUri);

        return uri;
    }

    private HttpClient GetHttpClient()
    {
        if (HttpClient is not null)
        {
            return HttpClient;
        }
        return IHttpClientFactory.CreateClient();
    }
}