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
        // 4문장
        talkData.talkData.Add(1000, new string[] { "음? 당신은 누구시죠?", "모험가이시군요. 어서오세요 저희 마을에. 편히 있다가시면 좋겠네요.", "으음.. 혹시 사실 물건이 있으신가요?", "골라보시겠어요? 제 나름대로 엄선한 상품들만 판매하고 있어요." });
        // 11문장
        talkData.talkData.Add(1001, new string[] { "처음 보는 분이군요? 어서오세요. 저희 마을에 오신 걸 환영합니다. 저는 이 마을에 살고 있는 주민 중 한 명입니다.", "어떤 일로 오신건가요?", "모험을 다니고 계신 모험가분이셨군요.",
                                                    "그럼... 혹시 죄송하지만 부탁 하나만 드려도 될까요?", "사실 최근 저희 마을 근처에 몬스터 한 마리가 기승을 부리고 있어어요.","가능하시면 그 몬스터를 토벌해주시겠어요?","....","제 부탁을 들어주신다니, 정말 감사합니다!",
                                                    "물리쳐주시면 사례는 꼭 해드리겠습니다.", "오른쪽에 있는 생선을 가져가셔도 좋습니다. 구워먹으면 맛있거든요.","오른쪽 길로 가시면 자연스럽게 만날 수 있을겁니다. 행운을 빕니다."});

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