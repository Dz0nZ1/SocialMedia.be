using Riok.Mapperly.Abstractions;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.Common.Mappers.Comment;

namespace SocialMedia.Application.Common.Mappers.Post;

[Mapper]
public static partial class PostMapper
{
    public static partial Domain.Entities.Post ToEntity(this CreatePostDetailsDto dto);

    public static partial Domain.Entities.Post ToEntity(this PostDetailsDto dto);

    public static PostDetailsDto ToDetailsDto(this Domain.Entities.Post entity)
    {
        return new PostDetailsDto(entity.Id.ToString(), entity.Content, entity.ImageUrl, entity.Comments.ToDetailsListDto());
    }
    public static partial List<PostDetailsDto> ToDetailsListDto(this List<Domain.Entities.Post> entity);
    
    public static void ToEntity(this Domain.Entities.Post entity, UpdatePostDetailsDto dto)
    {
        entity.UpdatePost(dto.Content);
    }
}