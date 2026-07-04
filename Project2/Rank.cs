////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Lab2
// File Name: Card.cs
// Description: Ceates a Card
// Course: CSCI 1260 – Introduction to Computer Science II
// Author: Christian Rock
// Created: 08/31/22
// Copyright: Christian Rock, 2022
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Rank
    {

        public static int GetCardValue(Card card)
        {
            switch (card.Face)
            {
                case Face.Ace:
                    return 11; // Initially, treat Ace as 11
                case Face.Two: return 2;
                case Face.Three: return 3;
                case Face.Four: return 4;
                case Face.Five: return 5;
                case Face.Six: return 6;
                case Face.Seven: return 7;
                case Face.Eight: return 8;
                case Face.Nine: return 9;
                case Face.Ten:
                case Face.Jack:
                case Face.Queen:
                case Face.King:
                    return 10;
                default:
                    return 0;
            }
        }


        public static int CalculateHandValue(List<Card> hand)
        {
            int totalValue = 0;
            int aceCount = 0;


            foreach (Card card in hand)
            {
                int cardValue = GetCardValue(card);
                totalValue += cardValue;


                if (card.Face == Face.Ace)
                    aceCount++;
            }

            //for aces to be 1s
            while (totalValue > 21 && aceCount > 0)
            {
                totalValue -= 10;
                aceCount--;
            }

            return totalValue;
        }

        /// <summary>
        /// Returns true if the hand is "soft" — i.e., contains an Ace being counted as 11.
        /// Used to implement the standard casino H17 rule (dealer hits on soft 17).
        /// </summary>
        public static bool IsSoftHand(List<Card> hand)
        {
            bool hasAce = hand.Any(c => c.Face == Face.Ace);
            if (!hasAce) return false;
            // Count all aces as 1 to get the hard total
            int hardTotal = 0;
            foreach (Card card in hand)
                hardTotal += card.Face == Face.Ace ? 1 : GetCardValue(card);
            // If adding 10 (upgrading one ace from 1 to 11) stays <= 21, the hand is soft
            return hardTotal + 10 <= 21;
        }
    }
}
