using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BullsAndCowsGame.Interfases;

namespace BullsAndCowsGame
{
    class GameEngine
    {
        private const int NumberLength = 4;
        private const int ScoreBoardSize = 5;
        private string helpPattern;
        private StringBuilder helpNumber;
        private string generatedNumber;
        private List<Player> scoreboard;
        private IRandomGenerator generator;


        // Iuli
        public GameEngine(IRandomGenerator generator)
        {
            scoreboard = new List<Player>();
            this.generator = generator;
        }

        private void GenerateNumber()
        {
            this.generatedNumber = this.generator.Generate(NumberLength);
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
                        if (IsValidInput(playerInput))
                        {
                            return PlayerCommand.Guess;
                        }
                        return PlayerCommand.Other;
                    }
            }
        }

        private void PrintWelcomeMessage()
        {
            MessageManager.WelcomeMessage();
        }

        private void PrintWrongCommandMessage()
        {
            MessageManager.WrongCommandMessage();
        }

        // Tsvety
        private void PrintCongratulateMessage(int attempts, int cheats)
        {
            MessageManager.CongratulateMessage(attempts, cheats);
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
                helpNumber = new StringBuilder(new String('X', NumberLength));
                helpPattern = null;
                do
                {
                    MessageManager.EnterCommandMessage();
                    string playerInput = Console.ReadLine();
                    enteredCommand = PlayerInputToPlayerCommand(playerInput);

                    //switch (enteredCommand)
                    //{
                    //    case PlayerCommand.Top:
                    //    {
                    //        PrintScoreboard();
                    //        break;
                    //    }
                    //    case PlayerCommand.Help:
                    //    {
                    //        cheats = PokajiHelp(cheats);
                    //        break;
                    //    }
                    //    case PlayerCommand.Guess:
                    //    {
                    //            attempts++;
                    //            int bullsCount;
                    //            int cowsCount;
                    //            CalculateBullsAndCowsCount(playerInput, generatedNumber, out bullsCount, out cowsCount);
                    //            if (bullsCount == NumberLength)
                    //            {
                    //                PrintCongratulateMessage(attempts, cheats);
                    //                FinishGame(attempts, cheats);
                    //                break;
                    //            }
                    //            else
                    //            {
                    //                Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bullsCount, cowsCount);
                    //            }
                    //        break;
                    //    }
                    //    case PlayerCommand.Restart:
                    //    {
                    //        // TODO
                    //        break;
                    //    }
                    //    case PlayerCommand.Exit:
                    //    {
                    //        // TODO
                    //        break;
                    //    }
                    //    case PlayerCommand.Other:
                    //    {
                    //        PrintWrongCommandMessage();
                    //        break;
                    //    }
                    //    default:
                    //    {
                    //        throw new NotImplementedException();
                    //    }
                    //}

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
                MessageManager.HelpMessage(this.helpNumber.ToString());
            }
            else
            {
                MessageManager.NoMoreHelpMessage();
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
            int randomPaternNumber = randomNumberGenerator.Next(helpPaterns.Length - 1);
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
            scoreboard.Add(player);
        }

        private void PrintScoreboard()
        {
            if (scoreboard.Count == 0)
            {
                Console.WriteLine("Top scoreboard is empty.");
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                scoreboard.Sort();
                int scoreBoardEnd = Math.Min(scoreboard.Count, ScoreBoardSize);

                for (int i = 0; i < scoreBoardEnd; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} guess" + ((scoreboard[i].Attempts == 1) ? "" : "es"), i + 1, scoreboard[i].Name, scoreboard[i].Attempts);
                }

                //foreach (Player p in scoreboard)
                //{
                //    Console.WriteLine("{0}. {1} --> {2} guess" + ((p.Attempts == 1) ? "" : "es"), i++, p.Name, p.Attempts);
                //}
            }
        }
    }
}