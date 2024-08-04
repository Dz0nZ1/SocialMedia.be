using Riok.Mapperly.Abstractions;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.Common.Mappers.Comment;
using SocialMedia.Application.Common.Mappers.Post;
using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Application.Common.Mappers.User;

[Mapper]
public static partial class UserMapper
{
    public static UserDetailsDto ToDetailsDto(this ApplicationUser entity)
    {
        return new UserDetailsDto(entity.FirstName, entity.LastName, entity.Username, entity.ProfilePictureUrl, entity.Bio, entity.PhoneNumber, entity.Location, entity.JobPosition, entity.Posts.ToDetailsListDto());
    }
    public static partial List<UserDetailsDto> ToDetailsListDto(this List<ApplicationUser> entity);
    
    public static void ToEntity(this ApplicationUser entity, UpdateUserDetailsDto dto)
    {
        entity.UpdateUser(dto.FirstName, dto.LastName, dto.Username, dto.Bio, dto.PhoneNumber, dto.JobPosition, dto.Location);
    }
    
    
}