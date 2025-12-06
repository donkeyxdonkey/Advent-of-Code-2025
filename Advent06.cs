using AdventUtility;
using System.Text;

namespace Aoc2025.Advent;

internal class Advent06
{
    private enum Oper
    {
        Add,
        Mul
    }

    private static long[][] _input = [];
    private static Oper[] _operators = [];

    internal static void Run(bool test, RunFlag flag, int val)
    {
        string[] temp = Utility.ReadInput(test, val).ConvertTo<string>();

        _input = new long[temp.Length - 1][];
        _operators = ParseOperators(temp[^1]);

        string[] inputSlice = temp[..^1];

        if (flag is RunFlag.All or RunFlag.Part1)
        {
            _input = ParsePart1(inputSlice);
            //Debug.Assert(_input[0].Length == _operators.Length);
            Part1();
        }

        if (flag is RunFlag.All or RunFlag.Part2)
        {
            _input = ParsePart2(inputSlice);
            //Debug.Assert(_input[0].Length == _operators.Length);
            Part2();
        }
    }

    private static Oper[] ParseOperators(string lastRow)
    {
        char mul = '*';

        string[] split = lastRow.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int len = split.Length;
        Oper[] temp = new Oper[len];
        for (int i = 0; i < len; i++)
        {
            temp[i] = split[i][0] == mul ? Oper.Mul : Oper.Add;
        }

        return temp;
    }

    private static long[][] ParsePart1(string[] inputSlice)
    {
        int len = inputSlice.Length;
        long[][] temp = new long[len][];

        for (int i = 0; i < len; i++)
        {
            temp[i] = Array.ConvertAll<string, long>(inputSlice[i].Split(' ', StringSplitOptions.RemoveEmptyEntries), long.Parse);
        }

        return temp;
    }

    private static long[][] ParsePart2(string[] inputSlice)
    {
        int len = inputSlice.Length;
        int tLen = inputSlice[0].Length;

        Dictionary<int, int> colSplits = [];

        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < tLen; j++)
            {
                char c = inputSlice[i][j];
                if (!char.IsDigit(c) && !colSplits.TryAdd(j, 1))
                {
                    colSplits[j]++;
                }
            }
        }

        int[] columns = colSplits.Where(x => x.Value == colSplits.Values.Max()).Select(x => x.Key).Append(tLen).ToArray();

        int cLen = columns.Length;
        int xLen = 0;
        for (int i = 1; i < columns.Length; i++)
        {
            xLen = Math.Max((columns[i] - columns[i - 1]), xLen);
        }

        long[][] temp = new long[cLen][];
        int ptr = 0;
        int wPtr;

        for (int i = 0; i < cLen; i++)
        {
            wPtr = 0;
            temp[i] = new long[xLen - 1];

            for (int j = columns[i] - 1; j >= ptr; j--)
            {
                StringBuilder s = new();
                for (int k = 0; k < len; k++)
                {
                    char c = inputSlice[k][j];
                    if (c == ' ')
                        continue;

                    s.Append(c);
                }

                if (s.Length == 0)
                    continue;

                temp[i][wPtr++] = long.Parse(s.ToString());
            }

            ptr = columns[i];
        }

        return temp;
    }

    private static void Part1()
    {
        long result = 0;

        int len = _input.Length;
        int iLen = _input[0].Length;

        for (int i = 0; i < iLen; i++)
        {
            Oper oper = _operators[i];
            long inp = _input[0][i];

            switch (oper)
            {
                case Oper.Add:
                    for (int j = 1; j < len; j++)
                    {
                        inp += _input[j][i];
                    }

                    result += inp;
                    break;
                case Oper.Mul:
                    for (int j = 1; j < len; j++)
                    {
                        inp *= _input[j][i];
                    }

                    result += inp;
                    break;
            }
        }

        Console.WriteLine("Result Part1: " + result);
    }

    private static void Part2()
    {
        long result = 0;

        int len = _input.Length;
        int iLen = _input[0].Length;

        for (int i = 0; i < len; i++)
        {
            Oper oper = _operators[i];

            long inp = _input[i][0];

            switch (oper)
            {
                case Oper.Add:
                    for (int j = 1; j < iLen; j++)
                    {
                        inp += _input[i][j];
                    }

                    result += inp;
                    break;
                case Oper.Mul:
                    for (int j = 1; j < iLen; j++)
                    {
                        long mul = _input[i][j];
                        if (mul != 0)
                            inp *= mul;
                    }

                    result += inp;
                    break;
            }
        }

        Console.WriteLine("Result Part2: " + result);
    }
}
