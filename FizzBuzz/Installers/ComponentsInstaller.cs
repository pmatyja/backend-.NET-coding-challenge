namespace FizzBuzz.Installers
{
    using System;
    using System.Reflection;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using FizzBuzz.Engines;
    using FizzBuzz.Outputs;
    using FizzBuzz.Rules;

    public class ComponentsInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        /// <exception cref="ArgumentNullException">container</exception>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(
                Classes.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<IRule>()
                    .WithServiceAllInterfaces()
                    .Unless(x => x == typeof(DivisableBy))
                    .LifestyleSingleton(),

                Component.For<IRule>().ImplementedBy<DivisableBy>()
                    .Named("div by 3")
                    .UsingFactoryMethod(x => new DivisableBy("Fizz", 3))
                    .LifestyleSingleton(),

                Component.For<IRule>().ImplementedBy<DivisableBy>()
                    .Named("div by 5")
                    .UsingFactoryMethod(x => new DivisableBy("Buzz", 5))
                    .LifestyleSingleton(),

                Component.For<IOutput>().ImplementedBy<ConsoleOutput>()
                    .LifestyleSingleton(),

                Component.For<IFizzBuzzEngine>().ImplementedBy<DefaultFizzBuzzEngine>()
                    .LifestyleSingleton()
            );
        }
    }
}
