using System;

namespace Test {
    internal class RandomNumber : IService {
        int randomNumber;
        static Random random = new Random();

        public RandomNumber() {
            randomNumber = random.Next(1000);
        }

        public void MyPrint() {
            string msg = "Random Number: " + randomNumber;
            Console.WriteLine(msg);
        }
    }
}