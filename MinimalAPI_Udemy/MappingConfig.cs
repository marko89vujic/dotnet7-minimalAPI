using AutoMapper;
using MinimalAPI_Udemy.Models;
using MinimalAPI_Udemy.Models.DTOs;

namespace MinimalAPI_Udemy
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Coupon, CouponCreateDTO>().ReverseMap();
            CreateMap<Coupon, CouponDTO>().ReverseMap();
        }
    }
}
