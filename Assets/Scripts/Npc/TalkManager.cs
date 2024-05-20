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
        LoadTalkData();
    }

    private void SaveTalkData()
    {
        // 4문장
        talkData.talkData.Add(1000, new string[] { "음? 당신은 누구시죠?", "모험가이시군요. 어서오세요 저희 마을에. 편히 있다가시면 좋겠네요.", "으음.. 혹시 사실 물건이 있으신가요?", "골라보시겠어요? 제 나름대로 엄선한 상품들만 판매하고 있어요." });
        // 11 + 7문장
        talkData.talkData.Add(1001, new string[] { "처음 보는 분이군요? 어서오세요. 저희 마을에 오신 걸 환영합니다. 저는 이 마을에 살고 있는 주민 중 한 명입니다.", "어떤 일로 오신건가요?", "모험을 다니고 계신 모험가분이셨군요.",
                                                    "그럼... 혹시 죄송하지만 부탁 하나만 드려도 될까요?", "사실 최근 저희 마을 근처에 몬스터 한 마리가 기승을 부리고 있어어요.","가능하시면 그 몬스터를 토벌해주시겠어요?","....","제 부탁을 들어주신다니, 정말 감사합니다!",
                                                    "물리쳐주시면 사례는 꼭 해드리겠습니다.", "오른쪽에 있는 생선을 가져가셔도 좋습니다. 구워먹으면 맛있거든요.","오른쪽 길로 가시면 자연스럽게 만날 수 있을겁니다. 행운을 빕니다.",
                                                    "오셨군요. 모험가님 수고하셨습니다. 이 마을의 골치덩이를 없애주시다니 정말 감사드립니다.", "몬스터를 처치하니 이런게 나왔군요. 무언가 불길한 기운이 느껴지네요.", "으음...",
                                                    "저희 마을에는 필요가 없지만 왠지 모험가님에게는 쓸모 있어 보이는군요. 예전에 몬스터가 드랍하는 정수로 무기를 가공할 수 있다는 말을 친구에게 들었습니다.", "마침 그 친구가 있는 곳을 알고 있는데, 모험가님이 괜찮으시다면 답례로 그 친구에게 작업을 받을 수 있도록 힘을 써보겠습니다.",
                                                    "모험가님 마음에 드시나보네요. 다행입니다. 그럼 이 편지를 가지고 북쪽에 있는 가름 마을로 가보세요. 아주 훌륭한 무기를 얻으실 수 있을겁니다.", "다시 한 번 감사드립니다. 부디 평안한 모험을 하실 수 있도록 기도하겠습니다."});

        string data = JsonConvert.SerializeObject(talkData.talkData);
        File.WriteAllText(path + talkDataFileName, data);
    }

    private void LoadTalkData()
    {
        string talkDataPath = path + talkDataFileName;
        if (!File.Exists(talkDataPath)) { SaveTalkData(); }
        string data = File.ReadAllText(talkDataPath);
        talkData.talkData = JsonConvert.DeserializeObject<Dictionary<int, string[]>>(data);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex >= talkData.talkData[id].Length) { return null; }

        return talkData.talkData[id][talkIndex];
    }
}