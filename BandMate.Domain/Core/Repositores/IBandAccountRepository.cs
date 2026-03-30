namespace BandMate.Domain.Core.Repositores
{
    public interface IBandAccountRepository
    {
        Task SaveDefaultBandAsync(int bandID, int accountID);
    }
}