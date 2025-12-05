using Range = (long Start, long End);
namespace AdventOfCodeNet10._2025.Day_05
{
  internal class Part_2_2025_Day_05 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/5
    The Elves start bringing their spoiled inventory to the trash chute at the back
    of the kitchen.
    
    So that they can stop bugging you when they get new inventory, the Elves would
    like to know all of the IDs that the fresh ingredient ID ranges consider to be
    fresh. An ingredient ID is still considered fresh if it is in any range.
    
    Now, the second section of the database (the available ingredient IDs) is
    irrelevant. Here are the fresh ingredient ID ranges from the above example:
    
    3-5
    10-14
    16-20
    12-18
    The ingredient IDs that these ranges consider to be fresh are 3, 4, 5, 10, 11,
    12, 13, 14, 15, 16, 17, 18, 19, and 20. So, in this example, the fresh
    ingredient ID ranges consider a total of 14 ingredient IDs to be fresh.
    
    Process the database file again. How many ingredient IDs are considered to be
    fresh according to the fresh ingredient ID ranges?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    List<Range> Ranges = new List<Range>();

    public override string Execute()
    {
      Ranges.Clear();

      string result = "";
      long totalCount = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2025_Day_05.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2025_Day_05.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        if (line.Contains('-'))
        {
          Ranges.Add((
            long.Parse(line.Split('-')[0]),
            long.Parse(line.Split('-')[1])
            ));
        }
      }

      var mergedRanges = MergeOverlappingRanges(Ranges);

      foreach (var range in mergedRanges)
      {
        totalCount += range.End - range.Start + 1; 
      }

      result = totalCount.ToString();
      return result;
    }

    private List<Range> MergeOverlappingRanges(List<Range> ranges)
    {
      var sortedRanges = ranges.OrderBy(r => r.Start).ToList();
      var merged = new List<Range>();
      var currentRange = sortedRanges[0];

      for (long i = 1; i < sortedRanges.Count; i++)
      {
        var nextRange = sortedRanges[(int)i];
        if ((nextRange.Start <= currentRange.End))
        {
          currentRange = (currentRange.Start, Math.Max(currentRange.End, nextRange.End));
        }
        else
        {
          merged.Add(currentRange);
          currentRange = nextRange;
        }
      }
      merged.Add(currentRange);
      return merged;
    }
  }
}