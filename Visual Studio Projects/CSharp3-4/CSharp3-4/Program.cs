using System;

class Program
{
    static void Main(string[] args)
    {
        Func();
        // GC.Collect();    // 意図的にガベージコレクションを実行することもできる
        Console.WriteLine("Hit Enter key...");
        Console.ReadLine();
    }

    static void Func()
    {
        MyClass myClass01 = new MyClass(100);   // このインスタンスは Main() を抜けた後に破棄される

        using (MyDisposableClass myClass02 = new MyDisposableClass("test")) // このインスタンスは using を抜けた後に破棄される
        {
            Console.WriteLine("using 句の中に入りました");
        }

        return;
    }
}

/// <summary>
/// 普通のクラス
/// </summary>
class MyClass
{
    int n;

    /// <summary>デフォルトコンストラクター</summary>
    public MyClass()
    {

    }

    /// <summary>コンストラクター</summary>
    public MyClass(int n)
    {
        this.n = n;
    }

    /// <summary>デストラクター</summary>
    ~MyClass()
    {
        Console.WriteLine($"{n} を持つインスタンスが破棄されました");
    }
}

/// <summary>
/// IDisposable を継承したクラス
/// using 句の中で使えるようになる
/// </summary>
class MyDisposableClass : IDisposable
{
    string s;

    public MyDisposableClass()
    {

    }

    public MyDisposableClass(string s)
    {
        this.s = s;
    }

    void IDisposable.Dispose()
    {
        Console.WriteLine($"{s} を持つインスタンスが破棄されました");
    }
}