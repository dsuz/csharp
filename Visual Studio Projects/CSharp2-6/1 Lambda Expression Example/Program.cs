using System;
using System.Collections.Generic;   // List を使うために必要
using System.Linq;  // Linq を使うために必要

class Program
{
    static void Main(string[] args)
    {
        int[] intArray = { 9, 3, 5, 8, 1, 7, 2, 4, 6, 0 };

        // 例題: 上の配列から「値が 5 より大きいもの」を抽出したリストまたは配列を作り、抽出したものを出力せよ

        // パターン 0 通常のやり方
        List<int> intList = new List<int>();

        foreach (var i in intArray)
        {
            if (i > 5)
            {
                intList.Add(i);
            }
        }

        foreach (var i in intList)
        {
            Console.WriteLine(i);
        }

        Console.WriteLine("-----");

        // パターン 1 Linq を使って「別に定義した関数」を渡す
        System.Func<int, bool> func = GreaterThanFive;
        var filteredList1 = intArray.Where(func).ToList();
        System.Action<int> proc = Print;
        filteredList1.ForEach(proc);
        Console.WriteLine("-----");

        // パターン 2 Linq を使って「delegate を使って定義した匿名メソッド」を渡す
        var filteredList2 = intArray.Where(delegate(int i)
        {
            return i > 5;
        }).ToList();

        filteredList2.ForEach(delegate (int i)
        {
            Console.WriteLine(i);
        });

        Console.WriteLine("-----");

        // パターン 3 ラムダ式を使って匿名メソッドを定義する
        var filteredList3 = intArray.Where((int i) => { return i > 5; }).ToList();
        filteredList1.ForEach((int i) => { Console.WriteLine(i); });
        Console.WriteLine("-----");

        // パターン 4 ラムダ式を使ったパターンから余計なものを全て削る
        intArray.Where(i => i > 5).ToList().ForEach(i => Console.WriteLine(i));

        Console.WriteLine("Hit Enter...");
        Console.ReadLine();
    }

    /// <summary>
    /// 引数として与えられた整数が 5 より大きいか判定する
    /// </summary>
    /// <param name="i">判定したい整数</param>
    /// <returns>5 より大きい場合は true, そうでない場合は false</returns>
    static bool GreaterThanFive(int i)
    {
        return i > 5;
    }

    /// <summary>
    /// 引数として与えられた整数を出力する
    /// </summary>
    /// <param name="i">出力したい整数</param>
    static void Print(int i)
    {
        Console.WriteLine(i);
    }
}
