const string fileName ="input.txt";
List<int[]> rows = File.ReadAllLines(fileName).Select(line => line.Split(' ').Select(int.Parse).ToArray()).ToList();
Part1();
void Part1()
{
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
