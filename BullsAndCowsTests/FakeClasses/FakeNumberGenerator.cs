using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BullsAndCowsGame.Interfaces;

namespace BullsAndCowsTests.FakeClasses
{
    public class FakeNumberGenerator : IRandomGenerator
    {
        public FakeNumberGenerator(string generateNumber, string helpPattern)
        {
            this.GenerateNumber = generateNumber;
            this.HelpPattern = helpPattern;
        }

        public string GenerateNumber { get; set; }

        public string HelpPattern { get; set; }

        public string Generate(int numberLenght)
        {
            return this.GenerateNumber;
        }

        public string GenerateNumberByDigits(int numberLenght, int digitMin, int digitMax)
        {
            return this.HelpPattern;
        }
    }
}
