using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public GameObject inventoryPanel;

    public int talkIndex;

    public bool isAction;
    public bool activeInventory = false;
    

    public void talkAction(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        if(talkData == null)        // �� ĳ������ ��ȭ�� ������ ��
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.checkQuest(id));
            return;         // -void �Լ����� return�� �Լ��� ���Ḧ �ǹ�-
        }

        if (isNpc)          // ��ȭ �б���(npc, ������ ���� ���� ����)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

   
}
