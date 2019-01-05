using System;
using Casino;
using Casino.TwentyOne;
using System.IO;

namespace TwentyOneGameFollowAlong 
{
    class Program
    {
        static void Main(string[] args)
        {
            //string text = "Here is some text";
            // file will be created or overwritten.  @ means read string exactly as written.  no escape chars needed
            //File.WriteAllText(@"C:\users\andy\logs\log.txt",text); 
            //string text = File.ReadAllText(@"C:\users\andy\logs\log.txt");

            //DateTime yearOfBirth = new DateTime(1995, 5, 23, 8, 32, 45);
            //DateTime yearOfGraduation = new DateTime(2013, 6, 1, 16, 34, 22);

            //TimeSpan ageAtGraduation = yearOfGraduation - yearOfBirth;
            
            // when not using the "using System"
            //System.Console.WriteLine("Welcome to the Grand Hotel and Casino. Let's start by telling me your name.");
            Console.WriteLine("Welcome to the Grand Hotel and Casino. Let's start by telling me your name.");
            string playerName = Console.ReadLine();

            Console.WriteLine("And how much money did you bring today?");
            int bank = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Hello, {0}.  Would you like to join a game of 21 right now?", playerName);
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes" || answer == "yeah" || answer == "ya" || answer == "y")
            {
                Player player = new Player(playerName, bank);  // use constructor to create player
                Game game = new TwentyOneGame();  // polymorphism happening here
                game += player; // using overloaded operators in Game to add player to the list, players
                player.IsActivelyPlaying = true;
                while (player.IsActivelyPlaying && player.Balance > 0)
                {
                    // Play one hand
                    game.Play(); // most everything will happen in the Play method to keep the main method clean
                }
                game -= player;
                Console.WriteLine("Thank you for playing!");
            }
            Console.WriteLine("Feel free to look around the casino.  Bye for now.");
            Console.Read();
            
        }
        
    }
}
