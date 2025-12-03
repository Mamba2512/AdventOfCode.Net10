namespace AdventOfCodeNet10._2025.Day_03
{
  internal class Part_2_2025_Day_03 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/3
    The escalator doesn't move. The Elf explains that it probably needs more
    joltage to overcome the static friction of the system and hits the big red
    "joltage limit safety override" button. You lose count of the number of times
    she needs to confirm "yes, I'm sure" and decorate the lobby a bit while you
    wait.
    
    Now, you need to make the largest joltage by turning on exactly twelve
    batteries within each bank.
    
    The joltage output for the bank is still the number formed by the digits of the
    batteries you've turned on; the only difference is that now there will be 12
    digits in each bank's joltage output instead of two.
    
    Consider again the example from before:
    
    987654321111111
    811111111111119
    234234234234278
    818181911112111
    Now, the joltages are much larger:
    
    In 987654321111111, the largest joltage can be found by turning on everything
    except some 1s at the end to produce 987654321111.
    In the digit sequence 811111111111119, the largest joltage can be found by
    turning on everything except some 1s, producing 811111111119.
    In 234234234234278, the largest joltage can be found by turning on everything
    except a 2 battery, a 3 battery, and another 2 battery near the start to
    produce 434234234278.
    In 818181911112111, the joltage 888911112111 is produced by turning on
    everything except some 1s near the front.
    The total output joltage is now much larger: 987654321111 + 811111111119 +
    434234234278 + 888911112111 = 3121910778619.
    
    What is the new total output joltage?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2025_Day_03.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2025_Day_03.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        List<int> currentSequence = new List<int>();
        var temp = GetSequenceOfBatteries(line, currentSequence);
        totalCount += long.Parse(string.Join("", temp));
      }
      result = totalCount.ToString();
      return result;
    }

    private List<int> GetSequenceOfBatteries(string input, List<int> currentSequence)
    {
      const int Total_Batteries = 12;
      if (currentSequence.Count == Total_Batteries)
      {
        return currentSequence;
      }

      List<int> digits = new List<int>();
      List<int> copiedDigits = new List<int>();
      foreach (var c in input)
      {
        digits.Add(int.Parse(c.ToString()));
      }
      copiedDigits = digits.ToList();

      digits.Sort();
      digits.Reverse();

      int batteriesStillNeeded = Total_Batteries - currentSequence.Count;
      int maxValidPosition = input.Length - batteriesStillNeeded;

      int selectedIndex = -1;

      foreach(var largestCandidate in digits)
      {
        int idxInOriginal = copiedDigits.IndexOf(largestCandidate);
        if (idxInOriginal <= maxValidPosition)
        {
          selectedIndex = idxInOriginal;
          break;
        }
      }

      currentSequence.Add(copiedDigits[selectedIndex]);
      var remainingBank = input.Substring(selectedIndex + 1);
      GetSequenceOfBatteries(remainingBank, currentSequence);
      return currentSequence;
    }
  }
}
