namespace BandMate.Shared.Services
{
    public class NavStateService
    {
        public string BandName { get; private set; } = string.Empty;

        public event Action? OnChange;

        public void UpdateBandName(string newText)
        {
            BandName = newText;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
