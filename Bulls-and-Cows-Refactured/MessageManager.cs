using System;

namespace BullsAndCowsGame
{
    class MessageManager
    {
        public static string WelcomeMessage ()
        {
            return "Welcome to “Bulls and Cows” game. " +
            "Please try to guess my secret 4-digit number." +
            "Use 'top' to view the top scoreboard, 'restart' " +
            "to start a new game and 'help' to cheat and 'exit' to quit the game.";
        }

        public static string WrongCommandMessage()
        {
            return "Incorrect guess or command!";
        }
    }
}
