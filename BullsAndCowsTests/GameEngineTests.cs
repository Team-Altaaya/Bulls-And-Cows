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
        public void NewGameShouldPrintWelcomeMessage()
        {
            var result = TestSetup("1234", "0123", "1334\nexit");

            string expected =
                 string.Format(
            "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.\r\nEnter your guess or command: Wrong number! Bulls: 3, Cows: 0\r\nEnter your guess or command: \r\nGood bye!\r\n");

            Assert.AreEqual<string>(expected, result);
        }

        [TestMethod]
        public void CorrectGuessWithoutUsingCheatsShouldAllowPlayerToWriteNameInScoreboard()
        {
            string result = TestSetup("1234", "0123", "1234\ntsvety\nexit");
            
            string expected =
                string.Format(WelcomeMessage +
                              EnterGuess +
                              "Congratulations! You guessed the secret number in 1 attempts.\n" +
                              "Please enter your name for the top scoreboard: " +
                              "Scoreboard:\n" +
                              "1. tsvety --> 1 guess\n" +
                              WelcomeMessage +
                              EnterGuess +
                              "Good Bye! "+ Environment.NewLine);
           
            Assert.AreEqual<string>(expected, result);
        }

        [TestMethod]
        public void PlayerRestartTheGame()
        {
            string result = TestSetup("1234", "5678", "restart\nexit");

            string expected =
                 string.Format(
            "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.\r\nEnter your guess or command: Wrong number! Bulls: 3, Cows: 0\r\nEnter your guess or command: \r\nGood bye!\r\n");

            Assert.AreEqual<string>(expected, result);
        }

        [TestMethod]
        public void PlayerEnterIncorrectCommand()
        {
            string result = TestSetup("1234", "0123", "incorrectCommand");

            string expected =
                string.Format(WelcomeMessage +
                              EnterGuess +
                              "You entered incorrect command.\n" +
                              "You can choose from one of the following commands: \n" +
                              "'restart', 'help', 'top', 'exit'." +
                              WelcomeMessage +
                              EnterGuess +
                              "Good Bye!" + Environment.NewLine);

            Assert.AreEqual<string>(expected, result);
        }
    }
}