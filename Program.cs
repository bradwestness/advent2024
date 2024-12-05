using advent;

Console.Write("\nEnter a number from 1-25 to execute that day's program (append -e or --example to use example input): ");
string input = Console.ReadLine() ?? string.Empty;
AdventDayName dayName;

while (!TryParseDayName(input, out dayName))
{
    Console.Write("Invalid input! Please enter a number from 1-25: ");
    input = Console.ReadLine() ?? string.Empty;
}

var className = dayName.ToString();
var useExampleInput = input.Contains("-e", StringComparison.OrdinalIgnoreCase);

if (typeof(IAdventDay).Assembly.GetTypes()
    .Where(type => type.IsAssignableTo(typeof(IAdventDay)) && type.Name.Equals(className))
    .FirstOrDefault() is Type classType
    && Activator.CreateInstance(classType) is IAdventDay adventDay)
{
    Console.WriteLine($"\nExecuting program for day {dayName}{(useExampleInput ? " using example input" : "")}...");

    var puzzleInput = useExampleInput
        ? adventDay.GetExampleInput()
        : adventDay.GetInput();

    var partOneResult = adventDay.ExecutePartOne(puzzleInput);

    Console.WriteLine($"\n\tPart 1) {partOneResult}");

    var partTwoResult = adventDay.ExecutePartTwo(puzzleInput);

    Console.WriteLine($"\n\tPart 2) {partTwoResult}\n");
}
else
{
    throw new InvalidOperationException($"No class for {dayName} was found");
}

static bool TryParseDayName(string value, out AdventDayName result)
{
    try
    {
        var number = int.Parse(value.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).First());
        result = (AdventDayName)number;
        return result != AdventDayName.Unknown;
    }
    catch (Exception)
    {
        result = AdventDayName.Unknown;
        return false;
    }
}

enum AdventDayName
{
    Unknown = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Eleven = 11,
    Twelve = 12,
    Thirteen = 13,
    Fourteen = 14,
    Fifteen = 15,
    Sixteen = 16,
    Seventeen = 17,
    Eighteen = 18,
    Nineteen = 19,
    Twenty = 20,
    TwentyOne = 21,
    TwentyTwo = 22,
    TwentyThree = 23,
    TwentyFour = 24,
    TwentyFive = 25,
}