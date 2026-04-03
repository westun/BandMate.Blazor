namespace BandMate.Blazor.Services
{
    public interface IUserService
    {
        Task<int> GetCurrentAccountIDAsync();
    }
}