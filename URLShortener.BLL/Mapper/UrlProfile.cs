using AutoMapper;
using URLShortener.BLL.DTO;
using URLShortener.DAL.Entities;

namespace URLShortener.BLL.Mapper
{
    public class UrlProfile : Profile
    {
        public UrlProfile()
        {
            CreateMap<Url, UrlDTO>().ReverseMap();
        }
    }
}