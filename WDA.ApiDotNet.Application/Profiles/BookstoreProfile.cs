using AutoMapper;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.DTOs;
using WDA.ApiDotNet.Application.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.DTOs.RentalsDTO;

namespace WDA.ApiDotNet.Application.Profiles
{
    public class BookstoreProfile : Profile
    {
        public BookstoreProfile()
        {
            CreateMap<Books, BooksDTO>().ReverseMap();
            CreateMap<Books, BooksCreateDTO>().ReverseMap();
            CreateMap<Books, BooksUpdateDTO>().ReverseMap();
            CreateMap<Books, BookRentalDTO>().ReverseMap();

            CreateMap<Publishers, PublishersDTO>().ReverseMap();
            CreateMap<Publishers, PublishersCreateDTO>().ReverseMap();
            CreateMap<Publishers, PublishersUpdateDTO>().ReverseMap();
            CreateMap<Publishers, PublisherBookDTO>().ReverseMap();

            CreateMap<Users, UsersDTO>().ReverseMap();
            CreateMap<Users, UsersCreateDTO>().ReverseMap();
            CreateMap<Users, UsersUpdateDTO>().ReverseMap();
            CreateMap<Users, UserRentalDTO>().ReverseMap();

            CreateMap<Rentals, RentalsCreateDTO>().ReverseMap();
            CreateMap<Rentals, RentalsUpdateDTO>().ReverseMap();
            CreateMap<Rentals, RentalsDTO>().ReverseMap();

        }
    }
}
