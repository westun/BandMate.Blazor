using System;

namespace BandMate.Domain.Core.Models
{
    public class PasswordResetRequest
    {
        private const int DefaultExpirationMinutes = 30;

        private PasswordResetRequest()
        {
            this.Token = this.GenerateToken();
        }

        public PasswordResetRequest(int accountID)
            : this(accountID, expirationInMinutes: 0)
        {
        }

        public PasswordResetRequest(int accountID, int expirationInMinutes)
            : this()
        {
            this.AccountID = accountID;
            this.Expires = this.GetExpirationDate(expirationInMinutes);
        }

        public int PasswordResetRequestID { get; private set; }
        public int AccountID { get; private set; }
        public string Token { get; private set; }
        public bool Used { get; private set; }
        public bool Revoked { get; private set; }
        public DateTimeOffset Expires { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }

        public virtual Account Account { get; set; }
        
        public bool IsExpired
        {
            get
            {
                return DateTimeOffset.UtcNow > this.Expires;
            }
        }

        public bool IsValid
        {
            get
            {
                return !this.IsExpired && !this.Used && !this.Revoked;
            }
        }

        public void MarkUsed() => this.Used = true;

        public void Revoke() => this.Revoked = true;

        private string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

        private DateTimeOffset GetExpirationDate(int minutes = 0)
        {
            return DateTimeOffset.UtcNow.AddMinutes(minutes > 0 ? minutes : DefaultExpirationMinutes);
        }
    }
}
