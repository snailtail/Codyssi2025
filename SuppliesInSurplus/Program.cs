const string fileName = "input.txt";
var boxLines = File.ReadAllLines(fileName);


part1();
part2();
part3();

void part3()
{
    List<StackOfBoxes> stacks = new();
    foreach (var boxLine in boxLines)
    {
        var stackDefinitions = boxLine.Split(" ");
        stacks.Add(new(stackDefinitions[0],stackDefinitions[1]));
    }

    int maxAdjacentCount = 0;
    //Loop through pairs and check for combined ranges.
    for (int i = 0; i < stacks.Count - 1; i++)
    {
        var combined = stacks[i].Range.Union(stacks[i + 1].Range);
        if (maxAdjacentCount < combined.Count())
        {
            maxAdjacentCount = combined.Count();
        }
    }
    Console.WriteLine($"Part 3\nMax adjacent count: {maxAdjacentCount}");
}
    
void part2()
{
    List<StackOfBoxes> stacks = new();

    foreach (var boxLine in boxLines)
    {
        var stackDefinitions = boxLine.Split(" ");
        stacks.Add(new(stackDefinitions[0],stackDefinitions[1]));
    }
    int sum = stacks.Sum(stack=> stack.BoxCount);
    Console.WriteLine($"Part 2\nSum: {sum}");
}

void part1()
{
    List<StackOfBoxes> stacks = new();

    foreach (var boxLine in boxLines)
    {
        var stackDefinitions = boxLine.Split(" ");
        foreach (var stackDefinition in stackDefinitions)
        {
            stacks.Add(new(stackDefinition));
        }
    }
    int sum = stacks.Sum(stack=> stack.BoxCount);
    Console.WriteLine($"Part 1\nSum: {sum}");
}



class StackOfBoxes
{
    private string _definition;
    private int _lowestNumber;
    private int _highestNumber;
    private IEnumerable<int> _range;
    public int BoxCount => _range.Count();
    string Definition
    {
        get => _definition;
        set => _definition = value;
    }
    
    public IEnumerable<int> Range => _range;

    public StackOfBoxes(string definition)
    {
        _definition = definition;
        processSingleDefinition();
    }

    public StackOfBoxes(string definition1, string definition2)
    {
        var range1Def = definition1.Split("-").Select(int.Parse);
        var range2Def = definition2.Split("-").Select(int.Parse);
        var range1 = Enumerable.Range(range1Def.Min(), range1Def.Max() - range1Def.Min() + 1);
        var range2 = Enumerable.Range(range2Def.Min(), range2Def.Max() - range2Def.Min() + 1);
        //Concatenate the two ranges
        _range = range1.Union(range2);

    }

    private void processSingleDefinition()
    {
        var numbers = _definition.Split("-").Select(int.Parse);
        _lowestNumber = numbers.Min();
        _highestNumber = numbers.Max();
        _range = Enumerable.Range(_lowestNumber, _highestNumber - _lowestNumber + 1);
    }
}