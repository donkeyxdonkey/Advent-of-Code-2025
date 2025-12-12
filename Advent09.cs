using AdventUtility;

namespace Aoc2025.Advent;

internal class Advent09
{
    private static Point[] _input = [];
    private static int _len;

    internal static void Run(bool test, RunFlag flag, int val)
    {
        string[] temp = Utility.ReadInput(test, val).ConvertTo<string>();
        _len = temp.Length;
        _input = new Point[_len];

        const char COMMA = ',';

        for (int i = 0; i < _len; i++)
        {
            _input[i] = new Point(temp[i].Split(COMMA));
        }

        if (flag is RunFlag.All or RunFlag.Part1)
            Part1();

        if (flag is RunFlag.All or RunFlag.Part2)
            Part2();
    }

    private static void Part1()
    {
        long result = 0;

        for (int i = 1; i < _len; i++)
        {
            for (int j = i + 1; j < _len; j++)
            {
                Point p1 = _input[i];
                Point p2 = _input[j];

                long ff = Math.Abs(p2.X - p1.X) + 1;
                long gg = Math.Abs(p2.Y - p1.Y) + 1;
                long area = ff * gg;

                //Console.WriteLine(p1);
                //Console.WriteLine(p2);
                //Console.WriteLine(area);

                result = Math.Max(result, area);
            }
        }

        Console.WriteLine("Result Part1: " + result);
    }

    private static void Part2()
    {

    }

    private struct Point
    {
        public readonly int X => _x;

        public readonly int Y => _y;

        private readonly int _x;
        private readonly int _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Point(string[] xy) : this(int.Parse(xy[0]), int.Parse(xy[1]))
        {
        }

        public override string ToString()
        {
            return "{ x = " + _x + ", y = " + _y + " }";
        }
    }
}
