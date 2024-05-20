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

    // ��� �ۼ� �Ŀ� �� ���� �����ϰ� ���� ���� ����
    private void SaveTalkData()
    {
        // 4����
        talkData.talkData.Add(1000, new string[] { "��? ����� ��������?", "���谡�̽ñ���. ������� ���� ������. ���� �ִٰ��ø� ���ڳ׿�.", "����.. Ȥ�� ��� ������ �����Ű���?", "��󺸽ðھ��? �� ������� ������ ��ǰ�鸸 �Ǹ��ϰ� �־��." });
        // 11����
        talkData.talkData.Add(1001, new string[] { "ó�� ���� ���̱���? �������. ���� ������ ���� �� ȯ���մϴ�. ���� �� ������ ��� �ִ� �ֹ� �� �� ���Դϴ�.", "� �Ϸ� ���Űǰ���?", "������ �ٴϰ� ��� ���谡���̼̱���.",
                                                    "�׷�... Ȥ�� �˼������� ��Ź �ϳ��� ����� �ɱ��?", "��� �ֱ� ���� ���� ��ó�� ���� �� ������ ����� �θ��� �־���.","�����Ͻø� �� ���͸� ������ֽðھ��?","....","�� ��Ź�� ����ֽŴٴ�, ���� �����մϴ�!",
                                                    "�������ֽø� ��ʴ� �� �ص帮�ڽ��ϴ�.", "�����ʿ� �ִ� ������ �������ŵ� �����ϴ�. ���������� ���ְŵ��.","������ ��� ���ø� �ڿ������� ���� �� �����̴ϴ�. ����� ���ϴ�."});

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