namespace AdventOfCodeNet10.Extensions
{
  internal class Directions
  {
    public static Point ToLeft { get; } = (-1, 0);
    public static Point ToRight { get; } = (1, 0);
    public static Point ToUp { get; } = (0, -1);
    public static Point ToDown { get; } = (0, 1);

    public static Point[] WithoutDiagonals { get; } =
    [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
    ];

    public static Point[] DiagonalsOnly { get; } =
    [
        (1, 1),
        (-1, 1),
        (1, -1),
        (-1, -1)
    ];

    public static Point[] WithDiagonals { get; } =
    [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
        (1, 1),
        (-1, 1),
        (1, -1),
        (-1, -1)
    ];
  }
}