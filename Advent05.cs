using AdventUtility;

namespace Aoc2025.Advent;

internal class Advent05
{
    private static List<(long Start, long End)> _ranges = [];
    private static List<long> _ids = [];

    internal static void Run(bool test, RunFlag flag, int val)
    {
        string[] korv = Utility.ReadInput(test, val).ConvertTo<string>();
        char c = '-';
        bool f = false;

        for (int i = 0; i < korv.Length; i++)
        {
            if (!f && korv[i].Contains(c))
            {
                string[] ff = korv[i].Split(c);
                _ranges.Add((long.Parse(ff[0]), long.Parse(ff[1])));
            }
            else
            {
                f = true;
                _ids.Add(long.Parse(korv[i]));
            }
        }

        if (flag is RunFlag.All or RunFlag.Part1)
            Part1();

        if (flag is RunFlag.All or RunFlag.Part2)
            Utility.TimeIt(() => Part2()); // --- 1ms 11950ticks
    }

    private static void Part1()
    {
        int result = 0;

        for (int i = 0; i < _ids.Count; i++)
        {
            for (int j = 0; j < _ranges.Count; j++)
            {
                if (_ids[i] >= _ranges[j].Start && _ids[i] <= _ranges[j].End)
                {
                    result++;
                    break;
                }
            }
        }

        Console.WriteLine("Result Part1: " + result);
    }

    private static void Part2()
    {
        _ranges.Sort((a, b) => a.Start.CompareTo(b.Start));

        List<int> used = [];

        int iPtr = 0;

    next:

        for (int i = used.Count - 1; i >= 0; i--)
        {
            _ranges.RemoveAt(used[i]);
        }

        int len = _ranges.Count;

        if (iPtr >= len)
            goto end;

        for (int i = iPtr; i < len;)
        {
            used = [];
            iPtr++;
            long s = _ranges[i].Start;
            long e = _ranges[i].End;

            for (int j = i + 1; j < len; j++)
            {
                long rS = _ranges[j].Start;
                long rE = _ranges[j].End;

                if (rS > e)
                    break;

                if (s <= rE && rS <= e)
                {
                    s = Math.Min(s, rS);
                    e = Math.Max(e, rE);
                    used.Add(j);
                }
            }

            _ranges[i] = (s, e);

            goto next;
        }

    end:

        long result = 0;
        for (int i = 0; i < len; i++)
        {
            result += _ranges[i].End - _ranges[i].Start + 1;
        }

        Console.WriteLine("Result Part2: " + result);
    }

    private static void Part2WasWorthATry()
    {
        // The 2min Take 1 - 48 GB Memory used...
        HashSet<long> fartMangler = [];

        for (int i = 0; i < _ranges.Count; i++)
        {
            for (long j = _ranges[i].Start; j <= _ranges[i].End; j++)
            {
                _ = fartMangler.Add(j);
            }
        }

        Console.WriteLine("Result Part2: " + fartMangler.Count);
    }
}
