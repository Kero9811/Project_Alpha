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
    // ��ȭ�� ����� ID �޾ƿͼ� �� ID�� �´� ��ȭ �������� ����
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
        talkData.talkData.Add(1000, new string[] { "���� 1�� ���", "���� 2�� ���", "���� 3�� ���(3 ~ 4 �ݺ� ���)", "���� 4�� ���", "���� 5�� ���" });
        talkData.talkData.Add(1001, new string[] { "NPC 2 1 ���", "NPC 2 2 ���" });

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