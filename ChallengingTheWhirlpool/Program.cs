using System.Text;

const string fileName = "input.txt";
var parts = File.ReadAllText(fileName).Split($"{Environment.NewLine}{Environment.NewLine}");
string[] gridData = parts[0].Split(Environment.NewLine);
List<Instruction> instructions = parts[1].Split(Environment.NewLine).Select(ParseInstruction).ToList();



var pool = new Whirlpool(gridData);
pool.PrintGrid();

Instruction ParseInstruction(string instruction)
{
    var instructionParts = instruction.Split(" ");
    
    InstructionType instructionType = (InstructionType)Enum.Parse(typeof(InstructionType), instructionParts[0]);
    TargetType targetType = instructionType == InstructionType.SHIFT
        ? (TargetType)Enum.Parse(typeof(TargetType), instructionParts[1])
        : (TargetType)Enum.Parse(typeof(TargetType), instructionParts[2]);

    int amount = instructionType == InstructionType.SHIFT ? int.Parse(instructionParts[4]) : int.Parse(instructionParts[1]);

    int? targetIndex;
    if (instructionType == InstructionType.SHIFT)
    {
        targetIndex = int.Parse(instructionParts[2]);
    }
    else if (targetType == TargetType.ALL)
    {
        targetIndex = null;
    }
    else
    {
        targetIndex = int.Parse(instructionParts[3]);
    }
    
    var parsedInstruction = new Instruction(instructionType, targetType,amount,targetIndex);
    return parsedInstruction;
}
enum InstructionType
{
    SHIFT,
    ADD,
    SUB,
    MULTIPLY
}

enum TargetType
{
    ROW,
    COL,
    ALL
}

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

record Instruction(InstructionType Type, TargetType Target, int Amount, int? TargetIndex = null);