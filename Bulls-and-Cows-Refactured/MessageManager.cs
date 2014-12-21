using System;

namespace BullsAndCowsGame
{
    class MessageManager
    {
        public static void WelcomeMessage()
        {
            PrintLine("Welcome to “Bulls and Cows” game. " +
            "Please try to guess my secret 4-digit number." +
            "Use 'top' to view the top scoreboard, 'restart' " +
            "to start a new game and 'help' to cheat and 'exit' to quit the game.");
        }

        public static void WrongCommandMessage()
        {
            PrintLine("Incorrect guess or command!");
        }

        public static void CongratulateMessage(int attempts, int cheats)
        {
            PrintLine(String.Format(
                "Congratulations! You guessed the secret number in {0} attempts{1}",
                attempts,
                (cheats == 0) ? "." : " and " + cheats + " cheats."));
        }

        public static void HelpMessage(string helpNumber)
        {
            PrintLine(String.Format("The number looks like {0}.", helpNumber));
        }

        public static void NoMoreHelpMessage()
        {
            PrintLine("You are not allowed to ask for more help!");
        }

        public static void Print(string message)
        {
            Console.Write(message);
        }

        public static void PrintLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void EnterCommandMessage()
        {
            Print("Enter your guess or command: ");
        }
    }
}
