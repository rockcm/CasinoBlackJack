using Project2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Hand
    {
        public Card[] GameHand { get; set; }
        public int HandSize { get; set; }
        public int CardsInHand { get; set; }



        public Hand()
        {
            HandSize = 5;
            CardsInHand = 5;
            GameHand = new Card[HandSize];
            for (int i = 0; i < GameHand.Length; i++)
            {
                GameHand[i] = new Card(i);
            }
        }

        public Hand(int handSize)
        {
            GameHand = new Card[HandSize];
            CardsInHand = handSize;
            for (int i = 0; i < HandSize; i++)
            {
                GameHand[i] = new Card(i);

                if (HandSize > 52)
                {
                    Console.WriteLine("Please enter number under 52");
                }
            }

        }

        public Hand(Hand existingHand)
        {
            CardsInHand = existingHand.HandSize;
            for (int i = 0; i < existingHand.GameHand.Length;)
            {
                existingHand.GameHand[i] = GameHand[i];
            }

        }

        public void AddCard(Card Card)
        {

            GameHand[GameHand.Length - 1] = new Card(Card);
            CardsInHand++;

        }
    }
}
