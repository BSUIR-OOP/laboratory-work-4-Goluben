using System;
using DI_LIB;

namespace Test {
    class Program {
        static void Main(string[] args) {

            var container = new DependencyContainer();

            container.AddDependency<RandomNumber, IService>(DependencyLifeTime.TRANSIENT);
            container.AddDependency<RandomServiceConsumer, IRandomServiceConsumer>(DependencyLifeTime.SINGLETON);

            var error = new DependencyCycleError();
            string err = error.CheckForDependencyCycles(typeof(RandomNumber));

            if (err != null) {
                Console.WriteLine(err);
                Console.Read();
                return;
            }

            var resolver = new DependencyResolver(container);

            IService randomNumberService = resolver.GetService<IService>();
            randomNumberService.MyPrint();

            var randomNumberService2 = resolver.GetService<IService>();
            randomNumberService2.MyPrint();

            var randomNumberService3 = resolver.GetService<IService>();
            randomNumberService3.MyPrint();


            var randomServiceConsumer = resolver.GetService<IRandomServiceConsumer>();
            randomServiceConsumer.MyPrint();

            var randomServiceConsumer2 = resolver.GetService<IRandomServiceConsumer>();
            randomServiceConsumer2.MyPrint();

            var randomServiceConsumer3 = resolver.GetService<IRandomServiceConsumer>();
            randomServiceConsumer3.MyPrint();

            Console.Read();
        }
    }
}
