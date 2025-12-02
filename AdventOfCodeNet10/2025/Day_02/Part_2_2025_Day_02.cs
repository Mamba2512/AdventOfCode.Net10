using System.Diagnostics;
using IdRange = (long Start, long End);
namespace AdventOfCodeNet10._2025.Day_02
{
  internal class Part_2_2025_Day_02 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/2
    The clerk quickly discovers that there are still invalid IDs in the ranges in
    your list. Maybe the young Elf was doing other silly patterns as well?
    
    Now, an ID is invalid if it is made only of some sequence of digits repeated at
    least twice. So, 12341234 (1234 two times), 123123123 (123 three times),
    1212121212 (12 five times), and 1111111 (1 seven times) are all invalid IDs.
    
    From the same example as before:
    
    11-22 still has two invalid IDs, 11 and 22.
    95-115 now has two invalid IDs, 99 and 111.
    998-1012 now has two invalid IDs, 999 and 1010.
    1188511880-1188511890 still has one invalid ID, 1188511885.
    222220-222224 still has one invalid ID, 222222.
    1698522-1698528 still contains no invalid IDs.
    446443-446449 still has one invalid ID, 446446.
    38593856-38593862 still has one invalid ID, 38593859.
    565653-565659 now has one invalid ID, 565656.
    824824821-824824827 now has one invalid ID, 824824824.
    2121212118-2121212124 now has one invalid ID, 2121212121.
    Adding up all the invalid IDs in this example produces 4174379265.
    
    What do you get if you add up all of the invalid IDs using these new rules?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    List<IdRange> IdRanges = new();
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      IdRanges.Clear();

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2025_Day_02.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2025_Day_02.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        var splittedIDRanges = line.Split(',');
        foreach (var idRange in splittedIDRanges)
        {
          if (string.IsNullOrWhiteSpace(idRange))
            continue;
          var splittedRange = idRange.Split('-');
          var startID = long.Parse(splittedRange[0]);
          var endID = long.Parse(splittedRange[1]);
          IdRanges.Add((startID, endID));
          totalCount = totalCount + GetInvalidIDNumsInRange((startID, endID)).Sum();
        }
      }
      result = totalCount.ToString();
      return result;
    }

    public List<long> GetInvalidIDNumsInRange(IdRange idRange)
    {
      List<long> invalidIDs = new();
      for (long id = idRange.Start; id <= idRange.End; id++)
      {
        var idString = id.ToString();
        if (IsInvalidNumber(id))
        {
          invalidIDs.Add(id);
        }

      }
      return invalidIDs;
    }

    public bool IsInvalidNumber(long number)
    {
      var numberString = number.ToString();
      int length = numberString.Length;
      
      for (int i = 0; i < length / 2; i++)
      {
        var numOfDigitsToCheck = i + 1;
        if (length % numOfDigitsToCheck != 0)
        {
          continue;
        }

        var chunk = numberString.Substring(0, numOfDigitsToCheck);
        bool allChunksMatch = false;
        for (int j = numOfDigitsToCheck; j < length; j += numOfDigitsToCheck)
        {
          var currentChunk = numberString.Substring(j, numOfDigitsToCheck);
          if (currentChunk != chunk)
          {
            allChunksMatch = false;
            break;
          }
          allChunksMatch = true;
        }
        if (allChunksMatch)
        {       
          return true;
        }
      }
      return false;
    }
  }
}

