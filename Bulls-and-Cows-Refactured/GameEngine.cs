namespace BullsAndCowsGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BullsAndCowsGame.Interfases;

    public class GameEngine
    {
        private const int NumberLength = 4;
        private const int MaxCheats = 4;
        private const int ScoreBoardSize = 5;
        private string helpPattern;
        private StringBuilder helpNumber;
        private string generatedNumber;
        private List<Player> scoreboard;
        private IRandomGenerator generator;

        public GameEngine(IRandomGenerator generator)
        {
            this.scoreboard = new List<Player>();
            this.generator = generator;
        }

        public void NewGame()
        {
            this.PrintWelcomeMessage();
            this.GenerateNumber();
            int attempts = 0;
            int cheats = 0;
            this.helpNumber = new StringBuilder(new string('X', NumberLength));
            this.helpPattern = null;

            this.NewMove(attempts, cheats);
        }

        public void NewMove(int attempts, int cheats)
        {
            MessageManager.EnterCommandMessage();
            string playerInput = Console.ReadLine();
            PlayerCommand enteredCommand = this.PlayerInputToPlayerCommand(playerInput);
            bool needNextMove = true;

            switch (enteredCommand)
            {
                case PlayerCommand.Top:
                    {
                        this.PrintScoreboard();
                        break;
                    }
                case PlayerCommand.Help:
                    {
                        cheats = this.ShowHelp(cheats);
                        break;
                    }
                case PlayerCommand.Guess:
                    {
                        attempts++;
                        int bullsCount;
                        int cowsCount;
                        this.CalculateBullsAndCowsCount(playerInput, generatedNumber, out bullsCount, out cowsCount);
                        if (bullsCount == NumberLength)
                        {
                            this.PrintCongratulateMessage(attempts, cheats);
                            this.FinishGame(attempts, cheats);
                            this.NewGame();
                            needNextMove = false;
                        }
                        else
                        {
                            Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bullsCount, cowsCount);
                        }
                        break;
                    }
                case PlayerCommand.Restart:
                    {
                        Console.WriteLine();
                        this.NewGame();
                        needNextMove = false;
                        break;
                    }
                case PlayerCommand.Exit:
                    {
                        Console.WriteLine();
                        Console.WriteLine("Good bye!");
                        needNextMove = false;
                        break;
                    }
                case PlayerCommand.Other:
                    {
                        PrintWrongCommandMessage();
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            if (needNextMove)
            {
                this.NewMove(attempts, cheats);
            }
        }

        public void Start()
        {
            this.NewGame();
        }

        private void GenerateNumber()
        {
            this.generatedNumber = this.generator.Generate(NumberLength);
        }

        private PlayerCommand PlayerInputToPlayerCommand(string playerInput)
        {
            PlayerCommand result;
            switch (playerInput.ToLower())
            {
                case "top":
                    result = PlayerCommand.Top;
                    break;
                case "restart":
                    result = PlayerCommand.Restart;
                    break;
                case "help":
                    result = PlayerCommand.Help;
                    break;
                case "exit":
                    result = PlayerCommand.Exit;
                    break;
                default:
                    if (this.IsValidInput(playerInput))
                    {
                        result = PlayerCommand.Guess;
                    }
                    else
                    {
                        result = PlayerCommand.Other;
                    }
                    break;
            }

            return result;
        }

        private void PrintWelcomeMessage()
        {
            MessageManager.WelcomeMessage();
        }

        private void PrintWrongCommandMessage()
        {
            MessageManager.WrongCommandMessage();
        }

        private void PrintCongratulateMessage(int attempts, int cheats)
        {
            MessageManager.CongratulateMessage(attempts, cheats);
        }

        private int ShowHelp(int cheats)
        {
            if (cheats < MaxCheats)
            {
                this.RevealDigit(cheats);
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
            if (this.helpPattern == null)
            {
                this.GenerateHelpPattern();
            }

            int digitToReveal = this.helpPattern[cheats] - '0';
            this.helpNumber[digitToReveal - 1] = this.generatedNumber[digitToReveal - 1];
        }

        private void GenerateHelpPattern()
        {
            string[] helpPaterns = 
            {
                "1234", "1243", "1324", "1342", "1432", "1423",
                "2134", "2143", "2314", "2341", "2431", "2413",
                "3214", "3241", "3124", "3142", "3412", "3421",
                "4231", "4213", "4321", "4312", "4132", "4123"
            };

            Random randomNumberGenerator = new Random(DateTime.Now.Millisecond);
            int randomPaternNumber = randomNumberGenerator.Next(helpPaterns.Length - 1);
            this.helpPattern = helpPaterns[randomPaternNumber];
        }

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
            if (playerInput == string.Empty || playerInput.Length != NumberLength)
            {
                return false;
            }

            for (int i = 0; i < playerInput.Length; i++)
            {
                char currentChar = playerInput[i];
                if (!char.IsDigit(currentChar))
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
                this.AddPlayerToScoreboard(playerName, attempts);
                this.PrintScoreboard();
            }
            else
            {
                Console.WriteLine("You are not allowed to enter the top scoreboard.");
            }
        }

        private void AddPlayerToScoreboard(string playerName, int attempts)
        {
            Player player = new Player(playerName, attempts);
            this.scoreboard.Add(player);
        }

        private void PrintScoreboard()
        {
            if (this.scoreboard.Count == 0)
            {
                Console.WriteLine("Top scoreboard is empty.");
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                this.scoreboard.Sort();
                int scoreBoardEnd = Math.Min(this.scoreboard.Count, ScoreBoardSize);

                for (int i = 0; i < scoreBoardEnd; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} guess" + ((this.scoreboard[i].Attempts == 1) ? string.Empty : "es"), i + 1, this.scoreboard[i].Name, this.scoreboard[i].Attempts);
                }
            }
        }
    }
}