using Point = AdventOfCodeNet10.Extensions.Point;
using Node = (string CurrentNode, string LeftValue, string RightValue);
using System.Diagnostics;
namespace AdventOfCodeNet10._2023.Day_08
{
  internal class Part_1_2023_Day_08 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/8
    --- Day 8: Haunted Wasteland ---
    You're still riding a camel across Desert Island when you spot a sandstorm
    quickly approaching. When you turn to warn the Elf, she disappears before your
    eyes! To be fair, she had just finished warning you about ghosts a few minutes
    ago.
    
    One of the camel's pouches is labeled "maps" - sure enough, it's full of
    documents (your puzzle input) about how to navigate the desert. At least,
    you're pretty sure that's what they are; one of the documents contains a list
    of left/right instructions, and the rest of the documents seem to describe some
    kind of network of labeled nodes.
    
    It seems like you're meant to use the left/right instructions to navigate the
    network. Perhaps if you have the camel follow the same instructions, you can
    escape the haunted wasteland!
    
    After examining the maps for a bit, two nodes stick out: AAA and ZZZ. You feel
    like AAA is where you are now, and you have to follow the left/right
    instructions until you reach ZZZ.
    
    This format defines each node of the network individually. For example:
    
    RL
    
    AAA = (BBB, CCC)
    BBB = (DDD, EEE)
    CCC = (ZZZ, GGG)
    DDD = (DDD, DDD)
    EEE = (EEE, EEE)
    GGG = (GGG, GGG)
    ZZZ = (ZZZ, ZZZ)
    Starting with AAA, you need to look up the next element based on the next
    left/right instruction in your input. In this example, start with AAA and go
    right (R) by choosing the right element of AAA, CCC. Then, L means to choose
    the left element of CCC, ZZZ. By following the left/right instructions, you
    reach ZZZ in 2 steps.
    
    Of course, you might not find ZZZ right away. If you run out of left/right
    instructions, repeat the whole sequence of instructions as necessary: RL really
    means RLRLRLRLRLRLRLRL... and so on. For example, here is a situation that
    takes 6 steps to reach ZZZ:
    
    LLR
    
    AAA = (BBB, BBB)
    BBB = (AAA, ZZZ)
    ZZZ = (ZZZ, ZZZ)
    Starting at AAA, follow the left/right instructions. How many steps are
    required to reach ZZZ?
     */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 
    public List<string> Direction = new List<string>();
    public List<Node> Nodes = new List<Node>();
    public override string Execute()
    {
      Nodes.Clear();
      Direction.Clear();

      string result = "";
      long totalCount = 0;

      bool isDirection = true;

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
        }
      }
      if (Lines.Count == 0)
      {
        return 0.ToString();
      }

      var startNode = Nodes.FirstOrDefault(x => x.CurrentNode == "AAA");
      var endNode = Nodes.FirstOrDefault(x => x.CurrentNode == "ZZZ");
      var currentDirection = NextDirection(0);

      totalCount = ReachToEndNode(startNode, endNode, 0, currentDirection);

      result = totalCount.ToString();
      return result;
    }

    public int ReachToEndNode(Node currentNode, Node endNode, int stepCount, string currentDirection)
    {
      while(currentNode.CurrentNode != endNode.CurrentNode)
      {
        var nextNodeValue = currentDirection == "L" ? currentNode.LeftValue : currentNode.RightValue;
        var nextNode = Nodes.First(x => x.CurrentNode == nextNodeValue);
        stepCount++;
        var nextDirection = NextDirection(stepCount);
        currentNode = nextNode;
        currentDirection = nextDirection;
      }

      return stepCount;


      /*
       * I get stackoverflow exception due to recursion : 
       * recursion is going too deep. 
       * Each recursive call adds a frame to the call stack, 
       * and after thousands of calls, you run out of stack space.
       * 
       * 
      if(currentNode.CurrentNode == endNode.CurrentNode)
      {
        return stepCount;
      }
      else
      {
        if(stepCount == 5339)
        {
          
        }
        var nextNodeValue = currentDirection == "L" ? currentNode.LeftValue : currentNode.RightValue;
        var nextNode = Nodes.First(x => x.CurrentNode == nextNodeValue);
        stepCount++;
        var nextDirection = NextDirection(stepCount);

        return ReachToEndNode(nextNode, endNode, stepCount, nextDirection);
    }*/
    }

    public string NextDirection(int stepCount)
    {
      //Debug.WriteLine($"StepCount: {stepCount}");
      var directionIndex = stepCount % Direction.Count;
      //Debug.WriteLine($"directionIndex: {directionIndex}");
      return Direction[directionIndex];
    }
  }
}
