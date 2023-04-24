using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;


    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()         // �⺻/����Ʈ ��� �߰�
    {
        // Talk Data
        // NPC Client : 1000(Weapon), 2000(Quest), 3000(Posion) 
        talkData.Add(2000, new string[] { "�� �� ����?" });

        //Quest Talk
        //1�� ����Ʈ : ������ ���� Ȯ��
        talkData.Add(10 + 2000, new string[] { "�ӹ���. �ĵ�.", "�� ���Ӹ��� �� �� ���� ������ �Դ�.", "�� ���� �аŸ��� ���� ������ ��� �ֺ� ������ ��Ż�ϴ� ����̾�.", "������ �����ϰ� �ִٴµ�, �̹� ���� ��� �� ���� �׾��ٰ� �Ѵ�.", "�켱 ������ �´��� ���� ���� �������� Ȯ���ϰ� ������." });
        talkData.Add(11 + 2000, new string[] { "�ٹ��Ÿ��� ���� �ٷ� �����." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        //��ȭ ����ó��
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);
            else
                return GetTalk(id - id % 10, talkIndex);
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

}
