using AdventUtility;

namespace Aoc2025.Advent;

internal class Advent04
{
    private static int[,] _input = null!;
    private static int _xBounds;
    private static int _yBounds;

    internal static void Run(bool test, RunFlag flag, int val)
    {
        string[] temp = Utility.ReadInput(test, val).ConvertTo<string>();
        char[,] fart = Utility.ToMatrix(temp);

        _yBounds = fart.GetUpperBound(0) + 1;
        _xBounds = fart.GetUpperBound(1) + 1;

        _input = new int[_yBounds, _xBounds];

        Dictionary<char, int> map = new(2)
        {
            { '.', 1 },
            { '@', 2 },
        };

        for (int x = 0; x < _xBounds; x++)
        {
            for (int y = 0; y < _yBounds; y++)
            {
                _input[x, y] = map[fart[x, y]];
            }
        }

        int[,] snapshot = new int[_xBounds, _yBounds];
        Array.Copy(_input, snapshot, _input.Length);

        if (flag is RunFlag.All or RunFlag.Part1)
            Part1();

        _input = snapshot;

        if (flag is RunFlag.All or RunFlag.Part2)
            Part2();
    }

    private static bool CheckOutOfBounds(int x, int y)
    {
        return x < 0 || y < 0 || x >= _xBounds || y >= _yBounds;
    }

    private static int Scan(int oX, int oY)
    {
        int x = oX;
        int y = oY;

        int @djactent = 0;

        for (int i = 0; i < 8; i++)
        {
            switch (i)
            {
                case 0:
                    x -= 1;
                    y -= 1;
                    break;
                case 1:
                case 2:
                    x += 1;
                    break;
                case 3:
                case 4:
                    y += 1;
                    break;
                case 5:
                case 6:
                    x -= 1;
                    break;
                case 7:
                    y -= 1;
                    break;
            }

            if (CheckOutOfBounds(x, y))
                continue;

            if ((_input[x, y] == 2) && ++@djactent == 4)
                return 0;
        }

        return 1;
    }

    private static void Part1()
    {
        int result = 0;

        for (int x = 0; x < _xBounds; x++)
        {
            for (int y = 0; y < _yBounds; y++)
            {
                if (_input[x, y] == 1)
                    continue;

                result += Scan(x, y);
            }
        }

        Console.WriteLine("Result Part1: " + result);
    }

    private static void Part2()
    {
        int result = 0;
        int temp;
        List<(int X, int Y)> remove;

        while (true)
        {
            temp = 0;
            remove = [];

            for (int x = 0; x < _xBounds; x++)
            {
                for (int y = 0; y < _yBounds; y++)
                {
                    if (_input[x, y] == 1)
                        continue;

                    int scan = Scan(x, y);
                    if (scan == 1)
                        remove.Add((x, y));

                    temp += scan;
                }

                foreach ((int X, int Y) in remove)
                {
                    _input[X, Y] = 1;
                }
            }

            result += temp;

            if (temp == 0)
                break;
        }

        Console.WriteLine("Result Part2: " + result);
    }
}
