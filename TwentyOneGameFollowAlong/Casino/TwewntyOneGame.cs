using System;
using System.Collections.Generic;
using System.Linq;
using Casino.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace Casino.TwentyOne
{
    // A class can inherit only one base class (Game).  But can inherit as many interfaces as necessary 
    public class TwentyOneGame : Game, IWalkAway
    {
        internal TwentyOneDealer Dealer { get; set; }

        public override void Play()
        {
            Dealer = new TwentyOneDealer(); // Create a dealer
            foreach (Player player in Players) // initialize players
            {
                player.Hand = new List<Card>();  // Give each player a new empty hand
                player.Stay = false;             // and reset stay (false)
            }
                    
            Dealer.Hand = new List<Card>(); // Dealer gets an empty hand.
            Dealer.Stay = false;
            Dealer.Deck = new Deck();  // Refresh the deck every single round
            Dealer.Deck.Shuffle();

            foreach (Player player in Players)  // protects program to add players in the future
            {
                bool validAnswer = false;
                int bet = 0;
                while (!validAnswer)
                {
                    Console.Write("\nPlace your bet!\n>> ");
                    validAnswer = int.TryParse(Console.ReadLine(), out bet);
                    if (!validAnswer) Console.WriteLine("Please enter only digits with no decimal");
                }
                if (bet < 0)
                {
                    Console.WriteLine();
                    throw new FraudException("Security! Kick this person out for cheating!");
                }

                bool successfullyBet = player.Bet(bet);
                if (!successfullyBet)  // not enough in bank to cover the bet
                {
                    return; // end this method.  It is over!!
                }
                Bets[player] = bet;
            }
            // Deal a hand to each player (2 cards) and the Dealer
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("\nDealing...");
                foreach (Player player in Players)
                {
                    Console.Write("{0}: ", player.Name);
                    Dealer.Deal(player.Hand);
                    if (i == 1) // if second card in initial deal
                    {
                        bool blackJack = TwentyOneRules.CheckForBlackJack(player.Hand);
                        if (blackJack)
                        {
                            Console.WriteLine("Blackjack! {0} wins {1}", player.Name, Bets[player] * 1.5);
                            player.Balance += Convert.ToInt32((Bets[player] * 1.5) + Bets[player]);
                            return;
                        }
                    }
                }
                // Deal a card to the Dealer
                Console.Write("Dealer: ");
                Dealer.Deal(Dealer.Hand);
                if (i == 1) // if second card in initial deal
                {
                    bool blackJack = TwentyOneRules.CheckForBlackJack(Dealer.Hand);
                    if (blackJack)
                    {
                        Console.WriteLine("Dealer has BlackJack! Everyone loses!");
                        // Add everybody's bet to the Dealer balance
                        foreach (KeyValuePair<Player, int> entry in Bets)
                        {
                            Dealer.Balance += entry.Value;
                        }
                        return;
                    }
                }
            } // end of initial deal of two cards

            foreach (Player player in Players)
            {
                while (!player.Stay)
                {
                    Console.WriteLine("\nYour cards are:");
                    foreach (Card card in player.Hand)
                    {
                        Console.Write("{0} ", card.ToString());  // using a custom To.String we overrode in Card class
                    }

                    Console.WriteLine("\n\nHit or stay?");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "stay")
                    {
                        player.Stay = true;
                        break;
                    }
                    else if (answer == "hit")
                    {
                        Dealer.Deal(player.Hand);  // deal one card
                    }
                    bool busted = TwentyOneRules.IsBusted(player.Hand);
                    if (busted)
                    {
                        Dealer.Balance += Bets[player];
                        Console.WriteLine("{0} Busted! You lose your bet of {1}. Your balance is now {2}",
                            player.Name, Bets[player], player.Balance);
                        Console.WriteLine("\nDo you want to play again?");
                        answer = Console.ReadLine().ToLower();
                        if (answer == "yes" || answer == "yeah" || answer == "ya" || answer == "y")
                        {
                            player.IsActivelyPlaying = true;
                            return; // to main()
                        }
                        else
                        {
                            player.IsActivelyPlaying = false;
                            return; // to main()
                        }
                    }
                }
            }
            Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand);
            Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);

            // as long as dealer is not busted and does not want to stay, keep playing
            while (!Dealer.isBusted && !Dealer.Stay)
            {
                Console.WriteLine("\nDealer is hitting...");
                Dealer.Deal(Dealer.Hand);
                Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand);
                Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);
            }
            if (Dealer.Stay)
            {
                Console.WriteLine("\nDealer is staying.");
            }
            if (Dealer.isBusted)
            {
                Console.WriteLine("\nDealer is busted.");
                // Give all the players their winnings
                // loop through each Player and associated Bet
                // Find players that match that name(where always returns a list, take the first one (only one is ever
                // returned), and add the winning plus the original bet amount (i.e., bet * 2)  to player's balance
                foreach (KeyValuePair<Player, int> entry in Bets)
                {
                    Console.WriteLine("{0} won {1}!", entry.Key.Name, entry.Value);
                    Players.Where(x => x.Name == entry.Key.Name).First().Balance += (entry.Value * 2);
                    Dealer.Balance -= entry.Value;
                }
                return;
            }
            
            // loop through all the players and compare the players' hands to the dealer's hand
            // 3 possibilities can occur
            // 1) player could have hand greater than dealer 
            // 2) dealer could have hand greater than player
            // 3) could have a tie
            foreach (Player player in Players)
            {
                // nullable boolean to allow for 3 choices
                bool? playerWon = TwentyOneRules.CompareHands(player.Hand, Dealer.Hand); 
                if (playerWon == null)
                {
                    Console.WriteLine("Push! No one wins!"); // return bets to players
                    player.Balance += Bets[player];
                    //Bets.Remove(player);  Bets table is reset at the beginning of loop.  No need here.
                }
                else if (playerWon == true)
                {
                    Console.WriteLine("{0} won {1}!", player.Name, Bets[player]);
                    player.Balance += (Bets[player] * 2); // return original bet plus winning amt (original bet)
                    Dealer.Balance -= (Bets[player]);
                }
                else
                {
                    Console.WriteLine("Dealer wins{0} ", Bets[player]);
                    Dealer.Balance += Bets[player];
                }
                Console.WriteLine("\nPlay again?");
                string answer = Console.ReadLine().ToLower();
                if (answer == "yes" || answer == "yeah" || answer == "ya" || answer == "y")
                {
                    player.IsActivelyPlaying = true;
                }
                else
                {
                    player.IsActivelyPlaying = false;
                }
            }
        }
        // In order to implement Play (an abstract method) in the inherited class, simply do the method the same 
        // way (public void  Play()) but use override keyword. This satisfies the contract that we will define 
        // this method here. 

        //// overriding ListPlayers implementation in the Class Game.  Customize by writing out a header
        public override void ListPlayers()
        {
            Console.WriteLine("21 Players:");
            base.ListPlayers();
        }

        public void WalkAway(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
