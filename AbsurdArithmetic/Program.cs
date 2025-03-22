const string fileName="input.txt";
using StreamReader fileInput = System.IO.File.OpenText(fileName);

int value1 = int.Parse(fileInput.ReadLine().Split(" ").Last());
int value2 = int.Parse(fileInput.ReadLine().Split(" ").Last());
int value3 = int.Parse(fileInput.ReadLine().Split(" ").Last());
_ = fileInput.ReadLine();
List<int> roomQualities = [];
while (fileInput.EndOfStream == false)
{
    string line = fileInput.ReadLine();
    roomQualities.Add(int.Parse(line));
}
CurrencyConverter converter = new(value3, value2, value1);

part1();
part2();
part3();


void part3()
{
    var roomList = roomQualities.Select(r => new Room(r, converter.Convert(r))).ToList();
    var bestRoomForBudget = roomList.Where(r => r.Price <= 15_000_000_000_000).Max();
    Console.WriteLine($"Part 3\nMax price for room within budget is: {bestRoomForBudget.Quality}");
    
}
void part2()
{
    int qualitySum = roomQualities[1..].Where(q => q % 2 == 0).Sum();
    long part2Sum = converter.Convert(qualitySum);
    Console.WriteLine($"Part 2\nSum: {part2Sum}");
}
void part1()
{
    roomQualities.Sort();
    int medianRoomQuality = roomQualities[(roomQualities.Count - 1)/2];
    Console.WriteLine($"Part 1. \nThe price for the median room qualities is: {converter.Convert(medianRoomQuality)}");
}

class Room : IComparable<Room>
{
    private int _quality;
    private long _price;

    public int Quality
    {
        get => _quality;
        private set => _quality = value;
    }

    public long Price
    {
        get => _price;
        set => _price = value;
    }

    public Room(int quality, long price)
    {
        _quality = quality;
        _price = price;
    }

    public int CompareTo(Room? other)
    {
        if (this.Price < other.Price)
        {
            return -1;
        }
        else if (this.Price > other.Price)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
class CurrencyConverter
{
    private int exponent;
    private int multiplier;
    private int addvalue;

    public CurrencyConverter(int exponent, int multiplier, int addvalue)
    {
        this.exponent = exponent;
        this.multiplier = multiplier;
        this.addvalue = addvalue;
    }

    public long Convert(int roomQuality)
    {
        return (long)Math.Pow(roomQuality, exponent) * multiplier + addvalue;
    }
}