using System.Drawing;
using System.Text.RegularExpressions;

const string fileName = "input.txt";
var allLines = File.ReadAllLines(fileName);
Point currentPoint = new Point(){X=0,Y=0};
List<Point> islands = new List<Point>();
foreach (var line in allLines)
{
    string pattern = @"\((.*),\s(.*)\)";
    var match = Regex.Match(line, pattern);
    Point point = new Point(){X=int.Parse(match.Groups[1].Value),Y=int.Parse(match.Groups[2].Value)};
    islands.Add(point);
}

//part1();
part2();

void part2()
{
    int minValue = int.MaxValue;
    Point closestPoint = new Point();
    foreach (var point in islands)
    {
        int manhattanDistance = ManhattanDistance(currentPoint, point);
        if (manhattanDistance < minValue)
        {
            minValue = manhattanDistance;
            closestPoint = point;
        }
    }

    var otherIslands = islands.Where(x => x.X != closestPoint.X && x.Y != closestPoint.Y).ToList();
    var sortedOtherIslands = otherIslands.OrderBy(other => ManhattanDistance(closestPoint, other));
    var closestOtherIsland = sortedOtherIslands.First(); 
    int distance = ManhattanDistance(closestPoint,closestOtherIsland);
    Console.WriteLine($"Part 2: Distance is {distance}");
    

}
void part1()
{
    int minValue = int.MaxValue;
    int maxValue = int.MinValue;
    foreach (var point in islands)
    {
        int manhattanDistance = ManhattanDistance(currentPoint, point);
        if (manhattanDistance < minValue)
        {
            minValue = manhattanDistance;
        }

        if (manhattanDistance > maxValue)
        {
            maxValue = manhattanDistance;
        }
    }
    Console.WriteLine($"Part 1: Difference is: {maxValue - minValue}");
}
int ManhattanDistance(Point p1, Point p2)
{
    return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
}