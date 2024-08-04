using Riok.Mapperly.Abstractions;
using SocialMedia.Application.Common.Dto.Like;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.Common.Mappers.Comment;
using SocialMedia.Application.Common.Mappers.Post;

namespace SocialMedia.Application.Common.Mappers.Like;

[Mapper]
public static partial class LikeMapper
{
    public static LikeDetailsDto ToDetailsDto(this Domain.Entities.Like entity)
    {
        return new LikeDetailsDto(entity.Post!.ToDetailsDto(), entity.Comment!.ToDetailsDto(),
            new UserFullNameDto(entity.User.FirstName, entity.User.LastName, entity.User.Username), entity.IsLocked);
    }

    public static List<LikeDetailsDto> ToDetailsListDto(this List<Domain.Entities.Like> entities)
    {
        return entities.Select(e => e.ToDetailsDto()).ToList();
    }
    
}