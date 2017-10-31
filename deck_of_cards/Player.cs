using System;
using System.Collections.Generic;

namespace deck_of_cards {
    public class Player {
        public string name;
        private List<Card> _hand = new List<Card>();
        public List<Card> hand{
            get {return _hand;}
            set {_hand = value;}
        }

        public Player(string val){
            name = val;
        }

        public Card Draw(Deck theDeck){
            Card newCard = theDeck.Deal();
            hand.Add(newCard);
            return newCard;
        }

        public Card Discard(int handIndex){
            if(handIndex < 0 || handIndex > hand.Count){
                return null;
            } else {
                Card discardedCard = hand[handIndex];
                hand.RemoveAt(handIndex);
                return discardedCard;
            }

        }
    }
}