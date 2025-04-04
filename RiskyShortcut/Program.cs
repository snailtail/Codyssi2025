using System.Text;
using System.Text.RegularExpressions;

const string fileName = "input.txt";
var rawInput = File.ReadAllLines(fileName);


part1(rawInput);
part2(rawInput);
part3(rawInput);

void part3(string[] input)
{
    StringBuilder sb = new();
    const string pattern = @"[a-zA-Z]\d|\d[a-zA-Z]";
    foreach (string line in input)
    {
        string thisLine = new string(line);
        bool hasmatches = Regex.Matches(thisLine, pattern).Count > 0;
        while (hasmatches)
        {
            thisLine = Regex.Replace(thisLine, pattern, "");
            hasmatches = Regex.Matches(thisLine, pattern).Count > 0;
        }
        sb.Append(thisLine);
    }
    var part3Result = sb.ToString().Length;
    Console.WriteLine($"Part 3: {part3Result}");
}

void part2(string[] input)
{
    StringBuilder sb = new();
    const string pattern = @"-\d|\d-|[a-zA-Z]\d|\d[a-zA-Z]";
    foreach (string line in input)
    {
        string thisLine = new string(line);
        bool hasmatches = Regex.Matches(thisLine, pattern).Count > 0;
        while (hasmatches)
        { 
            thisLine= Regex.Replace(thisLine, pattern, "");
            hasmatches = Regex.Matches(thisLine, pattern).Count > 0;
        }
        sb.Append(thisLine);
    }
    var part2Result = sb.ToString().Length;
    Console.WriteLine($"Part 2: {part2Result}");
}

void part1(string[] input)
{
    string part1input = string.Join("", input);
    var alphabetical = Regex.Replace(part1input, @"[^a-zA-Z]", "");
    Console.WriteLine($"Part 1: {alphabetical.Length}");
}