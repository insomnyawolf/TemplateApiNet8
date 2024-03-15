// <auto-generated/>
using JikanRest.Watch.Episodes;
using JikanRest.Watch.Promos;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace JikanRest.Watch {
    /// <summary>
    /// Builds and executes requests for operations under \watch
    /// </summary>
    public class WatchRequestBuilder : BaseRequestBuilder {
        /// <summary>The episodes property</summary>
        public EpisodesRequestBuilder Episodes { get =>
            new EpisodesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The promos property</summary>
        public PromosRequestBuilder Promos { get =>
            new PromosRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new WatchRequestBuilder and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WatchRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/watch", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new WatchRequestBuilder and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WatchRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/watch", rawUrl) {
        }
    }
}