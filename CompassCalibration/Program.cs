const string fileName = "input.txt";
var allLines = File.ReadAllLines(fileName);
//Part 1 and 2
//var numbers = allLines.Where(l=>l.StartsWith('-')==false && l.StartsWith('+')==false).Select(int.Parse).ToList();
//Part 3
var chars = allLines.Where(l => l.StartsWith('-') == false && l.StartsWith('+') == false);
bool second = false;
List<int> numbers = new List<int>();
string thisNumber = string.Empty;
foreach (var line in chars)
{
    thisNumber = $"{thisNumber}{line}";
    if(second==true)
    {
        numbers.Add(int.Parse(thisNumber));
        thisNumber = string.Empty;
    }
    second = !second;
}
var initialNumber = numbers.First();
//Part 3
numbers.RemoveAt(0);

//Part 1
//var commands = allLines.Where(l => l.StartsWith('-') || l.StartsWith('+')).ToList().First();
//Part 2 and 3
var commands = allLines.Where(l => l.StartsWith('-') || l.StartsWith('+')).ToList().First().Reverse().ToArray();
var sum=initialNumber;

// Part 1 and 2
/*
var index =1;
 foreach(char command in commands)
{
    if (command == '+')
    {
        sum += numbers[index];
    }
    else if (command == '-')
    {
        sum -= numbers[index];
    }

    index++;
}*/

var index = 0;
foreach (var number in numbers)
{
    if (commands[index] == '+')
    {
        sum += number;
    }
    else
    {
        sum -= number;
    }

    index++;
}

Console.WriteLine(sum);