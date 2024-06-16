using Riok.Mapperly.Abstractions;
using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.Dto.User;

namespace SocialMedia.Application.Common.Mappers.Comment;

[Mapper]
public static partial class CommentMapper
{
    public static Domain.Entities.Comment ToEntity(this CreateCommentDetailsDto dto)
    {
        return new Domain.Entities.Comment(dto.Content, dto.UserId, dto.PostId);
    }

    public static void ToEntity(this Domain.Entities.Comment entity, UpdateCommentDetailsDto dto)
    {
        entity.UpdateComment(dto.Content);
    }

    public static CommentDetailsDto ToDetailsDto(this Domain.Entities.Comment entity)
    {
        var info = new CommentInfo(
            new UserFullNameDto(entity.User!.FirstName, entity.User.LastName, entity.User.Username)
        );
        return new CommentDetailsDto(entity.Content,entity.CreatedAt, entity.ModifiedAt, info);
    }

    public static List<CommentDetailsDto> ToDetailsListDto(this List<Domain.Entities.Comment> entities)
    {
        List<CommentDetailsDto> dtoList = new List<CommentDetailsDto>();
        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToDetailsDto());
        }
        return dtoList;
    }
}