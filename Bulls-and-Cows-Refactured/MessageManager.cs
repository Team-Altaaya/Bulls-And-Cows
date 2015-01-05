namespace BullsAndCowsGame
{
    using System;

    public static class MessageManager
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
            PrintLine(string.Format(
                "Congratulations! You guessed the secret number in {0} attempts{1}",
                attempts,
                (cheats == 0) ? "." : " and " + cheats + " cheats."));
        }

        public static void HelpMessage(string helpNumber)
        {
            PrintLine(string.Format("The number looks like {0}.", helpNumber));
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

        public static void EnterNameToScoreboardMessage()
        {
            Print("Please enter your name for the top scoreboard: ");
        }

        public static void NotAllowedToEnterNameToScoreboardMessage()
        {
            PrintLine("You are not allowed to enter the top scoreboard.");
        }

        public static void ScoreboardIsEmptyMessage()
        {
            PrintLine("Top scoreboard is empty.");
        }

        public static void WrongNumberMessage(int bullsCount, int cowsCount)
        {
            PrintLine(string.Format("Wrong number! Bulls: {0}, Cows: {1}", bullsCount, cowsCount));
        }

        public static void PrintScoreboard(int orderNumber, int attempts, string name)
        {
            string guessesWord = (attempts == 1) ? "guess" : "guesses";
            PrintLine(string.Format("{0}. {1} --> {2} " + guessesWord, orderNumber, name, attempts));
        }
    }
}
