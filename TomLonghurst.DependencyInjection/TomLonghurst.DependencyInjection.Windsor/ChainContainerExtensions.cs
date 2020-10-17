using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace TomLonghurst.DependencyInjection.Windsor
{
    public static class ChainContainerExtensions
    {
        public static IWindsorContainer AddChained(this IWindsorContainer container,
            LifestyleType lifestyleType,
            Type interfaceType,
            params Type[] implementationTypes)
        {
            ValidateArguments(interfaceType, implementationTypes);

            var componentRegistrations = new List<IRegistration>();
            for (var index = 0; index < implementationTypes.Length; index++)
            {
                var componentRegistration = Component.For(interfaceType)
                    .ImplementedBy(implementationTypes[index]);
                
                if (index < implementationTypes.Length - 1)
                {
                    componentRegistration.DependsOn(
                        Dependency.OnComponent(interfaceType, implementationTypes[index + 1])
                    );
                }
                
                componentRegistrations.Add(componentRegistration.LifeStyle.Is(lifestyleType));
            }

            container.Register(
                componentRegistrations.ToArray()
            );

            return container;
        }

        private static void ValidateArguments(Type interfaceType, Type[] implementationTypes)
        {
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"{interfaceType.Name} must be an interface");
            }

            if (implementationTypes.Length == 0)
            {
                throw new InvalidOperationException($"No implementation defined for {interfaceType.Name}");
            }

            foreach (var type in implementationTypes)
            {
                if (!interfaceType.IsAssignableFrom(type))
                {
                    throw new ArgumentException($"{type.Name} must implement the interface {interfaceType.Name}");
                }
            }
        }

        public static IWindsorContainer AddChained<TService>(this IWindsorContainer container,
            LifestyleType lifestyleType,
            params Type[] implementationTypes) where TService : class
        {
            return container.AddChained(lifestyleType, typeof(TService), implementationTypes);
        }
        
        public static IWindsorContainer AddChained<TService, TImplementation1, TImplementation2>(this IWindsorContainer container,
            LifestyleType lifestyleType) 
            where TService : class 
            where TImplementation1 : TService
            where TImplementation2 : TService
        {
            return container.AddChained<TService>(lifestyleType, typeof(TImplementation1), typeof(TImplementation2));
        }
        
        public static IWindsorContainer AddChained<TService, TImplementation1, TImplementation2, TImplementation3>(this IWindsorContainer container,
            LifestyleType lifestyleType) 
            where TService : class
            where TImplementation1 : TService
            where TImplementation2 : TService
            where TImplementation3 : TService
        {
            return container.AddChained<TService>(lifestyleType, typeof(TImplementation1), typeof(TImplementation2), typeof(TImplementation3));
        }
        
        public static IWindsorContainer AddChained<TService, TImplementation1, TImplementation2, TImplementation3, TImplementation4>(this IWindsorContainer container,
            LifestyleType lifestyleType) 
            where TService : class
            where TImplementation1 : TService
            where TImplementation2 : TService
            where TImplementation3 : TService
            where TImplementation4 : TService
        {
            return container.AddChained<TService>(lifestyleType, typeof(TImplementation1), typeof(TImplementation2), typeof(TImplementation3), typeof(TImplementation4));
        }
        
        public static IWindsorContainer AddChained<TService, TImplementation1, TImplementation2, TImplementation3, TImplementation4, TImplementation5>(this IWindsorContainer container,
            LifestyleType lifestyleType) 
            where TService : class
            where TImplementation1 : TService
            where TImplementation2 : TService
            where TImplementation3 : TService
            where TImplementation4 : TService
            where TImplementation5 : TService
        {
            return container.AddChained<TService>(lifestyleType, typeof(TImplementation1), typeof(TImplementation2), typeof(TImplementation3), typeof(TImplementation4), typeof(TImplementation5));
        }
        
        public static IWindsorContainer AddChained<TService, TImplementation1, TImplementation2, TImplementation3, TImplementation4, TImplementation5, TImplementation6>(this IWindsorContainer container,
            LifestyleType lifestyleType) 
            where TService : class
            where TImplementation1 : TService
            where TImplementation2 : TService
            where TImplementation3 : TService
            where TImplementation4 : TService
            where TImplementation5 : TService
            where TImplementation6 : TService
        {
            return container.AddChained<TService>(lifestyleType, typeof(TImplementation1), typeof(TImplementation2), typeof(TImplementation3), typeof(TImplementation4), typeof(TImplementation5), typeof(TImplementation6));
        }
    }
}