using System;

namespace DI_LIB {
    public class Dependency {
   
        public Type interfaceType { get; set; }
        public Type type { get; set; }
        public object Implementation { get; set; }
        public bool IsImplemented { get; set; }
        public DependencyLifeTime lifeTime { get; set; }

        public Dependency(Type type, Type interfaceType, DependencyLifeTime lifeTime) {
            this.type = type;
            this.lifeTime = lifeTime;
            this.interfaceType = interfaceType;
        }

        public void AddImplementation(object obj) {
            Implementation = obj;
            IsImplemented = true;
        }
    }
}
