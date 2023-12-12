using Microsoft.AspNetCore.Identity;

namespace Pintureria_Acuarela.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public long BusinessID { get; set; }
        public virtual Business Business { get; set; } = null!;
    }
}
