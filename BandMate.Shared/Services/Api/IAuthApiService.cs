namespace BandMate.Shared.Services.Api
{
    public interface IAuthApiService
    {
        Task<bool> Login(string email, string password);
    }
}