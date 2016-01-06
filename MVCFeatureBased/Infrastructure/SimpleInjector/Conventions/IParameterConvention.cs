using System.Linq.Expressions;
using SimpleInjector;

namespace MVCFeatureBased.Web.Infrastructure.SimpleInjector.Conventions
{
    public interface IParameterConvention
    {
        bool CanResolve(InjectionTargetInfo target);
        Expression BuildExpression(InjectionConsumerInfo consumer);
    }
}