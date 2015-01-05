namespace BullsAndCowsGame
{
    using BullsAndCowsGame.Interfaces;

    public class BullsAndCows
    {
        public static void Main()
        {
            IRandomGenerator generator = new RandomNumberGenerator();
            GameEngine game = new GameEngine(generator);
            game.Start();

            //generator.GenerateNumberByDigits(5, 0, 3);
        }
    }
}