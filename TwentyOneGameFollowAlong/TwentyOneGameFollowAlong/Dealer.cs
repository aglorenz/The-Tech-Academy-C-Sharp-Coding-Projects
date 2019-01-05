using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwentyOneGameFollowAlong
{
    public class Dealer
    {
        public string Name { get; set; }
        public Deck Deck { get; set; }  // Dealer *HAS* a deck not is a deck so we include it as a property rather than inherit the deck
                                        // Err on the side of adding a class as a a property of another class over inheriting from it.
                                        // This is exactly what we did here with deck.
        public int Balance { get; set; }

        public void Deal(List<Card> Hand)
        {
            Hand.Add(Deck.Cards.First());
            string card = string.Format(Deck.Cards.First().ToString());
            Console.WriteLine(card);
            using (StreamWriter file = new StreamWriter(@"c:\users\andy\logs\log.txt", true)) // true indicates append
            {
                file.WriteLine(card);
            }  // once this reached, memory resources are disposed of
                Deck.Cards.RemoveAt(0);
        }
    }
}
