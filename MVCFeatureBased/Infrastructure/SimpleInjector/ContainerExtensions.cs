using MVCFeatureBased.Web.Infrastructure.SimpleInjector.Conventions;
using SimpleInjector;

namespace MVCFeatureBased.Web.Infrastructure.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static void RegisterParameterConvention(this ContainerOptions options, IParameterConvention convention)
        {
            options.DependencyInjectionBehavior = new ConventionDependencyInjectionBehavior(options.DependencyInjectionBehavior, convention);
        }
    }
}