using MediatR;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.User;

namespace SocialMedia.Application.User.Queries.GetAllUserDetailsQuery;

public class GetAllUserDetailsQueryHandler(IUserService userService) : IRequestHandler<GetAllUserDetailsQuery, List<UserDetailsDto>>
{
    public async Task<List<UserDetailsDto>> Handle(GetAllUserDetailsQuery request, CancellationToken cancellationToken)
    {
        return await userService.GetAllUsers();
    }
}