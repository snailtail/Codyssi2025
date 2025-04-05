using System.Linq;
using System.Text.RegularExpressions;

const string fileName = "input.txt";

var (officials, transactions) = ParseInput(fileName);
Part1(officials,transactions);

(officials, transactions) = ParseInput(fileName);
Part2(officials,transactions);

(officials, transactions) = ParseInput(fileName);
Part3(officials, transactions);
void Part3(List<Official> officials, List<Transaction> transactions)
{
    
}
void Part2(List<Official> officials, List<Transaction> transactions)
{
    int transactionCount = 0;
    foreach (var transaction in transactions)
    {
        Official? sender = officials.Where(o => o.Name == transaction.From).FirstOrDefault();
        Official? recipient = officials.Where(o => o.Name == transaction.To).FirstOrDefault();
        if (sender != null && recipient != null)
        {
            int amount;
            if (transaction.Amount > sender.Balance)
            {
                amount = sender.Balance;
            }
            else
            {
                amount = transaction.Amount;
            }
            sender.Balance -= amount;
            recipient.Balance += amount;
            transactionCount++;
        }
        else
        {
            Console.WriteLine("Error: sender or recipient could not be found.");
        }
    }
    
    var topThree = officials.OrderByDescending(o => o.Balance).Take(3).ToArray();
    int sumPart2 = topThree.Sum(o => o.Balance);
    Console.WriteLine($"Part 2: {sumPart2}");
}

void Part1(List<Official> officials, List<Transaction> transactions)
{
    foreach (var transaction in transactions)
    {
        Official sender = officials.Where(o => o.Name == transaction.From).FirstOrDefault();
        Official recipient = officials.Where(o => o.Name == transaction.To).FirstOrDefault();
        if (sender != null && recipient != null)
        {
            sender.Balance -= transaction.Amount;
            recipient.Balance += transaction.Amount;
        }
    }

    var topThree = officials.OrderByDescending(o => o.Balance).Take(3).ToArray();
    int sumPart1 = topThree.Sum(o => o.Balance);
    Console.WriteLine($"Part 1: {sumPart1}");
}

(List<Official>, List<Transaction>) ParseInput(string fileName)
{
    var input = File.ReadAllText(fileName).Split($"{Environment.NewLine}{Environment.NewLine}");
    List<Official> officials = input[0].Split(Environment.NewLine).Select(
        x => new Official(x.Split(" HAS ")[0], int.Parse(x.Split(" HAS ")[1]))
    ).ToList();

    List<Transaction> transactions = new();
    foreach(string line in input[1].Split(Environment.NewLine))
    {
        string pattern = @"FROM\s([A-Z-a-z-]+)\sTO\s([A-Z-a-z-]+)\sAMT\s(\d+)";
        var result = Regex.Match(line, pattern);
        Transaction transaction = new(result.Groups[1].Value, result.Groups[2].Value, int.Parse(result.Groups[3].Value));
        transactions.Add(transaction);
    }
    return (officials, transactions);
}

record Transaction(string From, string To, int Amount);


class Official
{
    public string Name { get; set; }
    public int Balance { get; set; }
    public Official(string name, int balance)
    {
        Name = name;
        Balance = balance;
    }

    public override string ToString()
    {
        return $"{Name} - {Balance}";
    }

}
