﻿using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.Dto.Common;

namespace SocialMedia.Application.Common.interfaces;

public interface ICommentService
{
    Task<CommentDetailsDto?> GetAsync(Guid id);

    Task<List<CommentDetailsDto>> GetAllAsync();
    
    Task<CommentDetailsDto> CreateAsync(CreateCommentDetailsDto comment, CancellationToken cancellationToken);

    Task<CommentDetailsDto?> UpdateAsync(UpdateCommentDetailsDto comment, CancellationToken cancellationToken);

    Task<DeleteResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken);
}