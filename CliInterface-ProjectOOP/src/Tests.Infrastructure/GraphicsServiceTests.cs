using CliInterface.Core.Services;
using CliInterface.Infrastructure.Services;
using Xunit;

namespace Tests.Infrastructure
{
    /// <summary>
    /// Graphics service: local buffer prevents redundant downloads,
    /// missing cache returns null, unreachable URLs degrade gracefully.
    /// </summary>
    public sealed class GraphicsServiceTests
    {
        [Fact]
        public async Task DownloadAndCacheAsync_UnreachableUrl_ReturnsNull()
        {
            IGraphicsService service = new GraphicsService();

            string? result = await service.DownloadAndCacheAsync(
                "http://127.0.0.1:1/nonexistent.png",
                CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public void GetCachedPath_UnknownUrl_ReturnsNull()
        {
            IGraphicsService service = new GraphicsService();

            string? result = service.GetCachedPath("http://example.com/unknown.png");

            Assert.Null(result);
        }

        [Fact]
        public void GetCachedPath_EmptyUrl_ReturnsNull()
        {
            IGraphicsService service = new GraphicsService();

            Assert.Null(service.GetCachedPath(""));
            Assert.Null(service.GetCachedPath("   "));
        }

        [Fact]
        public async Task DownloadAndCacheAsync_EmptyUrl_ReturnsNull()
        {
            IGraphicsService service = new GraphicsService();

            string? result = await service.DownloadAndCacheAsync("");

            Assert.Null(result);
        }

        [Fact]
        public void NullUrl_IsRejected()
        {
            IGraphicsService service = new GraphicsService();

            Assert.Null(service.GetCachedPath(null!));
        }
    }
}
