using System.Drawing;
using System.Text.RegularExpressions;

const string fileName = "input.txt";
var allLines = File.ReadAllLines(fileName);
Point currentPoint = new Point(){X=0,Y=0};
List<Island> islands = new List<Island>();
foreach (var line in allLines)
{
    string pattern = @"\((.*),\s(.*)\)";
    var match = Regex.Match(line, pattern);
    Point point = new Point(){X=int.Parse(match.Groups[1].Value),Y=int.Parse(match.Groups[2].Value)};
    islands.Add(new Island(point));
}

//part1();
//part2();
part3();

void part3()
{
    List<Island> islandsNotVisited = islands.Where(x => x.Visited == false).ToList();
    int part3Sum = 0;
    
    while (islandsNotVisited.Count > 0)
    {
        int minDistance = islandsNotVisited.Min(x => ManhattanDistance(currentPoint, x.Coordinates));
        Island closestIsland = islandsNotVisited.Where(islandsNotVisited => ManhattanDistance(currentPoint, islandsNotVisited.Coordinates) == minDistance).OrderBy(i => i.Coordinates.X).OrderBy(i => i.Coordinates.Y).ToArray().First();
        int thisDistance = ManhattanDistance(currentPoint, closestIsland.Coordinates);
        part3Sum += thisDistance;
        currentPoint = closestIsland.Coordinates;
        closestIsland.Visited = true;
        islandsNotVisited = islands.Where(x => x.Visited == false).ToList();
    }
    Console.WriteLine($"Part 3, total distance: {part3Sum}");
}

void part2()
{
    int minValue = int.MaxValue;
    Point closestPoint = new Point();
    foreach (var island in islands)
    {
        int manhattanDistance = ManhattanDistance(currentPoint, island.Coordinates);
        if (manhattanDistance < minValue)
        {
            minValue = manhattanDistance;
            closestPoint = island.Coordinates;
        }
    }

    var otherIslands = islands.Where(x => x.Coordinates.X != closestPoint.X && x.Coordinates.Y != closestPoint.Y).ToList();
    var sortedOtherIslands = otherIslands.OrderBy(other => ManhattanDistance(closestPoint, other.Coordinates));
    var closestOtherIsland = sortedOtherIslands.First(); 
    int distance = ManhattanDistance(closestPoint,closestOtherIsland.Coordinates);
    Console.WriteLine($"Part 2: Distance is {distance}");
    

}
void part1()
{
    int minValue = int.MaxValue;
    int maxValue = int.MinValue;
    foreach (var island in islands)
    {
        int manhattanDistance = ManhattanDistance(currentPoint, island.Coordinates);
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

class Island
{
    public Point Coordinates { get; set; }

    public Island(Point coordinates)
    {
        Coordinates = coordinates;
        Visited = false;
    }

    public bool Visited { get; set; }
    
}

class DistanceEntry
{
    public Point Coordinates { get; set; }
    public int Distance { get; set; }
    public DistanceEntry(Point coordinates, int distance)
    {
        Coordinates = coordinates;
        Distance = distance;
    }
}