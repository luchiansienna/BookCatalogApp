using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Domain
{
    [Index(nameof(Name), IsUnique = true)]
    public class Publisher : BaseModel
    {
        [MinLength(3), MaxLength(50)]
        public required string Name { get; set; }
    }
}
