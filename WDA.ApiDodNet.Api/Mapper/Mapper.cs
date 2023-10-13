using AutoMapper;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;

namespace WDA.ApiDotNet.Application.Profiles
{
    public class Mapper : AutoMapper.Profile
    {
        public Mapper()
        {
            CreateMap<Books, BooksDTO>().ReverseMap();
            CreateMap<Books, BooksCreateDTO>().ReverseMap();
            CreateMap<Books, BooksUpdateDTO>().ReverseMap();
            CreateMap<Books, BooksSummaryDTO>().ReverseMap();
            CreateMap<Books, MostRentedBooksDTO>().ReverseMap();


            CreateMap<Publishers, PublishersDTO>().ReverseMap();
            CreateMap<Publishers, PublishersCreateDTO>().ReverseMap();
            CreateMap<Publishers, PublishersUpdateDTO>().ReverseMap();
            CreateMap<Publishers, PublishersSummaryDTO>().ReverseMap();

            CreateMap<Users, UsersDTO>().ReverseMap();
            CreateMap<Users, UsersCreateDTO>().ReverseMap();
            CreateMap<Users, UsersUpdateDTO>().ReverseMap();
            CreateMap<Users, UsersSummaryDTO>().ReverseMap();

            CreateMap<Rentals, RentalsCreateDTO>().ReverseMap();
            CreateMap<Rentals, RentalsUpdateDTO>().ReverseMap();
            CreateMap<Rentals, RentalsDTO>().ReverseMap();

        }
    }
}
