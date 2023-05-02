using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;

    public GameObject required_Area_Quest1;     // ����Ʈ 1�� �� �� Ư�� ���� Ž��, �� ������Ʈ boxcollider2d �־ oncollisionenter2d�� ����
    public GameObject ongoingQuestImage_Quest1;      // ����Ʈ 1�� ���� ��Ȳ UI
    public GameObject ongoingQuestImage_Clear_Quest1;      // ����Ʈ 1�� ���� ��Ȳ UI

    public GameObject[] questObject;

    [HideInInspector] public bool isCheckQuestArea;
    [HideInInspector] public bool isGetMalfuncionedGun;
    [HideInInspector] public bool isGetInjector;
    [HideInInspector] public bool isGetClothesWithBlood;
    [HideInInspector] public int eliminateEnemyNum = 8;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()     // ����Ʈ ��� ����
    {
        questList.Add(10, new QuestData("���� ���� Ž��", new int[] { 2000 }));

        questList.Add(20, new QuestData("���� ����", new int[] { 2000 }));

        questList.Add(30, new QuestData("���� ���", new int[] { 2000 }));

        questList.Add(40, new QuestData("���Ӹ� �аŸ� ó��", new int[] { 2000 }));

    }

    public string checkQuest(int id)      // ������ npc�� ��ȭ�� �� ���� index++
    {
        // ����Ʈ ��ȭ�� ������ �� ����Ʈ�� ���� ��ȭ ���
        if (id == questList[questId].npcId[0])
            questActionIndex++;

        // ��ȭ ����ؼ� ���� ����Ʈ�� �Ѿ�� �ʰ� ����
        if (questActionIndex == 5)      
            questActionIndex = 1;

        // ����Ʈ�� ���� ������ �� object ����.
        ControlObject();

        if (isCheckQuestArea && questId == 10)
            NextQuest();
        if (isGetClothesWithBlood && isGetInjector && isGetMalfuncionedGun && questId == 20)
            NextQuest();
        if (eliminateEnemyNum == 0 && questId == 30)
            NextQuest();

        return questList[questId].questName;
    }

    public int GetQuestTalkIndex(int id)    // �� ����Ʈ �������� ���� ��ȭ ��� (���� x > ���� o)
    {
        return questId + questActionIndex;      // ����Ʈ ��ȣ + ����Ʈ ��ȭ ����
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    // ����Ʈ �̸� ��ȯ
    //public string checkQuest()
    //{
    //    return questList[questId].questName;
    //}

    

    void ControlObject()
    {
        switch (questId)
        {
            case 20:    // �� ��° ����Ʈ
                {
                    questObject[0].SetActive(true); // �ֻ��, ������ ��, ���峭 ��
                    questObject[1].SetActive(true);
                    questObject[2].SetActive(true);
                }
                break;
            case 30:    // �� ��° ����Ʈ
                    questObject[0].SetActive(false); 
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(false); // {} �߰��ؾ��ϴ��� Ȯ��
                break;
        }
    }

}
