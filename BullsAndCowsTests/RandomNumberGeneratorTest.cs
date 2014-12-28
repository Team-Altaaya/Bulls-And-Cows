using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullsAndCowsGame;
using BullsAndCowsGame.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCowsTests
{
    [TestClass]
    public class RandomNumberGeneratorTest
    {
        RandomNumberGenerator generator;

        private RandomNumberGenerator Setup()
        {
            return generator = new RandomNumberGenerator();
        }

        [TestMethod]
        public void GenerateMethodShouldReturnStringWithSpecificLength()
        {
            generator = Setup();

            int expected = 40;
            string number = generator.Generate(40);

            Assert.AreEqual(expected, number.Length, "String have different length that expected");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GenerateMethodShouldThrowExceptionWhenHaveNegativeParameter()
        {
            generator = Setup();

            string number = generator.Generate(-40);
        }


        [TestMethod]
        public void GenerateNumberByDigitMethodShouldReturnStringWithSpecificLength()
        {
            generator = Setup();

            int expected = 4;
            string number = generator.GenerateNumberByDigits(4, 0, 3);

            Assert.AreEqual(expected, number.Length, "String have different length that expected");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GenerateNumberByDigitMethodShouldThrowExceptionWhenHaveNegativeFirstParameter()
        {
            generator = Setup();

            string number = generator.GenerateNumberByDigits(-4, 0, 5);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GenerateNumberByDigitMethodShouldThrowExceptionWhenHaveNegativeSecondParameter()
        {
            generator = Setup();

            string number = generator.GenerateNumberByDigits(10, -1, 5);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GenerateNumberByDigitMethodShouldThrowExceptionWhenHaveNegativeThridParameter()
        {
            generator = Setup();

            string number = generator.GenerateNumberByDigits(20, 0, -5);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GenerateNumberByDigitMethodShouldThrowExceptionWhenHaveNumberLengthGreaterThenSumOfOtherParametersPlusOne()
        {
            generator = Setup();

            string number = generator.GenerateNumberByDigits(5, 0, 3);
        }

        [TestMethod]
        public void GenerateNumberByDigitMethodShouldReturnSpecificNumberWhenBoundsAreEqual()
        {
            generator = Setup();

            string expected = "10";
            string number = generator.GenerateNumberByDigits(1, 10, 10);


            Assert.AreEqual(expected, number, "When upper and lower bound are equal the result should be lower/upper bound");
        }
    }
}
