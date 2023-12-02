using BaseRestClient;
using System.Net.Http.Json;
using TvMazeClient.Models;

namespace TvMazeClient;

public class TvMazeApiClient : BaseHttpClient
{
    protected override int AuthRetryCount { get; set; } = 0;

    private static readonly Uri DefaultTvMazeUri = new Uri("https://api.tvmaze.com");
    public TvMazeApiClient(IHttpClientFactory IHttpClientFactory) : base(DefaultTvMazeUri, IHttpClientFactory)
    {
    }

    public TvMazeApiClient(HttpClient HttpClient) : base(DefaultTvMazeUri, HttpClient)
    {
    }


    protected override void SetupHttpClient(HttpClient httpClient)
    {

    }

    public async Task<Show> GetShow(int showId, CancellationToken cancellationToken = default)
    {
        const string Endpoint = "/shows";

        // Since on this endpoint the path changes we need an extra transformation
        var currentEndpoint = $"{Endpoint}/{showId}";

        var config = new RequestConfig<object>
        {
            Endpoint = currentEndpoint,
            QueryParams = null,
            Context = null,
            MessageBuilder = (object context) =>
            {
                var message = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                };

                return message;
            },
        };

        var result = await SendAsync<Show, object>(config, cancellationToken);

        return result!;
    }
}