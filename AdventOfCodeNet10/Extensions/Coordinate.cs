using System.Diagnostics;
using System.Numerics;

namespace AdventOfCodeNet10.Extensions
{
  [DebuggerDisplay("({X}, {Y})")]

  internal class Coordinate //: IComparable<Coordinate>
  {
    //public BigInteger X { get; set; }
    //public BigInteger Y { get; set; }


    //// Constructor for long
    //public Coordinate(BigInteger x, BigInteger y)
    //{
    //  X = x;
    //  Y = y;
    //}

    public int X { get; set; }
    public int Y { get; set; }



    public Coordinate(int x, int y)
    {
      X = x;
      Y = y;
    }

    // Method to add 2 coordinate
    public static Coordinate Add(Coordinate C1, Coordinate C2)
    {
      return new Coordinate(C1.X + C2.X, C1.Y + C2.Y);
    }
    // Method to add coordinate and a tuple
    public static Coordinate Add(Coordinate C1, (int X, int Y) C2)
    {
      return new Coordinate(C1.X + C2.X, C1.Y + C2.Y);
    }

    // Method to add tuple and a coordinate
    public static Coordinate Add((int X, int Y) C2, Coordinate C1)
    {
      return new Coordinate(C1.X + C2.X, C1.Y + C2.Y);
    }

    // Method to subtract 2 coordinate
    public static Coordinate Subtract(Coordinate C1, Coordinate C2)
    {
      return new Coordinate(C1.X - C2.X, C1.Y - C2.Y);
    }
    // Method to subtract coordinate and a tuple
    public static Coordinate Subtract(Coordinate C1, (int X, int Y) C2)
    {
      return new Coordinate(C1.X - C2.X, C1.Y - C2.Y);
    }

    // Method to subtract tuple and a coordinate
    public static Coordinate Subtract((int X, int Y) C2, Coordinate C1)
    {
      return new Coordinate(C2.X - C1.X, C2.Y - C1.Y);
    }

    //all of my overloads
    public static Coordinate operator +(Coordinate C1, Coordinate C2)
    {
      return Add(C1, C2);
    }
    public static Coordinate operator -(Coordinate C1, Coordinate C2)
    {
      return Subtract(C1, C2);
    }
    public static Coordinate operator +(Coordinate C1, (int X, int Y) C2)
    {
      return Add(C1, C2);
    }
    public static Coordinate operator +((int X, int Y) C2, Coordinate C1)
    {
      return Add(C1, C2);
    }
    public static Coordinate operator -(Coordinate C1, (int X, int Y) C2)
    {
      return Subtract(C1, C2);
    }

    public static Coordinate operator -((int X, int Y) C2, Coordinate C1)
    {
      return Subtract(C2, C1);
    }

    // Implicit conversion from (int, int) to Coordinate
    public static implicit operator Coordinate((int x, int y) tuple)
    {
      return new Coordinate(tuple.x, tuple.y);
    }
    // Overload the == operator for Coordinate comparison
    public static bool operator ==(Coordinate c1, Coordinate c2)
    {
      if (ReferenceEquals(c1, c2))
        return true;

      if (c1 is null || c2 is null)
        return false;

      return c1.X == c2.X && c1.Y == c2.Y;
    }

    // Overload the != operator for Coordinate comparison
    public static bool operator !=(Coordinate c1, Coordinate c2)
    {
      return !(c1 == c2);
    }

    // Override Equals method
    public override bool Equals(object obj)
    {
      if (obj is Coordinate other)
      {
        return this.X == other.X && this.Y == other.Y;
      }
      return false;
    }

    // Override GetHashCode method
    public override int GetHashCode()
    {
      return HashCode.Combine(X, Y);
    }

    // Override ToString method for readable display especially during breakpoints 
    public override string ToString()
    {
      return $"({X}, {Y})";
    }  

  }
}