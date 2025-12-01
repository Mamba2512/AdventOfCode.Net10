namespace AdventOfCodeNet10._2023.Day_09
{
  internal class Part_2_2023_Day_09 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/9
    --- Part Two ---
    Of course, it would be nice to have even more history included in your report.
    Surely it's safe to just extrapolate backwards as well, right?
    
    For each history, repeat the process of finding differences until the sequence
    of differences is entirely zero. Then, rather than adding a zero to the end and
    filling in the next values of each previous sequence, you should instead add a
    zero to the beginning of your sequence of zeroes, then fill in new first values
    for each previous sequence.
    
    In particular, here is what the third example history looks like when
    extrapolating back in time:
    
    5  10  13  16  21  30  45
    5   3   3   5   9  15
    -2   0   2   4   6
    2   2   2   2
    0   0   0
    Adding the new values on the left side of each sequence from bottom to top
    eventually reveals the new left-most history value: 5.
    
    Doing this for the remaining example data above results in previous values of
    -3 for the first history and 0 for the second history. Adding all three new
    values together produces 2.
    
    Analyze your OASIS report again, this time extrapolating the previous value for
    each history. What is the sum of these extrapolated values?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      var Input = new List<List<long>>();
      string result = "";
      long totalCount = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_09.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_09.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        var currentListOfNums = new List<long>();
        var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var num in split)
        {
          currentListOfNums.Add(long.Parse(num));
        }
        Input.Add(currentListOfNums);
      }

      foreach (var input in Input)
      {
        totalCount += GetNextValue(input);
      }
      result = totalCount.ToString();
      return result;
    }

    public long GetNextValue(List<long> CurrentListOfNums)
    {
      List<List<long>> allSequences = new List<List<long>>();
      allSequences.Add(CurrentListOfNums);
      while (!allSequences.Last().All(x => x == 0))
      {
        var currentNums = allSequences.Last();
        var nextSequence = new List<long>();
        for (int i = 1; i < currentNums.Count; i++)
        {
          nextSequence.Add(currentNums[i] - currentNums[i - 1]);
        }

        allSequences.Add(nextSequence);
      }
      // Now we have all sequences, we can add a last elts of all the lists to get last value for the main list
      long result = 0;
      var temp = new List<long>();

      for (int i = allSequences.Count - 1; i > 0; i--)
      {
        result = allSequences[i - 1].First() - result;
      }

      return result;

    }



  }
}