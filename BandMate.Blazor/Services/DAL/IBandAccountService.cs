namespace BandMate.Blazor.Services.DAL
{
    public interface IBandAccountService
    {
        Task SaveDefaultBandAsync(int bandID, int accountID);
    }
}