using advent;

Console.Write("\nEnter a number from 1-25 to execute that day's program: ");
int number = 0;
string input = string.Empty;

while (number < 1 || number > 25)
{
    input = Console.ReadLine() ?? string.Empty;
    if (!int.TryParse(input.Split(' ').First(), out number) || number < 1 || number > 25)
    {
        Console.WriteLine("Invalid input! Please enter a number form 1-25: ");
    }
}

var dayName = (AdventDayName)number;
var className = dayName.ToString();

if (typeof(IAdventDay).Assembly.GetTypes()
    .Where(type => type.IsAssignableTo(typeof(IAdventDay)) && type.Name.Equals(className))
    .FirstOrDefault() is Type classType
    && Activator.CreateInstance(classType) is IAdventDay adventDay)
{
    var puzzleInput = input.Contains('E', StringComparison.OrdinalIgnoreCase)
        ? adventDay.GetExampleInput()
        : adventDay.GetInput();

    var result = adventDay.Execute(puzzleInput);

    Console.WriteLine(result);
}
else
{
    throw new InvalidOperationException($"No class for {dayName} was found");
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