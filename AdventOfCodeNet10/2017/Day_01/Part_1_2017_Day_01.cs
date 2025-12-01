using System.Diagnostics;

namespace AdventOfCodeNet10._2017.Day_01
{

  // 1. Define an alias for the tuple type at the top of your file (or globally if using C# 12's global using)
  using Card = (string Rank, int Number, string Color, System.Collections.Generic.List<int> CurrentHand);
  #region Alternative Definitions
  // alternative
  //public readonly record struct Card(string Rank, int Number, string Color, System.Collections.Generic.List<int> CurrentHand);
  // 3rd alternative
  //public class Card
  //{
  //  public string Rank { get; }
  //  public int Number { get; }
  //  public string Color { get; }
  //  public System.Collections.Generic.List<int> CurrentHand { get; }
  //  public Card(string rank, int number, string color, System.Collections.Generic.List<int> currentHand)
  //  {
  //    Rank = rank;
  //    Number = number;
  //    Color = color;
  //    CurrentHand = currentHand;
  //  }
  //}
  #endregion Alternative Definitions

  #region 2. Define the extension container for the underlying type (C# 10-13) // no Properties possible !!
  //public static class CardExtension
  //{
  //  // The C# compiler sees 'Card' as '(string Rank, int Number, string Color, List<int> CurrentHand)'

  //  public static bool IsFaceCard(this Card card)
  //  {
  //    return (card.Rank is "King" or "Queen" or "Jack");
  //  }

  //  public static int GetDisplayValue(this Card card)
  //  {
  //    return CardExtension.IsFaceCard(card) ? 10 : card.Number;
  //  }
  //}
  #endregion

  // 2. Define the extension container for the underlying type (C# 14)
  internal static class KushalSaidThisShouldBeARandomNameToAvoidConflicts
  {
    extension(Card card)
    {
      //internal bool IsFaceCard => card.Rank is "King" or "Queen" or "Jack";
      internal bool IsFaceCard => GetAllFaces().Contains(card.Rank);

      internal int GetDisplayValue()
      {
        return card.IsFaceCard ? 10 : card.Number;
      }

      internal static string[] GetAllFaces() // static method inside extension container
      {
        return new string[] { "King", "Queen", "Jack" };
      }
    }
    extension(String str)
    {
      internal string SelfmadeLowerCase => str.ToLower(); // just to show that we can create property-extensions for other types too

      internal string SelfmadeUpperCaseRank(Card myCard) // just to show that we can pass parameters too
      {
        return myCard.IsFaceCard ? myCard.Rank.ToUpper() : myCard.Rank; // just to show that we can use other extensions inside this extension !!
      }
    }
  }

  internal class Part_1_2017_Day_01 : Days
  {
    public override string Execute()
    {
      // Use the alias as a type
      var deck = new List<Card>
      {
        ("Ace", 11, "rEd", new List<int>{1,2}),
        ("Two", 2, "blue", new List<int>{3,4}),
        ("King", 77, "pink", new List<int>{5,6})
      };

      deck.Sort((card1, card2) => card1.Number.CompareTo(card2.Number));
      deck.Add(new Card("Queen", 1, "blaCK", new List<int> { 7, 8 }));

      // first red card
      int indexR = deck.FindIndex(card => card.Color.SelfmadeLowerCase == "red");  // StringExtension Property
      Debug.WriteLine($"First red card Has a Rank: {deck[indexR].Rank} ");
      // first black card
      int indexB = deck.FindIndex(card => card.Color.SelfmadeLowerCase == "black");
      Debug.WriteLine("First black card Has a Rank:" + "".SelfmadeUpperCaseRank(deck[indexB])); // StringExtension Funktion with Parameter

      foreach (var face in Card.GetAllFaces()) // CardExtension Static Funktion
      {
        Debug.WriteLine($"Valid Faces: {face}");
      }
      // Check our deck for face cards
      foreach (var card in deck)
      {
        Debug.WriteLine($"Rank: {card.Rank}, IsFace: {(card.IsFaceCard ? "Yes" : "No")}"); // CardExtension Property
      }

      return deck.Sum(card => card.GetDisplayValue()).ToString(); // CardExtension Funktion
    }
  }
}