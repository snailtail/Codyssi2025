using System.Text;

const string fileName = "example.txt";
var input = File.ReadAllLines(fileName);


List<NumberEntry> numbers = new List<NumberEntry>();
foreach(var line in input)
{
    var parts = line.Split(' ');
    string number = parts[0];
    int baseValue = int.Parse(parts[1]);
    numbers.Add(new NumberEntry(number, baseValue));
}



Console.WriteLine($"Part 1: {Part1()}");



long Part1()
{
    long largestNumber = long.MinValue;
    foreach(var number in numbers)
    {
        long thisvalue = number.GetLongValue();
        largestNumber = Math.Max(thisvalue, largestNumber);
    }
    return largestNumber;
}


int slask = 1;

class NumberEntry
{
    public string NumberAsString { get; set; }
    public int BaseValue { get; set; }

    public NumberEntry(string numberAsString, int baseValue)
    {
        NumberAsString = numberAsString;
        BaseValue = baseValue;
    }

    public long GetLongValue()
    {
        long result = 0;
        int multiplier = 1;

        // Process the string from right to left
        for (int i = this.NumberAsString.Length - 1; i >= 0; i--)
        {
            char c = this.NumberAsString[i];
            long value = c switch
            {
                >= '0' and <= '9' => c - '0',
                >= 'A' and <= 'Z' => c - 'A' + 10,
                >= 'a' and <= 'z' => c - 'a' + 36,
                _ => throw new ArgumentException($"Invalid character {c} in number {this.NumberAsString}")
            };

            if (value >= this.BaseValue)
            {
                throw new ArgumentException($"Invalid character {c} for base {this.BaseValue}");
            }

            result += value * multiplier;
            multiplier *= this.BaseValue;
        }

        return result;
    }

}