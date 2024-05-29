using MediatR;
using SocialMedia.Application.Common.Dto.Comment;

namespace SocialMedia.Application.Comment.Queries.GetCommentDetailsCommand;

public record GetCommentDetailsQuery(string Id) : IRequest<CommentDetailsDto?>;
