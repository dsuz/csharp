using UnityEngine;

public class InstantiateTest : MonoBehaviour
{
    public void NotAllowed()
    {
        // コンポーネントは単体で存在することはできない
        Rigidbody rb = new Rigidbody(); // まず、これはできない（何も起きない）
        InstantiateTest test = new InstantiateTest();   // これもできない（警告が出力される）
    }

    public void Test1()
    {
        // コンポーネントは GameObject に追加されて初めて存在できる
        GameObject go = new GameObject();   // GameObject は new できる
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>(); // コンポーネントは GameObject に追加しなくてはならない
        sr.sprite = Resources.Load<Sprite>("Rock01");   // Resources フォルダからアセットを（Sprite として）読み込む
        //sr.sprite = (Sprite) Resources.Load("Rock01");  // こういう書き方もあるが、違いを理解することが重要（キャストが失敗した時は例外となる）
        //sr.sprite = Resources.Load("Rock01") as Sprite; // こういう書き方もあるが、違いを理解することが重要（キャストが失敗した時は null になる）
        go.transform.position = new Vector3(2, 3);
        go.AddComponent<Rigidbody2D>();
        // このことは「Unity Editor でできる操作はプログラムからもできる」ということを表している
    }

    public void Test2()
    {
        // 3D モデルを作ることもできる
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder); // Primitive なモデルはこうすれば生成できる
        go.transform.position = new Vector3(-2, 4, 1);
        go.transform.LookAt(new Vector3(1, -1, 0));
        go.AddComponent<Rigidbody>();   // コンポーネントはやはり追加しなくてはならない
    }
}
