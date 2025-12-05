using System.Diagnostics;

namespace AdventUtility;

public static class Utility
{
    public static string ReadInput(bool test, int advent)
    {
        string adv = (advent < 10 ? "0" : "") + advent.ToString();

        string path = Path.Combine(
            Path.Combine(AppContext.BaseDirectory, test ? "Test" : "Input"),
            $"input{adv}.txt"
        );
        return File.ReadAllText(path);
    }

    public static T[] ConvertTo<T>(this string input, string separator = "\r\n")
    {
        string[] x = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        return x.Select(p => (T)Convert.ChangeType(p, typeof(T))).ToArray();
    }

    public static char[,] ToMatrix(string[] input)
    {
        int rows = input.Length;
        int cols = input[0].Length;

        char[,] result = new char[rows, cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                result[y, x] = input[y][x];
            }
        }

        return result;
    }

    public static void TimeIt(Action action)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        Stopwatch sw = Stopwatch.StartNew();

        action();

        sw.Stop();

        Console.WriteLine(sw.ElapsedMilliseconds + "ms " + sw.ElapsedTicks + "ticks");
    }
}
