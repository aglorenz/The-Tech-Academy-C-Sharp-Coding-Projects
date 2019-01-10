using System;
using Casino;
using Casino.TwentyOne;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

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
            Console.Write("Welcome to the {0}. Let's start by telling me your name.\n>> ", casinoName);
            string playerName = Console.ReadLine();

            if (playerName == "admin")
            {
                List<ExceptionEntity> Exceptions = ReadExceptions();
                foreach (var exception in Exceptions)
                {
                    Console.Write(exception.Id + " | ");
                    Console.Write(exception.ExceptionType + " | ");
                    Console.Write(exception.ExceptionMessage + " | ");
                    Console.Write(exception.TimeStamp + " | ");
                    Console.WriteLine();
                }
                Console.ReadLine();
                return;
            }

            bool validAnswer = false;
            int bank = 0;
            while (!validAnswer)
            {
                Console.Write("\nAnd how much money did you bring today?\n>> ");
                validAnswer = int.TryParse(Console.ReadLine(), out bank);
                if (!validAnswer) Console.WriteLine("Please enter only digits with no decimal");
            }

            Console.Write("\nHello, {0}.  Would you like to join a game of 21 right now?\n>> ", playerName);
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes" || answer == "yeah" || answer == "ya" || answer == "y")
            {
                Player player = new Player(playerName, bank);  // use constructor to create player
                player.Id = Guid.NewGuid(); // Create a GUID  and assign to player
                using (StreamWriter file = new StreamWriter(@"C:\users\andy\logs\TwentyOneLog.txt", true)) // true indicates append
                {
                    file.WriteLine(player.Id);
                }  // once this reached, memory resources are disposed of
                Game game = new TwentyOneGame();  // polymorphism happening here
                game += player; // using overloaded operators in Game to add player to the list, players
                player.IsActivelyPlaying = true;
                while (player.IsActivelyPlaying && player.Balance > 0)
                {
                    try
                    {
                    // Play one hand
                    game.Play(); // most everything will happen in the Play method to keep the main method clean
                    }
                    catch (FraudException ex) // more specific exceptions first
                    {
                        Console.WriteLine(ex.Message);
                        //Console.WriteLine("\nSecurity! Kick this person out for cheating.");
                        UpdateDbWithException(ex);
                        Console.ReadLine();
                        return;
                    }
                    catch (Exception ex) // Generic exceptions are last
                    {
                        Console.WriteLine("\nAn error occurred. Please contact your System Administrator.");
                        UpdateDbWithException(ex);
                        Console.ReadLine();
                        return;
                    }
                }
                game -= player;
                Console.WriteLine("Thank you for playing!");
            }
            Console.WriteLine("\nFeel free to look around the casino.  Bye for now.");
            Console.Read();
            
        }

        // only accessible inside of this class.  Static so we don't have to create a new instance of Program
        // which will make it similar to main().  All Exceptions inherit from Exception so this method could
        // take a Fraudexception polymorphism.  Wer'e using ADO.net to write to the db.
        private static void UpdateDbWithException(Exception ex)
        {
            // need a connection string
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TwentyOneGame;
                                        Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                        TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                        MultiSubnetFailover=False";
            string queryString = "INSERT INTO Exceptions (ExceptionType, ExceptionMessage, TimeStamp) VALUES" +
                                 " (@ExceptionType, @ExceptionMessage, @TimeStamp)";

            /*  
             *  using is a way of controlling unmanaged resources.  When inside of a program in the.net 
             *  framework and the common language runtime, and you go outside of it to get something, such 
             *  the file system, which is not controlled by the common language runtime (CLR), it's risky.  You are 
             *  opening up resources which could use up memory and other things.  The CLR is worried about that.
             *  Anytime you open up these connections, you have to close them.  So a shorthand is created to handle
             *  this called "using".  Memory is freed up after the last closing curly brace.
            */
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                
                // by naming these datatypes, we are protecting against SQL injection
                command.Parameters.Add("@ExceptionType", SqlDbType.VarChar);
                command.Parameters.Add("@ExceptionMessage", SqlDbType.VarChar);
                command.Parameters.Add("@TimeStamp", SqlDbType.DateTime);

                command.Parameters["@ExceptionType"].Value = ex.GetType().ToString();
                command.Parameters["@ExceptionMessage"].Value = ex.Message;
                command.Parameters["@TimeStamp"].Value = DateTime.Now;

                // send it to the database
                connection.Open();
                command.ExecuteNonQuery();  // a Query would be like a SELECT,  this is an INSERT statement
                connection.Close();

            }
        }
        private static List<ExceptionEntity> ReadExceptions()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TwentyOneGame;
                                        Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                        TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                        MultiSubnetFailover=False";

            string queryString = "Select ID, ExceptionType, ExceptionMessage, TimeStamp " +
                                 "From Exceptions";

            List<ExceptionEntity> Exceptions = new List<ExceptionEntity>();

            using  (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ExceptionEntity exception = new ExceptionEntity();
                    exception.Id = Convert.ToInt32(reader["Id"]);
                    exception.ExceptionType = reader["exceptionType"].ToString();
                    exception.ExceptionMessage = reader["ExceptionMessage"].ToString();
                    exception.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
                    Exceptions.Add(exception);
                }
                connection.Close();
            }

            return Exceptions;

        }
    }
}
