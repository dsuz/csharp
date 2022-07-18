using System;

class Fibonacci
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("正の整数を入力してください: ");
            string line = Console.ReadLine();
            int i = int.Parse(line);
            int fibonacci = FibonacciNumber(i);
            Console.WriteLine($"{i} 番目のフィボナッチ数は {fibonacci} です。");
        }
    }

    /// <summary>
    /// number 番目のフィボナッチ数を返す。
    /// </summary>
    /// <param name="number">正の整数</param>
    /// <returns>number 番目のフィボナッチ数</returns>
    static int FibonacciNumber(int n)
    {
        if (n < 0)
        {
            Console.WriteLine("与えられた引数が不正です。");
            return -1;
        }
        else if (n == 0)
        {
            return 0;
        }
        else if (n == 1)
        {
            return 1;
        }

        // TODO: 以下では 0 を返しているが、これを修正して「1つ前のフィボナッチ数」と「2つ前のフィボナッチ数」の和を返すように修正する。
        return 0;
    }
}
