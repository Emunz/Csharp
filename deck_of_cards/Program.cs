using System;

namespace deck_of_cards
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck myDeck = new Deck();
            myDeck.Shuffle();
            // Console.WriteLine(myDeck);
            Player newPlayer = new Player("Evan");

            Console.WriteLine(newPlayer.name);
            newPlayer.Draw(myDeck);
            newPlayer.Draw(myDeck);
            newPlayer.Draw(myDeck);
            newPlayer.Draw(myDeck);
            newPlayer.Draw(myDeck);
            foreach(var card in newPlayer.hand){
                Console.WriteLine(card);
            }
            newPlayer.Discard(1);
            newPlayer.Discard(2);
            newPlayer.Draw(myDeck);
            newPlayer.Draw(myDeck);
            foreach(var card in newPlayer.hand){
                Console.WriteLine(card);
            }
        }
    }
}
