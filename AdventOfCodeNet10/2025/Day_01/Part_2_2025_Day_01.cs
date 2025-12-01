using System.Diagnostics;
using Move = (string Direction, int Distance);
namespace AdventOfCodeNet10._2025.Day_01
{
  internal class Part_2_2025_Day_01 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/1
    You're sure that's the right password, but the door won't open. You knock, but
    nobody answers. You build a snowman while you think.
    
    As you're rolling the snowballs for your snowman, you find another security
    document that must have fallen into the snow:
    
    "Due to newer security protocols, please use password method 0x434C49434B until
    further notice."
    
    You remember from the training seminar that "method 0x434C49434B" means you're
    actually supposed to count the number of times any click causes the dial to
    point at 0, regardless of whether it happens during a rotation or at the end of
    one.
    
    Following the same rotations as in the above example, the dial points at zero a
    few extra times during its rotations:
    
    The dial starts by pointing at 50.
    The dial is rotated L68 to point at 82; during this rotation, it points at 0
    once.
    The dial is rotated L30 to point at 52.
    The dial is rotated R48 to point at 0.
    The dial is rotated L5 to point at 95.
    The dial is rotated R60 to point at 55; during this rotation, it points at 0
    once.
    The dial is rotated L55 to point at 0.
    The dial is rotated L1 to point at 99.
    The dial is rotated L99 to point at 0.
    The dial is rotated R14 to point at 14.
    The dial is rotated L82 to point at 32; during this rotation, it points at 0
    once.
    In this example, the dial points at 0 three times at the end of a rotation,
    plus three more times during a rotation. So, in this example, the new password
    would be 6.
    
    Be careful: if the dial were pointing at 50, a single rotation like R1000 would
    cause the dial to point at 0 ten times before returning back to 50!
    
    Using password method 0x434C49434B, what is the password to open the door?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    List<Move> moves = new List<Move>();
    int zeroPositions = new();
    public override string Execute()
    {
      moves.Clear();
      zeroPositions = 0;
      string result = "";
      long totalCount = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2025_Day_01.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2025_Day_01.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        
        var direction = line.Substring(0, 1);
        var distance = int.Parse(line.Substring(1));
        moves.Add((direction, distance));
      }
      int nextPos = 50;
      foreach (var move in moves)
      {
        Debug.WriteLine($"---{move.Direction}{move.Distance} from position {nextPos} ---");
        var (newPos, currentZeroCounts) = TraverseLockPath(nextPos, move);

        Debug.WriteLine($"Result: Position {nextPos} → {newPos}, Zeros: {currentZeroCounts}");
        Debug.WriteLine($"Running total: {totalCount} + {currentZeroCounts} = {totalCount + currentZeroCounts}");
        nextPos = newPos;
        totalCount += currentZeroCounts;
      }

      result = totalCount.ToString();
      return result;
    }

    public (int newPosition, int zeroCount) TraverseLockPath(int pos, Move nextMove)
    {
      var direction = nextMove.Direction;
      var distance = nextMove.Distance;
      int finalPos = pos;
      int zeroCount = 0;
      
      
      switch (direction)
      {
        case "L":
          finalPos = (pos - distance) % 100;
          if (finalPos < 0)
          {
            finalPos += 100;
          }
          
          Debug.Write($"  L: pos={pos}, distance={distance}, finalPos={finalPos}, ");

          if (pos > 0 && distance >= pos)
          {
            // We definitely hit 0 at least once
            int remainingAfterFirstZero = distance - pos;
            zeroCount = 1 + (remainingAfterFirstZero / 100);
            Debug.WriteLine($"crosses 0, remaining after first={remainingAfterFirstZero}, count={zeroCount}");
          }
          else if (pos == 0 && distance >= 100)
          {
            zeroCount = distance/100;
            Debug.WriteLine($"distance == pos (lands on 0), count={zeroCount}");
          }
          else
          {
            Debug.WriteLine($"doesn't reach 0, count={zeroCount}");
          }
          break;
        case "R":
          finalPos = (pos + distance) % 100;
          Debug.Write($"  R: pos={pos}, distance={distance}, finalpos={finalPos}, ");

          if(pos > 0)
          {
            int distanceToZero = 100 - pos;

            if (distance >= distanceToZero)
            {
              int remainingAfterFirstZero = distance - distanceToZero;
              zeroCount = 1 + (remainingAfterFirstZero / 100);
              Debug.WriteLine($"crosses 0, remaining after first={remainingAfterFirstZero}, count={zeroCount}");
            }
            else
            {
              Debug.WriteLine($"doesn't reach 0, count={zeroCount}");
            }
          }
          else //pos == 0
          {
            if (distance >= 100)
            {
              zeroCount = distance / 100;
              Debug.WriteLine($"starting at 0, makes {zeroCount} full loops");
            }
            else
            {
              Debug.WriteLine($"starting at 0, doesn't loop back");
            }
      
          }
          break;
      }
      return (finalPos, zeroCount);
    }
  }
}