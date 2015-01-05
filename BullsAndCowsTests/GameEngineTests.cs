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

        GameEngine gameEngine;

        private GameEngine Setup(string generateNumber, string helpPattern)
        {
            IRandomGenerator generator = new FakeNumberGenerator(generateNumber, helpPattern);
            gameEngine = new GameEngine(generator);
            return gameEngine;
        }

        [TestMethod]
        public void NewGameShouldPrintWelcomeMessage()
        {
            gameEngine = Setup("1234", "0123");
            StringReader sr = new StringReader("1334\nexit");

            using (sw)
            {
                using (sr)
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    // Act
                    gameEngine.NewGame();

                    // Assert
                    var result = sw.ToString();
                    string expected =
                             string.Format(
                           "Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.Use 'top' to view the top scoreboard, 'restart' to start a new game and 'help' to cheat and 'exit' to quit the game.\r\nEnter your guess or command: Wrong number! Bulls: 3, Cows: 0\r\nEnter your guess or command: \r\nGood bye!\r\n");

                    Assert.AreEqual<string>(expected, result);
                }
            }
        }
    }
}
