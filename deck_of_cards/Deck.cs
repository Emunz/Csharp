using System;
using System.Collections.Generic;

namespace deck_of_cards {
    public class Deck {
        private List<Card> cards; 

        private int deckIndex = 0;

        public Deck(){
            cards = new List<Card>();
            string [] suits = new string[4] {"Hearts", "Diamonds", "Clubs", "Spades"};
            foreach(string suit in suits) {
                for(int i = 1; i <=13; i++){
                    cards.Add(new Card(suit, i));
                }
            }
        }

        public override string ToString(){
            string info = "";
            foreach(Card card in cards){
                info += card + "\n";
            }
            return info;
        }

        public Card Deal(){
            Card topCard = cards[deckIndex];
            deckIndex++;
            return topCard;
        }
        
        public void Reset(){
            deckIndex = 0;
        }

        Random random = new Random();
        public void Shuffle(){
            for(int i = 0; i < cards.Count; i++){
                int index = random.Next(0, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[index];
                cards[index] = temp;
            }
        }
    }
}