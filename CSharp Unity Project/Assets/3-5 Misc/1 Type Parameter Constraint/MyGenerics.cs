using System;
using UnityEngine;

public class MyGenerics1<T> where T : IComparable<T> // ここで「型 T は IComparable を実装していること」という条件を追加することで…
{
    public int Test(T x, T y)
    {
        return x.CompareTo(y);  // T 型の変数 x が CompareTo メソッドを持つと決定できる
    }
}

public class MyGenerics2<T> where T : MonoBehaviour // ここで「型 T は MonoBehaviour またはその派生クラスであること」という条件を追加することで…
{
    public void Test(T x)
    {
        Debug.Log(x.transform.position);    // T 型の変数 x が transform プロパティを持つと決定できる
    }
}

public class MyGenerics3<T> where T : MonoBehaviour, IComparable<T> // カンマ区切りで条件を複数指定することもできるので…
{
    public void Test(T x, T y)
    {
        // T は比較可能かつ GameObject としての name を持つと決定できる
        if (x.CompareTo(y) > 0)
        {
            Debug.Log($"{x.name} -> {y.name}");
        }
        else if (x.CompareTo(y) < 0)
        {
            Debug.Log($"{y.name} -> {x.name}");
        }
        else
        {
            Debug.Log($"{x.name} == {x.name}");
        }
    }
}
