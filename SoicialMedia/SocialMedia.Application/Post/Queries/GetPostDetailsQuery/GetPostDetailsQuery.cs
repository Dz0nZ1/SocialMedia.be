using MediatR;
using SocialMedia.Application.Common.Dto.Post;

namespace SocialMedia.Application.Post.Queries.GetPostDetailsQuery;

public record GetPostDetailsQuery(string Id) : IRequest<PostDetailsDto?>;