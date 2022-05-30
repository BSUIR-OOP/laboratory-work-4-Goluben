using System;
using System.Collections.Generic;
using System.Linq;

namespace DI_LIB {
    public class DependencyContainer {
        List<Dependency> dependencies;

        public DependencyContainer() {
            dependencies = new List<Dependency>();
        }
        public void AddDependency<TType, TInterface>(DependencyLifeTime lifeTime) {
            dependencies.Add(new Dependency(typeof(TType), typeof(TInterface), lifeTime));
        }

        public Dependency GetDependencyFromInterface(Type inerfaceType) {
            return dependencies.FirstOrDefault(x => x.interfaceType == inerfaceType);
        }

        public Dependency GetDependencyFromClass(Type type) {
            return dependencies.FirstOrDefault(x => x.type.Name == type.Name);
        }
    }
}
