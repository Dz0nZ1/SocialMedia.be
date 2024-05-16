using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Common.Behaviours;
using SocialMedia.Application.Configuration;

namespace SocialMedia.Application;

public static class DependencyInjection
{
   public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
      
      services.AddFluentValidationAutoValidation();
      services.AddFluentValidationClientsideAdapters();
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
      services.Configure<AesEncryptionConfiguration>(configuration.GetSection("AesEncryption"));
      
      return services;
   } 
}