using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
    // ラムダ式の活用例
    static void Main(string[] args)
    {
        string[] strNumArray = { "0", "1", "2", "3" };
        string[] strFruitArray = { "lemon", "apple", "kiwi", "strawberry", "banana" };

        // 配列やリストの要素を一括で型変換する
        Array.ForEach(Array.ConvertAll(strNumArray, s => int.Parse(s)), i => Console.Write($"{i + 1}, "));
        Console.WriteLine("\n-----");
        strNumArray.ToList().ConvertAll(s => int.Parse(s)).ForEach(i => Console.Write($"{i * 2}, "));
        Console.WriteLine("\n-----");

        // 配列から条件を指定して要素を検索できる
        Array.ForEach(Array.FindAll(strFruitArray, s => s.Length > 5), s => Console.Write($"{s}, "));
        Console.WriteLine("\n-----");
        Console.WriteLine(Array.Find(strFruitArray, s => s.Length > 5));
        Console.WriteLine("-----");

        // 配列に対する操作をする時は、System.Array クラスのスタティック メソッドを使います。
        // List に対して操作をする時は、List<T> 型のインスタンス メソッドを使います。
        // System.Array クラスには、他に以下のようなスタティック メソッドがあります。
        // Exists, FindIndex, FindLast, FindLastIndex
        // Array にカーソルを置いて F1 キーを押すと、ヘルプを見ることができます。

        // List に対してラムダ式を使って何かできるメソッドは以下に一覧があります。

        // 引数でデリゲート(≒ラムダ式)を受け取れるList<T>のメソッド【C#】
        // https://kan-kikuchi.hatenablog.com/entry/List_Lambda

        // List と Array では当然似たような処理が用意されていますが、RemoveAll と Sort は List のみ実行できます。
        
        // 条件に従って並べ替える (Sort)
        var fruitList = strFruitArray.ToList();
        fruitList.Sort((a, b) => a.Length - b.Length);  // 文字数が少ない順に並べ替えている。最近では Linq の OrderBy メソッドを使う事が多いかもしれない。
        fruitList.ForEach(s => Console.Write($"{s}, "));
        Console.WriteLine("\n-----");

        // 条件に合致した要素を削除する (RemoveAll)
        fruitList.RemoveAll(s => s.Contains("a"));  // "a" を含む要素を削除している。
        fruitList.ForEach(s => Console.Write($"{s}, "));
        Console.WriteLine("\n-----");

        // List にどんなメソッドがあるかは、List 型の変数にカーソルを置いて F1 キーを押すことでヘルプを見ることができます。

        Console.WriteLine("Hit Enter...");
        Console.ReadLine();
    }
}
