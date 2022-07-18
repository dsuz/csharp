using System;

class GenericMethod
{
    static void Main(string[] args)
    {
        // オーバーロードを使ったパターン
        int x = OverloadPattern.Max(1, 2);
        Console.WriteLine("x: {0}", x);
        char y = OverloadPattern.Max('け', 'わ'); // char は「文字」つまり一文字だけ。string（文字列）では "" で囲むが、char は '' で囲む。
        Console.WriteLine("y: {0}", y);

        // ジェネリックメソッドを使ったパターン
        int p = GenericsMethodPattern.Max<int>(1, 2);
        Console.WriteLine("p: {0}", p);
        char q = GenericsMethodPattern.Max<char>('け', 'わ'); // char は「文字」つまり一文字だけ。string（文字列）では "" で囲むが、char は '' で囲む。
        Console.WriteLine("q: {0}", q);

        Console.WriteLine("Hit Enter...");
        Console.ReadLine();
    }
}

/// <summary>
/// オーバーロードを使ったパターンで関数を定義したクラス
/// </summary>
class OverloadPattern
{
    public static int Max(int a, int b)
    {
        if (a >= b)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    public static char Max(char a, char b)
    {
        if (a >= b)
        {
            return a;
        }
        else
        {
            return b;
        }
    }
}

/// <summary>
/// ジェネリックメソッドを使ったパターンで関数を定義したクラス
/// </summary>
class GenericsMethodPattern
{
    /// <summary>
    /// 与えられた２つの引数のうち、大きい方を返す。
    /// 型は T とするが、これは型が不定であることを表す。
    /// where T : IComparable とは、与えられた型が比較可能であるという制限を与えている。
    /// この関数は「大きい方」という比較をする、つまり引数に与えられる値が比較可能な型でないと処理することができないため、制限している。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static T Max<T>(T a, T b) where T : IComparable
    {
        if (a.CompareTo(b) >= 0)
        {
            return a;
        }
        else
        {
            return b;
        }
    }
}