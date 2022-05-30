namespace Test {

    internal class RandomServiceConsumer : IRandomServiceConsumer {
        RandomNumber randomNumberService;

        public RandomServiceConsumer(RandomNumber randomNumberService) {
            this.randomNumberService = randomNumberService;
        }

        public void MyPrint() {
            randomNumberService.MyPrint();
        }
    }
}