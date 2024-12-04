using System;

namespace advent;

public interface IAdventDay
{
    string GetExampleInput();

    string GetInput();

    string Execute(string input);
}
