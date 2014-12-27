﻿namespace BullsAndCowsGame
{
    using System;

    public class Player : IComparable<Player>
    {
        public string Name { get; set; }

        public int Attempts { get; set; }

        private DateTime TimeAdded { get; set; }

        public Player(string playerName, int attempts)
        {
            this.Name = playerName;
            this.Attempts = attempts;
            this.TimeAdded = DateTime.Now;
        }

        public int CompareTo(Player other)
        {
            if (other == null)
            {
                return 1;
            }

            int value = other.Attempts - this.Attempts;

            if (value == 0)
            {
                value = other.TimeAdded.CompareTo(this.TimeAdded);
            }
            return value;
        }
    }
}
