using Microsoft.AspNetCore.Identity;

namespace BikeForSell.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirsName { get; set; }
        public string? LastName { get; set; }
        public bool AddedDetalInformation { get; set; }

        // 1:1 DetalInformation to ApplicationUser
        public DetailInformation DetailInformation { get; set; }
    }
}
