using System.Text;

const string fileName = "input.txt";
var allLines = File.ReadAllLines(fileName);
part1();
part2();
part3();


void part3()
{
    int sum = 0;
    foreach (string line in allLines)
    {
        sum += line.LossLessCompress().GetMemoryUnits();
    }
    Console.WriteLine($"Part 3: {sum}");
}


void part2()
{
    int sum = 0;
    foreach (string line in allLines)
    {
        sum += line.LossyCompress().GetMemoryUnits();
    }
    Console.WriteLine($"Part 2: {sum}");
}


void part1()
{
    int sum = 0;
    foreach (string line in allLines)
    {
        sum += line.GetMemoryUnits();
    }
    Console.WriteLine($"Part 1: {sum}");
}


static class extensions
{
    public static int GetMemoryUnits(this string input)
    {
        int sum = 0;
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                sum += int.Parse(c.ToString());
            }
            else
            {
                sum += (int)c - 64;
            }
        }

        return sum;
    }

    public static string LossyCompress(this string input)
    {
        int length = input.Length;
        int keep = length / 10;
        int numLossy = length - (2 * keep);
        string beginning = input.Substring(0, keep);
        string ending = input.Substring(input.Length - keep);
        return $"{beginning}{numLossy}{ending}";
    }

    public static string LossLessCompress(this string input)
    {
        List<CompressionPair> pairs = new List<CompressionPair>();
        CompressionPair activePair = new(input[0], 1);

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == activePair.Key)
            {
                activePair.Value++;
            }
            else
            {
                pairs.Add(activePair);
                activePair = new(input[i], 1);
            }
        }
        // Add the last pair
        pairs.Add(activePair);
        StringBuilder sb = new();
        foreach (CompressionPair cp in pairs)
        {
            sb.Append(cp.Value);
            sb.Append(cp.Key);
        }
        return sb.ToString();
    }

    private class CompressionPair
        {
            public CompressionPair(char key, int value)
            {
                Key = key;
                Value = value;
            }

            public char Key { get; set; }
            public int Value { get; set; }
            
        }
    }