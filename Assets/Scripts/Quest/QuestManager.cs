using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;

    [Header("Quest1")]
    public GameObject required_Area_Quest1;     // ����Ʈ 1�� �� �� Ư�� ���� Ž��, �� ������Ʈ boxcollider2d �־ oncollisionenter2d�� ����
    public GameObject inProgressQuestImage_Quest1;

    [Header("Quest2")]
    public GameObject required_ItemGroup_Quest2;
    public GameObject inProgressQuestImage_Quest2;
    public int getItemNum_Quest2;


    [Header("Quest3")]
    public GameObject enemyGroup_Quest3;
    public GameObject inProgressQuestImage_Quest3;      

    [Header("Quest4")]
    //public GameObject enemyGruop_Quest4;
    //public GameObject inProgressQuestImage_Quest4;


    [HideInInspector] public bool isCheckArea_Quest1;
    [HideInInspector] public int eliminateEnemyNum = 8;


    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();

        getItemNum_Quest2 = 3;
    }

    private void GenerateData()     // ����Ʈ ��� ����
    {
        questList.Add(10, new QuestData("���� ���� Ž��", new int[] { 2000, 2000 }));

        questList.Add(20, new QuestData("���� ����", new int[] { 2000, 2000 }));

        questList.Add(30, new QuestData("���� ���", new int[] { 2000, 2000 }));

        questList.Add(40, new QuestData("���Ӹ� �аŸ� ����", new int[] { 2000, 2000 }));

    }

    public void checkQuest(int id)      // ������ npc�� ��ȭ�� �� ���� index++
    {
        // ����Ʈ ��ȭ�� ������  �� ����Ʈ�� ���� ��ȭ ���
        if (id == questList[questId].npcId[0])
            questActionIndex++;

        // ��ȭ ����ؼ� ���� ����Ʈ�� �Ѿ�� �ʰ� ����
        if (questActionIndex == 5)      
            questActionIndex = 1;

        if (questId == 10)      // ���� ����Ʈ�� �ѱ�� �κ��� PlayerController�� oncollisionenter���� ó��
        {
            inProgressQuestImage_Quest1.SetActive(true);        // checkQuest �Լ��� ��ȭ(spacebar) '����' ȣ��ǹǷ� ����ƮUI������Ʈ�� ���⿡ ��ġ��
            required_Area_Quest1.SetActive(true);
        }
        else if (questId == 20)     // ���� ����Ʈ�� �ѱ�� �κ��� GameManager Talk()���� ó��
        {
            inProgressQuestImage_Quest1.SetActive(false);
            inProgressQuestImage_Quest2.SetActive(true);
            required_ItemGroup_Quest2.SetActive(true);
            //if (getItemNum_Quest2 == 0)
            //{
            //    required_ItemGroup_Quest2.SetActive(false);
            //    NextQuest();
            //    getItemNum_Quest2 += 100;
            //}
        }
        else if (questId == 30)     // ���� ����Ʈ�� �ѱ�� �κ� GameManager Talk()���� ó���� '����'
        {            
            inProgressQuestImage_Quest2.SetActive(false);
            inProgressQuestImage_Quest3.SetActive(true);
            enemyGroup_Quest3.SetActive(true);
            if (eliminateEnemyNum == 0)
                enemyGroup_Quest3.SetActive(false);
        }
        //else if (questid == 40)
        //{
        //    enemyGroup_Quest4.SetActive(true);
        //    if (������ ���ϵ� ���� num == 0)
        //        enemyGroup_Quest4.SetActive(false);
        //}
    }

    public int GetQuestTalkIndex(int id)    // �� ����Ʈ �������� ���� ��ȭ ��� (���� x ���� ��ȭ > ���� o ���� ��ȭ)
    {
        return questId + questActionIndex;      // ����Ʈ ��ȣ + ����Ʈ ��ȭ ����
    }

    public void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
}
