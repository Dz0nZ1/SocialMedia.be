using MediatR;
using SocialMedia.Application.Common.Dto.Comment;

namespace SocialMedia.Application.Comment.Queries.GetAllCommentDetailsQuery;

public record GetAllCommentDetailsQuery() : IRequest<List<CommentDetailsDto>>;