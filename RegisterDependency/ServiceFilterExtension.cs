using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RegisterDependency
{
    public  static class ServiceFilterExtension
    {


        // this is the extenstion method that is used to register all the service filter with the specified lifetime.


        public static void RegisterServices(this IServiceCollection services, string assemblyName)
        {
            // here first we can load the assembly by uusing the assembly name

            var types = Assembly.Load(assemblyName).GetTypes();


            // after this we can filter the types only those interface where we used this attribute 


            var interfaces = types.Where(t => t.IsInterface && t.GetCustomAttribute<RegisterDependencyAttribute>() != null);
            // its haas a interface and has this attribute



            // we will check the interface where we can use this attirubte have implementation


            foreach (var @interface in interfaces)
            {

                // we can find the class that implements the interface


                var @class= GetImplementationClasses(types, @interface);


                // Register the interface  and its implment in the service collecgion with the specified lifetime

                AddServiceDescriptor(services, @interface, @class);
            }

        }


        public static void AddServiceDescriptor(IServiceCollection services, Type @interface, Type @class)
        {

            // get the custom attribute from the interface to determine the lifetime of the service
            var scope = @interface.GetCustomAttribute<RegisterDependencyAttribute>().Scope;

            services.Add(new ServiceDescriptor(@interface, @class,scope));
        }


        public static Type GetImplementationClasses(Type[] types, Type @interface)
        {
            // we can find the first class that implement the given interface

            var @class= types.FirstOrDefault(t=>t.GetTypeInfo().ImplementedInterfaces.Any(i=>i==@interface));

            //if we have that class then we can return class otherwise we can throw an exception or return null here;

            return @class ?? throw new NotImplementedException($"No class found that implments {@interface.Name}");
        }

    }
}
