namespace BookCatalog.Services.DTO
{

    public record BookDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public ICollection<AuthorDTO> Authors { get; set; }
        public PublisherDTO Publisher { get; set; }
        public string Edition { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
