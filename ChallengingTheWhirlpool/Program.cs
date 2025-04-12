using System.Text;

const string fileName = "input.txt";
var parts = File.ReadAllText(fileName).Split($"{Environment.NewLine}{Environment.NewLine}");
string[] gridData = parts[0].Split(Environment.NewLine);
List<Instruction> instructions = parts[1].Split(Environment.NewLine).Select(ParseInstruction).ToList();
List<string> actions = parts[2].Split(Environment.NewLine).ToList();


// for part 2 we will use a queue
Queue<Instruction> instructionQueue = new Queue<Instruction>();
foreach (var instruction in instructions)
{
    instructionQueue.Enqueue(instruction);
}

part1();
part2();
part3();

void part3()
{
    var pool = new Whirlpool(gridData);
    Instruction activeInstruction = null;
    int loopCount = 1;
    
    while (instructions.Count > 0)
    {
        foreach (var action in actions)
        {
            if (activeInstruction == null && instructions.Count == 0)
            {
                break;
            }
            
            if (action == "TAKE")
            {
                activeInstruction = instructions[0];
                instructions.RemoveAt(0);
            }

            if (action == "CYCLE")
            {
                if (activeInstruction != null)
                {
                    instructions.Add(activeInstruction!);
                }
                else
                {
                    throw new ArgumentException("CYCLE action tried to cycle NULL instruction");
                }
            }

            if (action == "ACT")
            {
                pool.PerformInstruction(activeInstruction!);
                activeInstruction = null;
            }
        
        }
    }
    
    Console.WriteLine($"Part 3: {pool.LargestRowOrColumnSum}");
}

void part2()
{
    var pool = new Whirlpool(gridData);
    Instruction activeInstruction = null; 
    foreach (var action in actions)
    {
        if (action == "TAKE")
        {
            activeInstruction = instructionQueue.Dequeue();
        }

        if (action == "CYCLE")
        {
            if (activeInstruction != null)
            {
                instructionQueue.Enqueue(activeInstruction!);
            }
            else
            {
                throw new ArgumentException("CYCLE action tried to cycle NULL instruction");
            }
        }

        if (action == "ACT")
        {
            pool.PerformInstruction(activeInstruction);
            activeInstruction = null;
        }
        
    }
    
    Console.WriteLine($"Part 2: {pool.LargestRowOrColumnSum}");
}

void part1()
{
    var pool = new Whirlpool(gridData);
    foreach (var instruction in instructions)
    {
        pool.PerformInstruction(instruction);
    }
    Console.WriteLine($"Part 1: {pool.LargestRowOrColumnSum}");
}



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

    public long LargestColumnSum => GetLargestColumnSum();
    public long LargestRowSum => GetLargestRowSum();

    public long LargestRowOrColumnSum => Math.Max(GetLargestColumnSum(), GetLargestRowSum());
    //public long LargestRowOrColumnSum => GetLargestColumnSum() > GetLargestRowSum() ? GetLargestColumnSum() : GetLargestRowSum();

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

    private long GetLargestColumnSum()
    {
        long maxSum = 0;
        for (int col = 0; col < _grid.GetLength(0); col++)
        {
            long sum = 0;
            for (int row = 0; row < _grid.GetLength(0); row++)
            {
                sum += _grid[row, col];
            }
            maxSum = Math.Max(maxSum, sum);
        }
        return maxSum;
    }
    
    private long GetLargestRowSum()
    {
        long maxSum = 0;
        for (int row = 0; row < _grid.GetLength(0); row++)
        {
            long sum = 0;
            for (int col = 0; col < _grid.GetLength(0); col++)
            {
                sum += _grid[row, col];
            }
            maxSum = Math.Max(maxSum, sum);
        }
        return maxSum;
    }
    private void ShiftRow(int? index, int amount)
    {
        if (index == null)
            return;
        int length = _grid.GetLength(0);
        index -= 1; // the instructions are 1-based index thinking, but the real world is a 0-based grid
        var tempRow = new int[length];
        // shift the values in a temp array
        for (int col = 0; col < length; col++)
        {
            int newPlacement = (col + amount) % length;
            tempRow[newPlacement] = _grid[index.Value, col];
        }
        
        // put the values back into the grid row
        for (int col = 0; col < length; col++)
        {
            _grid[index.Value, col] = tempRow[col];
        }
    }

    
    
    private void ShiftCol(int? index, int amount)
    {
        if (index == null)
            return;
        int length = _grid.GetLength(0);
        index -= 1; // the instructions are 1-based index thinking, but the real world is a 0-based grid
        var tempCol = new int[length];
        // shift the values in a temp array
        for (int row = 0; row < length; row++)
        {
            int newPlacement = (row + amount) % length;
            tempCol[newPlacement] = _grid[row, index.Value];
        }
        // put the shifted values back into the grid
        for (int row = 0; row < length; row++)
        {
            _grid[row, index.Value] = tempCol[row];
        }
    }

    private int GetModifiedValue(int originalValue, InstructionType type, int Amount)
    {
        //depending on type of action - modify this cell accordingly
        int modifiedValue = 0;
        if (type == InstructionType.ADD)
        {
            modifiedValue = originalValue + Amount;
        }
        if (type == InstructionType.SUB)
        {
            modifiedValue = originalValue - Amount;
        }
        if (type == InstructionType.MULTIPLY)
        {
            modifiedValue = originalValue * Amount;
        }
        
        //each value in the grid must be between 0 and 1073741823 inclusive.
        //If a value ever leaves this range, then 1073741824 is added or subtracted until the value
        //returns to the range.
        while (modifiedValue < 0)
        {
            modifiedValue += 1073741824;
        }

        while (modifiedValue >= 1073741823)
        {
            modifiedValue -= 1073741824;
        }
        
        return modifiedValue;
    }
    
    
    private void ModifyRow(Instruction instruction)
    {
        int realIndex;
        if (instruction.TargetIndex == null)
        {
            return;
        }
        else
        {
            realIndex = (int)instruction.TargetIndex - 1;
        }
            
        
        int length = _grid.GetLength(0);
        for (int col = 0; col < length; col++)
        {
            _grid[realIndex,col]=GetModifiedValue(_grid[realIndex,col],instruction.Type, instruction.Amount);
        }
    }

   
    private void ModifyCol(Instruction instruction)
    {
        int realIndex;
        if (instruction.TargetIndex == null)
        {
            return;
        }
        else
        {
            realIndex = (int)instruction.TargetIndex - 1;
        }
            
        
        int length = _grid.GetLength(0);
        for (int row = 0; row < length; row++)
        {
            _grid[row,realIndex]=GetModifiedValue(_grid[row,realIndex],instruction.Type, instruction.Amount);
        }
    }
    
    private void ModifyAll(Instruction instruction)
    {
        // here we have no TargetIndex - we will modify all cells in the grid
            
        
        int length = _grid.GetLength(0);
        for (int row = 0; row < length; row++)
        {
            for (int col = 0; col < length; col++)
            {
                _grid[row,col]=GetModifiedValue(_grid[row,col],instruction.Type, instruction.Amount);
            }
        }
    }
    public void PerformInstruction(Instruction instruction)
    {
        if (instruction.Type == InstructionType.SHIFT)
        {
            if (instruction.Target == TargetType.COL)
            {
                ShiftCol(instruction.TargetIndex, instruction.Amount);
            }
            else if (instruction.Target == TargetType.ROW)
            {
                ShiftRow(instruction.TargetIndex, instruction.Amount);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Invalid target type: {instruction.Target} for SHIFT instruction");
            }
        }
        else
        {
            if (instruction.Target == TargetType.ALL)
            {
                ModifyAll(instruction);
            }
            else
            {
                if (instruction.Target == TargetType.COL)
                {
                    ModifyCol(instruction);
                }
                else if (instruction.Target == TargetType.ROW)
                {
                    ModifyRow(instruction);
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Invalid target type: {instruction.Target} for ADD instruction");
                }
            }
        }
    }
    
}

record Instruction(InstructionType Type, TargetType Target, int Amount, int? TargetIndex = null);

static class Extensions
{
    public static string Printable(this List<Instruction> thelist)
    {
        return string.Join(Environment.NewLine, thelist.Select(instruction => instruction.ToString()));   
    }
    
    public static string Printable(this List<string> thelist)
    {
        return string.Join(Environment.NewLine, thelist);
    }
}