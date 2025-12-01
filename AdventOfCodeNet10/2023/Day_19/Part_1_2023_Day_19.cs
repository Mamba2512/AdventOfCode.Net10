namespace AdventOfCodeNet10._2023.Day_19
{
  using System.Diagnostics;
  using Part = (int x, int m, int a, int s);
  using Rule = (string attribute, string comparator, int value, string destination);
  internal class Part_1_2023_Day_19 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/19
    The Elves of Gear Island are thankful for your help and send you on your way.
    They even have a hang glider that someone stole from Desert Island; since
    you're already going that direction, it would help them a lot if you would use
    it to get down there and return it to them.
    
    As you reach the bottom of the relentless avalanche of machine parts, you
    discover that they're already forming a formidable heap. Don't worry, though -
    a group of Elves is already here organizing the parts, and they have a system.
    
    To start, each part is rated in each of four categories:
    
    x: Extremely cool looking
    m: Musical (it makes a noise when you hit it)
    a: Aerodynamic
    s: Shiny
    Then, each part is sent through a series of workflows that will ultimately
    accept or reject the part. Each workflow has a name and contains a list of
    rules; each rule specifies a condition and where to send the part if the
    condition is true. The first rule that matches the part being considered is
    applied immediately, and the part moves on to the destination described by the
    rule. (The last rule in each workflow has no condition and always applies if
    reached.)
    
    Consider the workflow ex{x>10:one,m<20:two,a>30:R,A}. This workflow is named ex
    and contains four rules. If workflow ex were considering a specific part, it
    would perform the following steps in order:
    
    Rule "x>10:one": If the part's x is more than 10, send the part to the workflow
    named one.
    Rule "m<20:two": Otherwise, if the part's m is less than 20, send the part to
    the workflow named two.
    Rule "a>30:R": Otherwise, if the part's a is more than 30, the part is
    immediately rejected (R).
    Rule "A": Otherwise, because no other rules matched the part, the part is
    immediately accepted (A).
    If a part is sent to another workflow, it immediately switches to the start of
    that workflow instead and never returns. If a part is accepted (sent to A) or
    rejected (sent to R), the part immediately stops any further processing.
    
    The system works, but it's not keeping up with the torrent of weird metal
    shapes. The Elves ask if you can help sort a few parts and give you the list of
    workflows and some part ratings (your puzzle input). For example:
    
    px{a<2006:qkq,m>2090:A,rfg}
    pv{a>1716:R,A}
    lnx{m>1548:A,A}
    rfg{s<537:gd,x>2440:R,A}
    qs{s>3448:A,lnx}
    qkq{x<1416:A,crn}
    crn{x>2662:A,R}
    in{s<1351:px,qqz}
    qqz{s>2770:qs,m<1801:hdj,R}
    gd{a>3333:R,R}
    hdj{m>838:A,pv}
    
    {x=787,m=2655,a=1222,s=2876}
    {x=1679,m=44,a=2067,s=496}
    {x=2036,m=264,a=79,s=2244}
    {x=2461,m=1339,a=466,s=291}
    {x=2127,m=1623,a=2188,s=1013}
    The workflows are listed first, followed by a blank line, then the ratings of
    the parts the Elves would like you to sort. All parts begin in the workflow
    named in. In this example, the five listed parts go through the following
    workflows:
    
    {x=787,m=2655,a=1222,s=2876}: in -> qqz -> qs -> lnx -> A
    {x=1679,m=44,a=2067,s=496}: in -> px -> rfg -> gd -> R
    {x=2036,m=264,a=79,s=2244}: in -> qqz -> hdj -> pv -> A
    {x=2461,m=1339,a=466,s=291}: in -> px -> qkq -> crn -> R
    {x=2127,m=1623,a=2188,s=1013}: in -> px -> rfg -> A
    Ultimately, three parts are accepted. Adding up the x, m, a, and s rating for
    each of the accepted parts gives 7540 for the part with x=787, 4623 for the
    part with x=2036, and 6951 for the part with x=2127. Adding all of the ratings
    for all of the accepted parts gives the sum total of 19114.
    
    Sort through all of the parts you've been given; what do you get if you add
    together all of the rating numbers for all of the parts that ultimately get
    accepted?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    /// 
    List<Part> parts = new();
    List<Rule> rules = new();
    Dictionary<string, List<Rule>> workflows = new();
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      parts.Clear();
      workflows.Clear();
      rules.Clear();

      //
      // Automatically imported Text !!
      //
      // This code is running twice:
      //
      // First (is a try run, no-one really cares if it works)
      //   with the content of the Test-Example-Input_2023_Day_19.txt already stored in "Lines"
      //
      // Second -> THE REAL TEST !! <-
      // with the content of the Input_2023_Day_19.txt already stored in "Lines"
      //
      foreach (var line in Lines)
      {
        if (line.IndexOf('{') == 0)
        {
          // This is a part line 
          var currentPart = new Part();
          var cleanLine = line.Replace("{", "").Replace("}", "");
          var chunks = cleanLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
          foreach (var chunk in chunks)
          {
            var splitted = chunk.Split('=', StringSplitOptions.RemoveEmptyEntries);
            if (splitted[0] == "x")
            {
              // x value
              currentPart.x = int.Parse(splitted[1]);
            }
            else if (splitted[0] == "m")
            {
              // m value
              currentPart.m = int.Parse(splitted[1]);
            }
            else if (splitted[0] == "a")
            {
              // a value
              currentPart.a = int.Parse(splitted[1]);
            }
            else if (splitted[0] == "s")
            {
              // s value
              currentPart.s = int.Parse(splitted[1]);
            }
            else
            {
              // Unknown attribute
              throw new Exception("Unknown attribute in part definition: " + splitted[0]);
            }
          }
          parts.Add(currentPart);
        }
        else
        {
          // This is a workflow line
          PopulateWorkflowDictionary(line);
        }
      }
      // Now process each part
      foreach (var part in parts)
      {
        var res = ProcessCurrentPart(part);
        if (res == "A")
        {
          //part accepted, add its attributes to total count
          totalCount += part.x;
          totalCount += part.m;
          totalCount += part.a;
          totalCount += part.s;
        }
      }

      result = totalCount.ToString();
      return result;
    }

    private string ProcessCurrentPart(Part currentPart)
    {
      //begin with the workflow named in
      string currentWorkflowName = "in";
      bool isProcessed = false;
      bool isAccepted = false;

      Debug.Write($"{currentPart}: ");
      Debug.Write($"-> {currentWorkflowName}");

      while (!isProcessed)
      {

        var currentWorkflowRules = workflows[currentWorkflowName];

        foreach (var rule in currentWorkflowRules)
        {
          if (ProcessCurrentRule(currentPart, rule))
          {
            string nextDestination = rule.destination;
            if (nextDestination == "A" || nextDestination == "R")
            {
              //part completely processed
              Debug.Write($"-> {nextDestination}");
              isProcessed = true;
              return nextDestination;
            }
            else
            {
              Debug.Write($"-> {nextDestination}");
              currentWorkflowName = nextDestination;

            }
            break;
          }
        }
      }
      return "";
    }

    private bool ProcessCurrentRule(Part part, Rule rule)
    {
      if (rule.attribute == "Last")
      {
        //last rule 
        return true;
      }
      else
      {
        var attributeVal = rule.attribute switch
        {
          "x" => part.x,
          "m" => part.m,
          "a" => part.a,
          "s" => part.s,
          _ => throw new Exception($"Invalid attibute: {rule.attribute}")
        };

        switch (rule.comparator)
        {
          case ">":
            if (attributeVal > rule.value)
              return true;
            break;
          case "<":
            if (attributeVal < rule.value)
              return true;
            break;
          default:
            throw new Exception($"Invalid Comparator found : {rule.comparator}");
        }
        return false;
      }
    }

    private void PopulateWorkflowDictionary(string line)
    {
      var workflowName = line.Substring(0, line.IndexOf('{'));
      var rulesPart = line.Substring(line.IndexOf('{') + 1, line.IndexOf('}') - line.IndexOf('{') - 1); //a<2006:qkq,m>2090:A,rfg
      var rulesChunks = rulesPart.Split(',', StringSplitOptions.RemoveEmptyEntries);
      foreach (var rulesChunk in rulesChunks)
      {
        if (rulesChunk.IndexOf(':') > 0)
        {
          // This is a rule with condition
          var conditionPart = rulesChunk.Substring(0, rulesChunk.IndexOf(':'));
          var destinationPart = rulesChunk.Substring(rulesChunk.IndexOf(':') + 1);
          string attribute = "";
          string comparator = "";
          int value = 0;
          // Parse condition
          if (conditionPart.IndexOf('>') > 0)
          {
            var condChunks = conditionPart.Split('>', StringSplitOptions.RemoveEmptyEntries);
            attribute = condChunks[0];
            comparator = ">";
            value = int.Parse(condChunks[1]);
          }
          else if (conditionPart.IndexOf('<') > 0)
          {
            var condChunks = conditionPart.Split('<', StringSplitOptions.RemoveEmptyEntries);
            attribute = condChunks[0];
            comparator = "<";
            value = int.Parse(condChunks[1]);
          }
          else
          {
            throw new Exception("Unknown comparator in rule condition: " + conditionPart);
          }
          rules.Add((attribute, comparator, value, destinationPart));
        }
        else
        {
          // This is a rule without condition (always applies)
          rules.Add(("Last", "", 0, rulesChunk));
        }
      }
      workflows[workflowName] = new List<Rule>(rules);
      rules.Clear();
    }
  }
}
