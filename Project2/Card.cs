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
    /// <summary>
    /// Card class that creates a card object for further use in a deck. 
    /// </summary>
    public class Card
    {
        //card attributes
        public Suit Suit { get; set; }
        public Face Face { get; set; }

        /// <summary>
        /// Defualt constructor, assigning defualt values
        /// </summary>
        public Card()
        {
            Face = Face.Ace;
            Suit = Suit.Spades;
        }

        /// <summary>
        /// creates a card object based on used input value.
        /// </summary>
        /// <param name="n">the user input value that is selected to show a card object</param>
        public Card(int n)
        {
            Face = (Face)Enum.GetValues(typeof(Face)).GetValue(n % 13);
            Suit = (Suit)Enum.GetValues(typeof(Suit)).GetValue(n / 13);
        }
        public Card(Suit suit, Face face)
        {
            Suit = suit;
            Face = face;
        }

        /// <summary>
        /// Takes an existing card object and creates another card object from it.
        /// </summary>
        /// <param name="ExistingCard"> the value of the existing card object that can be copied to another card object</param>
        public Card(Card ExistingCard)
        {

            Suit = ExistingCard.Suit;
            Face = ExistingCard.Face;
        }

        /// <summary>
        /// Overriding the tostring to allow for cards to be displayed properly using the face and suit enum values. 
        /// </summary>
        public override string ToString()
        {
            string msg;
            msg = $"the {Face.ToString()} of {Suit.ToString()}";
            return msg;
        }


    }
}