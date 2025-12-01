using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using Point = AdventOfCodeNet10.Extensions.Point;
namespace AdventOfCodeNet10._2023.Day_17
{
  using state = (Point position, Point direction, int stepCount);
  internal class Part_2_2023_Day_17 : Days
  {

    /// <summary>
    /*
    https://adventofcode.com/2023/day/17
    The crucibles of lava simply aren't large enough to provide an adequate supply
    of lava to the machine parts factory. Instead, the Elves are going to upgrade
    to ultra crucibles.
    
    Ultra crucibles are even more difficult to steer than normal crucibles. Not
    only do they have trouble going in a straight line, but they also have trouble
    turning!
    
    Once an ultra crucible starts moving in a direction, it needs to move a minimum
    of four blocks in that direction before it can turn (or even before it can stop
    at the end). However, it will eventually start to get wobbly: an ultra crucible
    can move a maximum of ten consecutive blocks without turning.
    
    In the above example, an ultra crucible could follow this path to minimize heat
    loss:
    
    2>>>>>>>>1323
    32154535v5623
    32552456v4254
    34465858v5452
    45466578v>>>>
    143859879845v
    445787698776v
    363787797965v
    465496798688v
    456467998645v
    122468686556v
    254654888773v
    432267465553v
    In the above example, an ultra crucible would incur the minimum possible heat
    loss of 94.
    
    Here's another example:
    
    111111111111
    999999999991
    999999999991
    999999999991
    999999999991
    Sadly, an ultra crucible would need to take an unfortunate path like this one:
    
    1>>>>>>>1111
    9999999v9991
    9999999v9991
    9999999v9991
    9999999v>>>>
    This route causes the ultra crucible to incur the minimum possible heat loss of
    71.
    
    Directing the ultra crucible from the lava pool to the machine parts factory,
    what is the least heat loss it can incur?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    Dictionary<Point, char> Grid = new();
    HashSet<int> energyLoss = new();
    int Rows = 0;
    int Cols = 0;
    Point EndPos = new();
    Point StartPos = (0, 0);
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      Grid.Clear();
      energyLoss.Clear();
      int rowIdx = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_17.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_17.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        Cols = line.Length;
        int colIdx = 0;
        foreach (var ch in line)
        {
          Grid[new Point(colIdx, rowIdx)] = ch;
          colIdx++;
        }
        rowIdx++;
      }
      Rows = rowIdx;
      EndPos = (Cols - 1, Rows - 1);

      ExplorePaths(new Point(0, 0), new Point(1, 0)); // Start going right
      ExplorePaths(new Point(0, 0), new Point(0, 1)); // Start going down

      totalCount = energyLoss.Min();
      result = totalCount.ToString();
      return result;
    }


    private void ExplorePaths(Point startPos, Point currentDirection)
    {
      var pq = new PriorityQueue<state, int>();
      var visited = new HashSet<state>();

      pq.Enqueue((startPos, currentDirection, 0), 0);

      while (pq.Count > 0)
      {
        pq.TryDequeue(out var currentState, out var currentEnergyloss);

        if (!Grid.ContainsKey(currentState.position))
        {
          continue;
        }
        if (visited.Contains(currentState))
        {
          continue;
        }
        visited.Add(currentState);

        //calc cell cost
        var currentCellCost = Grid[currentState.position] - '0';
        if (currentState.position == StartPos && currentState.stepCount == 0)
        {
          currentCellCost = 0;
        }
        var newEnergyLoss = currentEnergyloss + currentCellCost;

        if (currentState.position == EndPos && currentState.stepCount >= 4)
        {
          //found the minimum value so return this value
          energyLoss.Add(newEnergyLoss);
          return;
        }

        //Option 1 : go straight
        if (currentState.stepCount < 10)
        {
          var newPos = currentState.position + currentState.direction;
          var newStepCount = currentState.stepCount + 1;
          var newState = (newPos, currentState.direction, newStepCount);
          pq.Enqueue(newState, newEnergyLoss);
        }

        if(currentState.stepCount < 4)
        {
          continue; //cannot turn yet
        }
        //Option 2: go left
        var leftDir = new Point(currentState.direction.Y, -currentState.direction.X);
        var leftPos = currentState.position + leftDir;
        var leftNewState = (leftPos, leftDir, 1);
        pq.Enqueue(leftNewState, newEnergyLoss);

        //Option 3: go right
        var rightDir = new Point(-currentState.direction.Y, currentState.direction.X);
        var rightPos = currentState.position + rightDir;
        var rightNewState = (rightPos, rightDir, 1);
        pq.Enqueue(rightNewState, newEnergyLoss);
      }

    }
  }
}
