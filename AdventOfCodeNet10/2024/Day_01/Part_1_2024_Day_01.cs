using System.Diagnostics;
namespace AdventOfCodeNet10._2024.Day_01
{
  internal class Part_1_2024_Day_01 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/1
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";
      int totalCount = 0;
      List<string> values = new List<string>();
      

      int counter = 0;
      int num = 0;
      string value = "";

      foreach (var line in Lines)
      {
        if (line.Trim().Contains("[DEBUG]"))
        {
          continue;
          
          //counter++;
        }
        Debug.WriteLine(line);

      }
      List<string> unique = values.Distinct().ToList();
      result = num.ToString();
      return result;
    }

    public static List<int> resultantList(List<int> l1, List<int> l2)
    {
      List<int> resultList = new List<int>();

      for (int i = 0; i < l1.Count; i++)
      {
        resultList.Add(Math.Abs(l1[i] - l2[i]));
      }
      return resultList;
    }
  }
}