namespace AdventOfCodeNet10._2023.Day_02
{
  internal class Part_2_2023_Day_02 : Days
  {
    public Dictionary<int, List<string>> Input = new Dictionary<int, List<string>>();
    List<string> GameInput = new List<string>();
    List<int> ValidGames = new List<int>();
    /// <summary>
    /*
    https://adventofcode.com/2023/day/2
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      int iResult = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_02.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_02.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        var colonSplit = line.Split(':');
        var gameNum = colonSplit[0].Split(' ')[1];
        var completeGameInput = colonSplit[1].Trim();
        var gameInputs = completeGameInput.Split(';');
        var gameInput = gameInputs.ToList();
        Input.Add(int.Parse(gameNum), gameInput); //this doesnt work

        totalCount++;
      }

      var listOfPowers = GetPowerOfCubes(Input);
      foreach (var elt in listOfPowers)
      {
        iResult += elt;
      }

      totalCount = iResult;

      result = totalCount.ToString();
      return result;
    }

    public List<int> GetPowerOfCubes (Dictionary<int, List<string>> myInput)
    {
      Dictionary<int, List<int>> gameColorValues = new Dictionary<int, List<int>>();
      List<int> result = new List<int>();
      foreach (var kvp in myInput)
      {
        var gameNum = kvp.Key;
        var gameInput = kvp.Value; //gameInput: red 5, blue 13, green 8; red 12, yellow 3, blue 7
        var redValues = new List<int>();
        var greenValues = new List<int>();
        var blueValues = new List<int>();

        foreach (var currentDraw in gameInput)//currentDraw: red 5, blue 13, green 8
        {
          var drawResult = currentDraw.Split(',');//drawResult: red 5 | blue 13 | green 8
          foreach (var elt in drawResult)
          {
            var color = elt.Trim().Split(' ')[1];
            var number = Int32.Parse(elt.Trim().Split(' ')[0]);
            if (color == "red")
            {
              redValues.Add(number);
            }
            if (color == "green")
            {
              greenValues.Add(number);
            }
            if (color == "blue")
            {
              blueValues.Add(number);
            }
          }
        }

        var maxRedValue = redValues.Count > 0 ? redValues.Max() : 0;
        var maxGreenValue = greenValues.Count > 0 ? greenValues.Max() : 0;
        var maxBlueValue = blueValues.Count > 0 ? blueValues.Max() : 0;

        result.Add(maxRedValue * maxGreenValue * maxBlueValue);
      }
      return result;
    }

    public List<int> GetValidGames(Dictionary<int, List<string>> myInput)
    {
      List<int> result = new List<int>();

      foreach (var kvp in myInput)
      {
        var gameNum = kvp.Key;
        var isValid = IsGameValid(kvp.Value);
        if (isValid)
        {
          result.Add(gameNum);
        }
      }
      return result;
    }

    public bool IsGameValid(List<string> gameInput) //gameInput: red 5, blue 13, green 8; red 12, yellow 3, blue 7
    {
      bool result = true;

      foreach (var currentDraw in gameInput)//currentDraw: red 5, blue 13, green 8
      {
        var drawResult = currentDraw.Split(',');//drawResult: red 5 | blue 13 | green 8
        foreach (var elt in drawResult)
        {
          var color = elt.Trim().Split(' ')[1];
          var number = Int32.Parse(elt.Trim().Split(' ')[0]);
          if (color == "red" && number > 12)
          {
            result = result && false;
          }
          if (color == "green" && number > 13)
          {
            result = result && false;
          }
          if (color == "blue" && number > 14)
          {
            result = result && false;
          }
        }
      }
      return result;
    }
  }
}
