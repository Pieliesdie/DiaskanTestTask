using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Api.Tests.TestUtils;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RemoveByServiceType<T>(this IServiceCollection services)
    {
        return services.RemoveByServiceType(typeof(T));
    }

    public static IServiceCollection RemoveByServiceType(this IServiceCollection services, Type serviceType)
    {
        var serviceDescriptors = services.Where(x => x.ServiceType == serviceType).ToList();
        foreach (var serviceDescriptor in serviceDescriptors)
        {
            services.Remove(serviceDescriptor);
        }

        return services;
    }

    public static IServiceCollection RemoveByServiceType<TService, TImplementation>(this IServiceCollection services)
    {
        return services.RemoveByServiceType(typeof(TService), typeof(TImplementation));
    }

    public static IServiceCollection RemoveByServiceType(this IServiceCollection services, Type serviceType, Type implementationType)
    {
        var serviceDescriptors = services.Where(x => x.ServiceType == serviceType && x.ImplementationType == implementationType).ToList();
        foreach (var serviceDescriptor in serviceDescriptors)
        {
            services.Remove(serviceDescriptor);
        }

        return services;
    }
}