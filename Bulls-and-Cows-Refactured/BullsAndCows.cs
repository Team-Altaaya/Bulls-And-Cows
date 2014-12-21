using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BullsAndCowsGame
{
    class BullsAndCows
    {
        private enum PlayerCommand
        {
            Top,
            Restart,
            Help,
            Exit,
            Other
        }

        private const int NumberLength = 4;
        private string helpPattern;
        private StringBuilder helpNumber;
        private string generatedNumber;
        private rating<Player> rating;

        // Iuli
        public BullsAndCows()
        {
            rating = new rating<Player>();
        }

        private void GenerateNumber()
        {
            StringBuilder number = new StringBuilder(NumberLength);
            Random randomNumberGenerator = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < NumberLength; i++)
            {
                int randomDigit = randomNumberGenerator.Next(9);
                number.Append(randomDigit);
            }

            this.generatedNumber = number.ToString();
        }

        private PlayerCommand PlayerInputToPlayerCommand(string playerInput)
        {
            switch (playerInput.ToLower())
            {
                case "top":
                {
                    return PlayerCommand.Top;
                }
                case "restart":
                {
                    return PlayerCommand.Restart;
                }
                case "help":
                {
                    return PlayerCommand.Help;
                }
                case "exit":
                {
                    return PlayerCommand.Exit;
                }
                default:
                {
                    return PlayerCommand.Other;
                }
            }
        }

        private void PrintWelcomeMessage()
        {
            Console.WriteLine(MessageManager.WelcomeMessage());
        }

        private void PrintWrongCommandMessage()
        {
            Console.WriteLine(MessageManager.WrongCommandMessage());
        }

        // Tsvety
        private void PrintCongratulateMessage(int attempts, int cheats)
        {
            Console.Write("Congratulations! You guessed the secret number in {0} attempts", attempts);
            if (cheats == 0)
            {




                Console.WriteLine(".");
            }
            else
            {
                Console.WriteLine(" and {0} cheats.", cheats);
            }
        }

        public void Start()
        {
            PlayerCommand enteredCommand;
            do
            {
                PrintWelcomeMessage();
                GenerateNumber();
                int attempts = 0;
                int cheats = 0;
                helpNumber = new StringBuilder("XXXX");
                helpPattern = null;
                do
                {
                    Console.Write("Enter your guess or command: ");
                    string playerInput = Console.ReadLine();
                    enteredCommand = PlayerInputToPlayerCommand(playerInput);

                    if (enteredCommand == PlayerCommand.Top)
                    {
                        PrintScoreboard();
                    }
                    else if (enteredCommand == PlayerCommand.Help)
                    {
                        cheats = PokajiHelp(cheats);
                    }
                    else
                    {
                        if (IsValidInput(playerInput))
                        {
                            attempts++;
                            int bullsCount;
                            int cowsCount;
                            CalculateBullsAndCowsCount(playerInput, generatedNumber, out bullsCount, out cowsCount);
                            if (bullsCount == NumberLength)
                            {
                                PrintCongratulateMessage(attempts, cheats);
                                FinishGame(attempts, cheats);
                                break;




                            }
                            else
                            {
                                Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bullsCount, cowsCount);
                            }
                        }
                        else
                        {
                            if (enteredCommand != PlayerCommand.Restart && enteredCommand != PlayerCommand.Exit)
                            {
                                PrintWrongCommandMessage();
                            }
                        }



                    }
                }
                while (enteredCommand != PlayerCommand.Exit && enteredCommand != PlayerCommand.Restart);
                Console.WriteLine();
            }
            while (enteredCommand != PlayerCommand.Exit);
            Console.WriteLine("Good bye!");
        }
        private int PokajiHelp(int cheats)
        {
            if (cheats < 4)
            {
                RevealDigit(cheats);
                cheats++;
                Console.WriteLine("The number looks like {0}.", helpNumber);
            }
            else
            {
                Console.WriteLine("You are not allowed to ask for more help!");
            }
            return cheats;
        }
        private void RevealDigit(int cheats)
        {
            if (helpPattern == null)
            {
                generateHelpPattern();
            }
            int digitToReveal = helpPattern[cheats] - '0';
            helpNumber[digitToReveal - 1] = generatedNumber[digitToReveal - 1];
        }

        private void generateHelpPattern()
        {
            string[] helpPaterns = {"1234", "1243", "1324", "1342", "1432", "1423",
                "2134", "2143", "2314", "2341", "2431", "2413",
                "3214", "3241", "3124", "3142", "3412", "3421",
                "4231", "4213", "4321", "4312", "4132", "4123",};


            Random randomNumberGenerator = new Random(DateTime.Now.Millisecond);
            int randomPaternNumber = randomNumberGenerator.Next(helpPaterns.Length-1);
            helpPattern = helpPaterns[randomPaternNumber];
        }

        // Andrei
        private void CalculateBullsAndCowsCount(string playerInput, string generatedNumber, out int bullsCount, out int cowsCount)
        {
            bullsCount = 0;
            cowsCount = 0;
            StringBuilder playerNumber = new StringBuilder(playerInput);
            StringBuilder number = new StringBuilder(generatedNumber);
            for (int i = 0; i < playerNumber.Length; i++)
            {
                if (playerNumber[i] == number[i])
                {
                    bullsCount++;
                    playerNumber.Remove(i, 1);
                    number.Remove(i, 1);
                    i--;
                }
            }

            for (int i = 0; i < playerNumber.Length; i++)
            {
                for (int j = 0; j < number.Length; j++)
                {
                    if (playerNumber[i] == number[j])
                    {
                        cowsCount++;
                        playerNumber.Remove(i, 1);
                        number.Remove(j, 1);
                        j--;
                        i--;
                        break;
                    }
                }
            }
        }

        private bool IsValidInput(string playerInput)
        {
            if (playerInput == String.Empty || playerInput.Length != NumberLength)
            {
                return false;
            }
            for (int i = 0; i < playerInput.Length; i++)
            {
                char currentChar = playerInput[i];
                if (!Char.IsDigit(currentChar))
                {
                    return false;
                }
            }
            return true;
        }

        private void FinishGame(int attempts, int cheats)
        {
            if (cheats == 0)
            {
                Console.Write("Please enter your name for the top scoreboard: ");
                string playerName = Console.ReadLine();
                AddPlayerToScoreboard(playerName, attempts);
                PrintScoreboard();
            }
            else
            {
                Console.WriteLine("You are not allowed to enter the top scoreboard.");
            }
        }

        private void AddPlayerToScoreboard(string playerName, int attempts)
        {
            Player player = new Player(playerName, attempts);
            rating.Add(player);
        }

        private void PrintScoreboard()
        {
            if (rating.Count == 0)
            {
                Console.WriteLine("Top scoreboard is empty.");
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                int i = 1;
                foreach (Player p in rating)
                {
                    Console.WriteLine("{0}. {1} --> {2} guess" + ((p.Attempts == 1)?"":"es"), i++, p.Name, p.Attempts);
                }
            }
        }

        static void Main(string[] args)
        {
            BullsAndCows game = new BullsAndCows();
            game.Start();
        }
    }
}
