Instructions:
- Open Solution in VS2026 Insiders >= Version 18 (27. Oktober 2025 - [11123.170])
  Solution uses .NET 10 C# 14 release on 11.Nov 2025

- Tools -> Options => Languages -> Default -> Tabs => 
  Tab size      = 2
  Indent size   = 2
  Tab character = Insert spaces 

- Rebuild both Projects in the Solution
- Set "FileGeneratorForAocNet10" as Startup Project and run it !!

- If there were no issues, "AdventOfCodeNet9"-Project should have reloaded
  On some of my tests the "2020"-Folder looked a bit weird, just restart VS2026 
- Rebuild both Projects in the Solution

- Set "AdventOfCodeNet9" as Startup Project and run it !!

- Press some random buttons to see if you get "0" everywhere.
- You can now delete the FileGeneratorForAocNet10-Project from the solution, 
  we'll never need it again.

- foreach Part of each Day in Every-Year of AOC
  you can write your C#-code in a dedicated class.
- Insert "Input" and "Test-Example-Input" for this day and Rebuild
- Pressing the corresponding button on the MainForm, 
  the Day-Part will be executed with both of them,
  measuring the time only for the "real Input"

Best Regards
Collin

PS: Try compressing the folder again and see how it compares to the initial ".zip" :-)
