using BandMate.Authentication.Data;
using BandMate.Shared.Services.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BandMate.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService songService;
        private readonly IBandService bandService;
        private readonly ApplicationDbContext applicationDbContext;

        public SongController(
            ISongService songService,
            IBandService bandService,
            ApplicationDbContext applicationDbContext)
        {
            this.songService = songService;
            this.bandService = bandService;
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet(Name = "GetByBandID")]
        public async Task<IActionResult> GetByBandID(int bandID)
        {
            if (bandID <= 0)
            {
                return this.BadRequest("must provide a valid bandID");
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.applicationDbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                return this.NotFound();
            }

            var accountID = user.AccountID;
            var bands = await this.bandService.GetAll(accountID);
            var band = bands.FirstOrDefault(b => b.BandID == bandID);
            if (band is null)
            {
                return this.NotFound();
            }

            var songs = await this.songService.GetSongsByBandIDAsync(bandID);

            return this.Ok(songs);
        }
    }
}
