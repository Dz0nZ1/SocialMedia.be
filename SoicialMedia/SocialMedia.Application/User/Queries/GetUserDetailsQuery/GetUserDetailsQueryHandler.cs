using MediatR;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.User;
using SocialMedia.Domain.Common.Extensions;

namespace SocialMedia.Application.User.Queries.GetUserDetailsQuery;

public class GetUserDetailsQueryHandler(IUserService userService) : IRequestHandler<GetUserDetailsQuery, UserDetailsDto?>
{
    public async Task<UserDetailsDto?> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        return await userService.GetUserAsync(request.Id);
    }
}