using AdventUtility;

namespace Aoc2025.Advent;

internal class Advent03
{
    private static string[] _input = [];
    private static int _len;
    private static int _iLen;

    internal static void Run(bool test, RunFlag flag, int val)
    {
        _input = Utility.ReadInput(test, val).ConvertTo<string>();
        _len = _input.Length;
        _iLen = _input[0].Length;

        if (flag is RunFlag.All or RunFlag.Part1)
            Part1();

        if (flag is RunFlag.All or RunFlag.Part2)
            Part2();
    }

    private static void Part1()
    {
        int[][] s = new int[_len][];

        int result = 0;

        for (int i = 0; i < _len; i++)
        {
            s[i] = new int[_iLen + 2];
            s[i][_iLen] = -1;
            s[i][_iLen + 1] = -1;

            for (int j = 0; j < _iLen; j++)
            {
                int x = Convert.ToInt32(_input[i][j] - '0');
                s[i][j] = x;

                if (s[i][_iLen] == -1 || x > s[i][s[i][_iLen]] && j != _iLen - 1)
                {
                    s[i][_iLen] = j;
                    s[i][_iLen + 1] = -1;
                    continue;
                }

                if (s[i][_iLen + 1] == -1 || x > s[i][s[i][_iLen + 1]])
                {
                    s[i][_iLen + 1] = j;

                    if (x == 9)
                        break;
                }
            }

            int a = s[i][s[i][_iLen]];
            int b = s[i][s[i][_iLen + 1]];
            result += int.Parse(a.ToString() + b.ToString());
        }

        Console.WriteLine("Result Part1: " + result);
    }

    private static void Part2()
    {

    }
}
