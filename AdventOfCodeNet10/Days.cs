using System.Text;

namespace AdventOfCodeNet10
{
  public abstract class Days
  {
    public required List<string> Lines;
    private StringBuilder sb = null!;

    public void Init(string input)
    {
      sb = new StringBuilder();

      using (FileStream fs = new FileStream(input, FileMode.Open))
      {
        while (fs.Position < fs.Length)
        {
          sb.Append((char)fs.ReadByte());
        }
      }

      List<string> temp = sb.ToString().Split(new char[] { '\r', '\n' },
        StringSplitOptions.RemoveEmptyEntries).ToList<string>();

      if (Lines != null) Lines.Clear();
      Lines = new List<string>(temp.Count);

      foreach (var line in temp)
      {
        var newTrim = line.Trim();
        newTrim = newTrim.Trim('ï', '»', '¿');
        Lines.Add(newTrim);
      }
      temp.Clear();
      sb.Clear();
      GC.Collect(GC.MaxGeneration, GCCollectionMode.Optimized, false);
    }

    public abstract string Execute();
  }
}
