﻿namespace SocialMedia.Application.Common.Dto.User;

public record CreateUserDetailsDto(string FirstName, string LastName, string Username, string ProfilePictureUrl, string Bio, string Email);
