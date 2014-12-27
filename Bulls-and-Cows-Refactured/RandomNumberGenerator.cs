namespace BullsAndCowsGame
{
    using System;
    using System.Text;
    using BullsAndCowsGame.Interfases;

    public class RandomNumberGenerator : IRandomGenerator
    {
        public RandomNumberGenerator()
        {
        }
        public string Generate(int numberLength)
        {
            StringBuilder number = new StringBuilder(numberLength);
            Random randomNumberGenerator = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < numberLength; i++)
            {
                int randomDigit = randomNumberGenerator.Next(9);
                number.Append(randomDigit);
            }

            return number.ToString();
        }
    }
}
