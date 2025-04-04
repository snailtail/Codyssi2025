using System.Linq;
using System.Text.RegularExpressions;


const string fileName = "example.txt";
var input = File.ReadAllText(fileName).Split($"{Environment.NewLine}{Environment.NewLine}");
List<Official> officials = input[0].Split(Environment.NewLine).Select(
        x => new Official(x.Split(" HAS ")[0], int.Parse(x.Split(" HAS ")[1]))
    ).ToList();

List<Transaction> transactions = new();
foreach(string line in input[1].Split(Environment.NewLine))
{
    string pattern = @"FROM\s(\w+)\sTO\s(\w+)\sAMT\s(\d+)";
    var result = Regex.Match(line, pattern);
    Transaction transaction = new(result.Groups[1].Value, result.Groups[2].Value, int.Parse(result.Groups[3].Value));
    transactions.Add(transaction);
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
}