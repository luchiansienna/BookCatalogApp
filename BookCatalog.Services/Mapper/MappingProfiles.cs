using AutoMapper;
using BookCatalog.Domain;
using BookCatalog.Services.DTO;

namespace BookCatalog.Services.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<DateTime, DateOnly>().ConvertUsing(src => DateOnly.FromDateTime(src));
            CreateMap<DateOnly, DateTime>().ConvertUsing(src => src.ToDateTime(TimeOnly.Parse("0:00 AM")));
            CreateMap<PublisherDTO, Publisher>();
            CreateMap<Publisher, PublisherDTO>();
            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorDTO, Author>();
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();

        }
    }
}
