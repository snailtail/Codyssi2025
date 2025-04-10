const string fileName ="example.txt";
var input = File.ReadAllLines(fileName);
Part1(input);
Part2(input);
void Part2(string[] input)
{
    int[][] grid = input.Select(line => line.Split(' ').Select(int.Parse).ToArray()).ToArray();
    
}




void Part1(string[] input)
{
    List<int[]> rows = input.Select(line => line.Split(' ').Select(int.Parse).ToArray()).ToList();
    int lowestRowSum = int.MaxValue;
    int lowestColSum = int.MaxValue;
    foreach (var row in rows)
    {
        var sum = row.Sum();
        lowestRowSum = Math.Min(lowestRowSum, sum);
    }

    for (int i = 0; i < rows[0].Length; i++)
    {
        int thisColSum = 0;
        foreach (var row in rows)
        {
            thisColSum += row[i];
        }
        lowestColSum = Math.Min(lowestColSum, thisColSum);
    }
    int lowestSum = Math.Min(lowestRowSum, lowestColSum);
    Console.WriteLine($"Part 1: {lowestSum}");
}
