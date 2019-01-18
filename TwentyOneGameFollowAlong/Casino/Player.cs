using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <see cref="float"/> testing
    /// </remarks>
    public class Player
    {
        // Constructor chaining or creating a consructor call chain.
        public Player(string name) : this(name, 100) // 100 is default balance if beginningBalance not provided
        {
        }

        // 
        /// <summary>
        /// This is a constructor to create a Player that takes two parms.  We assign them to properties in the class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginningBalance"></param>
        public Player(string name, int beginningBalance)
        {
            //Hand = new List<Card>();
            Balance = beginningBalance;
            Name = name;
        }
        //private List<Card> _hand = new List<Card>();
        //public List<Card> Hand { get { return _hand; } set { _hand = value; } }

        public List<Card> Hand { get; private set; } = new List<Card>();

        public int Balance { get; set; }
        public string Name { get; set; }
        public bool IsActivelyPlaying { get; set; }
        public bool Stay { get; set; }      // probably should go in a 21player class but for now keep here
        public Guid Id { get; set; }

        public bool Bet(int amount)
        {
            if (Balance - amount < 0)
            {
                Console.WriteLine("You do not have enough in the bank to place a bet that size.");
                return false;
            }
            else
            {
                Balance -= amount;
                return true;
            }
        }
        
        // overloaded operator method to Add a player
        public static Game operator +(Game game, Player player)
        {
            game.Players.Add(player);  // remember, Players is a list and Add is a function that works on lists
            return game;
        }

        //// overloaded operator method to Remove a player
        public static Game operator -(Game game, Player player)
        {
            game.Players.Remove(player);  // Players is a list and Remove is a function that works on lists
            return game;
        }
    }
}
