using System;
using System.Diagnostics;

class PrimeNumber
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("正の整数を入力してください: ");
            string line = Console.ReadLine();
            int i = int.Parse(line);
			Stopwatch sw = new Stopwatch(); // 実行時間を測る
			sw.Start();
			bool result = IsPrime(i);
			sw.Stop();

			if (result)
			{
				Console.Write($"{i} は素数です。");
			}
            else
            {
				Console.Write($"{i} は素数ではありません。");
			}

			Console.WriteLine($"実行時間: {sw.ElapsedTicks.ToString()}");
        }
    }

	/// <summary>
	/// 与えられた整数が素数であるか判定する
	/// </summary>
	/// <param name="n">素数かどうか判定したい整数</param>
	/// <returns>素数なら true</returns>
	static bool IsPrime(int n)
	{
		if (n < 2)	// 1 は素数ではない。0 以下も素数ではない。
		{
			return false;
		}
		else if (n == 2) // 2 は素数
		{
			return true;
		}
		else
		{
			// 3 以上に対しては、2 ~ n-1 で割ってみる
			for (int i = 2; i < n; i++)
			{
				// 割り切れたら素数ではない
				if (n % i == 0)
				{
					return false;
				}
			}

			// 2 ~ n-1 の全てで割り切れなかったら素数
			return true;
		}
	}
}
