using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ScopedFactory
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a TService into the DI container and also registers a factory for it. <br />
        /// In Controllers (or scoped services), TService can be injected in constructor directly.<br />
        /// In Singleton services, TService needs to be injected using a Func<TService>.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        public static void AddScopedFactory<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            services.TryAdd<IHttpContextAccessor, HttpContextAccessor>(lifetime: ServiceLifetime.Singleton);
            services.AddScoped<TService, TImplementation>();
            services.AddScoped<Func<TService>, Func<TImplementation>>(serviceProvider =>
            {
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                return () => (TImplementation)httpContextAccessor.HttpContext.RequestServices.GetRequiredService<TService>();
            });
        }

        /// <summary>
        /// Tries to add add the TService into the DI container if not already regitered
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        public static void TryAdd<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime) where TService : class where TImplementation : class, TService
        {
            var descriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);
            if (!services.Contains(descriptor)) services.Add(descriptor);
        }

        /// <summary>
        /// Tries to add the serviceType into the ID container if not already registered
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        public static void TryAdd(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);
            if (!services.Contains(descriptor)) services.Add(descriptor);
        }
    }
}
