using System.Text.RegularExpressions;

const string fileName = "input.txt";
var input = File.ReadAllText(fileName);
string cleanInput = Regex.Replace(input, @"\W", "");
string lowerCaseCleanInput = Regex.Replace(cleanInput, @"[A-Z]", "");
string upperCaseCleanInput = Regex.Replace(cleanInput, @"[a-z]", "");




part1();
part2();
part3();

void part3()
{
    List<int>Sums = new List<int>();
    int lastValue = int.MinValue;
    foreach(char c in input)
    {
        int asciiValue = (int)c;
        if(asciiValue < 123 && asciiValue > 96) // lowercase letters a-z
        {
            lastValue = asciiValue - 96;
        }
        else if (asciiValue < 91 && asciiValue > 64) // uppercase letters A-Z
        {
            lastValue = asciiValue - 38;
        }
        else // corrupted char
        {
            int generatedValue = lastValue * 2 - 5;
            while(generatedValue < 1)
            {
                generatedValue += 52;
            }
            while(generatedValue > 52)
            {
                generatedValue -= 52;
            }
            lastValue = generatedValue;
        }
        Sums.Add(lastValue);
    }
    int part3Sum = Sums.Sum();
    Console.WriteLine($"Part 3: {part3Sum}");
}

void part2()
{
    int part2Sum = 0;
    // lowercase chars - subtract 96 from the char's int value to get 1 for a, 2 for b etc.
    int subtract = 96;
    foreach(char c in lowerCaseCleanInput)
    {
        part2Sum += (int)c - subtract;
    }

    // uppercase chars - subtract 38 from the char's int value to get 27 for A, 28 for B etc.
    subtract = 38;
    foreach (char c in upperCaseCleanInput)
    {
        part2Sum += (int)c - subtract;
    }

    Console.WriteLine($"Part 2: {part2Sum}");
}
void part1()
{
    Console.WriteLine($"Part 1: {cleanInput.Length}");
}