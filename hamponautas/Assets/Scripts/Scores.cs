using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class Scores : MonoBehaviour
{
    static public Scores Instance { get; private set; }
    static string PATH => Application.persistentDataPath + "/scores.bt";
    public Score current;
    public ScoreList scoreList;

    private void Awake()
    {
        Instance = this;
    }


    public void Save()
    {
        scoreList.add(current);
        File.WriteAllBytes(PATH, Encoding.ASCII.GetBytes(JsonUtility.ToJson(scoreList, false)));
        current=new Score();
    }

    public void Load()
    {
        if (!File.Exists(PATH))
        {
            Save();
            var bytes = File.ReadAllBytes(PATH);
            scoreList = JsonUtility.FromJson<ScoreList>(Encoding.UTF8.GetString(bytes));
        }
    }

    private void OnDestroy()
    {
       Instance = null;
    }

    private void OnApplicationQuit()
    {
        OnDestroy();
    }
}
[System.Serializable]
public class ScoreList
{
    //comparar y ordenar de mayor a menor
    static Comparison<Score> comparisor = new Comparison<Score>((s0, s1) => -s0.Compute().CompareTo(s1.Compute()));

    public List<Score> scores = new List<Score>();
    public void add(Score score)
    {
        scores.Add(score);
        scores.Sort(comparisor);
        if (scores.Count > 10)
        {
            scores.RemoveAt(scores.Count - 1);
        }

    }
    public override string ToString()
    {
        return scores.Select(s => s.ToString()).Aggregate((a, b) => $"{a}\n--------------------------\n{b}");
    }

}


[System.Serializable]
public class Score
{
    public float seeds;
    public float km;
    public float gems;
    //Acuerdate de modificar el score cuando introduzcas las pipas, las pipas seran los multiplicadores hasta x10 y las gemas sumaran 100pts
    public float Compute() => Mathf.Clamp(km * (gems + 1), 0f, 999999999999f);

    public override string ToString()
    {
        return $"Distancia: {(km/10f).ToString("0.00")}\nGemas: {gems.ToString("0")}";
    }

}
