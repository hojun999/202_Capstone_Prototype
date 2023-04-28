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

    public int GetQuestTalkIndex(int id)    // �� ����Ʈ �������� ���� ��ȭ ��� (���� x > ���� o)
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

    private void OnCollisionEnter2D(Collision2D collision)      // 1�� ����Ʈ Ŭ���� ����
    {
        if (collision.gameObject.CompareTag("Quest1Area"))
        {
            //ongoingQuestImage_Quest1.SetActive(false);
            //ongoingQuestImage_Clear_Quest1.SetActive(true);
        }
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
