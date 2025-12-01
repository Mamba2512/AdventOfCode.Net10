// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

int startingFrom = 2015;
int UntilTheYear = 2025;

try
{
  var input = new List<string>();
  for (int year = startingFrom; year <= UntilTheYear; year++)
  {
    input.Add(year.ToString());
  }

  var executable = new FileInfo(Assembly.GetExecutingAssembly().Location);
  var diExe = new DirectoryInfo(executable!.DirectoryName!);

  // D:\\UserData\\Source\\Repos\\AdventOfCodeNet10\\FileGeneratorForMainApp\\bin\\Debug\\net10.0\\
  var diTarget = new DirectoryInfo(Path.Combine(diExe.Parent!.Parent!.Parent!.Parent!.FullName, "AdventOfCodeNet10"));

  if (!diTarget.Exists)
  {
    Console.WriteLine($"Cannot create Files where they need to go !!!\n\nCheck: ");
    Console.WriteLine("");
    Console.WriteLine($"{diTarget.FullName}");
    Console.WriteLine("");
    Console.ReadKey();
    return;
  }


  foreach (var subfolderYear in input)
  {
    GenerateFoldersAndFiles(subfolderYear, diTarget);
  }

  Console.WriteLine($"Done generating Files\n\nCheck: ");
  Console.WriteLine("");
  Console.WriteLine($"{diTarget.FullName}");
  Console.WriteLine("");

  Console.WriteLine("All the generated Files were placed into the Main ProjectFolder");
  Console.WriteLine("");
  Console.WriteLine("Also the \"AdventOfCodeNet10.csproj\" File was changed, so it should automatically reload.");
  Console.WriteLine("");
  Console.WriteLine("");
  Console.WriteLine("#################################################################################");
  Console.WriteLine("###   DON'T FORGET to set \"AdventOfCodeNet10.csproj\" as STARTUP Project !!   ###");
  Console.WriteLine("#################################################################################");
  Console.WriteLine("");
}
catch (Exception exception)
{
  string msg = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}: {exception.Message}";
  Console.WriteLine(exception.Message);
}

Console.ReadKey();


void GenerateFoldersAndFiles(string SubfolderYear, DirectoryInfo di)
{
  var sfy = new DirectoryInfo(Path.Combine(di.FullName, SubfolderYear));
  if (!sfy.Exists) sfy.Create();

  StringBuilder sb = new StringBuilder();

  using (StreamReader sr = new StreamReader(Path.Combine(di.FullName, "AdventOfCodeNet10.csproj")))
  {
    sb.Append(sr.ReadToEnd());
  }
  sb.Replace("</Project>", "");

  sb.AppendLine("  <ItemGroup>");

  for (int day = 1; day <= 25; day++)
  {
    var daypath = new DirectoryInfo(Path.Combine(sfy.FullName, $"Day_{day:00}"));
    if (!daypath.Exists) daypath.Create();
    GenerateFiles(SubfolderYear, day, daypath);

    string text = """
    <None Update="##YEAR##\Day_##01##\Input_##YEAR##_Day_##01##.txt">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>    
    <None Update="##YEAR##\Day_##01##\Test-Example-Input_##YEAR##_Day_##01##.txt">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>

""";
    text = text.Replace("##YEAR##", SubfolderYear);
    text = text.Replace("##01##", day.ToString("00"));
    sb.Append(text);
  }
  sb.AppendLine("  </ItemGroup>");
  sb.AppendLine("</Project>");

  using (StreamWriter srInput = new StreamWriter(Path.Combine(di.FullName, "AdventOfCodeNet10.csproj"),
           Encoding.ASCII, new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.Write }))
  {
    srInput.Write(sb.ToString());
  }
  sb.Clear();
}

void GenerateFiles(string year, int day, DirectoryInfo daypath)
{
  using (StreamWriter srInput = new StreamWriter(Path.Combine(daypath.FullName, $"Input_{year}_Day_{day:00}.txt"),
           Encoding.ASCII, new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.Write }))
  {
    srInput.Write("\n");
  }
  using (StreamWriter srTestInput = new StreamWriter(Path.Combine(daypath.FullName, $"Test-Example-Input_{year}_Day_{day:00}.txt"),
           Encoding.ASCII, new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.Write }))
  {
    srTestInput.Write("\n");
  }

  for (int i = 1; i <= 2; i++)
  {
    using (StreamWriter srParts = new StreamWriter(Path.Combine(daypath.FullName, $"Part_{i}_{year}_Day_{day:00}.cs"),
             Encoding.ASCII, new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.Write }))
    {
      string text = """
namespace AdventOfCodeNet10._##YEAR##.Day_##01##
{
  internal class Part_##PART##_##YEAR##_Day_##01## : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/##YEAR##/day/##1##
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_##YEAR##_Day_##01##.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_##YEAR##_Day_##01##.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        totalCount++;
      }
      result = totalCount.ToString();
      return result;
    }
  }
}

""";
      text = text.Replace("##YEAR##", year);
      text = text.Replace("##PART##", i.ToString());
      text = text.Replace("##1##", day.ToString());
      text = text.Replace("##01##", day.ToString("00"));

      srParts.Write(text);
    }
  }
}
