using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class TalkData
{
    public Dictionary<int, string[]> talkData = new Dictionary<int, string[]>();
}

public class TalkManager : MonoBehaviour
{
    // 대화할 대상의 ID 받아와서 그 ID에 맞는 대화 가져오는 역할
    private TalkData talkData = new TalkData();
    private string path;
    private string talkDataFileName = "TalkData";

    private void Awake()
    {
        path = Application.persistentDataPath + "/";
        //SaveTalkData();
        LoadTalkData();
    }

    private void SaveTalkData()
    {
        talkData.talkData.Add(1000, new string[] { "문맥 1을 재생", "문맥 2을 재생", "문맥 3을 재생(3 ~ 4 반복 재생)", "문맥 4을 재생", "문맥 5을 재생" });
        talkData.talkData.Add(1001, new string[] { "NPC 2 1 재생", "NPC 2 2 재생" });

        string data = JsonConvert.SerializeObject(talkData.talkData);
        File.WriteAllText(path + talkDataFileName, data);
    }

    private void LoadTalkData()
    {
        string talkDataPath = path + talkDataFileName;
        string data = File.ReadAllText(talkDataPath);
        talkData.talkData = JsonConvert.DeserializeObject<Dictionary<int, string[]>>(data);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex >= talkData.talkData[id].Length) { return null; }

        return talkData.talkData[id][talkIndex];
    }
}