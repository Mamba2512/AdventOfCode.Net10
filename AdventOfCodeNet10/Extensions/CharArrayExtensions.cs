using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeNet10.Extensions
{
  public static class CharArrayExtensions
  {
    /// <summary>
    /// Converts an array of chars to an array of Int32s, using Int32.Parse(c.ToString()).
    /// If any of the char cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static Int32[] AsInt32s(this char[] source)
    {
      return source.Select(c => Int32.Parse(c.ToString())).ToArray();
    }

    /// <summary>
    /// Converts an array of chars to a List<Int32>, using Int32.Parse(c.ToString()).
    /// If any of the char cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static List<Int32> AsInt32List(this char[] source)
    {
      return source.Select(c => Int32.Parse(c.ToString())).ToList();
    }
  }
}
