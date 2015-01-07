using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullsAndCowsGame;
using BullsAndCowsGame.Interfaces;
using BullsAndCowsTests.FakeClasses;

namespace BullsAndCowsTests
{
    [TestClass]
    public class GameEngineTests
    {
        StringWriter sw = new StringWriter();

        private const string WelcomeMessage = "Welcome to “Bulls and Cows” game. " +
                                              "Please try to guess my secret 4-digit number." +
                                              "Use 'top' to view the top scoreboard, 'restart' " +
                                              "to start a new game and 'help' to cheat and 'exit' to quit the game.";
        private const string EnterGuess = "Enter your guess or command: ";

        GameEngine gameEngine;

        private GameEngine EngineSetup(string generateNumber, string helpPattern)
        {
            IRandomGenerator generator = new FakeNumberGenerator(generateNumber, helpPattern);
            gameEngine = new GameEngine(generator);
            return gameEngine;
        }

        private string TestSetup(string secretNumber, string pattern, string input)
        {
            string result = "";

            gameEngine = EngineSetup(secretNumber, pattern);
            StringReader sr = new StringReader(input);

            using (sw)
            {
                using (sr)
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    gameEngine.NewGame();
                    result = sw.ToString();
                }
            }

            return result;
        }


        [TestMethod]
        public void WrongNumberAndExit()
        {
            var result = TestSetup("1234", "0123", "1334\r\nexit");

            string expected =
                 string.Format(
                "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.\r\n"
                + "Enter your guess or command: "
                + "Wrong number! Bulls: 3, Cows: 0\r\n"
                + "Enter your guess or command: "
                + "Good Bye!\r\n");

            Assert.AreEqual<string>(expected, result);
        }

        [TestMethod]
        public void RightNumberEnterNameToScoreBoardAndExit()
        {
            var result = TestSetup("1234", "0123", "1234\r\nandrei\r\nexit");

            string expected =
                 string.Format(
                "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.\r\n"
                + "Enter your guess or command: "
                + "Congratulations! You guessed the secret number in 1 attempts.\r\n"
                + "Please enter your name for the top scoreboard: Scoreboard:\r\n"
                + "1. andrei --> 1 guess\r\n"
                + "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.\r\n"
                + "Enter your guess or command: "
                + "Good Bye!\r\n");

            Assert.AreEqual<string>(expected, result);
        }

        [TestMethod]
        public void CheatCorrectNumberAndExit()
        {
            var result = TestSetup("1234", "0123", "help\r\n1234\r\nexit");

            string expected =
                 string.Format(
               "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.\r\n"
               + "Enter your guess or command: "
               + "The number looks like 1XXX.\r\n"
               + "Enter your guess or command: "
               + "Congratulations! You guessed the secret number in 1 attempts and 1 cheats.\r\n"
               + "You are not allowed to enter the top scoreboard.\r\n"
               + "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.\r\n"
               + "Enter your guess or command: "
               + "Good Bye!\r\n");

            Assert.AreEqual<string>(expected, result);
        }

        [TestMethod]
        public void PlayerRestartTheGame()
        {
            string result = TestSetup("1234", "0123", "restart\r\nexit");

            string expected =
                string.Format(WelcomeMessage + "\r\n" +
                              EnterGuess +
                              WelcomeMessage + "\r\n" +
                              EnterGuess +
                              "Good Bye!" + "\r\n");

            Assert.AreEqual<string>(expected, result);
        }

        [TestMethod]
        public void PlayerEnterIncorrectCommand()
        {
            string result = TestSetup("1234", "0123", "incorrectCommand\nexit");

            string expected =
                string.Format(WelcomeMessage + "\r\n" +
                              EnterGuess +
                              "Incorrect guess or command!\r\n" +
                              EnterGuess +
                              "Good Bye!" + "\r\n");

            Assert.AreEqual<string>(expected, result);
        }



        [TestMethod]
        public void PlayerUsedTwoCheatsAndExit()
        {
            var result = TestSetup("1234", "0123", "help\r\nhelp\r\n1234\r\nexit");

            string expected =
                 string.Format(
              WelcomeMessage + "\r\n" +
                              EnterGuess
               + "The number looks like 1XXX.\r\n"
               + EnterGuess
               + "The number looks like 12XX.\r\n"
               + EnterGuess
               + "Congratulations! You guessed the secret number in 1 attempts and 2 cheats.\r\n"
               + "You are not allowed to enter the top scoreboard.\r\n"
               + WelcomeMessage + "\r\n"
               + EnterGuess
               + "Good Bye!" + "\r\n");

            Assert.AreEqual<string>(expected, result);
        }

        [TestMethod]
        public void WrongNumberShouldReturnBullsAndCowsCount()
        {
            var result = TestSetup("1234", "0123", "2314\r\nexit");

            string expected =
                 string.Format(
                "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.\r\n"
                + "Enter your guess or command: "
                + "Wrong number! Bulls: 1, Cows: 3\r\n"
                + "Enter your guess or command: "
                + "Good Bye!\r\n");

            Assert.AreEqual<string>(expected, result);
        }
    }
}