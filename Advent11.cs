using AdventUtility;

namespace Aoc2025.Advent;

internal class Advent11
{
    private static int _youIndex;
    private static int _svrIndex;
    private static int _fftIndex;
    private static int _dacIndex;
    private static Device[] _input = [];
    private static Dictionary<(int node, bool dac, bool fft), long> _p2mem = [];

    internal static void Run(bool test, RunFlag flag, int val)
    {
        string[] temp = Utility.ReadInput(test, val).ConvertTo<string>();
        int len = temp.Length;
        _input = new Device[len];
        Dictionary<string, int> tempTargetMap = new(len + 1);

        string[] tempNames = new string[len];
        List<string[]> devices = [];

        for (int i = 0; i < len; i++)
        {
            string name = temp[i][0..3];
            tempNames[i] = name;
            tempTargetMap[name] = i;
            devices.Add(temp[i][4..].Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        tempTargetMap.Add("out", -1);

        for (int i = 0; i < len; i++)
        {
            int dLen = devices[i].Length;
            int[] targetDevices = new int[dLen];
            for (int j = 0; j < dLen; j++)
            {
                targetDevices[j] = tempTargetMap[devices[i][j]];
            }

            _input[i] = new Device(targetDevices);
        }

        if (flag is RunFlag.All or RunFlag.Part1)
        {
            _youIndex = tempTargetMap["you"];
            Part1();
        }

        if (flag is RunFlag.All or RunFlag.Part2)
        {
            _svrIndex = tempTargetMap["svr"];
            _dacIndex = tempTargetMap["dac"];
            _fftIndex = tempTargetMap["fft"];
            Part2();
            //Utility.TimeIt(() => Part2());
        }
    }

    private static void Part1()
    {
        Device start = _input[_youIndex];
        int result = 0;

        for (int i = 0; i < start.Len; i++)
        {
            FindIt(ref result, start.Targets[i]);
        }

        Console.WriteLine("Result Part1: " + result);
    }

    private static void FindIt(ref int result, int index)
    {
        if (index == -1)
        {
            ++result;
            return;
        }

        Device next = _input[index];

        for (int i = 0; i < next.Len; i++)
        {
            FindIt(ref result, next.Targets[i]);
        }
    }

    private static void Part2()
    {
        _p2mem ??= [];
        Device start = _input[_svrIndex];
        long result = 0;

        for (int i = 0; i < start.Len; i++)
            result += FindItAgain(start.Targets[i], false, false);

        Console.WriteLine("Result Part2: " + result);
    }

    private static long FindItAgain(int index, bool dacFound, bool fftFound)
    {
        if (index == -1)
            return (dacFound && fftFound) ? 1 : 0;

        if (index == _dacIndex)
            dacFound = true;

        if (index == _fftIndex)
            fftFound = true;

        (int index, bool dacFound, bool fftFound) key = (index, dacFound, fftFound);
        if (_p2mem.TryGetValue(key, out long cached))
            return cached;

        Device dev = _input[index];
        long total = 0;

        for (int i = 0; i < dev.Len; i++)
            total += FindItAgain(dev.Targets[i], dacFound, fftFound);

        _p2mem[key] = total;
        return total;
    }

    private readonly struct Device
    {
        private readonly int[] _targets;

        public readonly int Len => _targets.Length;

        public readonly int[] Targets => _targets;

        public Device(int[] targets)
        {
            _targets = targets;
        }
    }
}
