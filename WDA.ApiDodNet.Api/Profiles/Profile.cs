using AutoMapper;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;

namespace WDA.ApiDotNet.Application.Profiles
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<Books, BooksDTO>().ReverseMap();
            CreateMap<Books, BooksCreateDTO>().ReverseMap();
            CreateMap<Books, BooksUpdateDTO>().ReverseMap();
            CreateMap<Books, BookRentalDTO>().ReverseMap();
            CreateMap<Books, BooksCountDTO>().ReverseMap();


            CreateMap<Publishers, PublishersDTO>().ReverseMap();
            CreateMap<Publishers, PublishersCreateDTO>().ReverseMap();
            CreateMap<Publishers, PublishersUpdateDTO>().ReverseMap();
            CreateMap<Publishers, BookPublisherDTO>().ReverseMap();

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
