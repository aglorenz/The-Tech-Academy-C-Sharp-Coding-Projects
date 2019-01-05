
//namespace TwentyOneGameFollowAlong
//{
    //class Program
    //{
        //static void Main(string[] args)
        //{
            //Pillars of Object Oriented Programming  Inheritance,  polymorphism, abstraction, encapsulation

            //TwentyOneGame game = new TwentyOneGame();
            //game.Players = new List<string>() { "Andy", "Bill", "Joe" };

            //// Calling the method in the class you're INHERITING from, you're are calling the superclass method.  
            //game.ListPlayers(); // Game in this instance is the superclass.
            //game.Play();
            //Console.ReadLine();

            //Game game = new Game();
            //game.Dealer = "Andy";
            //game.Name = "TwentyOne";

            //Card card = new Card { Face = "King", Suit = "Spades" }; 
            //card.Face = "King";
            //card.Suit = "Spades";

            //List<Game> games = new List<Game>();
            //TwentyOneGame game = new TwentyOneGame();  // Classic polymorphism.  One object TwentyOneGame, morphs into a higher order object. 
            //Game game = new TwentyOneGame();  // Classic polymorphism.  One object, TwentyOneGame, morphs into a higher order object, game. 
            //games.Add(game);
            //Game game = new Game();  // Can't instantiate now that Game is an abstract class 
            // a base class is AKA  parent or superclass class
            // a child class is AKA subclass or derived class

            //TwentyOneGame game = new TwentyOneGame();
            //game.Players = new List<string>() { "Andy", "Bill", "Bob" };
            //game.ListPlayers();
            //Console.ReadLine();


            //Game game = new TwentyOneGame();
            //game.Players = new List<Player>(); // workaround to create empty list so we can add players to it.
            //Player player = new Player();
            //player.Name = "Andy";

            // ***************** Operator Overload
            //game += player;
            //game -= player;

            //****************** Enums
            // Enums limit the possible values you can receive from a user.  Great for drop down lists
            // Eliminates user entry error.
            // ConsoleColor color = ConsoleColor.Magenta;

            //Card card = new Card();
            //card.Suit = Suit.Clubs;

            // enums have underlying data type of int assigned in order as as 1,2,3, etc 
            // (or you can assign them manually -- see Card())
            //int underlyingValue = (int)Suit.Diamonds;  //Casting a string to an integer instead of using Convert.ToInt32()
            //Console.WriteLine(underlyingValue);  // will write the value 10 (see Card() class)

            //******************* Struct  - reference or value data type?
            // Every data type in C# is either a reference type (pointer) or a value (a copy of) type and these two types have 
            //different behavior

            // built in value types are integer, enum, boolean, datetime .  What is an integer if it's not a class?
            // value types can't have a value of null i.e., non-nullable
            //int number = 5;  //its a struct.  A struct is a value type that can't be inherited
            //bool isTrue = null; // non-nullable
            //Card card1 = new Card();
            //Card card2 = card1; // if Card is a Class, it is a reference type; card2 points to memory loc of card1  
            // if Card is a struct, its a value type;  card2 has its own memory location

            //card1.Face = Face.Eight;
            //card2.Face = Face.King;

            //Console.WriteLine(card1.Face);  // If Card is a struct, answer is Eight, if Card is a Class ans is King

            // Lambda functions expose lists to a variety of handy methods to make life much easier
            // how many ACES are in the loop?
            //Deck deck = new Deck();

            //int count = deck.Cards.Count(x => x.Face == Face.Ace); 

            // Create a new list of just Kings
            //List<Card> newList = deck.Cards.Where(x => x.Face == Face.King).ToList();
            //foreach (Card card in newList)
            //{
            //    Console.WriteLine("{0} of {1}",card.Face, card.Suit);
            //}
            // Caution:  they are very hard to debug esp when chaining together (which also makes it hard to read)
            //List<int> numberList = new List<int> { 1, 2, 3, 535, 342, 23 };

            //int sum = numberList.Sum(x => x + 5 );
            //int max = numberList.Max(); // Saves so much time and makes it more fun
            // chain methods together
            //int sum = numberList.Where(x => x > 20).Sum(); // create new list of num where value > 20 then sum it.


            //Console.WriteLine(sum);

            //deck.Shuffle(3);

            //foreach (Card card in deck.Cards)
            //{
            //    Console.WriteLine(card.Face + " of " + card.Suit);
            //}
            //Console.WriteLine(deck.Cards.Count);
//            Console.ReadLine();
//        }

//    }
//}
