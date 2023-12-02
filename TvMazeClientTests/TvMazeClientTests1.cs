using TvMazeClient;

namespace TvMazeClientTests
{
    [TestClass]
    public class TvMazeClientTests1
    {
        private readonly TvMazeApiClient TvMazeApiClient;

        public TvMazeClientTests1()
        {
            TvMazeApiClient = new TvMazeApiClient(new HttpClient());
        }

        [TestMethod]
        public async Task SampleRequest()
        {
            var show = await TvMazeApiClient.GetShow(1);

            Assert.IsNotNull(show);
        }
    }
}