using System;
using System.Linq;

namespace DI_LIB {
    public class DependencyResolver {
        DependencyContainer container;

        public DependencyResolver(DependencyContainer container) {
            this.container = container;
        }

        public TType GetService<TType>() {
            return (TType)GetService(typeof(TType), true);
        }

        public object CreateImplementation(Dependency dependency, Func<Type, object> func) {
            if (dependency.IsImplemented) {
                return dependency.Implementation;
            }

            var implementation = func(dependency.type);
            if (dependency.lifeTime == DependencyLifeTime.SINGLETON) {
                dependency.AddImplementation(implementation);
            }
            return implementation;
        }

        public object GetService(Type type, bool isInterface) {
            Dependency dependency = isInterface ? container.GetDependencyFromInterface(type) : container.GetDependencyFromClass(type);

            try {
                var constructor = dependency.type.GetConstructors().Single();
                var parameters = constructor.GetParameters().ToArray();
                if (parameters.Length > 0) {
                    var parameterImplementations = new object[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++) {
                        parameterImplementations[i] = GetService(parameters[i].ParameterType, false);
                    }
                    return CreateImplementation(dependency, t => Activator.CreateInstance(t, parameterImplementations));
                }
                return CreateImplementation(dependency, t => Activator.CreateInstance(t));
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
