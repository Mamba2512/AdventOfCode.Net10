using System.Diagnostics;
using Hand = (System.Collections.Generic.List<string> CurrentHand, int Bid, int Rank);

namespace AdventOfCodeNet10._2023.Day_07
{
  internal class Part_2_2023_Day_07 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/7
    --- Part Two ---
    To make things a little more interesting, the Elf introduces one additional
    rule. Now, J cards are jokers - wildcards that can act like whatever card would
    make the hand the strongest type possible.
    
    To balance this, J cards are now the weakest individual cards, weaker even than
    2. The other cards stay in the same order: A, K, Q, T, 9, 8, 7, 6, 5, 4, 3, 2,
    J.
    
    J cards can pretend to be whatever card is best for the purpose of determining
    hand type; for example, QJJQ2 is now considered four of a kind. However, for
    the purpose of breaking ties between two hands of the same type, J is always
    treated as J, not the card it's pretending to be: JKKK2 is weaker than QQQQ2
    because J is weaker than Q.
    
    Now, the above example goes very differently:
    
    32T3K 765
    T55J5 684
    KK677 28
    KTJJT 220
    QQQJA 483
    32T3K is still the only one pair; it doesn't contain any jokers, so its
    strength doesn't increase.
    KK677 is now the only two pair, making it the second-weakest hand.
    T55J5, KTJJT, and QQQJA are now all four of a kind! T55J5 gets rank 3, QQQJA
    gets rank 4, and KTJJT gets rank 5.
    With the new joker rule, the total winnings in this example are 5905.
    
    Using the new joker rule, find the rank of every hand in your set. What are the
    new total winnings?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>

    public List<Hand> Hands = new();

    private Dictionary<string, int> cardStrengths = new()
{
    {"A", 14}, {"K", 13}, {"Q", 12}, {"T", 10},
    {"9", 9}, {"8", 8}, {"7", 7}, {"6", 6},
    {"5", 5}, {"4", 4}, {"3", 3}, {"2", 2}, {"J", 1}
};
    public override string Execute()
    {
      Hands.Clear();
      string result = "";
      long totalCount = 0;
      foreach (var line in Lines)
      {
        var splitted = line.Split(' ');
        var hand = splitted[0].Select(c => c.ToString()).ToList();
        var bid = int.Parse(splitted[1]);
        if (hand.Contains("J"))
        {
          var rankWithJoker = RankForHandWithJoker(hand);
          Hands.Add((hand, bid, rankWithJoker));
        }
        else
        {
          var rankWithoutJoker = RankForHandWithoutJoker(hand);
          Hands.Add((hand, bid, rankWithoutJoker));
        }
      }

      Dictionary<List<string>, int> rankedHands = new();

      foreach (var elt in Hands)
      {
        var originalRank = elt.Rank;

        rankedHands[elt.CurrentHand] = originalRank;
      }

      Dictionary<int, List<List<string>>> groupedByRank = new();
      foreach (var kvp in rankedHands)
      {
        var hand = kvp.Key;    // List<string>
        var rank = kvp.Value;  // int
        if (!groupedByRank.ContainsKey(rank))
        {
          groupedByRank[rank] = new List<List<string>>();
        }
        groupedByRank[rank].Add(hand);
      }
      foreach (var kvp in groupedByRank)
      {
        var currentRank = kvp.Key;
        var currentListOfHands = kvp.Value;
        var sortedHands = CompareCardStrengthHands(currentListOfHands); //higher ranks come first
        groupedByRank[currentRank] = sortedHands;
      }

      //var sortedGroupedByRank = groupedByRank.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
      var sortedGroupedByRank = groupedByRank.OrderBy(x => x.Key);
      int temprank = 1;
      Dictionary<List<string>, int> tempRankedHands = new();
      foreach (var kvp in sortedGroupedByRank)
      {
        foreach (var hand in kvp.Value)
        {
          tempRankedHands[hand] = temprank;
          //Debug.WriteLine("Hand: " + string.Join("", hand) + " Assigned Rank: " + temprank);
          temprank++;
        }
      }

      for (int i = 0; i < tempRankedHands.Count; i++)
      {
        long actualRank = tempRankedHands.Count - i;
        long currentBid = Hands.First(h => h.CurrentHand.SequenceEqual(tempRankedHands.ElementAt(i).Key)).Bid;
        //var currentBid = Input[tempRankedHands.ElementAt(i).Key];
        Debug.WriteLine("Hand: " + string.Join("", tempRankedHands.ElementAt(i).Key) + " Final Rank: " + actualRank + " Bid: " + currentBid);
        var value = actualRank * currentBid;
        totalCount += value;
      }

      result = totalCount.ToString();
      return result;
    }

    private List<List<string>> CompareCardStrengthHands(List<List<string>> hands)
    {
      var numOfHands = hands.Count;
      List<string> sortedHands = new List<string>();
      for (int i = 0; i < numOfHands - 1; i++)
      {
        for (int j = i + 1; j < numOfHands; j++)
        {
          var comparisonResult = CompareHandsByCardStrength(hands[i], hands[j]);
          if (comparisonResult < 0)
          {
            // Swap hands if hands[i] is weaker than hands[j]
            var temp = hands[i];
            hands[i] = hands[j];
            hands[j] = temp;
          }
        }
      }
      return hands;
    }

    private int CompareHandsByCardStrength(List<string> hand1, List<string> hand2)
    {
      // Compare each card position
      for (int i = 0; i < hand1.Count; i++)
      {
        int strength1 = cardStrengths[hand1[i]];
        int strength2 = cardStrengths[hand2[i]];

        if (strength1 != strength2)
        {
          return strength1.CompareTo(strength2);
        }
      }
      return 0;  // Hands are identical
    }

    public int RankForHandWithoutJoker(List<string> currentHand) //returns the rank as per rules
    {
      Dictionary<string, int> cardWithCounts = new Dictionary<string, int>();

      foreach (var card in currentHand)
      {
        //if its a number
        if (int.TryParse(card, out int numberCardValue))
        {
          if (cardWithCounts.ContainsKey(card))
          {
            cardWithCounts[card] += 1;
          }
          else
          {
            cardWithCounts[card] = 1;
          }
        }
        if (card == "A" || card == "K" || card == "Q" || card == "T")
        {
          if (cardWithCounts.ContainsKey(card))
          {
            cardWithCounts[card] += 1;
          }
          else
          {
            cardWithCounts[card] = 1;
          }
        }
      }

      /*
      Five of a kind, where all five cards have the same label: AAAAA
      Four of a kind, where four cards have the same label and one card has a different label: AA8AA
      Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
      Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
      Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
      One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
      High card, where all cards' labels are distinct: 23456
      */

      var countsForCards = cardWithCounts.Values.ToList();
      //5 of a kind
      if (countsForCards.Contains(5))
      {
        return 1;
      }
      else if (countsForCards.Contains(4))//4 of a kind
      {
        return 2;
      }
      else if (countsForCards.Contains(3) && countsForCards.Contains(2))//full house
      {
        return 3;
      }
      else if (countsForCards.Contains(3))//three of a kind
      {
        return 4;
      }
      else if (countsForCards.Where(x => x == 2).Count() == 2)//two pair
      {
        return 5;
      }
      else if (countsForCards.Contains(2))//one pair
      {
        return 6;
      }
      else
      {
        //high card
        return 7;
      }
    }

    public int RankForHandWithJoker(List<string> currentHand)
    {
      var temp = new List<string>(currentHand);
      temp.RemoveAll(card => card == "J");
      int numOfJokers = currentHand.Count(card => card == "J");
      var tempRank = RankForHandWithoutJoker(temp);

      switch (tempRank)
      {
        case 1://five of a kind
          {
            //always five of a kind
            throw new NotImplementedException();
          }
        case 2: //four of a kind
          {
            //check if we got joker to make it five of a kind
            if (numOfJokers >= 1)
            {
              //becomes five of a kind
              return 1;
            }
            else
            {
              throw new NotImplementedException();
            }
          }
        case 3: //full house
          {
            throw new NotImplementedException();
          }
        case 4: //3 of a kind
          {
            if (numOfJokers == 2)
            {
              //becomes five of a kind
              return 1;
            }
            else if (numOfJokers == 1)
            {
              //becomes four of a kind
              return 2;
            }
            else
            {
              throw new NotImplementedException();
            }
          }
        case 5: //two pair
          {
            if (numOfJokers == 1)
            {
              //becomes full house
              return 3;
            }
            else
            {
              throw new NotImplementedException();
            }
          }
        case 6: //one pair
          {
            if (numOfJokers == 3)
            {
              //becomes 5 of a kind
              return 1;
            }
            else if (numOfJokers == 2)
            {
              //becomes 4 of a kind
              return 2;
            }
            else if (numOfJokers == 1)
            {
              //becomes 3 of a kind
              return 4;
            }
            else
            {
              throw new NotImplementedException();
            }
          }
        case 7: //high card
          {
            if (numOfJokers == 5)
            {
              //becomes 5 of a kind
              return 1;
            }
            else if (numOfJokers == 4)
            {
              //becomes 5 of a kind
              return 1;
            }
            else if (numOfJokers == 3)
            {
              //becomes 4 of a kind
              return 2;
            }
            else if (numOfJokers == 2)
            {
              //becomes 3 of a kind
              return 4;
            }
            else if (numOfJokers == 1)
            {
              //becomes one pair
              return 6;
            }
            else
            {
              throw new NotImplementedException();
            }
          }
        default:
          {
            throw new NotImplementedException();
          }
      }
    }
  }
}
