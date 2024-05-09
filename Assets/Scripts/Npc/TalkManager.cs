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

    // 대사 작성 후에 한 번만 저장하고 이후 쓰지 않음
    private void SaveTalkData()
    {
        talkData.talkData.Add(1000, new string[] { "어서오세요, 저희의 상점에.", "당신의 여행길에 큰 도움을 줄 물건들을 팔고 있어요.", "무슨 아이템이 있는지 보시겠어요?", "이 이상은 구하기 힘들꺼예요.", "문맥 5을 재생" });
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