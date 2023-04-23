using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;

    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();

    }

    private void GenerateData()     // ����Ʈ ��� ����
    {
        questList.Add(10, new QuestData("���� ���� Ž��", new int[] { 1000 }));

        questList.Add(20, new QuestData("���� ����", new int[] { 1000 }));

        questList.Add(30, new QuestData("���ϵ� ���", new int[] { 1000 }));

        questList.Add(40, new QuestData("���Ӹ� �аŸ� ���", new int[] { 1000 }));



    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string checkQuest(int id)      // ������ npc�� ��ȭ�� �� ���� index++
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;         // ����Ʈ ��ȭ�� ������ ���� ��ȭ�� ���� ����(���̵� ���󿡼��� 1��npc > 2��npc �̹Ƿ� �� �Լ��� ���� if���� ���� �ٸ� npc�� ��ȭ�� ȣ����. ���� ���� npc���� ��ȭ�� ȣ���ϹǷ� �̿� ���ؼ� �ٽ� �ۼ��ؾߵ�.

        // Control Quest Object
        ControlObject();            // ����Ʈ�� ���� ������ �� object ����. ���� if�� �ΰ� �����ϰ� Ȯ���ϱ�.

        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        return questList[questId].questName;
    }

    public string checkQuest()
    {
        // ����Ʈ �̸� ��ȯ
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 1)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }

}
