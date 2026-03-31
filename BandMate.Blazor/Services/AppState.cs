namespace BandMate.Blazor.Services
{
    using Microsoft.AspNetCore.Components;

    public class AppState
    {
        // [PersistentState] ensures this survives if the user's connection hiccups
        [PersistentState]
        public int CurrentAccountId { get; set; }

        [PersistentState]
        public int CurrentBandId { get; set; }

        // Event to notify UI components to refresh when the ID changes
        public event Action? OnChange;

        public void SetBandID(int id)
        {
            this.CurrentBandId = id;
            NotifyStateChanged();
        }

        public void SetAccountID(int id)
        {
            this.CurrentAccountId = id;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => this.OnChange?.Invoke();
    }
}
