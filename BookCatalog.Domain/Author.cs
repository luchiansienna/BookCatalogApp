using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Domain
{
    [Index(nameof(Name), nameof(Surname), IsUnique = true)]
    public class Author : BaseModel
    {
        [MinLength(3), MaxLength(60)]
        public required string Name { get; set; }

        [MinLength(3), MaxLength(60)]
        public required string Surname { get; set; }

        public required int Birthyear { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
