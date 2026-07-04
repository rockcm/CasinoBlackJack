///////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Lab2
// File Name: Deck.cs
// Description: Ceates a Card
// Course: CSCI 1260 – Introduction to Computer Science II
// Author: Christian Rock
// Created: 09/14/22
// Copyright: Christian Rock, 2022
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////




using Microsoft.VisualBasic;
using Project2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;



namespace Project2
{
    /// <summary>
    /// Deck class that creates a deck object from an array of card objects. 
    ///This class also includes methods to shuffle the deck, deal a card from the deck, and deal a hand from the deck. 
    /// Gives functionality to the deck. 
    /// </summary>
    public class Deck
    {
        public Card[] DeckCards { get; private set; } // creating a card array attribute
        public int NextCard { get; private set; } // Next card used to keep track of what card we are on in the deck. 


        /// <summary>
        /// Default constructor method, that fills a card array with 52 card objects. 
        /// </summary>
        public Deck()
        {
            NextCard = 0;
            DeckCards = new Card[52]; // makes DeckCards a card array of size 52.

            for (int i = 0; i < DeckCards.Length; i++) // fills the array with 52 card objects
            {
                DeckCards[i] = new Card(i);

            }
        }

        /// <summary>
        /// Copy constructor that takes an existing deck as a parameter and copys it to another card array.  
        /// </summary>
        /// <param name="ExistingDeck"></param>
        public Deck(Deck ExistingDeck)
        {

            DeckCards = new Card[52];// makes DeckCards a card array of size 52.
            for (int i = 0; i < DeckCards.Length; i++) // for loop for deep copy
            {

                DeckCards[i] = ExistingDeck.DeckCards[i]; // fills the array with 52 card objects, this is done by copying the object from the Card Array that has already been created into a new card array.

            }

        }

        /// <summary>
        /// Shuffll method, that takes a card at a random postion and changes its position with another card 
        /// </summary>
        public void Shuffle()
        {

            NextCard = 0; // setting next card back to 0 in case it has been changed
            Random rnd = new Random(); // random object used to get a random number to represent a random positon in deck. 
            for (int i = 0; i < DeckCards.Length; i++) // loops through all cards in deck
            {

                int number = rnd.Next(DeckCards.Length); // gettiing random number
                Card temp = DeckCards[i]; // creating temp card variable
                DeckCards[i] = DeckCards[number]; // replacing card in position (i) with a card at a random position from the random number we made
                DeckCards[number] = temp; // taking the card that was in the random postion and storing it in the temp variable. 
            }

        }

        /// <summary>
        /// Method that allows use to deal a card.
        /// </summary>
        /// <returns> The value of the next card in the deck</returns>
        public Card DealACard()
        {
            if (NextCard < DeckCards.Length)
                return DeckCards[NextCard++];
            return null;
        }

        
        /// <summary>
        /// Deals a hand of card objects based upon the value entered by the user for handsize. 
        /// </summary>
        /// <param name="handSize"> The value of the integer passed by the user to indicate the size of hand</param>
        /// <returns>Returns the amount of card objects specified by the user  as the handSize.</returns>
        public Hand DealAHand(int handSize)
        {


            Hand Hand1 = new Hand(handSize);
            for (int i = 0; i < handSize; i++)
            {
                Card c = DealACard();
                Hand1.AddCard(DealACard());

            }

            return Hand1;
        }

        /// <summary>
        /// To string that makes our deck readable and displayable to the user.
        /// </summary>
        /// <returns> Returns each card object in a card array Deck and displays it to screen.</returns>
        public override string ToString()
        {
            string msg = "";

            foreach (Card card in DeckCards) // Goes through each object in the card array and displays it to the screen by updating the string in the foreach statement. 
            {
                msg += $"\n{card.ToString()}";
            }
            return msg;


        }







    }



}