const string fileName = "input.txt";
var allLines = File.ReadAllText(fileName);
var split = allLines.Split($"{Environment.NewLine}{Environment.NewLine}");
//var frequencies = split[0].Split(Environment.NewLine);
var swaps = split[1].Split(Environment.NewLine);
var testFrequency = int.Parse(split[2]);

part1(split[0].Split(Environment.NewLine));
part2(split[0].Split(Environment.NewLine));
part3(split[0].Split(Environment.NewLine));

void part3(string[] frequencies)
{
    // For each swapping instruction (X-Y), consider pairs of same-length blocks starting at tracks X and Y, respectively.
    // Then, choose the pair of blocks with the maximum length that don’t overlap and don’t extend beyond the final track.
    Queue<string> tasks = new Queue<string>();
    foreach (var swap in swaps)
    {
        (int x, int y) = ParseSwap(swap);

        int maxlength = Math.Abs(y - x);

        int min = Math.Min(x, y);
        int max = Math.Max(x, y);

        while (max + maxlength >= frequencies.Length)
        {
            maxlength--;
        }
        

        for (int i = 1; i <= maxlength;i++)
        {
            string task = $"{min + i}-{max + i}";
            tasks.Enqueue(task);
        }

        while(tasks.Count > 0)
        {
            var task = tasks.Dequeue();
            var (from, to) = ParseSwap(task);
            var temp = frequencies[from];
            frequencies[from] = frequencies[to];
            frequencies[to] = temp;
        }

    }
    var part3Result = frequencies[testFrequency - 1];

    Console.WriteLine($"Part 3: {part3Result}");

}

void part2(string[] frequencies)
{
    for(int i = 0; i < swaps.Length; i++)
    {
        ThreePartSwap swap;
        if (i == swaps.Length - 1)
        {
            swap = ParseSwapForStandard2(swaps[i], swaps[0]);
        }
        else
        {
            swap = ParseSwapForStandard2(swaps[i], swaps[i + 1]);
        }

        string prevx, prevy, prevz;
     
        prevx = frequencies[swap.x-1];
        prevy = frequencies[swap.y-1];
        prevz = frequencies[swap.z-1];

        frequencies[swap.y-1] = prevx;
        frequencies[swap.z-1] = prevy;
        frequencies[swap.x-1] = prevz;
    }
    var part2Result = frequencies[testFrequency - 1];

    Console.WriteLine($"Part 2: {part2Result}");
}


void part1(string[] frequencies)
{
    foreach (var swap in swaps)
    {
        var (from, to) = ParseSwap(swap);
        var temp = frequencies[from];
        frequencies[from] = frequencies[to];
        frequencies[to] = temp;
    }
    var part1Result = frequencies[testFrequency - 1];
    Console.WriteLine($"Part 1: {part1Result}");
}


(int from, int to) ParseSwap(string swap)
{
    var parts = swap.Split("-");
    return (int.Parse(parts[0])-1, int.Parse(parts[1])-1);
}

ThreePartSwap ParseSwapForStandard2(string swap1, string swap2)
{
    var parts1 = swap1.Split("-");
    var parts2 = swap2.Split("-");
    return new ThreePartSwap(int.Parse(parts1[0]), int.Parse(parts1[1]), int.Parse(parts2[0]));
}


record ThreePartSwap(int x, int y, int z);
