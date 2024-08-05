using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Domain
{
    [Index(nameof(Title), IsUnique = true)]
    public class Book : BaseModel
    {

        [MaxLength(100)]
        public required string Title { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public required virtual Publisher Publisher { get; set; }

        [MinLength(3), MaxLength(50)]
        public required string Edition { get; set; }

        public required DateOnly PublishedDate { get; set; }
    }
}
