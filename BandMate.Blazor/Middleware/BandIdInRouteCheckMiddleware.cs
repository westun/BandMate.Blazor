using BandMate.Blazor.Services;

namespace BandMate.Blazor.Middleware
{
    public class BandIdInRouteCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBandService bandService;

        public BandIdInRouteCheckMiddleware(IBandService bandService)
        {
            this.bandService = bandService;
        }

        public BandIdInRouteCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            var hardCodedBandID = 2;

            var segments = path.Split('/');
            var bandIdSegment = segments[1];
            int bandID = 0;
            if (!int.TryParse(bandIdSegment, out bandID))
            {
                context.Response.Redirect($"/{hardCodedBandID}/song/list");
                return;
            }

            //verify the user can access the band id in segment

            var testPath = "/2/song/list";
            var testSegments = testPath.Split('/');

            await _next(context);
        }
    }
}
