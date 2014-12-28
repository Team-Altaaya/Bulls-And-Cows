namespace BullsAndCowsGame
{
    using System;
    using System.Text;
    using BullsAndCowsGame.Interfaces;
    using System.Collections.Generic;

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

        public string GenerateNumberByDigits(int numberLenght, int digitMin, int digitMax)
        {
            List<int> numberList = new List<int>(numberLenght);
            StringBuilder number = new StringBuilder(numberLenght);

            for (int i = 0; i < numberLenght; i++)
            {
                Random r = new Random(DateTime.Now.Millisecond);
                int randomDigit = r.Next(digitMin, digitMax + 1);

                if (numberList.IndexOf(randomDigit) == -1)
                {
                    numberList.Add(randomDigit);
                }
                else
                {
                    i--;
                }
            }


            for (int i = 0; i < numberList.Count; i++)
            {
                number.Append(numberList[i]);
            }

            return number.ToString();
        }
    }
}
