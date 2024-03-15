// <auto-generated/>
using JikanRest.RandomNamespace.Anime;
using JikanRest.RandomNamespace.Characters;
using JikanRest.RandomNamespace.Manga;
using JikanRest.RandomNamespace.People;
using JikanRest.RandomNamespace.Users;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace JikanRest.RandomNamespace {
    /// <summary>
    /// Builds and executes requests for operations under \random
    /// </summary>
    public class RandomRequestBuilder : BaseRequestBuilder {
        /// <summary>The anime property</summary>
        public AnimeRequestBuilder Anime { get =>
            new AnimeRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The characters property</summary>
        public CharactersRequestBuilder Characters { get =>
            new CharactersRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The manga property</summary>
        public MangaRequestBuilder Manga { get =>
            new MangaRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The people property</summary>
        public PeopleRequestBuilder People { get =>
            new PeopleRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The users property</summary>
        public UsersRequestBuilder Users { get =>
            new UsersRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new RandomRequestBuilder and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public RandomRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/random", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new RandomRequestBuilder and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public RandomRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/random", rawUrl) {
        }
    }
}
