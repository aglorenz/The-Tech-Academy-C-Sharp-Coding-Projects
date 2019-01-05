using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.TwentyOne
{
    public class TwentyOneRules
    {
        // underscore naming convention used for private property names
        private static Dictionary<Face, int> _cardValues = new Dictionary<Face, int>()
        {
            [Face.Two] = 2,
            [Face.Three] = 3,
            [Face.Four] = 4,
            [Face.Five] = 5,
            [Face.Six] = 6,
            [Face.Seven] = 7,
            [Face.Eight] = 8,
            [Face.Nine] = 9,
            [Face.Ten] = 10,
            [Face.Jack] = 10,
            [Face.Queen] = 10,
            [Face.King] = 10,
            [Face.Ace] = 1
        };

        private static int[] GetAllPossibleHandValues(List<Card> Hand)
        {
            int aceCount = Hand.Count(x => x.Face == Face.Ace); // get the number of Aces
            // unique combinations of ACE (ace=1 or ace=11)   if 1,11 or 11,1 -- only count as 1 combination
            int[] result = new int[aceCount + 1]; // initialize the size of the array e.g., 1 Ace = 1,11 = 2 entries
            int baseValue = Hand.Sum(x => _cardValues[x.Face]); // get hand value where all aces count as 1
            int aceValue = 0;                                   // used when counting Aces as 11
            result[0] = baseValue;                              // if array len = 1, then we have no aces and we're done
            if (result.Length == 1)  // if no Aces, then we are done, return
                return result;
            for (int i = 1; i <= aceCount; i++)  // for each Ace...
            {
                aceValue = baseValue + (i * 10);  // add 10 to the base sum (count each Ace as 11 now )
                result[i] = aceValue;  // On 1st Ace, value gets 10 added, on 2nd Ace, value gets 20 added and so on
            }
            return result;
        }

        // Blackjack is defined as an Ace card and a Face card in a hand of two cards.
        public static bool CheckForBlackJack(List<Card> Hand)
        {
            int[] possibleValues = GetAllPossibleHandValues(Hand);
            int value = possibleValues.Max(); 
                                              
            if (value == 21) return true;
            else return false;
        }

        public static bool IsBusted(List<Card> Hand)
        {
            int value = GetAllPossibleHandValues(Hand).Min();
            if (value > 21)
                return true;
            else return false;
        }

        public static bool ShouldDealerStay(List<Card> Hand)
        {
            int[] possibleHandValues = GetAllPossibleHandValues(Hand);
            foreach (int value in possibleHandValues)
            {
                if (value > 16 && value < 22)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool? CompareHands(List<Card> PlayerHand, List<Card> DealerHand)
        {
            int[] playerResults = GetAllPossibleHandValues(PlayerHand);
            int[] dealerResults = GetAllPossibleHandValues(DealerHand);

            int playerScore = playerResults.Where(x => x < 22).Max();
            int dealerScore = dealerResults.Where(x => x < 22).Max();

            if (playerScore > dealerScore) return true;
            else if (playerScore < dealerScore) return false;
            else return null;
        }
    }
}
