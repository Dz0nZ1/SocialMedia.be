using MediatR;
using SocialMedia.Application.Common.Dto.User;

namespace SocialMedia.Application.User.Queries.GetUserDetailsQuery;

public record GetUserDetailsQuery(string Id) : IRequest<UserDetailsDto?> ;