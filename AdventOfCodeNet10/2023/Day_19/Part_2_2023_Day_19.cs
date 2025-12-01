using System.Data;
using System.Diagnostics;
using Part = (int x, int m, int a, int s);
using Rule = (string attribute, string comparator, int value, string destination);
using Range = (int min, int max);


namespace AdventOfCodeNet10._2023.Day_19
{
  using RangeSet = (Range x, Range m, Range a, Range s);
  using Workflow = (string name, List<Rule> rules);

  internal static class MyExtensions
  {
    extension(Workflow workflow)
    {
      internal bool IsLeadingToAcceptance => workflow.rules.Any(rule => rule.destination == "A");

      //internal List<Rule> GetAllRulesForWorkflowName()
      //{
      //  return workflow.rules;
      //}

    }
  }

  internal class Part_2_2023_Day_19 : Days
  {

    /// <summary>
    /*
    https://adventofcode.com/2023/day/19
    Even with your help, the sorting process still isn't fast enough.
    
    One of the Elves comes up with a new plan: rather than sort parts individually
    through all of these workflows, maybe you can figure out in advance which
    combinations of ratings will be accepted or rejected.
    
    Each of the four ratings (x, m, a, s) can have an integer value ranging from a
    minimum of 1 to a maximum of 4000. Of all possible distinct combinations of
    ratings, your job is to figure out which ones will be accepted.
    
    In the above example, there are 167409079868000 distinct combinations of
    ratings that will be accepted.
    
    Consider only your list of workflows; the list of part ratings that the Elves
    wanted you to sort is no longer relevant. How many distinct combinations of
    ratings will be accepted by the Elves' workflows?
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    List<Rule> rules = new();
    List<Workflow> WorkflowList = new();

    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      WorkflowList.Clear();
      rules.Clear();
      RangeSet validRanges = ((1, 4000), (1, 4000), (1, 4000), (1, 4000));

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
        }
        else
        {
          // This is a workflow line
          PopulateWorkflowDictionary(line);
        }
      }
      ProcessWorkflow("in", new RangeSet(
        (1, 4000),
        (1, 4000),
        (1, 4000),
        (1, 4000)
      ));

      result = totalCount.ToString();
      return result;
    }

    private long ProcessWorkflow(string workflowName, RangeSet ranges)
    {
      // Terminal states
      if (workflowName == "A")
      {
        // Accepted! Count all combinations in this range
        return CalculateCombinations(ranges);
      }

      if (workflowName == "R")
      {
        // Rejected! No combinations count
        return 0;
      }

      // Find the workflow
      var workflow = WorkflowList.FirstOrDefault(w => w.name == workflowName);
      if (workflow.rules == null || workflow.rules.Count == 0)
      {
        throw new Exception($"Workflow '{workflowName}' not found!");
      }

      long totalCombinations = 0;
      var currentRanges = ranges;

      // Process each rule in the workflow
      foreach (var rule in workflow.rules)
      {
        if (rule.attribute == "Last")
        {
          // Unconditional rule - all remaining ranges go to destination
          totalCombinations += ProcessWorkflow(rule.destination, currentRanges);
          break; // No more rules to process
        }
        else
        {
          // Conditional rule - split ranges
          var (matchingRanges, nonMatchingRanges) = SplitRanges(currentRanges, rule);

          // Ranges that MATCH the condition go to the rule's destination
          if (matchingRanges.HasValue && IsValidRangeSet(matchingRanges.Value))
          {
            totalCombinations += ProcessWorkflow(rule.destination, matchingRanges.Value);
          }

          // Ranges that DON'T MATCH continue to the next rule
          if (nonMatchingRanges.HasValue && IsValidRangeSet(nonMatchingRanges.Value))
          {
            currentRanges = nonMatchingRanges.Value;
          }
          else
          {
            // No ranges left to process
            break;
          }
        }
      }

      return totalCombinations;
    }

    private (RangeSet? matching, RangeSet? nonMatching) SplitRanges(RangeSet ranges, Rule rule)
    {
      var currentRange = rule.attribute switch
      {
        "x" => ranges.x,
        "m" => ranges.m,
        "a" => ranges.a,
        "s" => ranges.s,
        _ => throw new Exception($"Invalid attribute: {rule.attribute}")
      };

      Range? matchRange = null;
      Range? nonMatchRange = null;

      if (rule.comparator == ">")
      {
        // Match: values > rule.value, so (rule.value + 1, max)
        if (currentRange.max > rule.value)
        {
          matchRange = (Math.Max(currentRange.min, rule.value + 1), currentRange.max);
        }

        // Non-match: values <= rule.value, so (min, rule.value)
        if (currentRange.min <= rule.value)
        {
          nonMatchRange = (currentRange.min, Math.Min(currentRange.max, rule.value));
        }
      }
      else if (rule.comparator == "<")
      {
        // Match: values < rule.value, so (min, rule.value - 1)
        if (currentRange.min < rule.value)
        {
          matchRange = (currentRange.min, Math.Min(currentRange.max, rule.value - 1));
        }

        // Non-match: values >= rule.value, so (rule.value, max)
        if (currentRange.max >= rule.value)
        {
          nonMatchRange = (Math.Max(currentRange.min, rule.value), currentRange.max);
        }
      }

      RangeSet? matchingSet = matchRange.HasValue ? UpdateRange(ranges, rule.attribute, matchRange.Value) : null;
      RangeSet? nonMatchingSet = nonMatchRange.HasValue ? UpdateRange(ranges, rule.attribute, nonMatchRange.Value) : null;

      return (matchingSet, nonMatchingSet);
    }

    private RangeSet UpdateRange(RangeSet ranges, string attribute, Range newRange)
    {
      return attribute switch
      {
        "x" => (x: newRange, ranges.m, ranges.a, ranges.s),
        "m" => (ranges.x, m: newRange, ranges.a, ranges.s),
        "a" => (ranges.x, ranges.m, a: newRange, ranges.s),
        "s" => (ranges.x, ranges.m, ranges.a, s: newRange),
        _ => ranges
      };
    }
    private bool IsValidRangeSet(RangeSet rangeSet)
    {
      return rangeSet.x.min <= rangeSet.x.max &&
             rangeSet.m.min <= rangeSet.m.max &&
             rangeSet.a.min <= rangeSet.a.max &&
             rangeSet.s.min <= rangeSet.s.max;
    }

    private long CalculateCombinations(RangeSet rangeSet)
    {
      if(!IsValidRangeSet(rangeSet))
      {
        return 0;
      }

      return (long)(rangeSet.x.max - rangeSet.x.min + 1) *
             (long)(rangeSet.m.max - rangeSet.m.min + 1) *
             (long)(rangeSet.a.max - rangeSet.a.min + 1) *
             (long)(rangeSet.s.max - rangeSet.s.min + 1);
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
      WorkflowList.Add((workflowName, new List<Rule>(rules)));
      rules.Clear();
    }
  }
}
