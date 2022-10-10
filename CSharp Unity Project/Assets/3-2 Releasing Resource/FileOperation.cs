using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FileOperation : MonoBehaviour
{
    [SerializeField] Text _console = default;

    /// <summary>
    /// ボタンに設定するための関数
    /// </summary>
    /// <param name="fileName"></param>
    public void Do(InputField fileName)
    {
        Do(fileName.text);
    }

    /// <summary>
    /// ファイルを操作する
    /// </summary>
    /// <param name="fileName"></param>
    void Do(string fileName)
    {
        // Unity でファイルを保存する時は Application.persistentDataPath の下に保存しなければならない
        string filePath = Application.persistentDataPath + @"\" + fileName;

        try
        {
            // ここでの using は「スコープを抜けたらファイルをクローズする」という意味の using です
            using (StreamReader sr = new StreamReader(filePath))
            {
                // 指定したファイルを開いて内容を読み込み、表示する
                while (sr.Peek() > -1)
                {
                    string s = sr.ReadLine();
                    WriteToConsole(s);
                }
            }
        }
        catch (FileNotFoundException e)
        {
            // ファイルが見つからなかったらメッセージを表示して新しくファイルを作る
            WriteToConsole(e.Message);

            try
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(DateTime.Now.ToString());  // 現在日時を書き込む
                    WriteToConsole($"{((FileStream)sw.BaseStream).Name} を作成しました。");
                }
            }
            catch (Exception ex)
            {
                WriteToConsole(ex.Message);
            }
        }
    }

    void WriteToConsole(string text)
    {
        _console.text = text + "\r\n" + _console.text;
        // Unity の Console にも出力しておく（コピペするため）
        Debug.Log(text);
    }
}
