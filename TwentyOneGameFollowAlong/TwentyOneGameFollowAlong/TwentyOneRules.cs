using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOneGameFollowAlong
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
            int value = Hand.Sum(x => _cardValues[x.Face]); // get hand value assuming 1 or 0 aces with ACE val=1
            result[0] = value;                              // if array len = 1, then we have no aces and we're done
            if (result.Length == 1)
                return result;
            for (int i = 1; i < result.Length; i++)
            {
                value += (i * 10);  // For each Ace, then add 10 * the position of the value
                result[i] = value;  // On 1st Ace, value gets 10 added, on 2nd Ace, value gets 20 added and so on
            }
            return result;
        }

        public static bool CheckForBlackJack(List<Card> Hand)
        {
            int[] possibleValues = GetAllPossibleHandValues(Hand);
            int value = possibleValues.Max(); // BUG!! could return value greater than 21 in a hand that has blackjack
                                              // MAX of ACE,ACE,9 returns 31 even though 21 is possible
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
