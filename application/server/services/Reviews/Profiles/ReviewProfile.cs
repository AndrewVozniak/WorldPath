using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Reviews.Dtos;
using Reviews.Models;

namespace Reviews.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {   
        // Source -> Target
        CreateMap<ReviewDto, Review>();
    }
}