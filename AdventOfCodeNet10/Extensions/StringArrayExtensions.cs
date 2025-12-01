using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeNet10.Extensions
{
  public static class StringArrayExtensions
  {
    /// <summary>
    /// Converts an array of strings to an array of Int32s, using Int32.Parse.
    /// If any of the strings cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static Int32[] AsInt32s(this string[] source)
    {
      return source.Select(Int32.Parse).ToArray();
    }

    /// <summary>
    /// Converts an array of strings to a List<Int32>, using Int32.Parse.
    /// If any of the strings cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static List<Int32> AsInt32List(this string[] source)
    {
      return source.Select(Int32.Parse).ToList();
    }

    /// <summary>
    /// Converts an array of strings to a List<Int32>, using Int32.TryParse.
    /// Values that cannot be parsed default to the specified defaultValue.
    /// </summary>
    public static List<Int32> AsInt32ListOrDefault(this string[] source, Int32 defaultValue = 0)
    {
      var result = new List<Int32>(source.Length);
      foreach (var s in source)
      {
        if (Int32.TryParse(s, out Int32 val))
        {
          result.Add(val);
        }
        else
        {
          result.Add(defaultValue);
        }
      }
      return result;
    }

    /// <summary>
    /// Converts an array of strings to an array of Int64s, using Int64.Parse.
    /// If any of the strings cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static Int64[] AsInt64s(this string[] source)
    {
      return source.Select(Int64.Parse).ToArray();
    }

    /// <summary>
    /// Converts an array of strings to a List<Int64>, using Int64.Parse.
    /// If any of the strings cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static List<Int64> AsInt64List(this string[] source)
    {
      return source.Select(Int64.Parse).ToList();
    }

    /// <summary>
    /// Converts an array of strings to a List<Int64>, using Int64.TryParse.
    /// Values that cannot be parsed default to the specified defaultValue.
    /// </summary>
    public static List<Int64> AsInt64ListOrDefault(this string[] source, Int64 defaultValue = 0L)
    {
      var result = new List<Int64>(source.Length);
      foreach (var s in source)
      {
        if (Int64.TryParse(s, out Int64 val))
        {
          result.Add(val);
        }
        else
        {
          result.Add(defaultValue);
        }
      }
      return result;
    }

    /// <summary>
    /// Converts an array of strings to an array of Int128s, using Int128.Parse.
    /// If any of the strings cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static Int128[] AsInt128s(this string[] source)
    {
      return source.Select(Int128.Parse).ToArray();
    }

    /// <summary>
    /// Converts an array of strings to a List<Int128>, using Int128.Parse.
    /// If any of the strings cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static List<Int128> AsInt128List(this string[] source)
    {
      return source.Select(Int128.Parse).ToList();
    }

    /// <summary>
    /// Converts an array of strings to a List<Int128>, using Int128.TryParse.
    /// Values that cannot be parsed default to the specified defaultValue.
    /// </summary>
    public static List<Int128> AsInt128ListOrDefault(this string[] source, Int128 defaultValue = default)
    {
      var result = new List<Int128>(source.Length);
      foreach (var s in source)
      {
        if (Int128.TryParse(s, out Int128 val))
        {
          result.Add(val);
        }
        else
        {
          result.Add(defaultValue == (Int128)default ? Int128.Zero : defaultValue);
        }
      }
      return result;
    }
  }
}
