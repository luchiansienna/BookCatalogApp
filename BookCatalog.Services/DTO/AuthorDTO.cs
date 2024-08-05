using System.Drawing;

namespace BookCatalog.Services.DTO
{
    public class AuthorDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Birthyear { get; set; }
    }
}
