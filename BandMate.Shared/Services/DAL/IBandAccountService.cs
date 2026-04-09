namespace BandMate.Shared.Services.DAL
{
    public interface IBandAccountService
    {
        Task SaveDefaultBandAsync(int bandID, int accountID);
    }
}