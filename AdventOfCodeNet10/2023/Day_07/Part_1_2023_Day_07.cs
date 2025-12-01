namespace AdventOfCodeNet10._2023.Day_07
{
  internal class Part_1_2023_Day_07 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/7
    --- Day 7: Camel Cards ---
    Your all-expenses-paid trip turns out to be a one-way, five-minute ride in an
    airship. (At least it's a cool airship!) It drops you off at the edge of a vast
    desert and descends back to Island Island.
    
    "Did you bring the parts?"
    
    You turn around to see an Elf completely covered in white clothing, wearing
    goggles, and riding a large camel.
    
    "Did you bring the parts?" she asks again, louder this time. You aren't sure
    what parts she's looking for; you're here to figure out why the sand stopped.
    
    "The parts! For the sand, yes! Come with me; I will show you." She beckons you
    onto the camel.
    
    After riding a bit across the sands of Desert Island, you can see what look
    like very large rocks covering half of the horizon. The Elf explains that the
    rocks are all along the part of Desert Island that is directly above Island
    Island, making it hard to even get there. Normally, they use big machines to
    move the rocks and filter the sand, but the machines have broken down because
    Desert Island recently stopped receiving the parts they need to fix the
    machines.
    
    You've already assumed it'll be your job to figure out why the parts stopped
    when she asks if you can help. You agree automatically.
    
    Because the journey will take a few days, she offers to teach you the game of
    Camel Cards. Camel Cards is sort of similar to poker except it's designed to be
    easier to play while riding a camel.
    
    In Camel Cards, you get a list of hands, and your goal is to order them based
    on the strength of each hand. A hand consists of five cards labeled one of A,
    K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, or 2. The relative strength of each card
    follows this order, where A is the highest and 2 is the lowest.
    
    Every hand is exactly one type. From strongest to weakest, they are:
    
    Five of a kind, where all five cards have the same label: AAAAA
    Four of a kind, where four cards have the same label and one card has a
    different label: AA8AA
    Full house, where three cards have the same label, and the remaining two cards
    share a different label: 23332
    Three of a kind, where three cards have the same label, and the remaining two
    cards are each different from any other card in the hand: TTT98
    Two pair, where two cards share one label, two other cards share a second
    label, and the remaining card has a third label: 23432
    One pair, where two cards share one label, and the other three cards have a
    different label from the pair and each other: A23A4
    High card, where all cards' labels are distinct: 23456
    Hands are primarily ordered based on type; for example, every full house is
    stronger than any three of a kind.
    
    If two hands have the same type, a second ordering rule takes effect. Start by
    comparing the first card in each hand. If these cards are different, the hand
    with the stronger first card is considered stronger. If the first card in each
    hand have the same label, however, then move on to considering the second card
    in each hand. If they differ, the hand with the higher second card wins;
    otherwise, continue with the third card in each hand, then the fourth, then the
    fifth.
    
    So, 33332 and 2AAAA are both four of a kind hands, but 33332 is stronger
    because its first card is stronger. Similarly, 77888 and 77788 are both a full
    house, but 77888 is stronger because its third card is stronger (and both hands
    have the same first and second card).
    
    To play Camel Cards, you are given a list of hands and their corresponding bid
    (your puzzle input). For example:
    
    32T3K 765
    T55J5 684
    KK677 28
    KTJJT 220
    QQQJA 483
    This example shows five hands; each hand is followed by its bid amount. Each
    hand wins an amount equal to its bid multiplied by its rank, where the weakest
    hand gets rank 1, the second-weakest hand gets rank 2, and so on up to the
    strongest hand. Because there are five hands in this example, the strongest
    hand will have rank 5 and its bid will be multiplied by 5.
    
    So, the first step is to put the hands in order of strength:
    
    32T3K is the only one pair and the other hands are all a stronger type, so it
    gets rank 1.
    KK677 and KTJJT are both two pair. Their first cards both have the same label,
    but the second card of KK677 is stronger (K vs T), so KTJJT gets rank 2 and
    KK677 gets rank 3.
    T55J5 and QQQJA are both three of a kind. QQQJA has a stronger first card, so
    it gets rank 5 and T55J5 gets rank 4.
    Now, you can determine the total winnings of this set of hands by adding up the
    result of multiplying each hand's bid with its rank (765 * 1 + 220 * 2 + 28 * 3
    + 684 * 4 + 483 * 5). So the total winnings in this example are 6440.
    
    Find the rank of every hand in your set. What are the total winnings?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public Dictionary<List<string>, int> Input = new(); //Hand, (Bid)
    public int NumOfHands = 0;

    private Dictionary<string, int> cardStrengths = new()
{
    {"A", 14}, {"K", 13}, {"Q", 12}, {"J", 11}, {"T", 10},
    {"9", 9}, {"8", 8}, {"7", 7}, {"6", 6},
    {"5", 5}, {"4", 4}, {"3", 3}, {"2", 2}
};
    public override string Execute()
    {
      string result = "";
      int totalCount = 0;
      foreach (var line in Lines)
      {
        var splitted = line.Split(' ');
        var hand = splitted[0].ToList();
        var bid = int.Parse(splitted[1]);
        Input[hand.Select(c => c.ToString()).ToList()] = bid;
      }
      NumOfHands = Input.Count;

      Dictionary<List<string>, int> rankedHands = new();

      foreach (var kvp in Input)
      {
        var originalRank = CustomSortHand(kvp.Key);

        rankedHands[kvp.Key] = originalRank;
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
          temprank++;
        }
      }

      for (int i = 0; i < tempRankedHands.Count; i++)
      {
        var actualRank = tempRankedHands.Count - i;
        var currentBid = Input[tempRankedHands.ElementAt(i).Key];
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

    public int CustomSortHand(List<string> currentHand) //returns the rank as per rules
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
        if (card == "A" || card == "K" || card == "Q" || card == "J" || card == "T")
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

      //Five of a kind, where all five cards have the same label: AAAAA
      //Four of a kind, where four cards have the same label and one card has a different label: AA8AA
      //Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
      //Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
      //Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
      //One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
      //High card, where all cards' labels are distinct: 23456
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
  }
}
