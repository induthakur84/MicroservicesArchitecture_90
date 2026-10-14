using Microsoft.Extensions.DependencyInjection;

namespace RegisterDependency
{
    public class RegisterDependencyAttribute: Attribute
    {


        public ServiceLifetime Scope { get; set; }

        public RegisterDependencyAttribute(ServiceLifetime serviceLifetime)
        {
            Scope= serviceLifetime;
        }

        // first attribute 
        // Singleton : the service created one for entire    
    }


    public class RegisterSingletonAttribute : RegisterDependencyAttribute
    {
        public RegisterSingletonAttribute():base(ServiceLifetime.Singleton)
        {

        }
    }


    public class RegisterScopedAttribute: RegisterDependencyAttribute
    {
        public RegisterScopedAttribute():base(ServiceLifetime.Scoped)
        {

        }
    }

    public class RegisterTransientAttibute : RegisterDependencyAttribute
    {
        public RegisterTransientAttibute() : base(ServiceLifetime.Transient)
        {
        }

    }
}
