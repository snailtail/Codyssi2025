
const string fileName ="input.txt";
var input = File.ReadAllLines(fileName);
Part1(input);
Part2(input);
Part3(input);


void Part3(string[] input)
{
    int[,] grid = GetIntGrid(input);
    // get square root of the grid.Length
    int gridSize = (int)Math.Sqrt(grid.Length) - 1;

    Coordinate currentCoordinate = new(0, 0);
    Coordinate targetCoordinate = new(gridSize, gridSize); // 

    int result = GetSafestPath(grid, currentCoordinate, targetCoordinate);

    Console.WriteLine($"Part 3: {result}");

}
void Part2(string[] input)
{
    int[,] grid = GetIntGrid(input);
    Coordinate currentCoordinate = new(0, 0); // Start is 1,1 in puzzle - this means 0,0 in our grid
    Coordinate targetCoordinate = new(14, 14); // Target is 15,15 in puzzle - this means 14,14 in our grid

    int result = GetSafestPath(grid, currentCoordinate, targetCoordinate);

    Console.WriteLine($"Part 2: {result}");
    
}

int GetSafestPath(int[,] grid, Coordinate currentCoordinate, Coordinate targetCoordinate)
{
    int gridSize = (int)Math.Sqrt(grid.Length);
    int[,] dp = new int[grid.Length, grid.Length];
    dp[0, 0] = grid[0, 0];

    // Fyll första raden
    for (int x = 1; x < gridSize; x++)
    {
        dp[0, x] = dp[0, x - 1] + grid[0, x];
    }

    // Fyll första kolumnen
    for (int y = 1; y < gridSize; y++)
    {
        dp[y, 0] = dp[y - 1, 0] + grid[y, 0];
    }

    // Fyll resten av dp-tabellen
    for (int y = 1; y < gridSize; y++)
    {
        for (int x = 1; x < gridSize; x++)
        {
            dp[y, x] = Math.Min(dp[y - 1, x], dp[y, x - 1]) + grid[y, x];
        }
    }
    return dp[targetCoordinate.Y, targetCoordinate.X];
}

IEnumerable<Coordinate> getNeighbors(Coordinate currentCoordinate, int[][] grid)
{
    if(currentCoordinate.X < grid[0].Length - 1)
    {
        yield return new Coordinate(currentCoordinate.X + 1, currentCoordinate.Y);
    }
    if(currentCoordinate.Y < grid.Length - 1)
    {
        yield return new Coordinate(currentCoordinate.X, currentCoordinate.Y + 1);
    }
}

int[,] GetIntGrid(string[] input)
{

    int rows = input.Length;
    int cols = input[0].Split(' ').Length;
    int[,] grid = new int[rows, cols];

    for (int y = 0; y < rows; y++)
    {
        string[] values = input[y].Split(' ');
        for (int x = 0; x < cols; x++)
        {
            grid[y, x] = int.Parse(values[x]);
        }
    }

    return grid;

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

/// <summary>
/// Coordinate class to hold the X and Y coordinates. X is the horizontal coordinate (columns +/- or left/right) and Y is the vertical coordinate (rows +/- or up/down).
/// </summary>
class Coordinate
{
    public int X { get; set; } // Horizontal ((columns +/-)
    public int Y { get; set; } // Vertical (rows +/-)
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}