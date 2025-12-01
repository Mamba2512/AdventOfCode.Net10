
using System.Diagnostics;
namespace AdventOfCodeNet10._2023.Day_08
{
  using Node = (string CurrentNode, string LeftValue, string RightValue);

  internal static class NodeExtension
  {
    extension(Node node)
    {
      internal bool IsEndNode => node.CurrentNode.EndsWith("Z");
      internal bool IsStartNode => node.CurrentNode.EndsWith("A");
    }
  }
  internal class Part_2_2023_Day_08 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/8
    --- Part Two ---
    The sandstorm is upon you and you aren't any closer to escaping the wasteland.
    You had the camel follow the instructions, but you've barely left your starting
    position. It's going to take significantly more steps to escape!

    What if the map isn't for people - what if the map is for ghosts? Are ghosts
    even bound by the laws of spacetime? Only one way to find out.

    After examining the maps a bit longer, your attention is drawn to a curious
    fact: the number of nodes with names ending in A is equal to the number ending
    in Z! If you were a ghost, you'd probably just start at every node that ends
    with A and follow all of the paths at the same time until they all
    simultaneously end up at nodes that end with Z.

    For example:

    LR

    11A = (11B, XXX)
    11B = (XXX, 11Z)
    11Z = (11B, XXX)
    22A = (22B, XXX)
    22B = (22C, 22C)
    22C = (22Z, 22Z)
    22Z = (22B, 22B)
    XXX = (XXX, XXX)
    Here, there are two starting nodes, 11A and 22A (because they both end with A).
    As you follow each left/right instruction, use that instruction to
    simultaneously navigate away from both nodes you're currently on. Repeat this
    process until all of the nodes you're currently on end with Z. (If only some of
    the nodes you're on end with Z, they act like any other node and you continue
    as normal.) In this example, you would proceed as follows:

    Step 0: You are at 11A and 22A.
    Step 1: You choose all of the left paths, leading you to 11B and 22B.
    Step 2: You choose all of the right paths, leading you to 11Z and 22C.
    Step 3: You choose all of the left paths, leading you to 11B and 22Z.
    Step 4: You choose all of the right paths, leading you to 11Z and 22B.
    Step 5: You choose all of the left paths, leading you to 11B and 22C.
    Step 6: You choose all of the right paths, leading you to 11Z and 22Z.
    So, in this example, you end up entirely on nodes that end in Z after 6 steps.

    Simultaneously start on every node that ends with A. How many steps does it
    take before you're only on nodes that end with Z?--- Part Two ---
    The sandstorm is upon you and you aren't any closer to escaping the wasteland.
    You had the camel follow the instructions, but you've barely left your starting
    position. It's going to take significantly more steps to escape!

    What if the map isn't for people - what if the map is for ghosts? Are ghosts
    even bound by the laws of spacetime? Only one way to find out.

    After examining the maps a bit longer, your attention is drawn to a curious
    fact: the number of nodes with names ending in A is equal to the number ending
    in Z! If you were a ghost, you'd probably just start at every node that ends
    with A and follow all of the paths at the same time until they all
    simultaneously end up at nodes that end with Z.

    For example:

    LR

    11A = (11B, XXX)
    11B = (XXX, 11Z)
    11Z = (11B, XXX)
    22A = (22B, XXX)
    22B = (22C, 22C)
    22C = (22Z, 22Z)
    22Z = (22B, 22B)
    XXX = (XXX, XXX)
    Here, there are two starting nodes, 11A and 22A (because they both end with A).
    As you follow each left/right instruction, use that instruction to
    simultaneously navigate away from both nodes you're currently on. Repeat this
    process until all of the nodes you're currently on end with Z. (If only some of
    the nodes you're on end with Z, they act like any other node and you continue
    as normal.) In this example, you would proceed as follows:

    Step 0: You are at 11A and 22A.
    Step 1: You choose all of the left paths, leading you to 11B and 22B.
    Step 2: You choose all of the right paths, leading you to 11Z and 22C.
    Step 3: You choose all of the left paths, leading you to 11B and 22Z.
    Step 4: You choose all of the right paths, leading you to 11Z and 22B.
    Step 5: You choose all of the left paths, leading you to 11B and 22C.
    Step 6: You choose all of the right paths, leading you to 11Z and 22Z.
    So, in this example, you end up entirely on nodes that end in Z after 6 steps.

    Simultaneously start on every node that ends with A. How many steps does it
    take before you're only on nodes that end with Z?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 

    public List<string> Direction = new List<string>();
    public List<Node> Nodes = new List<Node>();
    public List<Node> StartNodes = new List<Node>();
    public override string Execute()
    {
      Nodes.Clear();
      Direction.Clear();
      StartNodes.Clear();

      string result = "";
      long totalCount = 0;

      bool isDirection = true;
      Node testNode = ("", "", "");

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_08.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_08.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        if (isDirection)
        {
          foreach (var dir in line)
          {
            Direction.Add(dir.ToString());
          }
          isDirection = false;
        }
        else
        {
          var parts = line.Split(" = ", StringSplitOptions.RemoveEmptyEntries);
          var currentNode = parts[0];
          var parts1 = parts[1].Split(", ", StringSplitOptions.RemoveEmptyEntries);
          var leftVal = parts1[0].Split("(", StringSplitOptions.RemoveEmptyEntries)[0];
          var rightVal = parts1[1].Split(")", StringSplitOptions.RemoveEmptyEntries)[0];
          Nodes.Add((currentNode, leftVal, rightVal));
          //if (currentNode.EndsWith("A"))
          //{
          //  StartNodes.Add((currentNode, leftVal, rightVal));
          //}
          //if (currentNode.EndsWith("Z"))
          //{
          //  EndNodes.Add((currentNode, leftVal, rightVal));
          //}
        }
      }
      if (Lines.Count == 0)
      {
        return 0.ToString();
      }

      StartNodes = Nodes.Where(x => x.IsStartNode).ToList();
      //EndNodes = Nodes.Where(x => x.IsEndNode).ToList();


      List<int> stepCounts = new List<int>();

      foreach (var node in StartNodes)
      {
        var currentDirection = NextDirection(0);
        stepCounts.Add(ReachToEndNode(node, 0, currentDirection));
      }

      // Calculate LCM of all cycle lengths
      totalCount = stepCounts[0];
      for (int i = 1; i < stepCounts.Count; i++)
      {
        totalCount = LCM(totalCount, stepCounts[i]);
      }

      result = totalCount.ToString();
      return result;
    }

    private long GCD(long a, long b)
    {
      while (b != 0)
      {
        long temp = b;
        b = a % b;
        a = temp;
      }
      return a;
    }

    private long LCM(long a, long b)
    {
      return (a / GCD(a, b)) * b;
    }

    public int ReachToEndNode(Node currentNode, int stepCount, string currentDirection)
    {
      var endWithZ = currentNode.CurrentNode.EndsWith("Z");
      while (!currentNode.CurrentNode.EndsWith("Z"))
      {
        var nextNodeValue = currentDirection == "L" ? currentNode.LeftValue : currentNode.RightValue;
        var nextNode = Nodes.First(x => x.CurrentNode == nextNodeValue);
        stepCount++;
        var nextDirection = NextDirection(stepCount);
        currentNode = nextNode;
        currentDirection = nextDirection;
      }

      return stepCount;
    }

    public string NextDirection(int stepCount)
    {
      var directionIndex = stepCount % Direction.Count;
      return Direction[directionIndex];
    }

    public bool AreAllEndNodesReached(List<Node> currentInputNodeList)
    {
      return currentInputNodeList.All(x => x.IsEndNode);
    }
  }
}

