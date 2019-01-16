using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Deck
    {
        public Deck()
        {
            Cards = new List<Card>();
            
            // Easy way to create a deck of 52 cards
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Card card = new Card();
                    card.Face = (Face)i; // casting j, which is an int, to enum type Face!
                    card.Suit = (Suit)j; // Casting i, which is an int, to enum type Suit!
                    Cards.Add(card);     // Add the card to the Cards list
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Card> Cards { get; set; }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="times"></param>
        public void Shuffle(int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                List<Card> TempList = new List<Card>();
                Random random = new Random();

                while (Cards.Count > 0)
                {
                    int randomIndex = random.Next(0, Cards.Count);
                    TempList.Add(Cards[randomIndex]);
                    Cards.RemoveAt(randomIndex);
                }
                Cards = TempList;

            }
        }

    }
}
