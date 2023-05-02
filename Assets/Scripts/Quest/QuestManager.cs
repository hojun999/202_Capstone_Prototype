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

    public bool isCheckQuestArea;

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
        // ����Ʈ ��ȭ�� ������ ���� ��ȭ�� ���� ����(���̵� ���󿡼��� 1��npc > 2��npc �̹Ƿ� �� �Լ��� ���� if���� ���� �ٸ� npc�� ��ȭ�� ȣ����. ���� ���� npc���� ��ȭ�� ȣ���ϹǷ� �̿� ���ؼ� �ٽ� �ۼ��ؾߵ�.

        if (id == questList[questId].npcId[0])
            questActionIndex++;
        else if (id == questList[questId].npcId[1])
        {

        }

        // ����Ʈ�� ���� ������ �� object ����.
        ControlObject();

        //if (questActionIndex == questList[questId].npcId.Length)
        //    NextQuest();

        if (isCheckQuestArea && questId == 10)
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

    private void OnCollisionEnter2D(Collision2D collision)      // 1�� ����Ʈ Ŭ���� ����
    {
        if (collision.gameObject.CompareTag("Quest1Area"))
        {
            isCheckQuestArea = true;
            //ongoingQuestImage_Quest1.SetActive(false);
            //ongoingQuestImage_Clear_Quest1.SetActive(true);
        }
    }

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
