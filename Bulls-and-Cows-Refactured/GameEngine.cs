namespace BullsAndCowsGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BullsAndCowsGame.Interfaces;

    public class GameEngine
    {
        private const int NumberLength = 4;
        private const int MaxCheats = 4;
        private const int ScoreBoardSize = 5;
        private const int HelpPatternMinDigit = 0;
        private const int HelpPatternMaxDigit = NumberLength - 1;
        private const int StartAttempts = 0;
        private const int StartCheatsUsed = 0;
        private string helpPattern;
        private StringBuilder helpNumber;
        private string generatedNumber;
        private List<Player> scoreboard;
        private readonly IRandomGenerator generator;

        public GameEngine(IRandomGenerator generator)
        {
            this.scoreboard = new List<Player>();
            this.generator = generator;
        }

        public void NewGame()
        {
            MessageManager.WelcomeMessage();
            this.GenerateNumber();
            this.helpNumber = new StringBuilder(new string('X', NumberLength));
            this.helpPattern = null;

            this.NewMove(StartAttempts, StartCheatsUsed);
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
                            MessageManager.CongratulateMessage(attempts, cheats);
                            this.FinishGame(attempts, cheats);
                            this.NewGame();
                            needNextMove = false;
                        }
                        else
                        {
                            MessageManager.WrongNumberMessage(bullsCount, cowsCount);
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
                        MessageManager.WrongCommandMessage();
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
                    result = this.IsValidInput(playerInput) ? PlayerCommand.Guess : PlayerCommand.Other;
                    break;
            }

            return result;
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

            int digitToReveal = int.Parse(this.helpPattern[cheats] + string.Empty); //this.helpPattern[chears] is char + empty == string
            this.helpNumber[digitToReveal] = this.generatedNumber[digitToReveal];
        }

        private void GenerateHelpPattern()
        {
            this.helpPattern = this.generator.GenerateNumberByDigits(NumberLength, HelpPatternMinDigit, HelpPatternMaxDigit);
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

            return playerInput.All(char.IsDigit);
        }

        private void FinishGame(int attempts, int cheats)
        {
            if (cheats == 0)
            {
                MessageManager.EnterNameToScoreboardMessage();
                string playerName = Console.ReadLine();
                this.AddPlayerToScoreboard(playerName, attempts);
                this.PrintScoreboard();
            }
            else
            {
                MessageManager.NotAllowedToEnterNameToScoreboardMessage();
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
                MessageManager.ScoreboardIsEmptyMessage();
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                this.scoreboard.Sort();
                int scoreBoardEnd = Math.Min(this.scoreboard.Count, ScoreBoardSize);

                for (int i = 0; i < scoreBoardEnd; i++)
                {
                    MessageManager.PrintScoreboard(i + 1, this.scoreboard[i].Attempts, this.scoreboard[i].Name);
                }
            }
        }
    }
}