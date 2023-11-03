using AutoMapper;
using WDA.ApiDotNet.Business.Models;
using WDA.ApiDotNet.Business.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Business.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Business.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Business.Models.DTOs.UsersDTO;

namespace WDA.ApiDotNet.Business.Profiles
{
    public class Mapper : AutoMapper.Profile
    {
        public Mapper()
        {
            CreateMap<Books, BooksDTO>().ReverseMap();
            CreateMap<Books, BooksCreateDTO>().ReverseMap();
            CreateMap<Books, BooksUpdateDTO>().ReverseMap();
            CreateMap<Books, BooksSummaryDTO>().ReverseMap();
            CreateMap<Books, BooksAvailableDTO>().ReverseMap();
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
            CreateMap<Rentals, RentalsDTO>().ReverseMap();

        }
    }
}
