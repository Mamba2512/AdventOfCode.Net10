namespace AdventOfCodeNet10._2025.Day_06
{
  using System.Diagnostics;
  using Problem = (List<string> Numbers, string Operator);
  internal class Part_2_2025_Day_06 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/6
    The big cephalopods come back to check on how things are going. When they see
    that your grand total doesn't match the one expected by the worksheet, they
    realize they forgot to explain how to read cephalopod math.
    
    Cephalopod math is written right-to-left in columns. Each number is given in
    its own column, with the most significant digit at the top and the least
    significant digit at the bottom. (Problems are still separated with a column
    consisting only of spaces, and the symbol at the bottom of the problem is still
    the operator to use.)
    
    Here's the example worksheet again:
    
    123 328  51 64
    45 64  387 23
    6 98  215 314
    *   +   *   +
    Reading the problems right-to-left one column at a time, the problems are now
    quite different:
    
    The rightmost problem is 4 + 431 + 623 = 1058
    The second problem from the right is 175 * 581 * 32 = 3253600
    The third problem from the right is 8 + 248 + 369 = 625
    Finally, the leftmost problem is 356 * 24 * 1 = 8544
    Now, the grand total is 1058 + 3253600 + 625 + 8544 = 3263827.
    
    Solve the problems on the math worksheet again. What is the grand total found
    by adding together all of the answers to the individual problems?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    List<Problem> problems = new List<Problem>();
    public override string Execute()
    {
      problems.Clear();
      string result = "";
      long totalCount = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2025_Day_06.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2025_Day_06.txt already stored in "Lines"
      //
      var firstLine = Lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
      var problemCount = firstLine.Length;
      for (int i = 0; i < problemCount; i++)
      {
        problems.Add((new List<string>(), ""));
      }

      //foreach (var line in Lines)
      //{
      //  Debug.WriteLine($"Processing line: {line}");
      //}
      //string filePath = @"C:\_develop\Automation_Technologies\AdventOfCode.Net10\AdventOfCodeNet10\2025\Day_06\Test-Example-Input_2025_Day_06.txt";
      string filePath = @"C:\_develop\Automation_Technologies\AdventOfCode.Net10\AdventOfCodeNet10\2025\Day_06\Input_2025_Day_06.txt";
      var rawLines = File.ReadAllLines(filePath)
                         .Where(l => !string.IsNullOrWhiteSpace(l))
                         .ToList();


      var maxLineLength = rawLines.Max(l => l.Length);
      var myNumberStr = new List<string>();
      for (int i = maxLineLength - 1; i >= 0; i--)
      {
        bool transferToNextProblem = false;

        var columnChars = new List<char>();
        foreach (var line in rawLines)
        {
          if (line.Length > i)
          {
            columnChars.Add(line[i]);
          }
          else
          {
            columnChars.Add(' ');
          }
        }
        var columnCharsWithoutSpaces = columnChars.Where(c => c != ' ').ToList();
        char myOperator = ' ';
        if (columnCharsWithoutSpaces.Contains('*') || columnCharsWithoutSpaces.Contains('+'))
        {
          myOperator = columnCharsWithoutSpaces[columnCharsWithoutSpaces.Count - 1];
          columnCharsWithoutSpaces.RemoveAt(columnCharsWithoutSpaces.Count - 1);
          transferToNextProblem = true;
        }

        var columnString = new string(columnCharsWithoutSpaces.ToArray()).TrimEnd();
        myNumberStr.Add(columnString);
        if (transferToNextProblem)
        {
          problems.Add((new List<string>(myNumberStr), myOperator.ToString()));
          myNumberStr.Clear();
        }
      }
      foreach (var prob in problems)
      {
        totalCount += SolveMyProblem(prob);
      }

      result = totalCount.ToString();
      return result;
    }

    private long SolveMyProblem(Problem prob)
    {
      long problemResult = 0;
      if (prob.Numbers.Count > 0)
      {
        if (prob.Operator == "+")
        {
          problemResult = 0;
          foreach (var n in prob.Numbers)
          {
            if (Int32.TryParse(n, out int myN))
            {
              problemResult += myN;
            }
          }
        }
        else if (prob.Operator == "*")
        {
          problemResult = 1;
          foreach (var n in prob.Numbers)
          {
            if (Int32.TryParse(n, out int myN))
            {
              problemResult *= myN;
            }
          }
        }
      }
      else
      {
        problemResult = 0;
      }
      return problemResult;
    }
  }
}
