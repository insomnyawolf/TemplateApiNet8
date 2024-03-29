// <auto-generated/>
using JikanRest.Manga.Item.Characters;
using JikanRest.Manga.Item.External;
using JikanRest.Manga.Item.Forum;
using JikanRest.Manga.Item.Full;
using JikanRest.Manga.Item.Moreinfo;
using JikanRest.Manga.Item.News;
using JikanRest.Manga.Item.Pictures;
using JikanRest.Manga.Item.Recommendations;
using JikanRest.Manga.Item.Relations;
using JikanRest.Manga.Item.Reviews;
using JikanRest.Manga.Item.Statistics;
using JikanRest.Manga.Item.Userupdates;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace JikanRest.Manga.Item {
    /// <summary>
    /// Builds and executes requests for operations under \manga\{id}
    /// </summary>
    public class MangaItemRequestBuilder : BaseRequestBuilder {
        /// <summary>The characters property</summary>
        public CharactersRequestBuilder Characters { get =>
            new CharactersRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The external property</summary>
        public ExternalRequestBuilder External { get =>
            new ExternalRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The forum property</summary>
        public ForumRequestBuilder Forum { get =>
            new ForumRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The full property</summary>
        public FullRequestBuilder Full { get =>
            new FullRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The moreinfo property</summary>
        public MoreinfoRequestBuilder Moreinfo { get =>
            new MoreinfoRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The news property</summary>
        public NewsRequestBuilder News { get =>
            new NewsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The pictures property</summary>
        public PicturesRequestBuilder Pictures { get =>
            new PicturesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The recommendations property</summary>
        public RecommendationsRequestBuilder Recommendations { get =>
            new RecommendationsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The relations property</summary>
        public RelationsRequestBuilder Relations { get =>
            new RelationsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The reviews property</summary>
        public ReviewsRequestBuilder Reviews { get =>
            new ReviewsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The statistics property</summary>
        public StatisticsRequestBuilder Statistics { get =>
            new StatisticsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The userupdates property</summary>
        public UserupdatesRequestBuilder Userupdates { get =>
            new UserupdatesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new MangaItemRequestBuilder and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MangaItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/manga/{id}", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new MangaItemRequestBuilder and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MangaItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/manga/{id}", rawUrl) {
        }
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<MangaGetResponse?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default) {
#nullable restore
#else
        public async Task<MangaGetResponse> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default) {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<MangaGetResponse>(requestInfo, MangaGetResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default) {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default) {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public MangaItemRequestBuilder WithUrl(string rawUrl) {
            return new MangaItemRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
