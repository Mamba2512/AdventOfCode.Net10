using System.Diagnostics;
using System.Numerics;

namespace AdventOfCodeNet10.Extensions
{
  [DebuggerDisplay("({X}, {Y}, {Z})")]

  internal class Coordinate3d
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }


    // Constructor for long
    public Coordinate3d(int x, int y, int z)
    {
      X = x;
      Y = y;
      Z = z;
    }

  }
}