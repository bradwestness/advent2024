namespace advent;

public interface IAdventDay
{
    string GetExampleInput();

    string GetInput();

    string ExecutePartOne(string input);

    string ExecutePartTwo(string input);
}
