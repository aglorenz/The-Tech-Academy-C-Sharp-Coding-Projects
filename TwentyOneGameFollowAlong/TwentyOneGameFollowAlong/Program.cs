using System;
using Casino;
using Casino.TwentyOne;
using System.IO;
using System.Collections.Generic;

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

            // Constructor Chain
            //Player newPlayer = new Player("Andy");  // gets def bal of 100.. making use of constructor chain

            // Using var.  Rule of thumb:  if ever the data type is in question, don't use var.  not so good for
            // readability.
            var newPlayer = new Player("Andy"); // implicitly define variable.  Useful to avoid a lot of typing
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            //or
            var myDictionary1 = new Dictionary<string, string>();

            // declaring constants
            const string casinoName = "Grand Hotel and Casino";
            Console.WriteLine("Welcome to the {0}. Let's start by telling me your name.", casinoName);
            string playerName = Console.ReadLine();

            Console.WriteLine("And how much money did you bring today?");
            int bank = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Hello, {0}.  Would you like to join a game of 21 right now?", playerName);
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes" || answer == "yeah" || answer == "ya" || answer == "y")
            {
                Player player = new Player(playerName, bank);  // use constructor to create player
                player.Id = Guid.NewGuid(); // Create a GUID  and assign to player
                using (StreamWriter file = new StreamWriter(@"c:\users\andy\logs\log.txt", true)) // true indicates append
                {
                    file.WriteLine(player.Id);
                }  // once this reached, memory resources are disposed of
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
