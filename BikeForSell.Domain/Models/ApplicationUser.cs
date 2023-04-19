using Microsoft.AspNetCore.Identity;

namespace BikeForSell.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirsName { get; set; }
        public string? LastName { get; set; }
        public bool AddedDetalInformation { get; set; }

        // 1:N DetalInformation to ApplicationUser
        public ICollection<BikeDetailInformation> DetailInformation { get; set; }
    }
}
