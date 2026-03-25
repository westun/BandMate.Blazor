using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BandMate.Domain.Core.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
