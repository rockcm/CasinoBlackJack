////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Lab2
// File Name: DeckDriver.cs
// Description: Ceates a Card
// Course: CSCI 1260 – Introduction to Computer Science II
// Author: Christian Rock
// Created: 08/31/22
// Copyright: Christian Rock, 2022
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////


using Project2;

int playerChips = 500;
string playAgain = "Y";

while ((playAgain == "Y" || playAgain == "YES") && playerChips > 0)
{
    Deck deck = new Deck();
    deck.Shuffle();
    Console.Clear();

    // --- Betting ---
    Console.WriteLine($"=== BLACKJACK ===  Chips: ${playerChips}");
    Console.WriteLine();

    int bet = 0;
    while (true)
    {
        Console.Write($"Place your bet (1-{playerChips}): $");
        string betInput = Console.ReadLine()?.Trim();
        if (int.TryParse(betInput, out bet) && bet >= 1 && bet <= playerChips)
            break;
        Console.WriteLine($"Invalid bet. Enter a whole number between 1 and {playerChips}.");
    }
    playerChips -= bet;

    List<Card> playerHand = new List<Card>();
    List<Card> dealerHand = new List<Card>();

    // Deal two cards each, alternating (realistic deal order)
    playerHand.Add(deck.DealACard());
    dealerHand.Add(deck.DealACard());
    playerHand.Add(deck.DealACard());
    dealerHand.Add(deck.DealACard()); // hole card

    int playerValue = Rank.CalculateHandValue(playerHand);
    int dealerValue = Rank.CalculateHandValue(dealerHand);

    // Display initial state
    Console.WriteLine();
    Console.WriteLine("--- Your Hand ---");
    foreach (Card card in playerHand)
        Console.WriteLine($"  {card}");
    Console.WriteLine($"  Value: {playerValue}");

    Console.WriteLine();
    Console.WriteLine("--- Dealer's Hand ---");
    Console.WriteLine($"  {dealerHand[0]}");
    Console.WriteLine("  [Hidden]");

    bool roundOver = false;
    int outcome = 0;        // 1 = player wins, -1 = player loses, 0 = push
    bool naturalBlackjack = false;

    // --- Check for naturals (Ace + 10-value card on first two cards) ---
    bool playerNatural = playerValue == 21;
    bool dealerNatural = dealerValue == 21;

    if (playerNatural || dealerNatural)
    {
        Console.WriteLine();
        Console.WriteLine("--- Dealer Reveals Hole Card ---");
        foreach (Card card in dealerHand)
            Console.WriteLine($"  {card}");
        Console.WriteLine($"  Value: {dealerValue}");

        if (playerNatural && dealerNatural)
        {
            Console.WriteLine("\nBoth have Blackjack -- Push!");
            outcome = 0;
        }
        else if (playerNatural)
        {
            Console.WriteLine("\nBlackjack! You win 3:2!");
            outcome = 1;
            naturalBlackjack = true;
        }
        else
        {
            Console.WriteLine("\nDealer has Blackjack. You lose.");
            outcome = -1;
        }
        roundOver = true;
    }

    // --- Player's Turn ---
    if (!roundOver)
    {
        while (playerValue < 21)
        {
            bool canDouble = playerHand.Count == 2 && playerChips >= bet;
            Console.WriteLine();
            Console.Write("Action -- [H]it  [S]tand" + (canDouble ? "  [D]ouble down" : "") + ": ");

            string action = Console.ReadLine()?.ToLower().Trim();

            if (action == "h" || action == "hit")
            {
                playerHand.Add(deck.DealACard());
                playerValue = Rank.CalculateHandValue(playerHand);
                Console.WriteLine();
                Console.WriteLine("--- Your Hand ---");
                foreach (Card card in playerHand)
                    Console.WriteLine($"  {card}");
                Console.WriteLine($"  Value: {playerValue}");

                if (playerValue > 21)
                {
                    Console.WriteLine("\nBust! You lose.");
                    outcome = -1;
                    roundOver = true;
                    break;
                }
                else if (playerValue == 21)
                {
                    Console.WriteLine("21 -- Auto-stand.");
                }
            }
            else if (action == "s" || action == "stand")
            {
                break;
            }
            else if ((action == "d" || action == "double") && canDouble)
            {
                playerChips -= bet;     // second wager equal to the original bet
                bet *= 2;
                playerHand.Add(deck.DealACard());
                playerValue = Rank.CalculateHandValue(playerHand);
                Console.WriteLine();
                Console.WriteLine($"--- Your Hand (Doubled -- Total Bet: ${bet}) ---");
                foreach (Card card in playerHand)
                    Console.WriteLine($"  {card}");
                Console.WriteLine($"  Value: {playerValue}");

                if (playerValue > 21)
                {
                    Console.WriteLine("\nBust! You lose.");
                    outcome = -1;
                    roundOver = true;
                }
                break; // Only one card on a double down
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter H, S" + (canDouble ? ", or D" : "") + ".");
            }
        }
    }

    // --- Dealer's Turn ---
    if (!roundOver)
    {
        Console.WriteLine();
        Console.WriteLine("--- Dealer Reveals Hole Card ---");
        foreach (Card card in dealerHand)
            Console.WriteLine($"  {card}");
        int dealerVal = Rank.CalculateHandValue(dealerHand);
        Console.WriteLine($"  Value: {dealerVal}");

        // Dealer hits on soft 17 or less (standard H17 casino rule)
        while (dealerVal < 17 || (dealerVal == 17 && Rank.IsSoftHand(dealerHand)))
        {
            Console.WriteLine("\nDealer hits.");
            dealerHand.Add(deck.DealACard());
            dealerVal = Rank.CalculateHandValue(dealerHand);
            Console.WriteLine();
            Console.WriteLine("--- Dealer's Hand ---");
            foreach (Card card in dealerHand)
                Console.WriteLine($"  {card}");
            Console.WriteLine($"  Value: {dealerVal}");
        }

        Console.WriteLine();
        if (dealerVal > 21)
        {
            Console.WriteLine("Dealer busts! You win!");
            outcome = 1;
        }
        else if (playerValue > dealerVal)
        {
            Console.WriteLine("You win!");
            outcome = 1;
        }
        else if (playerValue == dealerVal)
        {
            Console.WriteLine("Push -- bet returned.");
            outcome = 0;
        }
        else
        {
            Console.WriteLine("Dealer wins.");
            outcome = -1;
        }
    }

    // --- Payout ---
    Console.WriteLine();
    if (outcome == 1)
    {
        if (naturalBlackjack)
        {
            int profit = (int)(bet * 1.5);
            playerChips += bet + profit;
            Console.WriteLine($"You win ${profit} (3:2 payout)! Chips: ${playerChips}");
        }
        else
        {
            playerChips += bet * 2;
            Console.WriteLine($"You win ${bet}! Chips: ${playerChips}");
        }
    }
    else if (outcome == 0)
    {
        playerChips += bet;
        Console.WriteLine($"Push. Chips: ${playerChips}");
    }
    else
    {
        Console.WriteLine($"You lose ${bet}. Chips: ${playerChips}");
    }

    if (playerChips <= 0)
    {
        Console.WriteLine("\nOut of chips. Game over!");
        break;
    }

    Console.WriteLine();
    do
    {
        Console.Write("Play again? (Y/N): ");
        playAgain = Console.ReadLine()?.ToUpper().Trim();
        if (playAgain != "Y" && playAgain != "N")
            Console.WriteLine("Please enter Y or N.");
    } while (playAgain != "Y" && playAgain != "N");
}

Console.WriteLine();
Console.WriteLine($"Thanks for playing! Final chips: ${playerChips}");