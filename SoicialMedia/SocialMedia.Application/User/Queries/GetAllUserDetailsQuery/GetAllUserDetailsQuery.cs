using MediatR;
using SocialMedia.Application.Common.Dto.User;

namespace SocialMedia.Application.User.Queries.GetAllUserDetailsQuery;

public record GetAllUserDetailsQuery() : IRequest<List<UserDetailsDto>>;