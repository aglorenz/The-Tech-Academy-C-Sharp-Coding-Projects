using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public abstract class Game // Game is a *base* class AKA an abstract class.  An abstract class can never be 
                               // instantiated. It can never be an object. It's only meant to be inherited from. We 
                               // are never going to have an instance of game.
    {
        // make sure there is always an instantiated empty list to avoid runtime error of trying to add player to 
        // non-existant list
        private List<Player> _players = new List<Player>();
        private Dictionary<Player, int> _bets = new Dictionary<Player, int>();

        // value represents whatever they are setting it as.  It's just a built-in .net thing
        // if someone says game.bets = this dictionary, then this dictionary becomes the value 
        public List<Player> Players { get { return _players; } set { _players = value; } }
        public string Name { get; set; }
        public Dictionary<Player,int> Bets { get { return _bets; } set { _bets = value; } }

        public abstract void Play();  // abstract methods can only exist in an abstract class.  Abstract method 
                                      // contains no implementation. It almost looks like a property. Any class 
                                      // inheriting this class must implement this method. it's sort of a contract 
                                      // between the base and the inheriting class.  

        // virtual means that this method get inherited by an inheriting class, but the inheriting class has the 
        // ability to override it. virtual methods HAVE implementation but they can be overridden.
        public virtual void ListPlayers()
        {
            foreach (Player player in Players)
            {
                Console.WriteLine(player.Name);
            }
        }
    }
}
