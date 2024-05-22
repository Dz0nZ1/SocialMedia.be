using MediatR;
using SocialMedia.Application.Common.Dto.Post;

namespace SocialMedia.Application.Post.Queries.GetAllPostDetailsQuery;

public record GetAllPostDetailsQuery() : IRequest<List<PostDetailsDto>>;