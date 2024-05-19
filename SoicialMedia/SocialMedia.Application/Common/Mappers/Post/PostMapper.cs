using Riok.Mapperly.Abstractions;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.Dto.User;

namespace SocialMedia.Application.Common.Mappers.Post;

[Mapper]
public static partial class PostMapper
{
    public static partial Domain.Entities.Post ToEntity(this CreatePostDetailsDto dto);

    public static partial PostDetailsDto ToDetailsDto(this Domain.Entities.Post entity);
    
    public static partial List<PostDetailsDto> ToDetailsListDto(this List<Domain.Entities.Post> entity);
}