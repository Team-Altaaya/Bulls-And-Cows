namespace BullsAndCowsGame
{
    using BullsAndCowsGame.Interfases;

    public class BullsAndCows
    {
        public static void Main()
        {
            IRandomGenerator generator = new RandomNumberGenerator();
            GameEngine game = new GameEngine(generator);
            game.Start();
        }
    }
}