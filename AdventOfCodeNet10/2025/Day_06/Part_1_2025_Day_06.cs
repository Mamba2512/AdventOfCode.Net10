namespace AdventOfCodeNet10._2025.Day_06
{
  using Problem = (List<long> Numbers, string Operator);
  internal class Part_1_2025_Day_06 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/6
    After helping the Elves in the kitchen, you were taking a break and helping
    them re-enact a movie scene when you over-enthusiastically jumped into the
    garbage chute!
    
    A brief fall later, you find yourself in a garbage smasher. Unfortunately, the
    door's been magnetically sealed.
    
    As you try to find a way out, you are approached by a family of cephalopods!
    They're pretty sure they can get the door open, but it will take some time.
    While you wait, they're curious if you can help the youngest cephalopod with
    her math homework.
    
    Cephalopod math doesn't look that different from normal math. The math
    worksheet (your puzzle input) consists of a list of problems; each problem has
    a group of numbers that need to either be either added (+) or multiplied (*)
    together.
    
    However, the problems are arranged a little strangely; they seem to be
    presented next to each other in a very long horizontal list. For example:
    
    123 328  51 64
    45 64  387 23
    6 98  215 314
    *   +   *   +
    Each problem's numbers are arranged vertically; at the bottom of the problem is
    the symbol for the operation that needs to be performed. Problems are separated
    by a full column of only spaces. The left/right alignment of numbers within
    each problem can be ignored.
    
    So, this worksheet contains four problems:
    
    123 * 45 * 6 = 33210
    328 + 64 + 98 = 490
    51 * 387 * 215 = 4243455
    64 + 23 + 314 = 401
    To check their work, cephalopod students are given the grand total of adding
    together all of the answers to the individual problems. In this worksheet, the
    grand total is 33210 + 490 + 4243455 + 401 = 4277556.
    
    Of course, the actual worksheet is much wider. You'll need to make sure to
    unroll it completely so that you can read the problems clearly.
    
    Solve the problems on the math worksheet. What is the grand total found by
    adding together all of the answers to the individual problems?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 
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
        problems.Add((new List<long>(), ""));
      }

      foreach (var line in Lines)
      {
        var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (line.Contains('*') || line.Contains('+'))
        {
          //operator
          for (int i = 0; i < split.Length; i++)
          {
            problems[i] = (problems[i].Numbers, split[i]);
          }
        }
        else
        {
          //number
          for (int i = 0; i < split.Length; i++)
          {
            problems[i].Numbers.Add(long.Parse(split[i]));
          }
        }
      }
      foreach (var problem in problems)
      {
        switch (problem.Operator)
        {
          case "*":
            {
              long product = 1;
              foreach (var number in problem.Numbers)
              {
                product *= number;
              }
              totalCount += product;
              break;
            }
            case "+":
            {
              long sum = 0;
              foreach (var number in problem.Numbers)
              {
                sum += number;
              }
              totalCount += sum;
              break;
            }
        }
      }
      result = totalCount.ToString();
      return result;
    }
  }
}
