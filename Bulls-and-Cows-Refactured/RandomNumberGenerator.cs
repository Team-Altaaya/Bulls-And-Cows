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
            if (numberLength < 0 )
            {
                throw new ArgumentException("Arguments cannot be negative numbers.");
            }

            StringBuilder number = new StringBuilder(numberLength);
            Random randomNumberGenerator = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < numberLength; i++)
            {
                int randomDigit = randomNumberGenerator.Next(9);
                number.Append(randomDigit);
            }

            return number.ToString();
        }

        public string GenerateNumberByDigits(int numberLength, int digitMin, int digitMax)
        {
            if (numberLength < 0 || digitMin < 0 || digitMax < 0)
            {
                throw new ArgumentException("Arguments cannot be negative numbers.");
            }

            if (numberLength > digitMin + digitMax + 1)
            {
                throw new ArgumentException("This method is designed to return number(string) " +
               "with different digits. Cannot calculate number with length " + numberLength +
               " in range " + (digitMin + digitMax + 1) + " (digitMin + digitMax + 1)");
            }

            List<int> numberList = new List<int>(numberLength);
            StringBuilder number = new StringBuilder(numberLength);

            for (int i = 0; i < numberLength; i++)
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
