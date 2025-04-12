using System.Text;

const string fileName = "input.txt";
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
Console.WriteLine($"Part 2: {Part2()}");
Console.WriteLine($"Part 3: {Part3()}");



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

string Part2()
{
    long sum = 0;
    foreach(var number in numbers)
    {
        long thisvalue = number.GetLongValue();
        sum += thisvalue;
    }
    return NumberEntry.GetBaseValueString(sum,68);
}

int Part3()
{
    long sum = 0;
    foreach(var number in numbers)
    {
        long thisvalue = number.GetLongValue();
        sum += thisvalue;
    }
    double root = Math.Pow(sum, 1.0 / 4);
    int result = (int)Math.Ceiling(root);
    return result;
}

int slask = 1;

class NumberEntry
{
    
    private const string Numbers = "0123456789";
    private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Lower = "abcdefghijklmnopqrstuvwxyz";
    private const string Specials = "!@#$%^";
    public string NumberAsString { get; set; }
    public int BaseValue { get; set; }

    public NumberEntry(string numberAsString, int baseValue)
    {
        NumberAsString = numberAsString;
        BaseValue = baseValue;
    }


    public static string GetBaseValueString(long number, int baseValue)
    {
        const string Numbers = "0123456789";
        const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string Lower = "abcdefghijklmnopqrstuvwxyz";
        const string Specials = "!@#$%^";
        string charset = (Numbers + Upper + Lower + Specials).Substring(0,baseValue);
        StringBuilder sb = new();
        while (number > 0)
        {
            long index = number % baseValue;
            sb.Insert(0, charset[(int)index]);
            number = (number - index) / baseValue;
        }
        return sb.ToString();
    }
    
    public long GetLongValue(bool part2=false)
    {
        const string Numbers = "0123456789";
        const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string Lower = "abcdefghijklmnopqrstuvwxyz";
        const string Specials = "!@#$%^";
        string charset;
        if(!part2)
        {
            charset = (Numbers + Upper + Lower).Substring(0,this.BaseValue);
        }
        else
        {
            charset = (Numbers + Upper + Lower + Specials).Substring(0,this.BaseValue);
        }
        
        long number = 0;
        foreach (var t in this.NumberAsString)
        {
            int value = charset.IndexOf(t);
            number = number * this.BaseValue + value;
        }
        return number;
    }

}