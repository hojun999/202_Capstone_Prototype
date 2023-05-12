using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;

    [Header("UI")]
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public GameObject inventoryPanel;
    public GameObject moveWoodsUIPanel;
    public GameObject moveCampUIPanel;

    [Header("Manager")]
    public TalkManager talkManager;
    public QuestManager questManager;

    [Header("Camera")]
    public GameObject MainCamera;

    [Header("SpawnArea")]
    public GameObject CampSpawnArea;
    public GameObject WestSpawnArea;
    public GameObject EastSpawnArea;
    public GameObject NorthSpawnArea;

    [Header("Talk")]
    public int talkIndex;

    [Header("Material")]
    public Material unlitMaterial;
    public Material litMaterial;


    [HideInInspector]public bool isAction;
    [HideInInspector]public bool activeInventory = false;
    

    public void talkAction(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);

        //if (scanObj.GetComponent<ObjData>().isQuestNpc)
        //{
        //    Talk(objData.id + questManager.questId + questManager.questActionIndex - 10, objData.isNpc);
        //    talkPanel.SetActive(isAction);
        //}
        //else
        //{
            
        //}

    }

    void Talk(int id, bool isNpc)
    {
        //Debug.Log("�Ѿ�� id : " + id);
        //Debug.Log("questManager.questId : " + questManager.questId);
        //Debug.Log("questManager.questActionIndex : " + questManager.questActionIndex);
        //Debug.Log("questTalkIndex : " +  questManager.GetQuestTalkIndex(id));
        //Debug.Log("talkindex : " + talkIndex);
        Debug.Log(questManager.getItemNum_Quest2);

        // ��ȭ ������ ����
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        if (questManager.getItemNum_Quest2 == 0)
        {
            questManager.required_ItemGroup_Quest2.SetActive(false);
            questManager.NextQuest();
            questManager.getItemNum_Quest2 += 100;
        }

        // ĳ������ �� ��ȭ�� ������ ��
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questManager.checkQuest(id);
            return;         // -void �Լ����� return�� �Լ��� ���Ḧ �ǹ�-
        }

        // ��ȭ �б���(npc, ������ ���� ���� ����)
        if (isNpc)          
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;

        Debug.Log("talkindex : " + talkIndex);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }


    public void LocatePlayerAtCamp()      // ķ���� �̵�
    {
        Player.transform.position = CampSpawnArea.transform.position;
        MainCamera.GetComponent<CameraController>().center = new Vector2(0, 0);
        MainCamera.GetComponent<CameraController>().size = new Vector2(18, 10);
        Player.transform.position = CampSpawnArea.transform.position;
        Player.GetComponent<PlayerController>().isPlayerInWoods = false;
        Player.GetComponent<SpriteRenderer>().material = litMaterial;

        moveCampUIPanel.SetActive(false);

        WestSpawnArea.GetComponent<Animator>().SetBool("isPlayerInWoods", false);
        EastSpawnArea.GetComponent<Animator>().SetBool("isPlayerInWoods", false);
        NorthSpawnArea.GetComponent<Animator>().SetBool("isPlayerInWoods", false);

        Time.timeScale = 1f;
    }

    public void LocatePlayerAtWoods()     // ������ �̵�, ���Ŀ� ����, ���� ���� �����ϰ� �����ϱ�
    {
        Player.transform.position = WestSpawnArea.transform.position;
        MainCamera.GetComponent<CameraController>().center = new Vector2(43, 9);
        MainCamera.GetComponent<CameraController>().size = new Vector2(54, 28);
        moveWoodsUIPanel.SetActive(false);
        Player.GetComponent<PlayerController>().isPlayerInWoods = true;
        Player.GetComponent<SpriteRenderer>().material = unlitMaterial;

        WestSpawnArea.GetComponent<Animator>().SetBool("isPlayerInWoods", true);
        EastSpawnArea.GetComponent<Animator>().SetBool("isPlayerInWoods", true);
        NorthSpawnArea.GetComponent<Animator>().SetBool("isPlayerInWoods", true);

        Time.timeScale = 1f;
    }

    public void CloseMoveWoodsUIPanelandResume()
    {
        moveWoodsUIPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CloseMoveCampUIPanelandResume()
    {
        moveCampUIPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ResumeGame()        // ���� �Ͻ����� ����
    {
        Time.timeScale = 1f;
    }

    public void LoadCamp()
    {
        SceneManager.LoadScene("Camp");
    }

    public void TurnOffGame()
    {
        Application.Quit();
    }


}
