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
    public GameObject subMenuUIPanel;
    public GameObject helpMenuPanel;
    public GameObject QuestClearText;
    public GameObject enterFightUIPanel;

    [Header("Manager")]
    public TalkManager talkManager;
    public QuestManager questManager;

    [Header("Camera")]
    public GameObject MainCamera;
    public GameObject FightCamera;


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

    [Header("EnterFight")]
    public Transform enterFightPos;
    [HideInInspector]public bool isEnterFight;


    [HideInInspector]public bool isAction;
    [HideInInspector]public bool activeInventory = false;
    private bool activeSubMenu;
    private bool activeHelpMenu;

    private int spawnNum;



    public void talkAction(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        //Debug.Log("�Ѿ�� id : " + id);
        //Debug.Log("questManager.questId : " + questManager.questId);
        //Debug.Log("questManager.questActionIndex : " + questManager.questActionIndex);
        //Debug.Log("questTalkIndex : " +  questManager.GetQuestTalkIndex(id));
        //Debug.Log("talkindex : " + talkIndex);
        //Debug.Log(questManager.getItemNum_Quest2);

        // ��ȭ ������ ����
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        //if (questManager.getItemNum_Quest2 == 0)
        //{
        //    questManager.required_ItemGroup_Quest2.SetActive(false);
        //    questManager.NextQuest();
        //    questManager.getItemNum_Quest2 += 100;
        //}

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

        if (Input.GetKeyDown(KeyCode.Escape) && !activeHelpMenu)        // helpmenu �������� ����(����ó��)
            OnOffSubMenuPanel();

        if (activeHelpMenu)     // ����ó��
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                OnOffHelpMenuPanel();
        }


        if (questManager.getItemNum_Quest2 == 0)        // ����Ʈ2 Ŭ���� ó��
        {
            Invoke("setActiveQuestClearText", 1f);
            questManager.NextQuest();
            questManager.required_ItemGroup_Quest2.SetActive(false);
            questManager.getItemNum_Quest2 += 100;      // ���ǹ� �� ���� ȣ��
        }
    }


    public void setActiveQuestClearText()
    {
        //  ĵ���� �ȿ� UI text ����
            Instantiate(QuestClearText, QuestClearText.transform.position, Quaternion.identity, GameObject.Find("UI").transform);
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
        GetRandomSpawnNum();

        switch (spawnNum)
        {
            case 1:
                Player.transform.position = WestSpawnArea.transform.position;
                break;
            case 2:
                Player.transform.position = NorthSpawnArea.transform.position;
                break;
            case 3:
                Player.transform.position = EastSpawnArea.transform.position;
                break;

        }
        MainCamera.GetComponent<CameraController>().center = new Vector2(62.5f, 9);
        MainCamera.GetComponent<CameraController>().size = new Vector2(54, 28);
        moveWoodsUIPanel.SetActive(false);
        Player.GetComponent<PlayerController>().isPlayerInWoods = true;
        Player.GetComponent<SpriteRenderer>().material = unlitMaterial;

        WestSpawnArea.GetComponent<Animator>().SetBool("isPlayerInWoods", true);
        EastSpawnArea.GetComponent<Animator>().SetBool("isPlayerInWoods", true);
        NorthSpawnArea.GetComponent<Animator>().SetBool("isPlayerInWoods", true);

        Time.timeScale = 1f;
    }

    private void GetRandomSpawnNum()
    {
        spawnNum = Random.Range(1, 4);
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

    public void CloseEnterFightUIPanelAndResume()
    {
        enterFightUIPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ResumeGame()        // ���� �Ͻ����� ����
    {
        activeSubMenu = false;
        subMenuUIPanel.SetActive(false);
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

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void OnOffSubMenuPanel()
    {
        activeSubMenu = !activeSubMenu;
        subMenuUIPanel.SetActive(activeSubMenu);
        Time.timeScale = activeSubMenu ? 0 : 1;
    }

    public void OnOffHelpMenuPanel()
    {
        activeHelpMenu = !activeHelpMenu;
        helpMenuPanel.SetActive(activeHelpMenu);
        Time.timeScale = activeHelpMenu ? 0 : 1;
    }

    #region
    public void ConverCameraNormalToFight()
    {
        MainCamera.SetActive(false);
        FightCamera.SetActive(true);
    }

    public void ConvertcameraFightToNormal()
    {
        MainCamera.SetActive(true);
        FightCamera.SetActive(false);
    }

    public void EnterFightSetting()
    {
        Player.transform.position = enterFightPos.position;
        isEnterFight = true;
    }

    #endregion

}
