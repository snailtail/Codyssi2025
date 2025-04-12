using System.Text;

const string fileName = "example.txt";
var parts = File.ReadAllText(fileName).Split($"{Environment.NewLine}{Environment.NewLine}");
string[] gridData = parts[0].Split(Environment.NewLine);

var pool = new Whirlpool(gridData);
pool.PrintGrid();

Console.ReadLine();

class Whirlpool
{
    private int[,] _grid;
    public void PrintGrid()
    {
        StringBuilder sb = new();
        int width = _grid.GetLength(0);
        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < width; col++)
            {
                sb.Append(_grid[row, col]);
                sb.Append(" ");
            }
            sb.AppendLine();
        }
        Console.WriteLine(sb.ToString());
    }

    public Whirlpool(string[] gridData)
    {
        _grid = InitializeGrid(gridData);
    }

    private int[,] InitializeGrid(string[] gridData)
    {
        int[,] grid;
        int size = gridData.Length;
        grid = new int[size, size];
        for (int row = 0; row < size; row++)
        {
            string[] rowData = gridData[row].Split(" ");
            for (int col = 0; col < size; col++)
            {
                grid[row,col]=int.Parse(rowData[col]);
            }
        }
        return grid;
    }
}