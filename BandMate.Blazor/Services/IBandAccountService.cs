namespace BandMate.Blazor.Services
{
    public interface IBandAccountService
    {
        Task SaveDefaultBandAsync(int bandID, int accountID);
    }
}