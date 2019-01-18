using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public struct Card
    {
        public Suit Suit { get; set; }
        public Face Face { get; set; }

        public override string ToString() => string.Format("{0} of {1}", Face, Suit);
        
    }
    public enum Suit  // you can assign numbers for future comparison purposes (see below)
    {
        //Clubs = 4, Diamonds = 10, Hearts = 12, Spades = 15
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    public enum Face
    {
        Two, 
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
}
