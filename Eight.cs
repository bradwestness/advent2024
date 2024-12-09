using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace advent;

public sealed class Eight : IAdventDay
{
    public string ExecutePartOne(string input)
    {
        var cityMap = new CityMap(input);
        Console.WriteLine(cityMap.ToString());

        var total = cityMap.Antinodes.Distinct().Count();

        return $"Total distinct antinode positions in map: {total}";
    }

    public string ExecutePartTwo(string input)
    {
        var cityMap = new CityMap(input);

        return "TBD";
    }

    private record struct Position(int Row, int Col)
    {
        public Position GetAntinodePosition(Position other)
        {
            var rowDiff = Math.Abs(Row - other.Row);
            var newRow = Row < other.Row
                ? Row - rowDiff
                : Row + rowDiff;

            var colDiff = Math.Abs(Col - other.Col);
            var newCol = Col < other.Col
                ? Col - colDiff
                : Col + colDiff;

            return new Position(newRow, newCol);
        }
    }

    private sealed class CityMap
    {
        private readonly Dictionary<char, IList<Position>> antennas = new();
        private readonly IList<Position> antinodes;
        private readonly char[,] grid;

        public CityMap(string input)
        {
            grid = ParseInput(input);
            antinodes = FindAntinodes();
        }

        public IDictionary<char, IList<Position>> Antennas => antennas;

        public IList<Position> Antinodes => antinodes;

        public char[,] Grid => grid;

        public override string ToString()
        {
            StringBuilder sb = new();

            for (var row = 0; row < grid.GetLength(0); row++)
            {
                for (var col = 0; col < grid.GetLength(1); col++)
                {
                    var isAntinode = antinodes.Contains(new Position(row, col));
                    sb.Append(isAntinode ? '#' : grid[row, col]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private char[,] ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var result = new char[lines.Length, lines[0].Length];

            for (var row = 0; row < lines.Length; row++)
            {
                var chars = lines[row].ToCharArray();

                for (var col = 0; col < chars.Length; col++)
                {
                    char c = chars[col];
                    result[row, col] = c;

                    if (c == '.')
                    {
                        continue;
                    }

                    if (!antennas.ContainsKey(c))
                    {
                        antennas[c] = new List<Position>();
                    }

                    antennas[c].Add(new Position(row, col));
                }
            }

            return result;
        }

        private IList<Position> FindAntinodes()
        {
            var result = new List<Position>();

            foreach (var (c, antennas) in antennas)
            {
                foreach (var antenna in antennas)
                {
                    foreach (var other in antennas)
                    {
                        if (antenna == other)
                        {
                            continue;
                        }

                        var antinode = antenna.GetAntinodePosition(other);

                        if (IsWithinGrid(antinode))
                        {
                            result.Add(antinode);
                        }
                    }
                }
            }

            return result;
        }

        private bool IsWithinGrid(Position position)
        {
            return 0 <= position.Row && position.Row < grid.GetLength(0)
                && 0 <= position.Col && position.Col < grid.GetLength(1);
        }
    }

    public string GetExampleInput() =>
    @"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............";

    public string GetInput() =>
    @".....................U.........w..................
l.................................................
...........o.a................U...w...............
............................................W.....
..........T....................s.............7....
.............................................W....
.........T..............4....n.d.H.........5......
......T.....oj...U.....n...w......H...........z...
.G..x..........................E.....V..H.........
.........a....................d....s.......7w.....
...j....r.............o.............V.......d...W.
.......r..J.Goa.U...............n................z
.........Jj.........M..........Pv.................
...J...........t..3..M..............sLV...........
...................t................n.............
....r...........X...........M........v............
...x....t......I......a.PM...............W........
...........1.Bj....I........vO.h.dL...............
.........6....Rr......B...X........h..5v.L..z.....
......1G...........x.....3B.......5...............
.................B....0..........4..E.............
.....................X.....5..h....P....f.....D...
.......1........J.....eK..........................
..................I....R....K...........k.........
......G..................O........................
...........H...9...............K8.P.4..k..E.......
............1....3.............8.F.............f..
.........................4........................
.l...........X............9.......................
....N.................R...t.e.....................
...g............3..R.........e....h.........f.....
...........................e......i...............
................2...I.7..9..O.....s.........k.....
....................6...9E.............F..O.......
........................KN........................
.......g......................Z.........F..f...Y..
...........................A....i.................
...........6g...b........8.......y.....S..........
..l.....6.....m...............8...................
....u..m...b...............p...A..................
..............b.p........................k........
....m......2...........Z..y....i..................
........g2.....b.........i....D..ZF...............
......2.0...........p............N..........A.....
...m.............S...y........A...Z...N...........
..S..l..........................................Y.
........S....0u.................y......DY.........
...........0.........................D............
.................u...................p...Y........
.......u..........................................";
}
