namespace BandMate.Shared.Services
{
    public interface IUserService
    {
        Task<int> GetCurrentAccountIDAsync();
        Task<bool> IsAuthenticatedAsync();
    }
}