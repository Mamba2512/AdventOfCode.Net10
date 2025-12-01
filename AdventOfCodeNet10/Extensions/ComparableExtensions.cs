using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeNet10.Extensions
{
  public enum IncludeBounds
  {
    None,
    Lower,
    Upper,
    Both
  }

  public static class ComparableExtensions
  {
    public static bool InRange<T>(this T value, T lower, T upper, IncludeBounds bounds = IncludeBounds.None) where T : IComparable<T>
    {
      switch (bounds)
      {
        case IncludeBounds.Lower:
          return value.CompareTo(lower) >= 0 && value.CompareTo(upper) < 0;
        case IncludeBounds.Upper:
          return value.CompareTo(lower) > 0 && value.CompareTo(upper) <= 0;
        case IncludeBounds.Both:
          return value.CompareTo(lower) >= 0 && value.CompareTo(upper) <= 0;
        default:
          return value.CompareTo(lower) > 0 && value.CompareTo(upper) < 0;
      }
    }
  }
}
